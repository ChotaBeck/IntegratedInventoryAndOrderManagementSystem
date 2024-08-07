using System;
using System.IO;
using IntegratedInventoryAndOrderManagementSystem.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace IntegratedInventoryAndOrderManagementSystem.Services
{
    public class InvoicePdfService
    {
        public byte[] GenerateInvoicePdf(Invoice invoice, List<SalesOrderItem> orderItems)
        {
            if (invoice == null)
            {
                throw new ArgumentNullException(nameof(invoice), "Invoice cannot be null");
            }

            if (orderItems == null)
            {
                throw new ArgumentNullException(nameof(orderItems), "Order items cannot be null");
            }

            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);

                document.Open();

                // Add company logo
                // string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "logo.png");
                // Image logo = Image.GetInstance(imagePath);
                // logo.ScaleToFit(140f, 120f);
                // logo.Alignment = Element.ALIGN_LEFT;
                // document.Add(logo);

                // Add invoice details
                Paragraph header = new Paragraph("INVOICE", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD));
                header.Alignment = Element.ALIGN_CENTER;
                document.Add(header);

                document.Add(new Paragraph($"Invoice Number: {invoice.InvoiceNumber}"));
                document.Add(new Paragraph($"Date: {invoice.InvoiceDate.ToShortDateString()}"));
                document.Add(new Paragraph($"Due Date: {invoice.DueDate?.ToShortDateString() ?? "N/A"}"));
                document.Add(new Paragraph($"Status: {invoice.Status}"));
                document.Add(new Paragraph("\n"));

                // Add order items table
                PdfPTable table = new PdfPTable(4);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 2f, 1f, 1f, 1f });

                table.AddCell("Product");
                table.AddCell("Quantity");
                table.AddCell("Unit Price");
                table.AddCell("Total");

                foreach (var item in orderItems)
                {
                    if (item.Product == null)
                    {
                        throw new ArgumentNullException(nameof(item.Product), "Product cannot be null");
                    }

                    table.AddCell(item.Product.Name);
                    table.AddCell(item.Quantity.ToString());
                    table.AddCell(item.SellingPrice.ToString("C"));
                    table.AddCell((item.Quantity * item.SellingPrice).ToString("C"));
                }

                document.Add(table);

                // Add total amount
                Paragraph total = new Paragraph($"Total Amount: {invoice.TotalAmount.ToString("C")}", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD));
                total.Alignment = Element.ALIGN_RIGHT;
                document.Add(total);

                document.Close();
                return ms.ToArray();
            }
        }
    }
}
