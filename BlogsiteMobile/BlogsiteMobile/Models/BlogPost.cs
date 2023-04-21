using BlogsiteMobile.Models;
using SQLite;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogsiteMobile.Models
{
    public class BlogPost
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [SQLite.MaxLength(40)]
        public string BlogPostTitle { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        [Required]
        public int Category { get; set; }
    }
}
