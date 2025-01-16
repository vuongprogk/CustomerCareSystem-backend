using System.Data;
using Microsoft.Data.SqlClient;

namespace CustomerCareSystem.DataAccess;

public class ApplicationDbContext: IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly string _connection;
    private IDbConnection? _db;

    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connection = _configuration.GetConnectionString("DefaultConnection")!;
    }

    public IDbConnection GetConnection()
    {
        if (_db is not { State: ConnectionState.Open })
        {
            _db = new SqlConnection(_connection);
            _db.Open();
        }
        return _db;
    }
    public void Dispose()
    {
        if (_db == null) return;
        if (_db.State == ConnectionState.Closed) return;
        _db?.Close();
        _db?.Dispose();
    }
}