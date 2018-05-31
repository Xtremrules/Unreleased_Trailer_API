using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trailer_NET_Library.Abstract;

namespace Trailer_NET_Library.Entities
{
    public class Movie: Entity<int>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Producer { get; set; }
        public int? GenreID { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Release_Date { get; set; }
        [Required, MaxLength(15)]
        public string Youtube_Video_Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Created_Date { get; set; }

        //[ForeignKey("ImageID")]
        //public virtual Image Image { get; set; }
        [ForeignKey("GenreID")]
        public virtual Genre Genre { get; set; }

        //public virtual ICollection<Image> Images { get; set; }
    }
}
