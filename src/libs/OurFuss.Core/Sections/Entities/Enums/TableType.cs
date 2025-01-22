using System.ComponentModel.DataAnnotations;

namespace OurFuss.Core.Sections.Entities.Enums;

/// <summary>
/// Тип таблицы
/// </summary>
public enum TableType
{
    [Display(Name = "Не определен")]
    Unknown = 0,

    [Display(Name = "Настраиваемое событие пользователя")]
    UserEventCustom = 1,

    [Display(Name = "Фотография настраиваемого события пользователя")]
    UserEventPhotoCustom = 2,

    [Display(Name = "Телеграм аккаунт")]
    TelegramAccount = 500,
}