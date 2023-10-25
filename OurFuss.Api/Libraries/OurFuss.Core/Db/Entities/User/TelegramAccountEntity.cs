using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFuss.Core.Db.Entities.User;

/// <summary>
/// Телеграмм аккаунт
/// </summary>
[Table("TelegramAccount", Schema = "tg")]
public class TelegramAccountEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор чата
    /// </summary>
    [Column("chatId")]
    public long ChatId { get; set; }

    /// <summary>
    /// Полное имя
    /// </summary>
    [Column("fullName")]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Выбранный код региона
    /// </summary>
    [Column("regionCodeSelected")]
    public int RegionCodeSelected { get; set; }

    /// <summary>
    /// Дата и время добавления
    /// </summary>
    [Column("dateTimeAdded")]
    public DateTime DateTimeAdded { get; set; }

    /// <summary>
    /// Дата и время изменения
    /// </summary>
    [Column("dateTimeModified")]
    public DateTime DateTimeModified { get; set; }
}
