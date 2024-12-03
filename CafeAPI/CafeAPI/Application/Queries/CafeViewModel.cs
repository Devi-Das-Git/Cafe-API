using System.ComponentModel.DataAnnotations;

namespace CafeAPI.Application.Queries
{
    public record Cafe
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public byte[]? Logo { get; set; }
        [Required]
        public string Location { get; set; }
        public int? Employees { get; set; }
    }
    public record Employee
    {
        [Required]
        [RegularExpression(@"UI[A-Za-z0-9]{7}")]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get;  set; }
        [Required]
        [RegularExpression(@"^[89]\d{7}$")]
        public string Phone { get;  set; }
        
        public string Gender { get;  set; }
        [Required]
        public Guid CafeId { get; set; }
        public string Cafe { get; set; }
        [Required]
        public DateTime StartDate { get; set; }

        public int DaysWorked {  get; set; }

    }

}
