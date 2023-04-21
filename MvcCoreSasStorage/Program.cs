using Azure.Data.Tables;
using MvcCoreSasStorage.Helpers;
using MvcCoreSasStorage.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//string azureKeys =
//    builder.Configuration.GetValue<string>
//    ("AzureKeys:StorageAccount");
//TableServiceClient tableServiceAlumnos =
//    new TableServiceClient(azureKeys);
//builder.Services.AddTransient<TableServiceClient>
//    (z => tableServiceAlumnos);
//builder.Services.AddTransient<ServiceStorageAlumnosXML>();
builder.Services.AddTransient<ServiceAzureAlumnos>();
builder.Services.AddSingleton<HelperPathProvider>();


builder.Services.AddControllersWithViews();

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
