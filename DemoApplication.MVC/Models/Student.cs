using System;
using System.ComponentModel.DataAnnotations;

namespace DemoApplication.MVC.Models
{
    public class Student
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        public DateTime Dob { get; set; }
        public bool Active { get; set; }
    }


}
