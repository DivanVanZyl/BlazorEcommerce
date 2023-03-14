namespace BlazorEcommerce.Client.Services.Order
{
    public interface IOrderService
    {
        public Task PlaceOrder();
        Task<List<OrderOverviewResponse>> GetOrders();
        Task<OrderDetailsResponse> GetOrderDetails(int orderId);
    }
}
