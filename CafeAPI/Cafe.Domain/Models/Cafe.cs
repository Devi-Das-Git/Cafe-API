using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Models
{
    public class Cafe
    {

        [Key]
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
}
