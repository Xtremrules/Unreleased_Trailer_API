using System;
using Trailer_NET_Library.Abstract;

namespace Trailer_NET_Library.Entities
{
    public class Genre: Entity<Guid>
    {
        public string Title { get; set; }
    }
}