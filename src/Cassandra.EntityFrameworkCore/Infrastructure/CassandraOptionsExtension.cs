using Cassandra.EntityFrameworkCore.Helpers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Cassandra.EntityFrameworkCore.Infrastructure;

public class CassandraOptionsExtension : IDbContextOptionsExtension
{
    const string MultipleConnectionConfigSpecifiedException =
        "Both ConnectionString and CassandraClient were specified. Specify only one set of connection details.";
    
    private string? _connectionString;
    private string? _databaseName;
    
    public CassandraOptionsExtension()
    {
    }

    /// <summary>
    /// Creates a <see cref="MongoOptionsExtension"/> by copying from an existing instance.
    /// </summary>
    protected CassandraOptionsExtension(CassandraOptionsExtension copyFrom)
    {
        _connectionString = copyFrom._connectionString;
    }

    public DbContextOptionsExtensionInfo Info { get; }
    
    public void ApplyServices(IServiceCollection services)
    {
        throw new NotImplementedException();
    }

    public void Validate(IDbContextOptions options)
    {
        throw new NotImplementedException();
    }
    
    public virtual CassandraOptionsExtension WithConnectionString(string connectionString)
    {
        ArgumentNullException.ThrowIfNull(connectionString);

        if (_cassandraClient != null)
        {
            throw new InvalidOperationException(MultipleConnectionConfigSpecifiedException);
        }

        var clone = Clone();
        clone._connectionString = connectionString;
        return clone;
    }
    
    public virtual CassandraOptionsExtension WithDatabaseName(string databaseName)
    {
        databaseName.ThrowArgumentExceptionIfNullOrEmpty();

        var clone = Clone();
        clone._databaseName = databaseName;
        return clone;
    }

    protected virtual CassandraOptionsExtension Clone() => new(this);
}