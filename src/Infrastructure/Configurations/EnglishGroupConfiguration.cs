using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Configurations
{
    public class EnglishGroupConfiguration : IEntityTypeConfiguration<EnglishGroup>
    {
        public void Configure(EntityTypeBuilder<EnglishGroup> builder)
        {
            builder.ToTable(nameof(EnglishGroup));
            builder.HasKey(x => x.Id);

            builder.Metadata.FindNavigation(nameof(EnglishGroup.EnglishWords))
                            .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(x => x.Name)
                   .IsRequired();
        }
    }
}
