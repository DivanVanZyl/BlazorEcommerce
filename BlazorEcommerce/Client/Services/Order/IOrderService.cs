namespace BlazorEcommerce.Client.Services.Order
{
    public interface IOrderService
    {
        public Task PlaceOrder();
        Task<List<OrderOverviewResponse>> GetOrders();
    }
}
