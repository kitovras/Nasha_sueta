using System.ComponentModel.DataAnnotations;

namespace OurFuss.Core.Sections.Telegram.Enums;

/// <summary>
/// The type of custom event sub-command
/// </summary>
public enum CustomEventSubcommandType
{
    [Display(Name = "Не определен")]
    Unknown = 0,

    [Display(Name = "Принято")]
    Accepted = 1,

    [Display(Name = "Отклонено")]
    Reject = 2,

    [Display(Name = "Отложенная публикация")]
    DefPublication = 50,
}
