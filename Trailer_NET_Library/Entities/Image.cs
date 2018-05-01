using System.Collections.Generic;
using Trailer_NET_Library.Abstract;

namespace Trailer_NET_Library.Entities
{
    public class Image: Entity<int>
    {
        //public string Title { get; set; }
        public string File_Name { get; set; }
        //public string URI { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
