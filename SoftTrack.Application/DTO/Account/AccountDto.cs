using SoftTrack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTrack.Application.DTO
{
    public class AccountDto
    {

        public int AccId { get; set; }
        public string Account1 { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        //public virtual ICollection<Device> Devices { get; set; }
        public string token { get; set; }
        public virtual ICollection<RoleAccountDto> RoleAccounts { get; set; }
    }
    public class RoleAccountDto
    {

        public int Id { get; set; }
        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }
    }

}
