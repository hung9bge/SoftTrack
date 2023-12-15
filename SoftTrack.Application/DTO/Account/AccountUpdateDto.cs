using SoftTrack.Domain;

namespace SoftTrack.Manage.DTO
{
    public class AccountUpdateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public int RoleId { get; set; }
   

    }
}
