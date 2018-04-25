using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using Trailer_NET_Library.Entities;

namespace Trailer_NET_DL
{
    public class AppUser: IdentityUser
    {
        public virtual ICollection<Movie> Liked { get; set; }
        public virtual ICollection<Movie> Watch_Later { get; set; }
    }
}
