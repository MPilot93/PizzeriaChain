namespace YI.ChainOfResponsibility
{
    public class AddsHandler : PriceFiller
    {
        public AddsHandler(IPriceManager priceManager) 
            : base(priceManager) { }

        public override void ProcessPrice(Pizza pizza)
        {
            foreach(var component in pizza.Components)
            {
                //if (component.Equals("Ananas"))
                //    component.Ammount -= (_priceManager.GetPrice(pizza.Impasto.ItemName) + _priceManager.GetPrice(pizza.TipoPizza.ItemName));
                
                //else

                    component.Ammount = _priceManager.GetPrice(component.ItemName);
            }
            _next?.ProcessPrice(pizza);
        }
    }
}
