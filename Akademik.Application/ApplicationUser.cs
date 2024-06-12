using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Akademik.Application
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string StudentCardNumber { get; set; }
    }
}
