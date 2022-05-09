namespace YI.ChainOfResponsibility
{
    public class PizzaBuilder
    {
        private Pizza _pizza;
        public PizzaBuilder()
        {
            _pizza = new Pizza();
        }

        public static PizzaBuilder Create()
            => new PizzaBuilder();

        public PizzaBuilder WithPizzaType(string pizzaType)
        {
            _pizza.TipoPizza = new PizzaPart() { ItemName = pizzaType };
            return this;
        }

        public PizzaBuilder WithDough(string dough)
        {
            _pizza.Impasto = new PizzaPart() { ItemName = dough };
            return this;
        }

        public PizzaBuilder WithComponent(string component)
        {
            if(_pizza.Components?.Any() != true)
            {
                _pizza.Components = new List<PizzaPart>();
            }

            _pizza.Components.Add(new PizzaPart() { ItemName = component });
            return this;
        }

        public PizzaBuilder WithComponents(IEnumerable<string> components)
        {
            if (_pizza.Components?.Any() != true)
            {
                _pizza.Components = new List<PizzaPart>();
            }
            _pizza.Components.AddRange(
                components
                .Select(x => new PizzaPart() { ItemName = x }));
            return this;
        }


        public Pizza Build() => _pizza;
    }
}
