using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotellWebMvc.ViewModels
{
    /// <summary>
    /// Hjelpeklasse for å vurdere riktig parametre for innlogging
    /// </summary>
    public class AuthLogin
    {
        [Required]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}