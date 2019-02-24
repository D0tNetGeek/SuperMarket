namespace SuperMarket.Service.Interfaces
{
    public interface ICheckout
    {
        void ScanItem(char item);
        decimal CalculateTotalPrice();
    }
}