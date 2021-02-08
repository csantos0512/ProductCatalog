namespace ProductCatalog.Infra.Data.SQLScripts
{
    public static class ProductScripts
    {
        public const string DELETE_PRODUCT = "DELETE FROM Product WHERE ProductId = @ProductId";

        public const string INSERT_PRODUCT = "INSERT INTO Product(ProductName, CategoryId, Price, Stock) VALUES(@ProductName, @CategoryId, @Price, @Stock)";

        public const string SELECT_ALL_PRODUCTS = "SELECT p.ProductId, p.ProductName, p.CategoryId, p.Price, p.Stock, c.CategoryId, c.CategoryName FROM Product p INNER JOIN Category c ON c.CategoryId = p.CategoryId";

        public const string SELECT_PRODUCT = "SELECT p.ProductId, p.ProductName, p.CategoryId, p.Price, p.Stock, c.CategoryId, c.CategoryName FROM Product p INNER JOIN Category c ON c.CategoryId = p.CategoryId WHERE p.ProductId = @ProductId";

        public const string UPDATE_PRODUCT = "UPDATE Product SET  ProductName = @ProductName, CategoryId = @CategoryID, Price = @Price, Stock = @Stock WHERE ProductId = @ProductId";

        public const string INCREASE_PRICES = "UPDATE Product SET Price = Price + (Price * @PercentageValue) WHERE CategoryId = @CategoryId";

        public const string REDUCE_PRICES = "UPDATE Product SET Price = Price - (Price * @PercentageValue) WHERE CategoryId = @CategoryId";
    }
}
