namespace YI.ChainOfResponsibility
{
    public abstract class PriceFiller
    {
        protected IPriceManager _priceManager;
        public PriceFiller(IPriceManager priceManager)
        {
            _priceManager = priceManager;
        }

        protected PriceFiller? _next;
        public void  SetSuccesor(PriceFiller next)
        {
            _next = next;
        }
        public abstract void ProcessPrice(Pizza pizza);
    }
}
