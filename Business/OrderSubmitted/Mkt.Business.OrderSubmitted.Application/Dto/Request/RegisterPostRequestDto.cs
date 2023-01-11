namespace Mkt.Business.OrderSubmitted.Application.Dto.Request
{
    public class RegisterPostRequestDto
    {
        public int? OrderId { get; set; }
        public ProfileDto Profile { get; set; }
        public IEnumerable<ItemDto> Items { get; set; }
        public IEnumerable<PaymentDto> Payments { get; set; }

        public class ProfileDto
        {
            public int ProfileId { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Document { get; set; }
        }

        public class ItemDto
        {
            public string ProductId { get; set; }
            public string DisplayName { get; set; }
            public int Quantity { get; set; }
            public decimal UnityPrice { get; set; }
            public string SellerId { get; set; }
        }

        public class PaymentDto
        {
            public string PaymentMethod { get; set; }
            public decimal Amount { get; set; }
            public int? Installments { get; set; }
        }
    }
}
