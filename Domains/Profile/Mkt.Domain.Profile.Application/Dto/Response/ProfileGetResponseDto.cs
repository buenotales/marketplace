namespace Mkt.Domain.Profile.Application.Dto.Response
{
    public class ProfileGetResponseDto
    {
        public int ProfileId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }

        public ProfileGetResponseDto(int profileId, string fullName, string email, string document)
        {
            ProfileId = profileId;
            FullName = fullName;
            Email = email;
            Document = document;
        }
    }
}
