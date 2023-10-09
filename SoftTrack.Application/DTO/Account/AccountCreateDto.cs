using SoftTrack.Domain;

namespace SoftTrack.Application.DTO
{
    public class AccountCreateDto
    {

        public int AccId { get; set; }
        public string Account1 { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        //public virtual ICollection<Device> Devices { get; set; }
        public string token { get; set; }
        public virtual ICollection<RoleAccountDto> RoleAccounts { get; set; }
    }
    public class RoleAccountDto
    {

        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? AccId { get; set; }
        public virtual Role Role { get; set; }
    }

}
