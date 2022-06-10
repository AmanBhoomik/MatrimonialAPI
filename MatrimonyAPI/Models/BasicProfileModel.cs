using System.ComponentModel.DataAnnotations;
namespace MatrimonyAPI.Models
{
    public class BasicProfileModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime? BirthDate { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        public string? City { get; set; }
        public bool IsLiveWithFamily { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Diet { get; set; }
        public decimal height { get; set; }
        public string? SubCommunity { get; set; }

        public IFormFile Profile { get; set; }



    }
}
