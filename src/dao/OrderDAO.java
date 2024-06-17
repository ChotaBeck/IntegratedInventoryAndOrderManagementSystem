package dao;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

import enumFile.OrderStatus;
import model.Order;
import service.DatabaseUtil;

public class OrderDAO {

    // Method to add an order
    public void addOrder(Order order) throws SQLException {
        String sql = "INSERT INTO orders (customerId, orderDate, status, shippingAddress) VALUES (?, ?, ?, ?)";

        try (Connection conn = DatabaseUtil.getConnection(); PreparedStatement pstmt = conn.prepareStatement(sql)) {
            pstmt.setInt(1, order.getCustomerId());
            pstmt.setString(2, order.getOrderDate().toString());
            pstmt.setString(3, order.getStatus().toString());
            pstmt.setString(4, order.getShippingAddress());
            pstmt.executeUpdate();
        }
    }

    // Method to get an order by ID
    public Order getOrderById(int orderId) throws SQLException {
        String sql = "SELECT * FROM orders WHERE orderId = ?";
        Order order = null;

        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement pstmt = conn.prepareStatement(sql)) {
            pstmt.setInt(1, orderId);
            try (ResultSet rs = pstmt.executeQuery()) {
                if (rs.next()) {
                    order = new Order(
                            rs.getInt("orderId"),
                            rs.getInt("customerId"),
                            LocalDate.parse(rs.getString("orderDate")),
                            OrderStatus.valueOf(rs.getString("status")),
                            rs.getString("shippingAddress")
                    );
                }
            }
        }

        return order;
    }

    // Method to get all orders
    public List<Order> getAllOrders() throws SQLException {
        List<Order> orders = new ArrayList<>();
        String sql = "SELECT * FROM orders";

        try (Connection conn = DatabaseUtil.getConnection();
             Statement stmt = conn.createStatement();
             ResultSet rs = stmt.executeQuery(sql)) {
            while (rs.next()) {
                Order order = new Order(
                        rs.getInt("orderId"),
                        rs.getInt("customerId"),
                        LocalDate.parse(rs.getString("orderDate")),
                        OrderStatus.valueOf(rs.getString("status")),
                        rs.getString("shippingAddress")
                );
                orders.add(order);
            }
        }

        return orders;
    }

    // Method to update an order
    public void updateOrder(Order order) throws SQLException {
        String sql = "UPDATE orders SET customerId = ?, orderDate = ?, status = ?, shippingAddress = ? WHERE orderId = ?";

        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement pstmt = conn.prepareStatement(sql)) {
            pstmt.setInt(1, order.getCustomerId());
            pstmt.setString(2, order.getOrderDate().toString());
            pstmt.setString(3, order.getStatus().toString());
            pstmt.setString(4, order.getShippingAddress());
            pstmt.setInt(5, order.getOrderId());
            pstmt.executeUpdate();
        }
    }

    // Method to delete an order
    public void deleteOrder(int orderId) throws SQLException {
        String sql = "DELETE FROM orders WHERE orderId = ?";

        try (Connection conn = DatabaseUtil.getConnection();
             PreparedStatement pstmt = conn.prepareStatement(sql)) {
            pstmt.setInt(1, orderId);
            pstmt.executeUpdate();
        }
    }
}
