using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.ConfigureBase();

        builder.Property(x => x.FirstName)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.LastName)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.Username)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.HasIndex(x => x.Username);

        builder.HasMany(x => x.ChatMessages)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Когда у Entry и MoodRecord есть UserId/User:
        // builder.HasMany(x => x.Entries)
        //     .WithOne(x => x.User)
        //     .HasForeignKey(x => x.UserId)
        //     .OnDelete(DeleteBehavior.Cascade);
        //
        // builder.HasMany(x => x.Emotions)
        //     .WithOne(x => x.User)
        //     .HasForeignKey(x => x.UserId)
        //     .OnDelete(DeleteBehavior.Cascade);
    }
}