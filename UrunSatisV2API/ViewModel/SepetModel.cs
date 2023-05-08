using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrunSatisV2API.ViewModel
{
    public class SepetModel
    {
        public int sepetId { get; set; }
        public int uyeId { get; set; }
        public int urunId { get; set; }
        public Nullable<int> Adet { get; set; }
        public UrunModel urunBilgi { get; set; }
    }
}