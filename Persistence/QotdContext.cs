using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;

namespace Persistence;

public class QotdContext : DbContext
{
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Quote> Quotes => Set<Quote>();

    public QotdContext(DbContextOptions<QotdContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //modelBuilder.Entity<Author>().HasKey(c => c.Id);
        //modelBuilder.ApplyConfiguration(new AuthorEntityConf());

        modelBuilder.SeedData();
    }
}