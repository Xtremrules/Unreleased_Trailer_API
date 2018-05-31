using System;
using System.ComponentModel.DataAnnotations;

namespace Trailer_NET_API.Models
{
    public class Movie
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        [Required]
        public string Producer { get; set; }
        public string Image_Url { get; set; }
        public int? GenreID { get; set; }
        public DateTime? Release_Date { get; set; }
        [Required]
        [RegularExpression("^(http(s)??\\:\\/\\/)?(www\\.|m\\.)?((youtube\\.com\\/watch\\?v=)|(youtu.be\\/))([a-zA-Z0-9\\-_]{11})$", ErrorMessage = "Not a youtube link")]
        public string Trailer_Url { get; set; }
        public DateTime? Created_Date { get; set; }
    }
}