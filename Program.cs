using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(); //Serviço: Banco de Dados

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

app.MapPost("/products", (Product product) => {
    ProductRepository.Add(product);
    return Results.Created("/products/" + product.Code, product);
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