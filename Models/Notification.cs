using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegratedInventoryAndOrderManagementSystem.Enums;

namespace IntegratedInventoryAndOrderManagementSystem.Models
{
   public class Notification
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public NotificationType Type { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
}

}