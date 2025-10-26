
using Microsoft.AspNetCore.Mvc;
using Store.Application.DTOs;
using Store.Application.Services;

namespace Store.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // POST api/orders/start
        [HttpPost("start")]
        public async Task<ActionResult<CreateOrderResponse>> StartOrder()
        {
            var id = await _orderService.StartOrderAsync();
            return Ok(new CreateOrderResponse { Id = id });
        }

        // POST api/orders/{orderId}/items
        [HttpPost("{orderId}/items")]
        public async Task<ActionResult> AddItem(Guid orderId, [FromBody] AddItemRequest req)
        {
            await _orderService.AddItemAsync(orderId, req);
            return Ok();
        }

        // DELETE api/orders/{orderId}/items/{itemId}
        [HttpDelete("{orderId}/items/{itemId}")]
        public async Task<ActionResult> RemoveItem(Guid orderId, Guid itemId)
        {
            await _orderService.RemoveItemAsync(orderId, itemId);
            return Ok();
        }

        // PUT api/orders/{orderId}/close
        [HttpPut("{orderId}/close")]
        public async Task<ActionResult> CloseOrder(Guid orderId)
        {
            await _orderService.CloseOrderAsync(orderId);
            return Ok();
        }

        // GET api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> ListOrders(
            int page = 1,
            int pageSize = 20,
            string? status = null)
        {
            var list = await _orderService.ListOrdersAsync(page, pageSize, status);
            return Ok(list);
        }

        // GET api/orders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(Guid id)
        {
            var order = await _orderService.GetOrderAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }
    }
}
