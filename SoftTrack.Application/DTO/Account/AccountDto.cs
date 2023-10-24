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
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        //public virtual RoleDTO Role { get; set; }
        //public virtual ICollection<Software> Softwares { get; set; }
    }
    //public class RoleDTO
    //{
    //    public int RoleId { get; set; }
    //    public string Name { get; set; }
    //}

}
