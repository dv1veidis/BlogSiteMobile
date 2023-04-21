using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogsiteMobile.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Required]
        [SQLite.MaxLength(16)]
        public string Name { get; set; }
        [Required]
        [SQLite.MaxLength(30)]
        public string Email { get; set; }
        [Required]
        [SQLite.MaxLength(20)]
        public string Password { get; set; }

    }
}
