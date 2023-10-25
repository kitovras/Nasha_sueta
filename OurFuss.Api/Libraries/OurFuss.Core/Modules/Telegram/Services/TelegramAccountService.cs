using OurFuss.Core.Modules.Common.Repositories;

namespace OurFuss.Core.Modules.Telegram.Services;

public class TelegramAccountService : ITelegramAccountService
{
    private readonly IEntityRepository _entityRepository;

    public TelegramAccountService(IEntityRepository entityRepository)
    {
        _entityRepository = entityRepository;
    }

    public async Task GetTelegramAccountAsync()
    {

    }
}
