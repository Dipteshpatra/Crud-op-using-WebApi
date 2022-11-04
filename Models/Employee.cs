using System.ComponentModel.DataAnnotations;

namespace Demo_Core_Api.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; } 
        [Required]
        public string EmpName { get; set; }
        [Required]

        public int EmpAge { get; set; }
    }
}
