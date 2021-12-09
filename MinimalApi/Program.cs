using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ClienteDbContext>(options =>
    options.UseInMemoryDatabase("Clientes"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/clientes", ([FromServices] ClienteDbContext dbContext) =>
{
    var clientes = dbContext.Clientes.ToList();

    return Results.Ok(clientes);
});

app.MapGet("/clientes/{id}", async ([FromServices] ClienteDbContext dbContext, string id) =>
{
    var clienteAtual = await dbContext.Clientes.FindAsync(id);
    return clienteAtual == null ? Results.NotFound() : Results.Ok(clienteAtual);
});

app.MapPost("/clientes", async ([FromServices] ClienteDbContext dbContext,
    [FromBody] Cliente[] clientes) =>
{
    dbContext.Clientes.AddRange(clientes);
    await dbContext.SaveChangesAsync();
    return Results.Ok();
});

app.MapPut("/clientes", async ([FromServices] ClienteDbContext dbContext,
    [FromBody] Cliente cliente) =>
{
    var clienteAtual = await dbContext.Clientes.FindAsync(cliente.Id);
    if (clienteAtual == null)
        return Results.NotFound();
    clienteAtual.Nome = cliente.Nome;
    clienteAtual.Endereco = cliente.Endereco;
    clienteAtual.Cidade = cliente.Cidade;
    clienteAtual.Pais = cliente.Pais;
    clienteAtual.Telefone = cliente.Telefone;
    await dbContext.SaveChangesAsync();
    return Results.Ok();
});

app.MapDelete("/clientes/{id}", async ([FromServices] ClienteDbContext dbContext,
    string id) =>
{
    var clienteAtual = await dbContext.Clientes.FindAsync(id);
    if (clienteAtual == null)
        return Results.NotFound();
    dbContext.Clientes.Remove(clienteAtual);
    await dbContext.SaveChangesAsync();
    return Results.StatusCode(204);
});

app.UseSwagger();
app.UseSwaggerUI();

await app.RunAsync();
