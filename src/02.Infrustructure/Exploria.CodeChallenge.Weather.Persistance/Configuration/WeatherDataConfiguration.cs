using Exploria.CodeChallenge.Weather.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exploria.CodeChallenge.Weather.Persistance.Configuration;

public class WeatherDataConfiguration: IEntityTypeConfiguration<WeatherData>
{

    public void Configure(EntityTypeBuilder<WeatherData> builder)
    {
        builder.ToTable("Weather");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Latitude).IsRequired();
        builder.Property(e => e.Longitude).IsRequired();
        builder.Property(e => e.GenerationTimeMs).IsRequired();
        builder.Property(e => e.UtcOffsetSeconds).IsRequired();
        builder.Property(e => e.Timezone).HasMaxLength(50).IsRequired();
        builder.Property(e => e.TimezoneAbbreviation).HasMaxLength(10).IsRequired();
        builder.Property(e => e.Elevation).IsRequired();
        builder.OwnsOne(e => e.HourlyData);
       
    }
}
