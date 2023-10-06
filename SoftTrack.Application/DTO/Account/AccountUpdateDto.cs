using SoftTrack.Domain;

namespace SoftTrack.Application.DTO
{
    public class AccountUpdateDto
    {
        public int AccId { get; set; }
        public string Account1 { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        //public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<RoleAccount> RoleAccounts { get; set; }

    }
}
