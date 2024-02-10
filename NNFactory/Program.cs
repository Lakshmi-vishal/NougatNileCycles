using Microsoft.EntityFrameworkCore;
using ChocolateFactory.Models;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5200"); // Change the port as needed

// Add services to the container.
builder.Services.AddControllersWithViews(); // For MVC + API Controllers
builder.Services.AddRazorPages(); // If you're using Razor Pages

// Register DbContext with SQL Server
builder.Services.AddDbContext<AdventureWorks2022Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Server=VH_LVH;Database=AdventureWorks2022;Integrated Security=true")));

// Configure CORS to allow requests from Swagger UI and your React app
builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenCorsPolicy", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


// Register the Swagger generator and include XML comments for better API documentation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // To serve Swagger UI at the app's root (http://localhost:<port>)
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Apply CORS policy


app.UseAuthorization();

// Map controllers and Razor Pages (if used)
app.MapControllers();
app.MapRazorPages(); // If you're using Razor Pages
app.UseCors("OpenCorsPolicy");

app.Run();
