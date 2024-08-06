using IntegratedInventoryAndOrderManagementSystem.Models.ViewModel;

public class ReceivingViewModel
{
    public int PurchaseOrderId { get; set; }
    public DateTime ReceiveDate { get; set; }
    public string ReceivedBy { get; set; }
    public string Notes { get; set; }
    public bool IsCompleted { get; set; }
    public List<RecievePurchaseOrderItemViewModel> PurchaseOrderItems { get; set; }
}