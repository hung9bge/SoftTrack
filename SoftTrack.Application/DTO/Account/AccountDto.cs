﻿using SoftTrack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTrack.Application.DTO
{
    public class AccountDto
    {

       
        public string Account1 { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        //public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<RoleAccount> RoleAccounts { get; set; }
    }
}
