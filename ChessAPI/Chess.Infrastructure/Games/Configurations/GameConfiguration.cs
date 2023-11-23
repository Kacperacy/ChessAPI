using Chess.Domain.Games.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chess.Infrastructure.Games.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder
            .HasOne(x => x.WhitePlayer)
            .WithMany(x => x.GamesAsWhite)
            .HasForeignKey(u => u.WhitePlayerId);
        
        builder
            .HasOne(x => x.BlackPlayer)
            .WithMany(x => x.GamesAsBlack)
            .HasForeignKey(u => u.BlackPlayerId);

        builder.HasQueryFilter(x => x.DeletedById == null);
    }
}