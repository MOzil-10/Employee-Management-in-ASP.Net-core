using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Entity
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
    }
}
