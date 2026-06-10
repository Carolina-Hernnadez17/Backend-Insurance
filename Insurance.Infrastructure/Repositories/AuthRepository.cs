using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Insurance.Application.Interfaces.Repositories;
using Insurance.Domain.Entities;
using Insurance.Infrastructure.Data;

namespace Insurance.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly SqlConnectionFactory _factory;

    public AuthRepository(
        SqlConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<Usuario?> LoginAsync(
        string usuario,
        string password)
    {
        using var connection =
            _factory.CreateConnection();

        return await connection
            .QueryFirstOrDefaultAsync<Usuario>(
                "sp_Login",
                new
                {
                    Usuario = usuario,
                    PasswordHash = password
                },
                commandType:
                CommandType.StoredProcedure);
    }
}