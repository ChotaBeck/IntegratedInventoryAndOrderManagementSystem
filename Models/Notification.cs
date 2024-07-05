using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratedInventoryAndOrderManagementSystem.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Type { get; set; }
        string Message { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
        public string  RecpientDepartment { get; set; }
    }
}