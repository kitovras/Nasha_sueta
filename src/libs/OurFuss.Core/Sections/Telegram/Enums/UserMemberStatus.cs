using System.ComponentModel.DataAnnotations;

namespace OurFuss.Core.Sections.Telegram.Enums;

/// <summary>
/// The status of the user's membership in the chat
/// </summary>
public enum UserMemberStatus
{
    [Display(Name = "Не определен")]
    Unknown = 0,

    [Display(Name = "Член")]
    Member = 3,

    [Display(Name = "Исключен")]
    Kicked = 5,
}