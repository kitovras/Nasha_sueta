using System.ComponentModel.DataAnnotations;

namespace OurFuss.TelegramBot.EventGeneration.Enums;

/// <summary>
/// Type of command telegram
/// </summary>
public enum TelegramCommandType
{
    [Display(Name = "Не определён")]
    Unknown = 0,

    [Display(Name = "Начало")]
    Start = 1,

    [Display(Name = "Членство")]
    Membership = 2,

    [Display(Name = "События")]
    Event = 3,
}