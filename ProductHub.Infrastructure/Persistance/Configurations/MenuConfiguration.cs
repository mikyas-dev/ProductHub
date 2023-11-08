// using BuberDinner.Domain.Host.ValueObjects;
// using BuberDinner.Domain.Menu;
// using BuberDinner.Domain.Menu.Entities;
// using BuberDinner.Domain.Menu.ValueObjects;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace BuberDinner.Infrastructure.Persistance.Configurations;

// public class MenuConfiguration : IEntityTypeConfiguration<Menu>
// {
//     public void Configure(EntityTypeBuilder<Menu> builder)
//     {
//         ConfigureMenusTable(builder);
//         ConfigureMenuSectionsTable(builder);
//         ConfigureMenuDinnersTable(builder);
//         ConfigureMenuReviewsTable(builder);
//     }

//     private void ConfigureMenuReviewsTable(EntityTypeBuilder<Menu> builder)
//     {
//         builder.OwnsMany(menu => menu.MenuReviewIds, reviewBuilder =>
//         {
//             reviewBuilder.ToTable("MenuReviews");

//             reviewBuilder.WithOwner().HasForeignKey("MenuId");

//             reviewBuilder.Property(review => review.Value)
//                 .HasColumnName("MenuReviewId")
//                 .ValueGeneratedNever();

//             reviewBuilder.HasKey("Id");

//         });

//         builder.Metadata.FindNavigation(nameof(Menu.MenuReviewIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
//     }

//     private void ConfigureMenuDinnersTable(EntityTypeBuilder<Menu> builder)
//     {
//         builder.OwnsMany(menu => menu.DinnerIds, dinnerBuilder =>
//         {
//             dinnerBuilder.ToTable("Dinners");

//             dinnerBuilder.WithOwner().HasForeignKey("MenuId");

//             dinnerBuilder.Property(dinner => dinner.Value)
//                 .HasColumnName("DinnerId")
//                 .ValueGeneratedNever();

//             dinnerBuilder.HasKey("Id");

//         });

//         builder.Metadata.FindNavigation(nameof(Menu.DinnerIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
//     }

//     private void ConfigureMenuSectionsTable(EntityTypeBuilder<Menu> builder)
//     {
//         builder.OwnsMany(menu => menu.Sections, sectionBuilder =>
//         {
//             sectionBuilder.ToTable("MenuSections");

//             sectionBuilder.WithOwner().HasForeignKey("MenuId");

//             sectionBuilder.Property(section => section.Id)
//                 .HasColumnName("MenuSectionId")
//                 .ValueGeneratedNever()
//                 .HasConversion(
//                     menuSectionId => menuSectionId.Value,
//                     value => MenuSectionId.CreateUnique(value)
//                 );

//             sectionBuilder.Property(section => section.Name)
//                 .IsRequired()
//                 .HasMaxLength(50);

//             sectionBuilder.HasKey("Id", "MenuId");

//             sectionBuilder.Property(section => section.Description)
//                 .IsRequired()
//                 .HasMaxLength(500);
            
//             sectionBuilder.OwnsMany(section => section.Items, itemBuilder =>
//             {
//                 itemBuilder.ToTable("MenuItems");

//                 itemBuilder.WithOwner().HasForeignKey("MenuSectionId", "MenuId");

//                 itemBuilder.Property(item => item.Id)
//                     .HasColumnName("MenuItemId")
//                     .ValueGeneratedNever()
//                     .HasConversion(
//                         menuItemId => menuItemId.Value,
//                         value => MenuItemId.CreateUnique(value)
//                     );

//                 itemBuilder.Property(item => item.Name)
//                     .IsRequired()
//                     .HasMaxLength(50);

//                 itemBuilder.HasKey(nameof(MenuItem.Id), "MenuId", "MenuSectionId");

//                 itemBuilder.Property(item => item.Description)
//                     .IsRequired()
//                     .HasMaxLength(500);
//             });
//             sectionBuilder.Navigation(section => section.Items)!.Metadata.SetField("_items");
//             sectionBuilder.Navigation(section => section.Items)!.UsePropertyAccessMode(PropertyAccessMode.Field);
//         });

//         builder.Metadata.FindNavigation(nameof(Menu.Sections))!.SetPropertyAccessMode(PropertyAccessMode.Field);
//     }

//     public void ConfigureMenusTable(EntityTypeBuilder<Menu> builder)
//     {
//         builder.ToTable("Menus");
//         builder.HasKey(menu => menu.Id);
//         builder.Property(menu => menu.Id)
//             .ValueGeneratedNever()
//             .HasConversion(
//                 menuId => menuId.Value,
//                 value => MenuId.CreateUnique(value)
//             );
//         builder.Property(menu => menu.Name)
//             .IsRequired()
//             .HasMaxLength(50);
//         builder.Property(menu => menu.Description)
//             .IsRequired()
//             .HasMaxLength(500);
//         builder.OwnsOne(menu => menu.AverageRating);

//         builder.Property(menu => menu.HostId)
//             .IsRequired()
//             .HasConversion(
//                 hostId => hostId.Value,
//                 value => HostId.CreateUnique(value)
//             );
//     }
// }