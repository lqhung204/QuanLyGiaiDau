using System;

namespace QuanLyGiaiDau.Models
{
    public class TiSoModel
    {
        public DoiDau Doi1 { get; set; }
        public DoiDau Doi2 { get; set; }
        public int TiSoDoi1 { get; set; }
        public int TiSoDoi2 { get; set;}
        public int DoiThang { get; set; }
        public string SanDau { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public GiaiDau GiaiDau { get; set; }
    }
}
