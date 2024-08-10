using System.ComponentModel.DataAnnotations;

namespace ManagmentApp.Dtos
{
    public class CreateCompensationDto
    {
        [Required]
        public double Salary { get; set; }
        public double Bonus { get; set; }   
    }
}
