using RapidFireLib;
using RapidFireLib.Extensions;
using RapidFireUI;
using Web.Config;
using Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Register HTTP client for API calls
builder.Services.AddHttpClient<QuizApiService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7001");
});


builder.Services.AddScoped<QuizApiService>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddRapidFire(new AppConfig());
builder.Services.AddRapidFireUI();
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCookiePolicy();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.UseRapidFire();

app.Run();
