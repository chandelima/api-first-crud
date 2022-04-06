public class ProductRepository {
    public static List<Product>? Products {get; set; } = new List<Product>();

    public static void Init (IConfiguration config) {
        var products = config.GetSection("Products").Get<List<Product>>();
        Products = products;
    }

    public static void Add(Product product) {
        Products.Add(product);
    }

    public static Product GetByCode(string code) {
        return Products.FirstOrDefault(p => p.Code == code);
    }

    public static void Remove(Product product) {
        Products.Remove(product);
    }
}