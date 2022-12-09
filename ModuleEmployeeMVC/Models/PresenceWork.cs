using System.ComponentModel.DataAnnotations;

namespace ModuleEmployeeMVC.Models
{
    public class PresenceWork
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateAttenddance { get; set; }

        public char StatusAttendance { get; set; } = '1';

        public string Status { get; set; }
        //------
        public Employee? Employee { get; set; }
        public int EmployeeId { get; set; }
    }
}
