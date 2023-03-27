using VivesHelpdesk.Ui.WebApp.Sdk;
using VivesHelpdesk.Ui.WebApp.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var appSettings = new AppSettings();
builder.Configuration.GetRequiredSection(nameof(AppSettings)).Bind(appSettings);

if (!string.IsNullOrWhiteSpace(appSettings.ApiBaseUrl))
{
    builder.Services.AddHttpClient("HelpdeskApi", options =>
    {
        options.BaseAddress = new Uri(appSettings.ApiBaseUrl);
    });
}

builder.Services.AddScoped<PersonSdk>();
builder.Services.AddScoped<TicketSdk>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
