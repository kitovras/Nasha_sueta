using Microsoft.EntityFrameworkCore;
using OurFuss.Core.Sections.Events.Models.Entities;
using OurFuss.Core.Sections.Telegram.Models.Entities;

namespace OurFuss.Data.Postgre.Db;

public class OurFussContextPostgre : DbContext
{
    public OurFussContextPostgre(DbContextOptions<OurFussContextPostgre> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    #region DbSets

    /// <summary>
    /// Телеграм пользователь
    /// </summary>
    internal DbSet<TelegramAccountEntity> TelegramAccount => Set<TelegramAccountEntity>();

    internal DbSet<EventCustomPhotoEntity> EventCustomPhoto => Set<EventCustomPhotoEntity>();

    internal DbSet<UserEventCustomEntity> UserEventCustom => Set<UserEventCustomEntity>();

    #endregion
}
