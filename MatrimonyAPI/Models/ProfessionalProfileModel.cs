using System.ComponentModel.DataAnnotations;
namespace MatrimonyAPI.Models
{
    public class ProfessionalProfileModel
    {
        [Required]
        public int UserId { get; set; }
		[Required]
		public string? HighestQualification { get; set; }
		[Required]
		public string? HighestQualificationCollege { get; set; }
		[Required]
		public string? WorkType { get; set; }
		[Required]
		public string? WorkDesignation { get; set; }
		[Required]
		public string? WorkCompany { get; set; }
		[Required]
		public string? IncomeType { get; set; }
		[Required]
		public string? IncomeRange { get; set; }
	}
}
