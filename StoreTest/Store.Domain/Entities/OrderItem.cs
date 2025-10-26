using System;

namespace Store.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid ProductId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        private OrderItem() { } // usado pelo Entity Framework

        public OrderItem(Guid productId, string name, decimal unitPrice, int quantity)
        {
            ProductId = productId;
            Name = name;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public decimal Total => UnitPrice * Quantity;

        public void ChangeQuantity(int newQty)
        {
            if (newQty <= 0)
                throw new ArgumentException("Quantidade deve ser maior que 0");

            Quantity = newQty;
        }
    }
}
