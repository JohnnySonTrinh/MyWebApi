var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Basic routes
app.MapGet("/", () => "Welcome to the Simple Web API!");

app.Run();
