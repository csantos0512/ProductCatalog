namespace ProductCatalog.Infra.Data.SQLScripts
{
    public static class ReportScripts
    {
        public const string SELECT_REPORT = "SELECT p.ProductId, p.ProductName, c.CategoryName, p.Stock, p.Price FROM Product p INNER JOIN Category c ON c.CategoryId = p.CategoryId";

        public const string SELECT_MOVEMENT = "SELECT o.OrderId, o.OrderDate, i.UnitPrice, i.Quantity, i.UnitPrice, i.ItemTotal AS Total FROM [dbo].[OrderItem] i INNER JOIN [dbo].[Order] o ON o.OrderId = i.OrderId WHERE i.ProductId = @ProductId AND o.OrderTypeId = @OrderTypeId";
    }
}
