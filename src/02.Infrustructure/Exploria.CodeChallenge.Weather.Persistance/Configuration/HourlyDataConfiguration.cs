using Exploria.CodeChallenge.Weather.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exploria.CodeChallenge.Weather.Persistance.Configuration;

public class HourlyDataConfiguration : IEntityTypeConfiguration<HourlyData>
{
    public void Configure(EntityTypeBuilder<HourlyData> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Time).IsRequired();
        builder.Property(e => e.Temperature2M).IsRequired();
        builder.Property(e => e.RelativeHumidity2M).IsRequired();
        builder.Property(e => e.WindSpeed10M).IsRequired();
    }
}
