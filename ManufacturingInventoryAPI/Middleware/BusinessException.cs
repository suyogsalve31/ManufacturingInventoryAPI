namespace ManufacturingInventoryAPI.Middleware
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {
        }
    }
}