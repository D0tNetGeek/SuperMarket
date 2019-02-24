namespace SuperMarket.Service.Interfaces
{
    public interface ICheckout
    {
        void ScanItem(string item);
        decimal CalculateTotalPrice();
    }
}