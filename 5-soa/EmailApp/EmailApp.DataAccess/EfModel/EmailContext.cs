using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmailApp.DataAccess.EfModel
{
    public class EmailContext : DbContext
    {
        public EmailContext(DbContextOptions<EmailContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(m => m.OrigDate)
                    .IsRequired()
                    .HasColumnType("datetimeoffset(0)");

                entity.Property(m => m.IsDeleted)
                    .HasDefaultValue(false);

                entity.HasOne(m => m.From)
                    .WithMany(a => a.SentMessages)
                    .HasForeignKey(m => m.FromId)
                    .IsRequired();

                entity.HasOne(m => m.To)
                    .WithMany(a => a.ReceivedMessages)
                    .HasForeignKey(m => m.ToId);

                entity.HasIndex(m => m.Guid)
                    .IsUnique();
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(a => a.Address)
                    .IsRequired();

                entity.HasIndex(a => a.Address)
                    .IsUnique();
            });

            List<Message> initialMessages = new()
            {
                new()
                {
                    Id = 1,
                    Guid = new Guid("57d462ca-a9ce-4417-b8a4-d9b59907c7a6"),
                    FromId = 1,
                    ToId = 2,
                    OrigDate = new DateTimeOffset(2021, 3, 20, 22, 37, 10, new TimeSpan(-6, 0, 0)),
                    Subject = "hello",
                    Body = "this is a message to say hello"
                },
                new()
                {
                    Id = 2,
                    Guid = new Guid("bd682c41-68db-4c00-9dd2-814b8013e563"),
                    FromId = 2,
                    ToId = 1,
                    OrigDate = new DateTimeOffset(2021, 3, 20, 22, 40, 1, new TimeSpan(-6, 0, 0)),
                    Subject = "Re: hello",
                    Body = "this is a reply to hello"
                },
            };

            modelBuilder.Entity<Message>().HasData(initialMessages);
            modelBuilder.Entity<Account>().HasData(
                new Account { Id = 1, Address = "nick.escalona@revature.com" },
                new Account { Id = 2, Address = "nicholasescalona@outlook.com" });
        }
    }
}
