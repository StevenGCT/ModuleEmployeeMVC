using System.ComponentModel.DataAnnotations;

namespace ModuleEmployeeMVC.Models
{
    public class Event
    {
        public int EventId { get; set; }
        
        public string AddressEvent { get; set; }

        public string NameEvent { get; set; }
        public DateTime DateEvent { get; set; } = DateTime.Now;

        public char Status { get; set; } = '1';

        [DataType(DataType.Date)]
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public List<Employee>? Employees { get; set; } //Para hacer realacion con la otra tabla (ICollection = 1
    }
}
