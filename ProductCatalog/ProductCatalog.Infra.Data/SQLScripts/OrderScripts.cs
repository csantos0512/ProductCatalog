namespace ProductCatalog.Infra.Data.SQLScripts
{
    public static class OrderScripts
    {
        public const string INSERT_ORDER = "INSERT INTO [dbo].[Order] (OrderDate, OrderTypeId, OrderTotal) VALUES(@OrderDate, @OrderTypeId, @OrderTotal) SELECT SCOPE_IDENTITY()";
        
        public const string INSERT_ORDER_ITEM = "INSERT INTO [dbo].[OrderItem] ([OrderId], [ProductId], [UnitPrice], [Quantity], [ItemTotal]) VALUES(@OrderId, @ProductId, @UnitPrice, @Quantity, @ItemTotal);";
    }
}
