using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace QuanLyGiaiDau.Models
{
    public class GiaiDau
    {
        [Key]
        
        [Column(TypeName = "int")]
        public int IdGiaiDau { get; set; }

       
        [Column(TypeName ="nvarchar(150)")]
        public string TenGiaiDau { get; set; }

       
        [Column(TypeName = "Datetime")]
        public DateTime NgayBatDau { get; set; }

       
        [Column(TypeName = "nvarchar(150)")]
        public string MoTa { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string DiaDiem { get; set; }

        [Column(TypeName = "bit")]
        public bool TrangThai { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string IdMonTheThao { get; set; }

        [ForeignKey("IdMonTheThao")]
        public virtual MonTheThao MonTheThao { get; set; }

    }
    
}
