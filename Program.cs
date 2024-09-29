using WebApplication310824_Books.Middleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<TokenMiddleware>();

var books = new List<Book>
{
    new Book { Id = 1, Title = "Book 1", Category = "music" },
    new Book { Id = 2, Title = "Book 2", Category = "science" },
    new Book { Id = 3, Title = "Book 3", Category = "music" }
};

app.MapGet("/allbooks", () =>
{
    return Results.Content(GenerateHtmlTable(books), "text/html");
});

app.MapGet("/getbooks", (HttpContext context) =>
{
    var category = context.Request.Query["category"].ToString();
    var filteredBooks = books.Where(b => b.Category == category).ToList();
    return Results.Content(GenerateHtmlTable(filteredBooks), "text/html");
});

app.Run();

string GenerateHtmlTable(List<Book> books)
{
    var html = "<table><tr><th>ID</th><th>Title</th><th>Category</th></tr>";
    foreach (var book in books)
    {
        html += $"<tr><td>{book.Id}</td><td>{book.Title}</td><td>{book.Category}</td></tr>";
    }
    html += "</table>";
    return html;
}
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
}

//https://localhost:7008/allbooks
//https://localhost:7008/getbooks?token=token12345&category=music
