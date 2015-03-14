using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotellWebMvc.ViewModels
{

    /// <summary>
    /// Hjelpeklasse for å vurdere riktig parametre 
    /// for innlogging før autorisering
    /// </summary>
    public class AuthLogin
    {
        [Required]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }

    /// <summary>
    /// Ny hjelpeklasse for å vurdere riktig parametre
    /// for registrering av en ny bruker på websiden
    /// </summary>
    public class NewGuest
    {
        [Required, MaxLength(20)]
        [Display(Name="First name")]
        public string FirstName { get; set; }

        [Required, MaxLength(30)]
        [Display(Name="Last name")]
        public string LastName { get; set; }

        [Required, MaxLength(40), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

    }



}