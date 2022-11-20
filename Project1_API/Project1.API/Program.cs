
using Microsoft.AspNetCore.Builder;
using Project1.Data;
using Project1_API.Logic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();

//The connection string, derived from our Secrets JSON
var connvalue = builder.Configuration.GetValue<string>("ConnectionStrings:ConnxString");
builder.Services.AddTransient<SqlRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//The HTTP requests will be going here

//Register a user. POST
app.MapPost("/register", (string email, string password, string? role, SqlRepository repo) =>
{
    repo.setConnectionString(connvalue);
    Employee e = repo.CreateEmployee(email, password, role);

    if (e.Role == "Employee") return Results.Created($"/Employee/{e.Id}", e);
    else return Results.Created($"/Manager/{e.Id}", e);
});

//Login a User. POST
app.MapPost("/login", (string email, string password, SqlRepository repo) =>
{
    repo.setConnectionString(connvalue);
    Employee? e = repo.GetEmployee(email, password);
    //Console.WriteLine(e.ToString());
    if (e == null)
    {
        //Do something here, probably return a specific code   
        return Results.StatusCode(401);
    }
    Console.WriteLine($"This means I actually returned a good object: {e.ToString()}");
    return Results.Ok(e);
    //else return Results.Ok(e);
});

//Create a ticket. POST
app.MapPost("/tickets", (int emplId, double amt, string desc, SqlRepository repo) =>
{
    repo.setConnectionString(connvalue);
    Ticket t = repo.CreateTicket(emplId, amt, desc);

    return Results.Created($"/tickets/{t.TicketID}", t);
});

//Update a ticket's status to Approved or Rejected
app.MapPut("/tickets/{id}", (int id, string status, SqlRepository repo) =>
{
    repo.setConnectionString(connvalue);
    repo.UpdateTicketStatus(id, status);
    return Results.NoContent();
});

//Return an enumerable collection of Tickets. GET

app.Run();
