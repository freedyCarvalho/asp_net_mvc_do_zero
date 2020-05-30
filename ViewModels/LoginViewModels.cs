using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LanchesMacCurso.ViewModels
{
    public class LoginViewModels
    {
        [Required]
        [Display(Name ="Usuário")]     
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string  Password { get; set; }


        public string ReturnUrl { get; set; }
    }
}
