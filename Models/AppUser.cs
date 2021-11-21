using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthStore.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Messagess = new HashSet<Messagess>();
        }

        // 1 -* AppUser || Messagess

        public virtual ICollection<Messagess> Messagess { get; set; }
    }
}
