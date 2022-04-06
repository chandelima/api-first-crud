using api_first_crud.Models;

public class Product {
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public Category MyProperty { get; set; }
    public Tag Tag { get; set; }
}