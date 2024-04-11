using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebUni_Management.Infrastructure.Data.Models;

namespace WebUni_Management.Infrastructure.SeedDb
{
	public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.HasOne(x => x.Menu)
            .WithMany(x => x.Dishes)
            .HasForeignKey(x => x.MenuId)
            .OnDelete(DeleteBehavior.Restrict);

            var data = new SeedData();

            builder.HasData( new Dish[] { data.Salad1, data.Salad2, data.MainDish1, data.MainDish2, data.Dessert1, data.Dessert2 });
        }
    }
}
