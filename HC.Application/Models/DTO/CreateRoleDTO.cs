﻿using HC.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Models.DTO
{
    public class CreateRoleDTO
    {
        public string RoleName { get; set; }

        public List<AppUser> AppUsers { get; set; }
    }
}
