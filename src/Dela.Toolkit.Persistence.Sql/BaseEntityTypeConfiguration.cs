using Dela.Toolkit.Domain;
using Dela.Toolkit.Domain.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dela.Toolkit.Persistence.Sql;

public class BaseEntityTypeConfiguration<T,TKey>: IEntityTypeConfiguration<T> where T : Entity<TKey>
{ 
    
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(b => b.Id);
    }
    
    protected static ValueConverter<T, int> GetEnumerationConverter<T>() where T : Enumeration
    {
        return new ValueConverter<T, int>(
            v => v.Id,
            v => Enumeration.FromValue<T>(v));
    }
}

public class StringCheckConverter : ValueConverter<StringCheck, string>
{
    public StringCheckConverter() 
        : base(
            v => v.Value,
            v => new StringCheck(v).Value)
    {
    }
}