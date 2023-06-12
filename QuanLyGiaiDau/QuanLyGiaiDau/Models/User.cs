using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace QuanLyGiaiDau.Models
{
    public class User
    {
        [Key]
        [Column(TypeName = "varchar(10)")]
        public string IdUser { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string FullName { get; set; }

        public string gender;
        public string Gender { get => gender; set => gender = value; }

        [Column(TypeName = "date")]
        public DateTime Birthday { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Password { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Address { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Role { get; set; }

        [Column(TypeName = "bit")]
        public bool TrangThai { get; set; }


    }
}