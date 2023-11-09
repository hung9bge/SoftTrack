using SoftTrack.Domain;

namespace SoftTrack.Application.DTO
{
    public class AccountCreateDto
    {
        
        public string Name { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public int RoleId { get; set; }
        //public string RoleName { get; set; }
    }
}
