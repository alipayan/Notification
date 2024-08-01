using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using Notifier.Features.Sms.Models;

namespace Notifier.Features.Sms;

public class SmsDbContext : DbContext
{
    public SmsDbContext(DbContextOptions<SmsDbContext> options) : base(options)
    {

    }

    public DbSet<SmsTrace> SmsTraces { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SmsTrace>().ToCollection("sms_traces");

    }
}
