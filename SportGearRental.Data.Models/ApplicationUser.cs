using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportGearRental.Common;
using static SportGearRental.Common.EntityValidationConstants.UserValidation;

namespace SportGearRental.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MinLength(UsernameMinLength)]
        [MaxLength(UsernameMaxLength)]
        [Comment("Username of the user")]
        public override string? UserName { get; set; }

        [Comment("Is the account deleted or not")]
        public bool IsDeleted { get; set; } = false;
        public ICollection<Rental> Rentals { get; set; } = new HashSet<Rental>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}
