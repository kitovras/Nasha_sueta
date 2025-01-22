using System.ComponentModel.DataAnnotations;

namespace OurFuss.Core.Sections.Telegram.Enums;

public enum TelegramRoleType
{
    [Display(Name = "Не определен")]
    Unknown = 0,

    [Display(Name = "Пользователь")]
    User = 1,

    [Display(Name = "Администратор")]
    Admin = 200,
}
