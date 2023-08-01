using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core;

public class Ogrenci
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string Isim { get; set; }
    public string Soyisim { get; set; }
    public string Cinsiyet { get; set; }
    public int OkulNo { get; set; }
    public long TcKimlikNo { get; set; }
    public DateTime DogumTarih { get; set; }
    public bool Nakil { get; set; }
    public double Kilo { get; set; }
}
