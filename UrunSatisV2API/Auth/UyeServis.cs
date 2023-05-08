using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UrunSatisV2API.Models;
using UrunSatisV2API.ViewModel;

namespace UrunSatisV2API.Auth
{
    public class UyeServis
    {
        UrunSatisV21Entities db = new UrunSatisV21Entities();


        public UyeModel UyeOturumAc(string kadi, string parola)
        {
            UyeModel uye = db.Uye.Where(x => x.KullaniciAdi == kadi && x.Sifre == parola).Select(s => new UyeModel()
            {
                uyeId = s.uyeId,
                Adsoyad = s.Adsoyad,
                Email = s.Email,
                KullaniciAdi = s.KullaniciAdi,
                Sifre = s.Sifre,
                uyeAdmin = s.uyeAdmin,
                Foto = s.Foto
            }).SingleOrDefault();
            return uye;
        }
    }
}