using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ModuleEmployeeMVC.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }

        public string NameDay { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan FinalTime { get; set; }

        public char Status { get; set; } = '1';

        [DataType(DataType.Date)]
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        //---
        [JsonIgnore]
        public List<Employee>? Employees { get; set; }
    }
}
