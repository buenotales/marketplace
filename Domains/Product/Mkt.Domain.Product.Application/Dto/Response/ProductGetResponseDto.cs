using Mkt.Domain.Product.Application.Infraestructure.Entities;

namespace Mkt.Domain.Product.Application.Dto.Response
{
    public class ProductGetResponseDto
    {
        public IEnumerable<ItemDto> Items { get; set; }

        public ProductGetResponseDto()
        {
            Items = new List<ItemDto>();
        }

        public class ItemDto
        {
            public int ProductId { get; set; }
            public string DisplayName { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }

            public static explicit operator ItemDto(ProductEntity v)
            {
                if (v == null) return null;

                return new ItemDto()
                {
                    ProductId = v.ProductId,
                    DisplayName = v.DisplayName,
                    Quantity = v.Quantity,
                    UnitPrice = v.UnitPrice,
                };
            }
        }
    }
}
