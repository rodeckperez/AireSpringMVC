using AireSpringMVC.data.Implementation;
using AireSpringMVC.data.Interfaces;
using AireSpringMVC.workflows.Implementation;
using AireSpringMVC.workflows.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//set dependency injection
builder.Services.AddScoped<IEmployeeWorkflow, EmployeeWorkflow>();
builder.Services.AddScoped<IEmployeeData, EmployeeDataImplementation>();

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
    pattern: "{controller=Employee}/{action=Index}");

app.Run();
