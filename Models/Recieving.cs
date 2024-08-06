using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IntegratedInventoryAndOrderManagementSystem.Models;

public class Receiving
{
    public int Id { get; set; }
    
    [Required]
    public int PurchaseOrderId { get; set; }
    
    [ForeignKey("PurchaseOrderId")]
    public PurchaseOrder PurchaseOrder { get; set; }
    
    [Required]
    public DateTime ReceiveDate { get; set; }
    
    [Required]
    [StringLength(50)]
    public string ReceivedBy { get; set; }
    
    public string Notes { get; set; }
    
    public bool IsCompleted { get; set; }
}