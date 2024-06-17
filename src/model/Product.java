package model;

import enumFile.ProductStatus;

public class Product {
    private int productId;
    private String name;
    private int quantity;
    private int locationId;
    private ProductStatus status;

    public Product(int productId, String name, int quantity, int locationId, ProductStatus status) {
        this.productId = productId;
        this.name = name;
        this.quantity = quantity;
        this.locationId = locationId;
        this.status = status;
    }

    public int getProductId() {
        return productId;
    }

    public void setProductId(int productId) {
        this.productId = productId;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
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

    public ProductStatus getStatus() {
        return status;
    }

    public void setStatus(ProductStatus status) {
        this.status = status;
    }

    

}
