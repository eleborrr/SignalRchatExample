using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Data;

namespace SignalRChat;

public class ApplicationDbContext: IdentityDbContext<IdentityUser>
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Message> Messages { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<IdentityUser>().HasData(
        //     new IdentityUser
        //     {
        //         Id = "1",
        //         UserName = "user1"
        //     },
        //     new IdentityUser
        //     {
        //         Id = "4",
        //         UserName = "user4"
        //     },
        //     new IdentityUser
        //     {
        //         Id = "3",
        //         UserName = "user3"
        //     },
        //     new IdentityUser
        //     {
        //         Id = "2",
        //         UserName = "user2"
        //     }
        // );
        //
        // modelBuilder.Entity<Room>().HasData(
        //     new Room()
        //     {
        //         Id = 1,
        //         FirstUserId = "1",
        //         SecondUserId = "2",
        //         Name = "Room1"
        //     },
        //     new Room()
        //     {
        //         Id = 2,
        //         FirstUserId = "3",
        //         SecondUserId = "4",
        //         Name = "Room2"
        //     });

        
        base.OnModelCreating(modelBuilder);
    }
}