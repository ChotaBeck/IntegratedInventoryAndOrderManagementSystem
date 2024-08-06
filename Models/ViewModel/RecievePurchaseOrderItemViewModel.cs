using System.ComponentModel.DataAnnotations;

public class RecievePurchaseOrderItemViewModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    [Display(Name = "Product")]
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public int ReceivedQuantity { get; set; }
}