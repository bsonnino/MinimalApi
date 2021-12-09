using Microsoft.EntityFrameworkCore;
using System.Text.Json;

public class Cliente
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public string Endereco { get; set; }
    public string Cidade { get; set; }
    public string Pais { get; set; }
    public string Telefone { get; set; }
}

public class ClienteDbContext : DbContext
{
    public ClienteDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
}

