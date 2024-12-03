using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Models
{
    public class Employee
    {
        [Key]
        [Required]
        [RegularExpression(@"UI[A-Za-z0-9]{7}")]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^[89]\d{7}$")]
        public string Phone { get; set; }
        
        public string Gender { get; set; }
        [Required] 
        public Guid CafeId { get; set; }
        public Cafe Cafe { get; set; }
        [Required] 
        public DateTime StartDate { get; set; }
        
    }
}
