using Mkt.Domain.Product.Application.Dto.Request;

namespace Mkt.Domain.Product.Application.Infraestructure.Entities
{
    public class ProductEntity
    {
        public int ProductId { get; set; }
        public string DisplayName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public static explicit operator ProductEntity(ProductPostRequestDto.ItemDto v)
        {
            return new ProductEntity()
            {
                DisplayName = v.DisplayName,
                Quantity = v.Quantity,
                UnitPrice = v.UnitPrice,
            };
        }
    }
}
