using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrunSatisV2API.ViewModel
{
    public class KategoriModel
    {
        public int kategoriId { get; set; }
        public string kategoriAdi { get; set; }
        public int urunSayisi { get; set; }
    }
}