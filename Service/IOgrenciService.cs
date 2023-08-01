using Core;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service;

public interface IOgrenciService
{
    Task<Ogrenci> GetAsync(int okulNo);

    Task<List<Ogrenci>> GetAllAsync();

    Task<List<Ogrenci>> GetPagedAsync(Request request);

    Task<Ogrenci> AddAsync(Ogrenci ogrenci);

    Task UpdateAsync(Ogrenci ogrenci);

    Task UpsertAsync(Ogrenci ogrenci);

    Task<BulkWriteResult<Ogrenci>> UpsertManyBulkAsync(List<Ogrenci> ogrenciler);

    Task DeleteAsync(int okulNo);
}
