using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["Database:SqlServer"]); //Serviço: Banco de Dados

var app = builder.Build();

var configuration = app.Configuration;
ProductRepository.Init(configuration);


//Endpoints
app.MapGet("/products/{code}", ([FromRoute] string code) => {
    var product = ProductRepository.GetByCode(code);
    if(product == null)
        return Results.NotFound();
    return Results.Ok(product);
    
});

app.MapPost("/products", (ProductRequest productRequest, ApplicationDbContext context) => {
    var category = context.Category.Where(
        c => c.Id == productRequest.CategoryId
    ).First();
    var product = new Product {
        Code = productRequest.Code,
        Name = productRequest.Name,
        Description = productRequest.Description,
        Category = category
    };
    context.Products.Add(product);
    context.SaveChanges();
    return Results.Created($"/products/{product.Id}", product.Id);
});

app.MapPut("products", (Product product) => {
    var productSaved = ProductRepository.GetByCode(product.Code);
    productSaved.Name = product.Name;
});

app.MapDelete("products/{code}", ([FromRoute] string code) => {
    var productSaved = ProductRepository.GetByCode(code);
    ProductRepository.Remove(productSaved);
});

app.MapGet("/configuration/database", (IConfiguration config) => {
    return Results.Ok(config["Database:Port"]);
});


app.Run();
