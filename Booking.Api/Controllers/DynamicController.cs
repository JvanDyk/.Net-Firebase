namespace Booking.Api.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class DynamicController : Controller
{
    private readonly IDynamicRepository<DynamicEntity> _dynamicRepo;

    public DynamicController(IDynamicRepository<DynamicEntity> dynamicRepo)
    {
        _dynamicRepo = dynamicRepo;
    }

    [HttpPost("Add")]
    public async Task<IActionResult> AddDynamicBluePrint([FromBody] dynamic bluePrintRequest)
    {
        var entity = new DynamicEntity();
        var bluePrint = JsonConvert.DeserializeObject<JObject>(bluePrintRequest.ToString());
        foreach (var property in bluePrint.Properties())
        {
            if(property.Name.ToLower() == "accountid")
            {
                entity.AccountId = property.Value.ToObject<string>();
            }
            else
            {
                entity.SetProperty(property.Name, property.Value.ToObject<dynamic>());
            }
        }

        var id = await _dynamicRepo.SetAsync(entity);
        return Ok(new { Id = id });
    }

    [HttpGet]
    public async Task<IActionResult> GetDynamicBluePrints([FromQuery] string accountId)
    {
        if (string.IsNullOrEmpty(accountId))
        {
            return BadRequest();
        }
        var bookingCollection = await _dynamicRepo.GetAllAsync(accountId);
        return Ok(bookingCollection);
    }

    [HttpGet("Get")]
    public async Task<IActionResult> GetDynamicBluePrint([FromQuery] string accountId, string id)
    {
        if (string.IsNullOrEmpty(accountId))
        {
            return BadRequest();
        }
        var booking = await _dynamicRepo.GetAsync(id, accountId);
        if (booking == null)
        {
            return NotFound();
        }
        return Ok(booking);
    }

    [HttpPatch("Patch")]
    public async Task<IActionResult> PatchDynamicBluePrint([FromBody] dynamic bluePrintRequest)
    {
        var entity = new DynamicEntity();
        var bluePrint = JsonConvert.DeserializeObject<JObject>(bluePrintRequest.ToString());
        foreach (var property in bluePrint.Properties())
        {
            if (property.Name.ToLower() == "accountid")
            {
                entity.AccountId = property.Value.ToObject<string>();
            }
            else if (property.Name.ToLower() == "id")
            {
                entity.Id = property.Value.ToObject<string>();
            }
            else
            {
                entity.SetProperty(property.Name, property.Value.ToObject<dynamic>());
            }
        }
        await _dynamicRepo.UpdateAsync(entity);
        return Ok();
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteDynamicBluePrint([FromQuery] string accountId, [FromQuery] string id)
    {
        if (string.IsNullOrEmpty(accountId) || string.IsNullOrEmpty(id))
        {
            return BadRequest();
        }
        await _dynamicRepo.DeleteAsync(id, accountId);
        return Ok();
    }
}
