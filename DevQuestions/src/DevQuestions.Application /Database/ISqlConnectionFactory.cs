using System.Data;

namespace DevQuestion.Infrastructure.Postgres;

public interface ISqlConnectionFactory
{
    IDbConnection Create();
}