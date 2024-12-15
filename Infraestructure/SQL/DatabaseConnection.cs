/*using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DAL.SQL;

public class DatabaseConnection
{
    private static DatabaseConnection _instance;
    private readonly string _connectionString;
    private SqlConnection _connection;

    // Constructor privado que acepta IConfiguration
    private DatabaseConnection(IConfiguration configuration)
    {
        // Obtiene la cadena de conexión desde la configuración
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        _connection = new SqlConnection(_connectionString);
    }

    // Método para obtener la instancia singleton
    public static DatabaseConnection Instance(IConfiguration configuration)
    {
        if (_instance == null)
        {
            _instance = new DatabaseConnection(configuration);
        }
        return _instance;
    }

    public SqlConnection GetConnection()
    {
        if (_connection.State == System.Data.ConnectionState.Closed)
        {
            _connection.Open();
        }
        return _connection;
    }

    public void CloseConnection()
    {
        if (_connection.State == System.Data.ConnectionState.Open)
        {
            _connection.Close();
        }
    }
}*/
