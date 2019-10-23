using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.API.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        [Phone]
        [StringLength(15)]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(78)]
        public string Email { get; set; }
    }

    public class UserLoginResultDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }


    public class PhoneValidationResultDTO : BooleanResultDTO
    {
        public string AccessToken { get; set; }
    }

    public class UserRegisterDTO : UserLoginDTO
    {

    }

    public class UserRegisterResultDTO : UserLoginResultDTO
    {

    }
}
