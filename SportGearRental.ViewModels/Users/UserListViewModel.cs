using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGearRental.ViewModels.Users
{
    public class UserListViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string? Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDisabled { get; set; }
    }
}
