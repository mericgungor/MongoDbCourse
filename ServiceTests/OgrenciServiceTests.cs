using Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Tests
{
    [TestClass()]
    public class OgrenciServiceTests
    {
        [TestMethod()]
        public void UpsertManyBulkAsyncTest()
        {
            List<Ogrenci> ogrenciler = new List<Ogrenci>();

            //eski
            ogrenciler.Add(new Ogrenci()
            {
                Isim = "Karakaya",
                Soyisim = "KOYUNCUK",
                Cinsiyet = "E",
                OkulNo = 100,
                TcKimlikNo = 19384699554,
                DogumTarih = Convert.ToDateTime("2006-09-23"),
                Nakil = false,
                Kilo = 128.9d
            });
            //eski
            ogrenciler.Add(new Ogrenci()
            {
                Isim = "Uluer",
                Soyisim = "ÖZALP",
                Cinsiyet = "E",
                OkulNo = 101,
                TcKimlikNo = 74110979908,
                DogumTarih = Convert.ToDateTime("1977-09-08"),
                Nakil = true,
                Kilo = 72.7d
            });

            //eski
            ogrenciler.Add(new Ogrenci()
            {
                Isim = "Abdulmenav",
                Soyisim = "AKCA ÇAĞLAR",
                Cinsiyet = "E",
                OkulNo = 102,
                TcKimlikNo = 32811980772,
                DogumTarih = Convert.ToDateTime("1985-03-13"),
                Nakil = false,
                Kilo = 95d
            });

            //yeni
            ogrenciler.Add(new Ogrenci()
            {
                Isim = "Süleyman",
                Soyisim = "KORU",
                Cinsiyet = "E",
                OkulNo = 702,
                TcKimlikNo = 12345678903,
                DogumTarih = Convert.ToDateTime("1965-03-13"),
                Nakil = false,
                Kilo = 65d
            });

            OgrenciService ogrenciService = new OgrenciService();

            var result = ogrenciService.UpsertManyBulkAsync(ogrenciler).GetAwaiter().GetResult();

            Assert.IsTrue(result.Upserts.Count == 1);//Eklenen Sayısı
        }
    }
}