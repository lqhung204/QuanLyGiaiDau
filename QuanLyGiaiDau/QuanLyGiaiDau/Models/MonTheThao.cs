using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QuanLyGiaiDau.Models
{
    public class MonTheThao
    {
        [Key]
        
        [Column(TypeName = "varchar(10)")]
        public string IdMonTheThao { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string TenMonTheThao { get; set; }
    }
}