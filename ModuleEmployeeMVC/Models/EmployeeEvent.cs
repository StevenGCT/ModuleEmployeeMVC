using System.ComponentModel.DataAnnotations;

namespace ModuleEmployeeMVC.Models
{
    public class EmployeeEvent
    {
        public int EmployeeId { get; set; }
        public int EventId { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateAttendance { get; set; }
        public char StatusAttendance { get; set; } = '0';
        public char Status { get; set; } = '1';
    }
}
