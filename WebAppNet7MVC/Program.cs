
using WebAppNet7MVC.models;
using WebAppNet7MVC.repositorio.contrato;
using WebAppNet7MVC.repositorio.implementacion;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//OJO - Referencias de repositorios genericos
builder.Services.AddScoped<IGenericRepository<Departamento>, DepartamentoRepositorio>();
builder.Services.AddScoped<IGenericRepository<Empleado>, EmpleadoRepositorio>();


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
