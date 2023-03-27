using Microsoft.EntityFrameworkCore;
using VivesHelpdesk.Data;
using VivesHelpdesk.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddDbContext<VivesHelpdeskDbContext>(options =>
{
    options.UseInMemoryDatabase(nameof(VivesHelpdeskDbContext));
    //options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<TicketService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<VivesHelpdeskDbContext>();
    if (dbContext.Database.IsInMemory())
    {
        dbContext.Seed();
    }
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
