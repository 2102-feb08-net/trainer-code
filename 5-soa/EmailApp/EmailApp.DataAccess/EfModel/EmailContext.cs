using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmailApp.DataAccess.EfModel
{
    public class EmailContext : DbContext
    {
        //public EmailContext()
        //{
        //}

        public EmailContext(DbContextOptions<EmailContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(m => m.Subject)
                    .IsRequired();
                entity.Property(m => m.Body)
                    .IsRequired();
                entity.Property(m => m.Date)
                    .IsRequired()
                    .HasColumnType("datetimeoffset");

                entity.HasOne(m => m.From)
                    .WithMany(a => a.SentMessages)
                    .HasForeignKey(m => m.FromId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Address)
                .IsUnique();

            List <Message> initialMessages = new()
            {
                new()
                {
                    Id = 1,
                    FromId = 1,
                    Date = DateTimeOffset.Parse("Mon, 01 Mar 2021 12:58:58 -0700"),
                    Subject = "qc",
                    Body = "Aenean elit massa, eleifend id feugiat a, semper in massa. Praesent ex lectus, vehicula eget mi ut, dictum commodo tortor. Sed congue leo ac mollis hendrerit. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Pellentesque ut magna non sapien efficitur ullamcorper. Donec elementum, purus aliquet facilisis auctor, massa justo finibus leo, a feugiat purus tortor vitae ante. Suspendisse ipsum nibh, tincidunt congue mattis ut, tristique in felis."
                },
                new()
                {
                    Id = 2,
                    FromId = 2,
                    Date = DateTimeOffset.Parse("Mon, 01 Mar 2021 13:00:10 -0700"),
                    Subject = "RE: qc",
                    Body = "Donec egestas lorem viverra augue placerat interdum. Nulla id mollis purus. Quisque eget libero ultricies est tincidunt tempor. Integer lobortis sapien et pellentesque tincidunt. Nulla euismod pulvinar lorem sed pellentesque. Ut sit amet quam non elit pharetra cursus. In hac habitasse platea dictumst. Proin accumsan a justo ac molestie. Phasellus eu metus neque. Donec vel sollicitudin libero. Donec sed leo leo."
                },
            };

            modelBuilder.Entity<Message>().HasData(initialMessages);
            modelBuilder.Entity<Account>().HasData(
                new Account { Id = 1, Address = "fred@fred.com" },
                new Account { Id = 2, Address = "kevin@kevin.com" },
                new Account { Id = 3, Address = "me@me.com" });
        }
    }
}
