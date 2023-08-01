using Core;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service;

public class OgrenciService : IOgrenciService
{
    public IMongoCollection<Ogrenci> GetOgrencilerCollection()
    {
        var client = new MongoClient("mongodb://localhost:27017/?retryWrites=true&connectTimeoutMS=10000");
        var database = client.GetDatabase("UdemyTutorial");

        return database.GetCollection<Ogrenci>("Ogrenciler");
    }
    public async Task<Ogrenci> AddAsync(Ogrenci ogrenci)
    {
        await GetOgrencilerCollection().InsertOneAsync(ogrenci);

        return ogrenci;
    }

    public async Task DeleteAsync(int okulNo)
    {
        var filters = Builders<Ogrenci>.Filter.Eq(x => x.OkulNo, okulNo);

        var result = await GetOgrencilerCollection().DeleteOneAsync(filters);
    }

    public async Task<List<Ogrenci>> GetAllAsync()
    {
        var filters = Builders<Ogrenci>.Filter.Empty;

        var ogrenciler = await GetOgrencilerCollection().FindAsync(filters);

        return ogrenciler.ToList();
    }

    public async Task<List<Ogrenci>> GetPagedAsync(Request request)
    {
        var filters = Builders<Ogrenci>.Filter.Empty;

        var ogrenciler = GetOgrencilerCollection().Find(filters);


        if (!string.IsNullOrEmpty(request.OrderBy))
        {
            ogrenciler.Sort("{\"" + request.OrderBy + "\":" + (request.OrderByDirection?.ToLower() == "desc" ? "-1" : "1") + "}");
        }

        ogrenciler.Skip(request.Skip()).Limit(request.PageSize);


        return await ogrenciler.ToListAsync();
    }

    public async Task<Ogrenci> GetAsync(int okulNo)
    {
        var filters = Builders<Ogrenci>.Filter.Eq(x => x.OkulNo, okulNo);

        var ogrenci = await GetOgrencilerCollection().FindAsync(filters);

        return ogrenci.FirstOrDefault();
    }

    public async Task UpdateAsync(Ogrenci ogrenci)
    {

        var updateDefinition = Builders<Ogrenci>.Update
            .Set(x => x.Isim, ogrenci.Isim)
            .Set(x => x.Soyisim, ogrenci.Soyisim)
            .Set(x => x.TcKimlikNo, ogrenci.TcKimlikNo)
            .Set(x => x.Cinsiyet, ogrenci.Cinsiyet)
            .Set(x => x.DogumTarih, ogrenci.DogumTarih)
            .Set(x => x.Nakil, ogrenci.Nakil)
            .Set(x => x.Kilo, ogrenci.Kilo);

        var filters = Builders<Ogrenci>.Filter.Eq(x => x.OkulNo, ogrenci.OkulNo);

        var result = await GetOgrencilerCollection().UpdateOneAsync(filters, updateDefinition);


    }


    public async Task UpsertAsync(Ogrenci ogrenci)
    {

        var updateDefinition = Builders<Ogrenci>.Update
            .Set(x => x.Isim, ogrenci.Isim)
            .Set(x => x.Soyisim, ogrenci.Soyisim)
            .Set(x => x.TcKimlikNo, ogrenci.TcKimlikNo)
            .Set(x => x.Cinsiyet, ogrenci.Cinsiyet)
            .Set(x => x.DogumTarih, ogrenci.DogumTarih)
            .Set(x => x.Nakil, ogrenci.Nakil)
            .Set(x => x.Kilo, ogrenci.Kilo);

        var filters = Builders<Ogrenci>.Filter.Eq(x => x.OkulNo, ogrenci.OkulNo);

        var result = await GetOgrencilerCollection().UpdateOneAsync(filters, updateDefinition, new UpdateOptions() { IsUpsert = true });


    }

    public async Task<BulkWriteResult<Ogrenci>> UpsertManyBulkAsync(List<Ogrenci> ogrenciler)
    {
        var listWrites = new List<WriteModel<Ogrenci>>();
        foreach (var ogrenci in ogrenciler)
        {
            var updateDefinition = Builders<Ogrenci>.Update
                .Set(x => x.Isim, ogrenci.Isim)
                .Set(x => x.Soyisim, ogrenci.Soyisim)
                .Set(x => x.TcKimlikNo, ogrenci.TcKimlikNo)
                .Set(x => x.Cinsiyet, ogrenci.Cinsiyet)
                .Set(x => x.DogumTarih, ogrenci.DogumTarih)
                .Set(x => x.Nakil, ogrenci.Nakil)
                .Set(x => x.Kilo, ogrenci.Kilo);

            var filters = Builders<Ogrenci>.Filter.Eq(x => x.OkulNo, ogrenci.OkulNo);

            var upsertOne = new UpdateOneModel<Ogrenci>(filters, updateDefinition) { IsUpsert = true };

            listWrites.Add(upsertOne);
        }

        var result = await GetOgrencilerCollection().BulkWriteAsync(listWrites, new BulkWriteOptions()
        {
            IsOrdered = false
        });

        return result;
    }
}
