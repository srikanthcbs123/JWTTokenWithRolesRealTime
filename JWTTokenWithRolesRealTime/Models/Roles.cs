﻿using System.ComponentModel.DataAnnotations;

namespace JWTTokenWithRolesRealTime.Models
{
    public class Roles
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}
