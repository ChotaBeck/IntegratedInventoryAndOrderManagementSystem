package model;

import java.util.List;

public class Location {
    private int locationId;
    private String name;
    private int capacity;
    private List<Product> currentInventory;

    public Location(int locationId, String name, int capacity, List<Product> currentInventory) {
        this.locationId = locationId;
        this.name = name;
        this.capacity = capacity;
        this.currentInventory = currentInventory;
    }

    public int getLocationId() {
        return locationId;
    }

    public void setLocationId(int locationId) {
        this.locationId = locationId;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getCapacity() {
        return capacity;
    }

    public void setCapacity(int capacity) {
        this.capacity = capacity;
    }

    public List<Product> getCurrentInventory() {
        return currentInventory;
    }

    public void setCurrentInventory(List<Product> currentInventory) {
        this.currentInventory = currentInventory;
    }

    

}   
