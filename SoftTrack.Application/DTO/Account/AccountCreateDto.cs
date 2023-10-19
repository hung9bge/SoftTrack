using SoftTrack.Domain;

namespace SoftTrack.Application.DTO
{
    public class AccountCreateDto
    {
        public int AccId { get; set; }
        public string Account1 { get; set; }
        //public string Password { get; set; }
        public string Email { get; set; }
        public string Role_Name { get; set; }
        //public string token { get; set; }
    }
}
