
using Microsoft.AspNetCore.Builder;
using Project1.Data;
using Project1_API.Logic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();

//The connection string, found in our Secrets JSON
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
    
    if (e == null)
    {
        //Return a 401 status code, meaning credentials were off 
        return Results.StatusCode(401);
    }
    //User verified, return the Employee and produce a 200OK response
    return Results.Ok(e);
});

//Create a ticket. POST
app.MapPost("/tickets", (int emplId, double amt, string desc, SqlRepository repo) =>
{
    repo.setConnectionString(connvalue);
    Ticket t = repo.CreateTicket(emplId, amt, desc);

    return Results.Created($"/tickets/{t.TicketID}", t);
});

//Return a queue of pending Tickets. GET
app.MapGet("/tickets/{status}", (string status, SqlRepository repo) =>
{
    repo.setConnectionString(connvalue);
    Queue<Ticket> ticketsPending = repo.GetTicketQueue(status);

    return Results.Ok(ticketsPending);
});

//Return a list of Tickets, GET
app.MapGet("/tickets/employee/{id}", (int id, string email, SqlRepository repo) =>
{
    repo.setConnectionString(connvalue);
    List<Ticket> tickets = repo.GetTicketList(id, email);

    return Results.Ok(tickets);
});

//Update a ticket's status to Approved or Rejected
app.MapPut("/tickets/{id}", (int id, string status, SqlRepository repo) =>
{
    repo.setConnectionString(connvalue);
    repo.UpdateTicketStatus(id, status);
    return Results.NoContent();
});



app.Run();
