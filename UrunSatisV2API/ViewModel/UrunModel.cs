using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrunSatisV2API.ViewModel
{
    public class UrunModel
    {
        public int urunId { get; set; }
        public string urunAdi { get; set; }
        public int urunFiyat { get; set; }
        public int urunStok { get; set; }
        public string urunFoto { get; set; }
        public string urunTarih { get; set; }
        public int kategoriId { get; set; }
    }
}