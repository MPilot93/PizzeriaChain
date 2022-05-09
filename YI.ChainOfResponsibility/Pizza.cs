namespace YI.ChainOfResponsibility
{
    public class Pizza
    {
        public PizzaPart? TipoPizza { get; set; }
        public PizzaPart? Impasto { get; set; }
        public List<PizzaPart>? Components { get; set; }

        public decimal GetTotals()
        {
            var total = 0M;
            total += TipoPizza?.Ammount ?? 0;
            total += Impasto?.Ammount ?? 0;
            total += Components?.Sum(x => x.Ammount) ?? 0;
            return total;
        }
    }

    public class PizzaPart
    {
        public string ItemName { get; set; }
        public decimal Ammount { get; set; }
    }

}
