using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OurFuss.Core.Db.Entities.Event;
using OurFuss.Core.Db.Entities.User;
using OurFuss.Core.Db.EntityConfigurations;

namespace OurFuss.Core.Db;

/// <summary>
/// Контекст базы данных
/// </summary>
public class OurFussDbContext : IdentityDbContext<UserEntity>
{
    public OurFussDbContext(DbContextOptions<OurFussDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    #region DbSets
    
    /// <summary>
    /// Пользователь
    /// </summary>
    internal DbSet<UserEntity> User => Set<UserEntity>();

    /// <summary>
    /// Телеграмм аккаунт
    /// </summary>
    internal DbSet<TelegramAccountEntity> TelegramAccount => Set<TelegramAccountEntity>();

    /// <summary>
    /// События
    /// </summary>
    internal DbSet<EventEntity> Events => Set<EventEntity>();
    
    /// <summary>
    /// Тэги события
    /// </summary>
    internal DbSet<EventTagEntity> EventTag => Set<EventTagEntity>();

    /// <summary>
    /// Тэг
    /// </summary>
    internal DbSet<TagEntity> Tag => Set<TagEntity>();

    #endregion
}