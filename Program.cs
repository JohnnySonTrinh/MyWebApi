using MyWebApi.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// In-memory data store
var items = new List<Item>
{
    new() { Id = 1, Name = "Apple",  Price = 9.90m },
    new() { Id = 2, Name = "Banana", Price = 5.50m }
};

// GET all items
app.MapGet("/", () => "API is running. Try /api/items");

app.MapGet("/api/items", () => items);

// GET item by id
app.MapGet("/api/items/{id:int}", (int id) =>
{
    var item = items.FirstOrDefault(i => i.Id == id);
    return item is null ? Results.NotFound() : Results.Ok(item);
});

// POST create item
app.MapPost("/api/items", (Item newItem) =>
{
    if (newItem.Id == 0)
        newItem.Id = items.Count == 0 ? 1 : items.Max(i => i.Id) + 1;

    items.Add(newItem);
    return Results.Created($"/api/items/{newItem.Id}", newItem);
});

// PUT update item
app.MapPut("/api/items/{id:int}", (int id, Item updated) =>
{
    var existing = items.FirstOrDefault(i => i.Id == id);
    if (existing is null) return Results.NotFound();

    existing.Name  = updated.Name;
    existing.Price = updated.Price;
    return Results.NoContent();
});

// DELETE item
app.MapDelete("/api/items/{id:int}", (int id) =>
{
    var existing = items.FirstOrDefault(i => i.Id == id);
    if (existing is null) return Results.NotFound();

    items.Remove(existing);
    return Results.NoContent();
});

app.Run();
