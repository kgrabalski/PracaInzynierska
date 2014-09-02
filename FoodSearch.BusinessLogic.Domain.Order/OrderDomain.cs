
using FoodSearch.BusinessLogic.Domain.Order.Interface;
using FoodSearch.Data.Mapping.Interface;


namespace FoodSearch.BusinessLogic.Domain.Order
{
    public class OrderDomain : IOrderDomain
    {
        private readonly IRepositoryProvider _provider;

        public OrderDomain(IRepositoryProvider provider)
        {
            _provider = provider;
        }
    }
}
