using SoftTrack.Domain;

namespace SoftTrack.Application.DTO
{
    public class AccountCreateDto
    {
        public int AccId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public int RoleId { get; set; }
        //public string RoleName { get; set; }
    }
}
