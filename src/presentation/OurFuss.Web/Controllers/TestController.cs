using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OurFuss.Core.Sections.Entities;
using OurFuss.Core.Sections.Telegram.Models.Entities;

namespace OurFuss.Web.Controllers;

[ApiController]
public class TestController(IEntityRepository entityRepository) : Controller
{
    private readonly IEntityRepository _entityRepository = entityRepository;

    [HttpGet("SetUserFirstAdmin")]
    public async Task<ActionResult> SetUserFirstAdmin()
    {
        var contextDetails = _entityRepository.GetContextDetails<TelegramAccountEntity>();
        var entity = await contextDetails.Queryable.FirstOrDefaultAsync();
        contextDetails.ContextDestroy(contextDetails.DbContextId);

        if (entity is null)
            return BadRequest("User is not found!");

        entity.TelegramRole = Core.Sections.Telegram.Enums.TelegramRoleType.Admin;
        await _entityRepository.UpdateAsync(entity);

        return Ok(entity);
    }
}
