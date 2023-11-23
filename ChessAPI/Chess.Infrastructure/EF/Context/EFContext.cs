using System.Security.Claims;
using Chess.Domain;
using Chess.Domain.Games.Entities;
using Chess.Domain.Identity.Entities;
using Chess.Infrastructure.Games.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chess.Infrastructure.EF.Context;

public class EFContext : IdentityDbContext<User, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
   public DbSet<Game> Games { get; set; }
   
   private readonly Guid _userId = Guid.Empty;
   
   public EFContext (DbContextOptions<EFContext> options) : base(options) {}
   
   public EFContext (DbContextOptions<EFContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
   {
      _ = Guid.TryParse(httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier),
         out _userId);
   }
   
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      base.OnModelCreating(modelBuilder);
      
      modelBuilder.ApplyConfiguration(new GameConfiguration());
   }

   public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
   {
      foreach (var entry in ChangeTracker.Entries())
      {
         if(entry.Entity is Entity)
            switch (entry.State)
            {
               case EntityState.Added:
                  entry.CurrentValues["CreatedById"] = _userId;
                  entry.CurrentValues["CreatedAt"] = DateTimeOffset.UtcNow;
                  entry.CurrentValues["DeletedById"] = null;
                  entry.CurrentValues["DeletedAt"] = null;
                  break;
               
               case EntityState.Deleted:
                  entry.State = EntityState.Modified;
                  entry.CurrentValues["DeletedById"] = _userId;
                  entry.CurrentValues["DeletedAt"] = DateTimeOffset.UtcNow;
                  break;
            }
      }

      return base.SaveChangesAsync(cancellationToken);
   }
}