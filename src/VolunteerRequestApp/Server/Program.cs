using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Globalization;
using System.Reflection;
using VolunteerRequestApp.Server.Core;
using VolunteerRequestApp.Server.Infrastructure;
using VolunteerRequestApp.Server.Infrastructure.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite("Data Source = Data/RequestDB.db");
});

builder.Services.AddAutoMapper(typeof(AppAutoMapper).Assembly);
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Volunteer Request App Project API",
        Description = "Education project with KN3, Ostroh Academy",        
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Email = "yuriy.kleban@oa.edu.ua",
            Name = "Yurii Kleban",
            Url = new Uri("https://kleban.page")
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});


builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    // Define the list of cultures your app will support 
    var supportedCultures = new List<CultureInfo>()
                {
                    new CultureInfo("uk-UA")
                };

    // Set the default culture 
    options.DefaultRequestCulture = new RequestCulture("uk-UA");
    options.DefaultRequestCulture.Culture.NumberFormat.CurrencySymbol = supportedCultures[1].NumberFormat.CurrencySymbol;

    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders = new List<IRequestCultureProvider>() {
                 new QueryStringRequestCultureProvider() 
                };
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddScoped<StatusRepository>();
builder.Services.AddScoped<CurrencyPairRepository>();
builder.Services.AddScoped<CurrencyApiHelper>();
builder.Services.AddScoped<RequestRepository>();
builder.Services.AddScoped<DonationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();

}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();

app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
