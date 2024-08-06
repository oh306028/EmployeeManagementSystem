namespace ManagmentApp.Models
{
    public class EmployeeWithDetails
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HireDate { get; set; }
        public string DepartmentName { get; set; }
        public string Email { get; set; }
        public double? SalaryPerMonth { get; set; }
        public double? BonusPerMonth { get; set; }
    }
}
