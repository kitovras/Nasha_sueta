using OurFuss.Core.Sections.Entities;
using OurFuss.Core.Sections.Events.Enums;
using OurFuss.Core.Sections.Events.Extensions.Queryable;
using OurFuss.Core.Sections.Events.Models.Domains;
using OurFuss.Core.Sections.Events.Models.Entities;
using OurFuss.Core.Sections.Telegram.Models.Domains;
using OurFuss.Core.Sections.Telegram.Models.Entities;
using OurFuss.Core.Utils.Guid;

namespace OurFuss.Core.Sections.Events.Services;

public class EventService : IEventService
{
    private readonly IEntityRepository _entityRepository;
    private readonly IGuidGenerator _guidGenerator;

    public EventService(
        IEntityRepository entityRepository,
        IGuidGenerator guidGenerator)
    {
        _entityRepository = entityRepository;
        _guidGenerator = guidGenerator;
    }

    public async Task AddUserEventAsync(UserEventCustom inputModel)
    {
        var userEventCustom = MapUserEventCustomEntity(inputModel);
        await _entityRepository.AddAsync(userEventCustom);

        inputModel.Id = userEventCustom.Id;
    }

    public async Task<UserEventCustom?> GetUserEventAsync(Guid customEventId)
    {
        var userEventCustom = await _entityRepository.GetContextDetails<UserEventCustomEntity>().GetUserEventCustomAsync(customEventId);
        if (userEventCustom is null)
            return null;

        var outputModel = MapUserEventCustom(userEventCustom);
        return outputModel;
    }

    public async Task<UserEventCustom?> GetUserEventCustomWithTelegramAccountAsync(Guid customEventId)
    {
        var userEventCustom = await _entityRepository.GetContextDetails<UserEventCustomEntity>()
            .GetUserEventCustomWithTelegramAccountAsync(customEventId);

        if (userEventCustom is null)
            return null;

        var outputModel = MapUserEventCustom(userEventCustom);
        return outputModel;
    }

    public async Task<UserEventCustom?> GetUserEventFullAsync(Guid customEventId)
    {
        var userEventCustom = await _entityRepository.GetContextDetails<UserEventCustomEntity>().GetUserEventFullAsync(customEventId);
        if (userEventCustom is null)
            return null;

        var outputModel = MapUserEventCustom(userEventCustom);
        return outputModel;
    }

    public async Task SaveUserEventAsync(UserEventCustom inputModel)
    {
        var userEventCustom = MapUserEventCustomEntity(inputModel);
        await _entityRepository.UpdateAsync(userEventCustom);
    }

    public async Task<bool> UserEventCustomRemoveByIdAsync(Guid customEventId)
    {
        var userEventCustom = await _entityRepository.GetContextDetails<UserEventCustomEntity>().GetUserEventCustomAsync(customEventId);
        if (userEventCustom is null)
            return false;

        await _entityRepository.RemoveAsync(userEventCustom);
        return true;
    }

    public async Task<int> GetEventCountAsync(EventStatusType eventStatus) =>
        await _entityRepository.GetContextDetails<UserEventCustomEntity>().GetEventCountAsync(eventStatus);

    public async Task<List<UserEventCustom>> GetUserEventsAsync(int skip, int take, EventStatusType eventStatus)
    {
        var userEventCustoms = await _entityRepository.GetContextDetails<UserEventCustomEntity>().GetUserEventFullAsync(skip, take, eventStatus);
        if (!userEventCustoms.Any())
            return new();

        var outputModel = userEventCustoms.Select(MapUserEventCustom).ToList();
        return outputModel;
    }

    #region Map

    private UserEventCustomEntity MapUserEventCustomEntity(UserEventCustom inputModel)
    {
        var userEventCustom = new UserEventCustomEntity
        {
            Id = inputModel.Id == Guid.Empty ? _guidGenerator.Sequential() : inputModel.Id,
            TelegramAccountId = inputModel.TelegramId,
            Text = inputModel.MessageText,
            Created = DateTime.UtcNow,
            Modified = DateTime.UtcNow,
            EventStatus = inputModel.EventStatus,
            DeferredPublication = inputModel.DeferredPublication,
        };

        if (inputModel.UserEventCustomImage is not null)
        {
            userEventCustom.Photos.Add(new EventCustomPhotoEntity
            {
                Id = inputModel.UserEventCustomImage.Id == Guid.Empty ? _guidGenerator.Sequential() : inputModel.UserEventCustomImage.Id,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                FileId = inputModel.UserEventCustomImage.FileId,
                FileSize = inputModel.UserEventCustomImage.FileSize,
                FileUniqueId = inputModel.UserEventCustomImage.FileUniqueId,
                Height = inputModel.UserEventCustomImage.Height,
                Width = inputModel.UserEventCustomImage.Width,
            });
        }

        if (inputModel.TelegramAccount is not null)
        {
            userEventCustom.TelegramAccount = new TelegramAccountEntity
            {
                Id = inputModel.TelegramAccount!.Id,
                ChatId = inputModel.TelegramAccount!.ChatId,
                Created = inputModel.TelegramAccount!.Created,
                Modified = inputModel.TelegramAccount!.Modified,
                FullName = inputModel.TelegramAccount!.FullName,
                TelegramId = inputModel.TelegramAccount!.TelegramId,
                TelegramRole = inputModel.TelegramAccount!.TelegramRole,
                UserMemberStatus = inputModel.TelegramAccount!.UserMemberStatus,
            };
        }

        return userEventCustom;
    }

    private UserEventCustom MapUserEventCustom(UserEventCustomEntity userEventCustom)
    {
        var outputModel = new UserEventCustom
        {
            Id = userEventCustom.Id,
            Created = userEventCustom.Created,
            Modified = userEventCustom.Modified,
            MessageText = userEventCustom.Text,
            TelegramId = userEventCustom.TelegramAccountId,
            DeferredPublication = userEventCustom.DeferredPublication,
            EventStatus = userEventCustom.EventStatus,
        };

        if (userEventCustom!.Photos is not null)
        {
            if (userEventCustom.Photos.Count > 0)
            {
                var photo = userEventCustom!.Photos!.FirstOrDefault();
                outputModel.UserEventCustomImage = new UserEventCustomImage
                {
                    Created = photo!.Created,
                    Modified = photo.Modified,
                    FileId = photo.FileId,
                    FileSize = photo.FileSize,
                    FileUniqueId = photo.FileUniqueId,
                    Height = photo.Height,
                    Width = photo.Width,
                    Id = photo.Id
                };
            }
        }

        if (userEventCustom.TelegramAccount is not null)
        {
            outputModel.TelegramAccount = new TelegramAccount
            {
                Id = userEventCustom.TelegramAccount!.Id,
                ChatId = userEventCustom.TelegramAccount!.ChatId,
                Created = userEventCustom.TelegramAccount!.Created,
                Modified = userEventCustom.TelegramAccount!.Modified,
                FullName = userEventCustom.TelegramAccount!.FullName,
                TelegramId = userEventCustom.TelegramAccount!.TelegramId,
                TelegramRole = userEventCustom.TelegramAccount!.TelegramRole,
                UserMemberStatus = userEventCustom.TelegramAccount!.UserMemberStatus,
            };
        }

        return outputModel;
    }

    #endregion
}
