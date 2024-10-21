using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class BaiHat
    {
        public Guid Id { get; set; }
        public string NgheSi {  get; set; }
        public string TenBaiHat { get; set; }   
        public string Album { get; set; }
        public string TheLoai { get; set; } 
        public TimeSpan ThoiGianPhatHanh { get; set; }
        public DateTime NgayPhatHanh { get; set; }
        public string Status { get; set; }
    }
}
