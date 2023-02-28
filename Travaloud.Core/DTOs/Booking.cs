namespace Travaloud.Core.DTOs
{
    public class Booking 
    {
        public Guid? Id { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public string CurrencyCode { get; set; }
        public int ItemQuantity { get; set; }
        public bool IsPaid { get; set; }
        public int? InvoiceId { get; set; }
        public List<BookingItem> Items { get; set; } 

        public Booking()
        {

        }

        public Booking(Guid? id, string description, decimal totalAmount, string currencyCode, int itemQuantity, bool isPaid, int? invoiceId, List<BookingItem> items)
        {
            Id = id;
            Description = description;
            TotalAmount = totalAmount;
            CurrencyCode = currencyCode;
            ItemQuantity = itemQuantity;
            IsPaid = isPaid;
            InvoiceId = invoiceId;
            Items = items;
        }
    }
}