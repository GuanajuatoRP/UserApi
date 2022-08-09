﻿using System.ComponentModel.DataAnnotations;
using UserApi.Data.Enum;

namespace UserApi.Models.Sessions
{
    public class EditSessionDateDTO
    {
        [Required]
        public Guid SessionId { get; set; }
        [Required]
        public string Debut { get; set; }
        [Required]
        public string Fin { get; set; }
    }
}
