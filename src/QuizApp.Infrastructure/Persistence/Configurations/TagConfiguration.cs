﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuizApp.Domain.Entities;
using QuizApp.Infrastructure.Persistence.Constants;

namespace QuizApp.Infrastructure.Persistence.Configurations;
public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder
            .ToTable(TableNames.Tags);

        builder
            .HasKey(t => t.Id);

        builder
            .Property(t => t.Title)
            .IsRequired(true);

        builder
            .HasOne(t => t.User)
            .WithMany(u => u.Tags)
            .HasForeignKey(t => t.TesterId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
