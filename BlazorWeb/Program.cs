using BlazorWeb.Components;
using BlazorWeb.Components.Authentication;
using BlazorWeb.Data;
using BlazorWeb.Services.Admins;
using BlazorWeb.Services.brands;
using BlazorWeb.Services.categories;
using BlazorWeb.Services.Carts;
using BlazorWeb.Services.Customers;
using BlazorWeb.Services.Orders;
using BlazorWeb.Services.PaymentMethods;
using BlazorWeb.Services.Products;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

var builder = WebApplication.CreateBuilder(args);

//Add connection
builder.Services.AddDbContext<AppDbContext>(option => 
    option.UseMySQL(
        builder.Configuration.GetConnectionString("DefaultString")
    )
);

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBrandService, BrandsService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, AdminAuthStateProvider>();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorizationCore();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    dbContext.Database.ExecuteSqlRaw("""
        ALTER TABLE products
        MODIFY COLUMN price DECIMAL(18,2) NOT NULL;
        """);

    dbContext.Database.ExecuteSqlRaw("""
        ALTER TABLE orderdetail
        MODIFY COLUMN price DECIMAL(18,2) NOT NULL;
        """);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
