namespace YI.ChainOfResponsibility
{
    public class DoughHandler : PriceFiller
    {
        public DoughHandler(IPriceManager priceManager) 
            : base(priceManager){ }

        public override void ProcessPrice(Pizza pizza)
        {
            pizza.Impasto.Ammount = _priceManager.GetPrice(pizza.Impasto.ItemName);
            _next?.ProcessPrice(pizza);
        }
    }
}
