using System;
using System.Collections.Generic;
using System.Linq;
using Store.Domain.Enums;

namespace Store.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public OrderStatus Status { get; private set; } = OrderStatus.Open;

        private readonly List<OrderItem> _items = new();
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        public Order() { } // usado pelo EF Core

        public void AddItem(OrderItem item)
        {
            if (Status == OrderStatus.Closed)
                throw new InvalidOperationException("Não é possível adicionar itens a um pedido fechado.");

            var existing = _items.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (existing != null)
            {
                existing.ChangeQuantity(existing.Quantity + item.Quantity);
            }
            else
            {
                _items.Add(item);
            }
        }

        public void RemoveItem(Guid itemId)
        {
            if (Status == OrderStatus.Closed)
                throw new InvalidOperationException("Não é possível remover itens de um pedido fechado.");

            var item = _items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                throw new InvalidOperationException("Item não encontrado.");

            _items.Remove(item);
        }

        public void Close()
        {
            if (Status == OrderStatus.Closed)
                throw new InvalidOperationException("Pedido já está fechado.");

            if (!_items.Any())
                throw new InvalidOperationException("Não é possível fechar um pedido sem itens.");

            Status = OrderStatus.Closed;
        }

        public decimal Total => _items.Sum(i => i.Total);
    }
}
