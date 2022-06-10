namespace MatrimonyAPI.Services;
using AutoMapper;
using BCrypt.Net;
using MatrimonyAPI.Helpers;
using MatrimonyAPI.Models;
using MatrimonyAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;


public interface IUserService
{
    public List<User> GetAllUser();
    public User GetUser(int id);
    public User RegisterUser(RegisterUserModel model);
    public AuthenticateResponse AuthenticateUser(LoginUserModel model);
    public User UpdateBasicInfo(BasicProfileModel model);
    public User UpdateProfessionalDetail(ProfessionalProfileModel model);

    public List<User> GetAllUserByCondition(SearchModel model);

    public bool CheckUser(int id);
    public List<string> GetAllUserEmail();
    public void DeleteUser(int id);
}

public class UserService : IUserService
{
    readonly DatabaseContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _environment;


    public UserService(DatabaseContext dbContext, IMapper mapper, IWebHostEnvironment environment)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _environment = environment;

    }

    public User RegisterUser(RegisterUserModel model)
    {
        try
        {
            if (_dbContext.Users.Any(x => x.Email == model.Email))
                throw new AppException("Email '" + model.Email + "' is already taken");

            var user = _mapper.Map<User>(model);

            user.Password = BCrypt.HashPassword(model.Password);
            user.CreatedDate = DateTime.Now;

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user;
        }
        catch
        {
            throw;
        }
    }

    public User UpdateBasicInfo(BasicProfileModel model)
    {
        try
        {
            var user = getUser(model.UserId);

            _mapper.Map(model, user);

            string? path = UploadFile(model.Profile);

            user.ProfilePhoto = path;

            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();

            return user;
        }
        catch
        {
            throw;
        }
    }

    private string? UploadFile(IFormFile file)
    {
        if (file.Length > 0)
        {
            if (!file.FileName.Contains("wwwroot"))
            {

                if (!Directory.Exists(_environment.WebRootPath + "\\uploads\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\uploads\\");
                }
                using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\uploads\\" + file.FileName))
                {
                    file.CopyTo(filestream);
                    filestream.Flush();
                    return "\\uploads\\" + file.FileName;
                }
            }
            else
            {
                string[] s = file.FileName.Split("\\");
                return "\\uploads\\" + s[s.Length-1];
            }

        }
        else
        {
            return null;
        }
    }

    public User UpdateProfessionalDetail(ProfessionalProfileModel model)
    {
        try
        {
            var user = getUser(model.UserId);
            _mapper.Map(model, user);

            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();

            return user;
        }
        catch
        {
            throw;
        }
    }
    public void DeleteUser(int id)
    {
        try
        {
            var user = getUser(id);

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();

        }
        catch
        {
            throw;
        }
    }

    public bool CheckUser(int id)
    {
        return _dbContext.Users.Any(e => e.UserId == id);
    }

    //public User DeleteUser(int id)
    //{
    //    try
    //    {
    //        User? user = _dbContext.Users.Find(id);

    //        if (user != null)
    //        {
    //            _dbContext.Users.Remove(user);
    //            _dbContext.SaveChanges();
    //            return user;
    //        }
    //        else
    //        {
    //            throw new ArgumentNullException();
    //        }
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    public User GetUser(int id)
    {
        return getUser(id);

    }

    public List<User> GetAllUser()
    {
        try
        {
            return _dbContext.Users.ToList();
        }
        catch
        {
            throw;
        }
    }

        public List<string> GetAllUserEmail()
        {
            List<string> data = new();
            try
            {
                List<User> users= _dbContext.Users.ToList();
                foreach (var u in users)
                {
                    data.Add(u.Email);

                }
            return data.Distinct().ToList();
            }
            catch
            {
                throw;
            }
        }

        public List<User> GetAllUserByCondition(SearchModel model)
    {
        try
        {
            var filteredData = _dbContext.Users.ToList();
            if (!string.IsNullOrWhiteSpace(model.Gender))
            {
                filteredData = filteredData.Where(e => e.Gender==model.Gender).ToList();
            }

            if (!string.IsNullOrWhiteSpace(model.Diet))
            {
                filteredData = filteredData.Where(e => e.Diet == model.Diet).ToList();
            }


            return filteredData;
        }
        catch
        {
            throw;
        }
    }

    private int getAge(DateTime dob)
    {
        int age = 0;
        age = DateTime.Now.AddYears(-dob.Year).Year;
        return age;
    }

    public AuthenticateResponse AuthenticateUser(LoginUserModel model)
    {
        var user = _dbContext.Users.SingleOrDefault(x => x.Email == model.Email);
     

        var response = _mapper.Map<AuthenticateResponse>(user);
        return response;
    }

    private User getUser(int id)
    {
        var user = _dbContext.Users.Find(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }
}
