var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped<NewsApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Serve static files (like index.html from wwwroot)
app.UseStaticFiles();

// Redirect root "/" to /index.html
app.MapGet("/", context =>
{
    context.Response.Redirect("/index.html");
    return Task.CompletedTask;
});

app.UseAuthorization();

app.MapControllers();

app.Run();