using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UrunSatisV2API.Models;
using UrunSatisV2API.ViewModel;

namespace UrunSatisV2API.Controllers
{
    
    public class ServisController : ApiController
    {
        UrunSatisV21Entities db = new UrunSatisV21Entities();
        SonucModel sonuc = new SonucModel();

        #region Kategori


        [HttpGet]
        [Route("api/kategoriliste")]
        public List<KategoriModel> KategoriListe()
        {
            List<KategoriModel> liste = db.Kategori.Select(s => new KategoriModel()
            {
                kategoriId = s.kategoriId,
                kategoriAdi = s.kategoriAdi,
                urunSayisi = s.Urun.Count
            }).ToList();

            return liste;
        }
        [HttpGet]
        [Route("api/kategoribyid/{katId}")]
        public KategoriModel KategoriById(int katId)
        {
            KategoriModel kayit = db.Kategori.Where(d => d.kategoriId == katId).Select(s => new KategoriModel()
            {
                kategoriId = s.kategoriId,
                kategoriAdi = s.kategoriAdi,
                urunSayisi = s.Urun.Count
            }).SingleOrDefault();

            return kayit;
        }
        [HttpPost]
        [Route("api/kategoriekle")]
        public SonucModel KategoriEkle(KategoriModel model)
        {
            if (db.Kategori.Count(s => s.kategoriAdi == model.kategoriAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori zaten Kayıtlı!";
                return sonuc;
            }
            Kategori yeni = new Kategori();

            yeni.kategoriAdi = model.kategoriAdi;
            db.Kategori.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/kategoriduzenle")]
        public SonucModel KategoriDuzenle(KategoriModel model)
        {
            Kategori kayit = db.Kategori.Where(s => s.kategoriId == model.kategoriId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadı!";
                return sonuc;
            }
            kayit.kategoriAdi = model.kategoriAdi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori Duzenlendi!";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/kategorisil/{katId}")]
        public SonucModel KategoriSil(int katId)
        {
            Kategori kayit = db.Kategori.Where(s => s.kategoriId == katId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadı!";
                return sonuc;
            }
            if (db.Urun.Count(s => s.kategoriId == katId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Ürün bulunan Kategori Silinemez!";
                return sonuc;
            }
            db.Kategori.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi!";
            return sonuc;
            return sonuc;
        }
        #endregion

        #region Urun
        [HttpGet]
        [Route("api/urunliste")]
        public List<UrunModel> UrunListe()
        {
            List<UrunModel> liste = db.Urun.Select(s => new UrunModel()
            {
                urunId = s.urunId,
                urunAdi = s.urunAdi,
                urunFiyat = (int)s.urunFiyat,
                urunStok = (int)s.urunStok,
                urunFoto = s.urunFoto,
                urunTarih = s.urunTarih,
                kategoriId = s.kategoriId
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/urunlistesoneklenenler/{x}")]
        public List<UrunModel> UrunListeSonlar(int x)
        {
            List<UrunModel> liste = db.Urun.OrderByDescending(o => o.urunId).Take(x).Select(s => new UrunModel()
            {
                urunId = s.urunId,
                urunAdi = s.urunAdi,
                urunFiyat = (int)s.urunFiyat,
                urunStok = (int)s.urunStok,
                urunFoto = s.urunFoto,
                urunTarih = s.urunTarih,
                kategoriId = s.kategoriId
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/urunbykatid/{katId}")]
        public List<UrunModel> UrunByKatId(int katId)
        {
            List<UrunModel> liste = db.Urun.Where(d => d.kategoriId == katId).Select(s => new UrunModel()
            {
                urunId = s.urunId,
                urunAdi = s.urunAdi,
                urunFiyat = (int)s.urunFiyat,
                urunStok = (int)s.urunStok,
                urunFoto = s.urunFoto,
                urunTarih = s.urunTarih,
                kategoriId = s.kategoriId
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/urunbyid/{urunId}")]
        public UrunModel UrunById(int urunId)
        {
            UrunModel kayit = db.Urun.Where(d => d.urunId == urunId).Select(s => new UrunModel()
            {
                urunId = s.urunId,
                urunAdi = s.urunAdi,
                urunFiyat = (int)s.urunFiyat,
                urunStok = (int)s.urunStok,
                urunFoto = s.urunFoto,
                urunTarih = s.urunTarih,
                kategoriId = s.kategoriId
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/urunekle")]
        public SonucModel UrunEkle(UrunModel model)
        {
            if (db.Urun.Count(s => s.urunAdi == model.urunAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ürün zaten Kayıtlı!";
                return sonuc;
            }
            Urun kayit = new Urun();
            kayit.urunAdi = model.urunAdi;
            kayit.urunFiyat = model.urunFiyat;
            kayit.urunFoto = model.urunFoto;
            kayit.urunStok = model.urunStok;
            kayit.urunTarih = model.urunTarih;
            kayit.kategoriId = model.kategoriId;

            db.Urun.Add(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ürün Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/urunduzenle")]
        public SonucModel UrunDuzenle(UrunModel model)
        {
            Urun kayit = db.Urun.Where(d => d.urunId == model.urunId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ürün bulunamadı!";
                return sonuc;
            }
            kayit.urunAdi = model.urunAdi;
            kayit.urunFiyat = model.urunFiyat;
            kayit.urunFoto = model.urunFoto;
            kayit.urunStok = model.urunStok;
            kayit.urunTarih = model.urunTarih;
            kayit.kategoriId = model.kategoriId;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ürün Düzenlendi!";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/urunsil/{urunId}")]

        public SonucModel UrunSil(int urunId)
        {
            Urun kayit = db.Urun.Where(d => d.urunId == urunId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ürün bulunamadı!";
                return sonuc;
            }
            if (db.Yorum.Count(s => s.urunId == urunId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Yorum buulunan Ürün Silinemez!";
                return sonuc;
            }

            db.Urun.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ürün Silindi!";
            return sonuc;
        }
        #endregion

        #region Uye
        [HttpGet]
        [Route("api/uyeliste")]
        public List<UyeModel> UyeListe()
        {
            List<UyeModel> liste = db.Uye.Select(s => new UyeModel()
            {
                uyeId = s.uyeId,
                KullaniciAdi = s.KullaniciAdi,
                Adsoyad = s.Adsoyad,
                Email = s.Email,
                Sifre = s.Sifre,
                Foto = s.Foto,
                uyeAdmin = s.uyeAdmin,
            }).ToList();

            return liste;
        }
        [HttpGet]
        [Route("api/uyebyid/{uyeId}")]
        public UyeModel UyeById(int uyeId)
        {
            UyeModel kayit = db.Uye.Where(d => d.uyeId == uyeId).Select(s => new UyeModel()
            {
                uyeId = s.uyeId,
                KullaniciAdi = s.KullaniciAdi,
                Adsoyad = s.Adsoyad,
                Email = s.Email,
                Sifre = s.Sifre,
                Foto = s.Foto,
                uyeAdmin = s.uyeAdmin,
            }).SingleOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/uyeekle")]
        public SonucModel UyeEkle(UyeModel model)
        {
            if (db.Uye.Count(s => s.KullaniciAdi == model.KullaniciAdi || s.Email == model.Email) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kullanıcı Adı ve ya Email Kayıtlı!";
                return sonuc;
            }
            Uye kayit = new Uye();
            kayit.KullaniciAdi = model.KullaniciAdi;
            kayit.Adsoyad = model.Adsoyad;
            kayit.Email = model.Email;
            kayit.Sifre = model.Sifre;
            kayit.Foto = model.Foto;
            kayit.uyeAdmin = model.uyeAdmin;
            db.Uye.Add(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Üye Eklendi!";
            return sonuc;
        }

        [HttpPost]
        [Route("api/uyekayit")]
        public SonucModel UyeKayit(UyeModel model)
        {
            if (db.Uye.Count(s => s.KullaniciAdi == model.KullaniciAdi || s.Email == model.Email) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kullanıcı Adı ve ya Email Kayıtlı!";
                return sonuc;
            }
            Uye kayit = new Uye();
            kayit.KullaniciAdi = model.KullaniciAdi;
            kayit.Adsoyad = model.Adsoyad;
            kayit.Email = model.Email;
            kayit.Sifre = model.Sifre;
            kayit.Foto = model.Foto;
            kayit.uyeAdmin = model.uyeAdmin;
            db.Uye.Add(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Başarıyla Kayıt Oldunuz!";
            return sonuc;
        }

        [HttpPut]
        [Route("api/uyeduzenle")]
        public SonucModel UyeDuzenle(UyeModel model)
        {
            Uye kayit = db.Uye.Where(d => d.uyeId == model.uyeId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üye Bulunamadı!";
                return sonuc;
            }
            kayit.KullaniciAdi = model.KullaniciAdi;
            kayit.Adsoyad = model.Adsoyad;
            kayit.Email = model.Email;
            kayit.Sifre = model.Sifre;
            kayit.Foto = model.Foto;
            kayit.uyeAdmin = model.uyeAdmin;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Düzenlendi!";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/uyesil/{uyeId}")]
        public SonucModel UyeSil(int uyeId)
        {
            Uye kayit = db.Uye.Where(d => d.uyeId == uyeId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üye Bulunamadı!";
                return sonuc;
            }
            if (db.Sepet.Count(d => d.uyeId == uyeId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Sepeti Boşaltman gerek!";
                return sonuc;
            }
            db.Uye.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Silindi!";
            return sonuc;
        }


        [HttpPost]
        [Route("api/fotoguncelle")]
        public SonucModel UyeFotoGuncelle(UyeFotoModel model)
        {
            Uye uye = db.Uye.Where(d => d.uyeId == model.uyeId).SingleOrDefault();
            if (uye== null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Uye Bulunamadı!";
                return sonuc;

            }

            if (uye.Foto != "profil.jpg")
            {
                string yol = System.Web.Hosting.HostingEnvironment.MapPath("~/Dosyalar/"+uye.Foto);
                if (File.Exists(yol))
                {
                    File.Delete(yol);
                }
            }

            string data = model.fotoData;
            string base64 = data.Substring(data.IndexOf(',')+1);
            base64 = base64.Trim('\0');
            byte[] imgbytes = Convert.FromBase64String(base64);
            string dosyaAdi = uye.uyeId + model.fotoUzanti.Replace("image/", ".");

            using (var ms = new MemoryStream(imgbytes,0,imgbytes.Length))
            {
                Image img = Image.FromStream(ms, true);
                img.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/Dosyalar/" + dosyaAdi));
            }
            uye.Foto = dosyaAdi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Fotoğraf Güncellendi!";
            return sonuc;
        }
        #endregion

        #region Yorum
        [HttpGet]
        [Route("api/yorumliste")]
        public List<YorumModel> YorumListe()
        {
            List<YorumModel> liste = db.Yorum.Select(s => new YorumModel()
            {
                yorumId = s.yorumId,
                yorumIcerik = s.yorumIcerik,
                Tarih = s.Tarih,
                urunId = s.urunId,
                uyeId = s.uyeId,
                KullaniciAdi = s.Uye.KullaniciAdi,
                urunAdi = s.Urun.urunAdi
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/yorumlistebyuyeid/{uyeId}")]
        public List<YorumModel> YorumListeByUyeId(int uyeId)
        {
            List<YorumModel> liste = db.Yorum.Where(d => d.uyeId == uyeId).Select(s => new YorumModel()
            {
                yorumId = s.yorumId,
                yorumIcerik = s.yorumIcerik,
                Tarih = s.Tarih,
                urunId = s.urunId,
                uyeId = s.uyeId,
                KullaniciAdi = s.Uye.KullaniciAdi,
                urunAdi = s.Urun.urunAdi
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/yorumlistebyurunid/{urunId}")]
        public List<YorumModel> YorumListeByUrunId(int urunId)
        {
            List<YorumModel> liste = db.Yorum.Where(d => d.urunId == urunId).Select(s => new YorumModel()
            {
                yorumId = s.yorumId,
                yorumIcerik = s.yorumIcerik,
                Tarih = s.Tarih,
                urunId = s.urunId,
                uyeId = s.uyeId,
                KullaniciAdi = s.Uye.KullaniciAdi,
                urunAdi = s.Urun.urunAdi
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/yorumlistesoneklenenler/{x}")]
        public List<YorumModel> YorumListeSonEklenenler(int x)
        {
            List<YorumModel> liste = db.Yorum.OrderByDescending(o => o.urunId).Take(x).Select(s => new YorumModel()
            {
                yorumId = s.yorumId,
                yorumIcerik = s.yorumIcerik,
                Tarih = s.Tarih,
                urunId = s.urunId,
                uyeId = s.uyeId,
                KullaniciAdi = s.Uye.KullaniciAdi,
                urunAdi = s.Urun.urunAdi
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/yorumbyid/{yorumId}")]
        public YorumModel YorumById(int yorumId)
        {
            YorumModel kayit = db.Yorum.Where(d => d.yorumId == yorumId).Select(s => new YorumModel()
            {
                yorumId = s.yorumId,
                yorumIcerik = s.yorumIcerik,
                Tarih = s.Tarih,
                urunId = s.urunId,
                uyeId = s.uyeId,
                KullaniciAdi = s.Uye.KullaniciAdi,
                urunAdi = s.Urun.urunAdi
            }).SingleOrDefault();
            return kayit;
        }
        [HttpPost]
        [Route("api/yorumekle")]
        public SonucModel YorumEkle(YorumModel model)
        {
            if (db.Yorum.Count(d => d.uyeId == model.uyeId && d.urunId == model.urunId && d.yorumIcerik == model.yorumIcerik) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Aynı Yorumu sadece Bir Kez yapabilirsiniz!";
                return sonuc;
            }
            Yorum kayit = new Yorum();
            kayit.yorumIcerik = model.yorumIcerik;
            kayit.Tarih = model.Tarih;
            kayit.urunId = model.urunId;
            kayit.uyeId = model.uyeId;
            db.Yorum.Add(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Yorum Eklendi!";
            return sonuc;
        }
        [HttpPut]
        [Route("api/yorumduzenle")]
        public SonucModel YorumDuzenle(YorumModel model)
        {
            Yorum kayit = db.Yorum.Where(d => d.yorumId == model.yorumId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Yorum Bulunamadı!";
                return sonuc;
            }

            kayit.yorumId = model.yorumId;
            kayit.yorumIcerik = model.yorumIcerik;
            kayit.Tarih = model.Tarih;
            kayit.urunId = model.urunId;
            kayit.uyeId = model.uyeId;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Yorum Düzenlendi!";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/yorumsil/{yorumId}")]
        public SonucModel YorumSil(int yorumId)
        {
            Yorum kayit = db.Yorum.Where(d => d.yorumId == yorumId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Yorum Bulunamadı!";
                return sonuc;
            }
            db.Yorum.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Yorum Silindi!";
            return sonuc;
        }
        #endregion

        #region Sepet
        [HttpGet]
        [Route("api/sepetlistebyuyeid/{uyeId}")]
        public List<SepetModel> SepetListeByUyeId(int uyeId)
        {
            List<SepetModel> liste = db.Sepet.Where(d => d.uyeId == uyeId).Select(s => new SepetModel()
            {
                sepetId = s.sepetId,
                uyeId = s.uyeId,
                urunId = s.urunId,
                Adet = s.Adet,
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.urunBilgi = UrunById(kayit.urunId);
            }
            return liste;
        }

        [HttpDelete]
        [Route("api/sepettemizle/{uyeId}")]
        public SonucModel SepetTemizle(int uyeId)
        {
            List<Sepet> liste = db.Sepet.Where(d => d.uyeId == uyeId).ToList();

            foreach (var item in liste)
            {
                db.Sepet.Remove(item);
            }
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Sepet temizlendi!";
            return sonuc;
        }

        [HttpPost]
        [Route("api/sepetekle")]
        public SonucModel SepetEkle(SepetModel model)
        {
            if (model.uyeId == null)
            {
                sonuc.islem = true;
                sonuc.mesaj = "Lütfen Giriş yapınız!";
                return sonuc;
            }
            Sepet kayit = new Sepet();
            kayit.Adet = model.Adet;
            kayit.urunId = model.urunId;
            kayit.uyeId = model.uyeId;
            db.Sepet.Add(kayit);
            UrunModel urun = UrunById(model.urunId);
            urun.urunStok -= (int)model.Adet;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ürün Sepete Eklendi!";
            return sonuc;
        }
        [HttpPut]
        [Route("api/sepetduzenle")]
        public SonucModel SepetDuzenle(SepetModel model)
        {
            Sepet kayit = db.Sepet.Where(d => d.sepetId == model.sepetId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ürün Bulunamadı!";
                return sonuc;
            }
            kayit.Adet = model.Adet;
            kayit.urunId = model.urunId;
            kayit.uyeId = model.uyeId;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Sepet Güncellendi!";
            return sonuc;
        }


        [HttpDelete]
        [Route("api/sepetsil/{sepetId}")]
        public SonucModel SepetSil(int sepetId)
        {
            Sepet kayit = db.Sepet.Where(d => d.sepetId == sepetId).SingleOrDefault();

            db.Sepet.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ürün Silindi!!";
            return sonuc;
        }
        #endregion

        #region Begen

        [HttpGet]
        [Route("api/begenlistebyuyeid/{uyeId}")]
        public List<BegenModel> BegenListeByUyeId(int uyeId)
        {   
            List<BegenModel> liste = db.Begen.Where(d => d.uyeId == uyeId).Select(s => new BegenModel()
            {
                begenId = s.begenId,
                uyeId = s.uyeId,
                urunId = s.urunId
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.urunBilgi = UrunById(kayit.urunId);
            }
            return liste;
        }
        [HttpPost]
        [Route("api/begenekle")]
        public SonucModel BegenEkle(BegenModel model)
        {
            if (db.Begen.Count(d=> d.uyeId == model.uyeId && d.urunId == model.urunId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ürünü Zaten Beğendiniz!";
                return sonuc;
            }
            Begen kayit = new Begen();
            kayit.urunId = model.urunId;
            kayit.uyeId = model.uyeId;
            db.Begen.Add(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Başarıyla Beğendiniz!";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/begentemizle/{urunId}")]
        public SonucModel BegenTemizle(int urunId)
        {
            List<Begen> liste = db.Begen.Where(d => d.urunId == urunId).ToList();

            foreach (var item in liste)
            {
                db.Begen.Remove(item);
            }
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Beğeniler temizlendi!";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/begensil/{begenId}")]
        public SonucModel BegenSil(int begenId)
        {
            Begen kayit = db.Begen.Where(d => d.begenId == begenId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ürün Beğendiklerinizde Bulunamadı!";
                return sonuc;
            }
            db.Begen.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ürün Beğendiklerinizden Silindi!";
            return sonuc;
        }
        #endregion
    }
}
