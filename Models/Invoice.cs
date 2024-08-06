using System.ComponentModel.DataAnnotations.Schema;
using IntegratedInventoryAndOrderManagementSystem.Models;

namespace IntegratedInventoryAndOrderManagementSystem.Models;

public class Invoice
{
    public int Id { get; set; }
    public int SalesOrderId { get; set; }
    [ForeignKey("SalesOrderId")]
    public SalesOrder SalesOrder { get; set; }
    public string InvoiceNumber { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public InvoiceStatus Status { get; set; } // e.g., "Paid", "Unpaid", "Overdue"
    public DateTime? DueDate { get; set; }
}

public enum InvoiceStatus
{
    Paid,
    Unpaid,
    Overdue
}