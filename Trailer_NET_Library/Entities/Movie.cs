using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Trailer_NET_Library.Abstract;

namespace Trailer_NET_Library.Entities
{
    public class Movie: Entity<Guid>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        public string Director { get; set; }
        public string Writer { get; set; }
        public string Producer { get; set; }

        public string ImageID { get; set; }
        public string GenreID { get; set; }

        public DateTime Release_Date { get; set; }
        public string Trailer_Url { get; set; }
        public DateTime Created_Date { get; set; }

        public virtual Image Image { get; set; }
        public virtual Genre Genre { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
