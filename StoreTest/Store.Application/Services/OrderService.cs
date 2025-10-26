using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Application.DTOs;
using Store.Domain.Entities;
using Store.Infrastructure.Repositories;

namespace Store.Application.Services
{
    public interface IOrderService
    {
        Task<Guid> StartOrderAsync();
        Task AddItemAsync(Guid orderId, AddItemRequest req);
        Task RemoveItemAsync(Guid orderId, Guid itemId);
        Task CloseOrderAsync(Guid orderId);
        Task<OrderDto?> GetOrderAsync(Guid orderId);
        Task<IEnumerable<OrderDto>> ListOrdersAsync(int page, int pageSize, string? status);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;

        public OrderService(IOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task<Guid> StartOrderAsync()
        {
            var order = new Order();
            await _repo.AddAsync(order);
            await _repo.SaveChangesAsync();
            return order.Id;
        }

        public async Task AddItemAsync(Guid orderId, AddItemRequest req)
        {
            var order = await _repo.GetByIdAsync(orderId)
                ?? throw new InvalidOperationException("Pedido não encontrado");

            var item = new OrderItem(req.ProductId, req.Name, req.UnitPrice, req.Quantity);
            order.AddItem(item);

            await _repo.SaveChangesAsync();
        }

        public async Task RemoveItemAsync(Guid orderId, Guid itemId)
        {
            var order = await _repo.GetByIdAsync(orderId)
                ?? throw new InvalidOperationException("Pedido não encontrado");

            order.RemoveItem(itemId);
            await _repo.SaveChangesAsync();
        }

        public async Task CloseOrderAsync(Guid orderId)
        {
            var order = await _repo.GetByIdAsync(orderId)
                ?? throw new InvalidOperationException("Pedido não encontrado");

            order.Close();
            await _repo.SaveChangesAsync();
        }

        public async Task<OrderDto?> GetOrderAsync(Guid orderId)
        {
            var order = await _repo.GetByIdAsync(orderId);
            if (order == null) return null;
            return Map(order);
        }

        public async Task<IEnumerable<OrderDto>> ListOrdersAsync(int page, int pageSize, string? status)
        {
            var list = await _repo.ListAsync(page, pageSize, status);
            return list.Select(Map);
        }

        private OrderDto Map(Order o)
        {
            return new OrderDto
            {
                Id = o.Id,
                CreatedAt = o.CreatedAt,
                Status = o.Status.ToString(),
                Total = o.Total,
                Items = o.Items.Select(i => new OrderItemDto
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Name = i.Name,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity,
                    Total = i.Total
                }).ToList()
            };
        }
    }
}
