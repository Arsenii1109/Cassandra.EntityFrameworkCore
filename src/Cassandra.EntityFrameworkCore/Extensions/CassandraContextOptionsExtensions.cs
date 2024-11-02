using Cassandra.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Cassandra.EntityFrameworkCore.Extensions;

public static class CassandraContextOptionsExtensions
{
    public static DbContextOptionsBuilder UseCassandraDatabase(
        this DbContextOptionsBuilder optionsBuilder,
        string connectionString,
        string databaseName)
    {
        ArgumentNullException.ThrowIfNull(optionsBuilder);
        ArgumentNullException.ThrowIfNull(connectionString);
        
        var extension = (optionsBuilder.Options.FindExtension<CassandraOptionsExtension>()
                         ?? new CassandraOptionsExtension())
            .WithConnectionString(connectionString)
            .WithDatabaseName(databaseName);
        
        ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);
        
        return optionsBuilder;
    }
}