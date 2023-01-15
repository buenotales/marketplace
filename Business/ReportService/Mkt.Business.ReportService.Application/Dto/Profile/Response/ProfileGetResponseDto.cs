namespace Mkt.Business.ReportService.Application.Dto.Profile.Response
{
    public class ProfileGetResponseDto
    {
        public IEnumerable<Item> Items { get; set; }

        public class Item
        {
            public int ProfileId { get; set; }
        }
    }
}
