namespace MatrimonyAPI.Models.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
		public string? MobileNo { get; set; }
		public DateTime? BirthDate { get; set; }
		public string? Gender { get; set; }
		public string? City { get; set; }
		public bool IsLiveWithFamily { get; set; }
		public string? MaritalStatus { get; set; }

		public string? Diet { get; set; }

		public decimal height { get; set; }

		public string? SubCommunity { get; set; }

		public string? HighestQualification { get; set; }

		public string? HighestQualificationCollege { get; set; }
		public string? WorkType { get; set; }
		public string? WorkDesignation { get; set; }

		public string? WorkCompany { get; set; }

		public string? IncomeType { get; set; }

		public string? IncomeRange { get; set; }

		public string? ProfilePhoto { get; set; }

		public DateTime? CreatedDate { get; set; }

	}
}
