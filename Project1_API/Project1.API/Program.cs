
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
app.MapPost("/register", (CredentialRecord cr, SqlRepository repo) =>
{
    repo.setConnectionString(connvalue);

    Employee e = repo.CreateEmployee(cr.E.Email, cr.Pass, cr.E.Role);
    //if the status of e reads "Exception", return the excepted employee and produce a response
    if (e.Role == "Exception") return Results.BadRequest(e);

    //if e is null, then no records were found despite a valid insert, therefore the request failed
    if (e is null) return Results.BadRequest(e);

    if (e.Role == "Employee") return Results.Created($"/Employee/{e.Id}", e);
    else return Results.Created($"/Manager/{e.Id}", e);
});

//Login a User. POST
app.MapPost("/login", (CredentialRecord cr, SqlRepository repo) =>
{
    repo.setConnectionString(connvalue);
    Employee? e = repo.GetEmployee(cr.E.Email, cr.Pass);
    
    if (e == null)
    {
        //Return a 401 status code, meaning credentials were off 
        return Results.StatusCode(401);
    }
    //User verified, return the Employee and produce a 202 response with the right URL
    //If employee is an Employee, return the Employee URI, else Manager
    return Results.Accepted($"/{e.Role}", e);
});

//Create a ticket. POST
app.MapPost("Employee/{emplId}/tickets", (TicketRecord tr, SqlRepository repo) =>
{
    repo.setConnectionString(connvalue);

    Ticket t = repo.CreateTicket(tr.EmplID, (double)tr.T.Amount, tr.T.Description);

    if (t.Description == "Exception") return Results.BadRequest(t);

    return Results.Created($"Employee/{tr.EmplID}/tickets/{t.TicketID}", t);
});

//Return a queue of pending Tickets. GET
app.MapGet("Manager/tickets/{status}", (string status, SqlRepository repo) =>
{
    repo.setConnectionString(connvalue);
    Queue<Ticket> ticketsPending = repo.GetTicketQueue(status);

    return Results.Ok(ticketsPending);
});

//Return a list of Tickets, GET
app.MapGet("/Employee/{emplid}/tickets", (int emplid, string email, string? status, SqlRepository repo) =>
{
    repo.setConnectionString(connvalue);
    List<Ticket> tickets = repo.GetTicketList(emplid, email, status);

    return Results.Ok(tickets);
});

//Update a ticket's status to Approved or Rejected
app.MapPut("Manager/tickets/{id}", (TicketRecord tr, SqlRepository repo) =>
{
    //if the ID is invalid, or the status is not Approved/Denied, return Status 400
    if (tr.T.TicketID is null || tr.T.TicketID < 1) return Results.BadRequest();
    if (tr.T.Status != "Approved" && tr.T.Status != "Denied") return Results.BadRequest();

    repo.setConnectionString(connvalue);
    repo.UpdateTicketStatus(tr.T.TicketID, tr.T.Status);
    return Results.NoContent();
});



app.Run();
