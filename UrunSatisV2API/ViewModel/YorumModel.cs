using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrunSatisV2API.ViewModel
{
    public class YorumModel
    {
        public int yorumId { get; set; }
        public string yorumIcerik { get; set; }
        public int uyeId { get; set; }
        public int urunId { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public string KullaniciAdi { get; set; }
        public string urunAdi { get; set; }
    }
}