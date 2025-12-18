using MVC_Demo.Interfaces;
using MVC_Demo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container.
builder.Services.AddControllersWithViews();

//Customer Service Registration in DI Container
builder.Services.AddSingleton<CustomerService>(); // in-memory service
builder.Services.AddSingleton<ProductService>(); // in-memory service
builder.Services.AddSingleton<IGetService, CustomerService>(); // Interface to Service mapping

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Pre-defined Middlewares are added to the pipeline
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
