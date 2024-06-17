package model;

public class Inventory {
    private int inventoryId;
    private int productId;
    private int quantity;
    private int locationId;
    
    public Inventory(int inventoryId,int productId, int quantity, int locationId) {
        this.inventoryId = inventoryId;
        this.productId = productId;
        this.quantity = quantity;
        this.locationId = locationId;
    }

    
    public int getProductId() {
        return productId;
    }

    public void setProductId(int productId) {
        this.productId = productId;
    }

    public int getQuantity() {
        return quantity;
    }

    public void setQuantity(int quantity) {
        this.quantity = quantity;
    }

    public int getLocationId() {
        return locationId;
    }

    public void setLocationId(int locationId) {
        this.locationId = locationId;
    }


    public int getInventoryId() {
        return inventoryId;
    }


    public void setInventoryId(int inventoryId) {
        this.inventoryId = inventoryId;
    }

    

}
