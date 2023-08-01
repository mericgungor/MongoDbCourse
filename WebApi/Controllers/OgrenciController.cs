using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OgrenciController : ControllerBase
{
    private readonly IOgrenciService _ogrenciService;

    public OgrenciController(IOgrenciService ogrenciService)
    {
        _ogrenciService = ogrenciService;
    }

    [HttpGet("{okulNo}")]
    public async Task<IActionResult> Get(int okulNo)
    {
        return Ok(await _ogrenciService.GetAsync(okulNo));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _ogrenciService.GetAllAsync());
    }

    //  /Ogrenci/GetPaged?page=1&pagesize=10&orderby=Soyisim&orderbydirection=asc
    [HttpGet("GetPaged")]
    public async Task<IActionResult> GetPaged([FromQuery] Request request)
    {
        return Ok(await _ogrenciService.GetPagedAsync(request));
    }

    [HttpPost]
    public async Task<IActionResult> Add(Ogrenci ogrenci)
    {
        try
        {
            return Ok(await _ogrenciService.AddAsync(ogrenci));
        }
        catch (Exception ex)
        {
            //return BadRequest(ex.Message);
            return BadRequest("Kayıt Başarısız");
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(Ogrenci ogrenci)
    {
        await _ogrenciService.UpdateAsync(ogrenci);
        return Ok();
    }

    [HttpPost("Upsert")]
    public async Task<IActionResult> Upsert(Ogrenci ogrenci)
    {
        await _ogrenciService.UpsertAsync(ogrenci);
        return Ok();
    }

    [HttpDelete("{okulNo}")]
    public async Task<IActionResult> Delete(int okulNo)
    {
        await _ogrenciService.DeleteAsync(okulNo);
        return Ok();
    }

}
