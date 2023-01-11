namespace Mkt.Domain.Product.Application.Dto.Request
{
    public class ProductPostRequestDto
    {
        public IEnumerable<ItemDto> Items { get; set; }

        public class ItemDto
        {
            public string DisplayName { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
        }
    }
}
