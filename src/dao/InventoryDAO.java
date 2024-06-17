package dao;

import model.Inventory;
import service.DatabaseUtil;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class InventoryDAO {

    // Method to add inventory
    public void addInventory(Inventory inventory) throws SQLException {
        String sql = "INSERT INTO inventory (productId, quantity, locationId) VALUES (?, ?, ?)";

        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement pstmt = conn.prepareStatement(sql)) {
            pstmt.setInt(1, inventory.getProductId());
            pstmt.setInt(2, inventory.getQuantity());
            pstmt.setInt(3, inventory.getLocationId());
            pstmt.executeUpdate();
        }
    }

    // Method to get inventory by ID
    public Inventory getInventoryById(int inventoryId) throws SQLException {
        String sql = "SELECT * FROM inventory WHERE inventoryId = ?";
        Inventory inventory = null;

        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement pstmt = conn.prepareStatement(sql)) {
            pstmt.setInt(1, inventoryId);
            try (ResultSet rs = pstmt.executeQuery()) {
                if (rs.next()) {
                    inventory = new Inventory(
                            rs.getInt("inventoryId"),
                            rs.getInt("productId"),
                            rs.getInt("quantity"),
                            rs.getInt("locationId")
                    );
                }
            }
        }

        return inventory;
    }

    // Method to get all inventory
    public List<Inventory> getAllInventory() throws SQLException {
        List<Inventory> inventoryList = new ArrayList<>();
        String sql = "SELECT * FROM inventory";

        try (Connection conn = DatabaseUtil.getConnection();
             Statement stmt = conn.createStatement();
             ResultSet rs = stmt.executeQuery(sql)) {
            while (rs.next()) {
                Inventory inventory = new Inventory(
                        rs.getInt("inventoryId"),
                        rs.getInt("productId"),
                        rs.getInt("quantity"),
                        rs.getInt("locationId")
                );
                inventoryList.add(inventory);
            }
        }

        return inventoryList;
    }

    // Method to update inventory
    public void updateInventory(Inventory inventory) throws SQLException {
        String sql = "UPDATE inventory SET productId = ?, quantity = ?, locationId = ? WHERE inventoryId = ?";

        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement pstmt = conn.prepareStatement(sql)) {
            pstmt.setInt(1, inventory.getProductId());
            pstmt.setInt(2, inventory.getQuantity());
            pstmt.setInt(3, inventory.getLocationId());
            pstmt.setInt(4, inventory.getInventoryId());
            pstmt.executeUpdate();
        }
    }

    // Method to delete inventory
    public void deleteInventory(int inventoryId) throws SQLException {
        String sql = "DELETE FROM inventory WHERE inventoryId = ?";

        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement pstmt = conn.prepareStatement(sql)) {
            pstmt.setInt(1, inventoryId);
            pstmt.executeUpdate();
        }
    }
}