﻿using System.ComponentModel.DataAnnotations;

namespace UserApi.Models.Sessions
{
    public class AddUserDTO
    {
        [Required]
        public string userId { get; set; }
    }
}
