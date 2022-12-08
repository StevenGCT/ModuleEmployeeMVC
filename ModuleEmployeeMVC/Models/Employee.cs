using System.ComponentModel.DataAnnotations;

namespace ModuleEmployeeMVC.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string SecondLastName { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Type { get; set; }

        public string Ci { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public string Photo { get; set; }

        public char Status { get; set; } = '1';

        public DateTime RegisterDate { get; set; } = DateTime.Now;
        //----
        [System.Text.Json.Serialization.JsonIgnore]
        public List<Event>? Events { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public List<Schedule>? Schedules { get; set; }
    }
}
