
using Microsoft.EntityFrameworkCore;
public class CustomerDbContext : DbContext
{
    public CustomerDbContext(DbContextOptions <CustomerDbContext> options): base(options){
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Order>(entity=>{
                entity.ToTable("Order");
                entity.HasKey(e=>e.Order_Id);

            });
             modelBuilder.Entity<OutboxMessage>(entity=>{
                entity.ToTable("OutboxMessage");
                entity.HasKey(e=>e.Event_Id);

            }) ;
            base.OnModelCreating(modelBuilder);
    }
    public DbSet<OutboxMessage> OutboxMessages {get; set;}
    public DbSet<Order> Orders {get; set;}
}