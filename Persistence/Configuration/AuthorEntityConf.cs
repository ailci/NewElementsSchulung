using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

internal class AuthorEntityConf : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("Authoren");

        builder.HasKey(c => c.Id);

        //Name
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(150);

        //Geburtsdatum
        builder.Property(c => c.BirthDate).IsRequired(false).HasColumnType("datetime2");

        //Relation
        builder.HasMany(c => c.Quotes)
            .WithOne(c => c.Author)
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}