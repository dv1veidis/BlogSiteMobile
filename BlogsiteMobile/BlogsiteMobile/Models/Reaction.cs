using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsiteMobile.Models
{
    public class Reaction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Required]
        [Display(Name = "ApplicationUserId")]
        public int ApplicationUserId { get; set; }
        public string Action { get; set; }
        [Required]
        [Display(Name = "BlogPost")]
        public int BlogPostId { get; set; }

    }
}
