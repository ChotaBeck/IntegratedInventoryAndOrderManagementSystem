import java.sql.SQLException;
import java.time.LocalDate;

import dao.OrderDAO;
import enumFile.OrderStatus;
import model.Order;

public class App {
    public static void main(String[] args) throws Exception {
         // Example usage of the DAO
        OrderDAO orderDAO = new OrderDAO();
        Order order = new Order(1, 1, LocalDate.now(), OrderStatus.PROCESSING, "123 Main St");
        try {
            orderDAO.addOrder(order);
            System.out.println("Order added successfully!");

            // Retrieve and print all orders
            orderDAO.getAllOrders().forEach(System.out::println);
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}
