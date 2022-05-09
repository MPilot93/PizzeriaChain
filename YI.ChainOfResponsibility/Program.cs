using System.Text;
using YI.ChainOfResponsibility;

//attenzione, stai leggendo TUTTI i file del percorso!!!!!!!
const string OrdersDirectory = @"C:\Users\marco\Desktop\progetti VS\PizzeriaChain\ordini";
const string OutputFile = @"C:\Users\marco\Desktop\progetti VS\PizzeriaChain\ordini\OrderTotals.TXT";

//Inizio creazione di Chain-Of-Responsibility
//Creiamo gli handler
var pizzaHandler = new PizzaTypeHandler(PriceManagerFactory.GetPriceManager(typeof(PizzaTypeHandler)));
var doughHandler = new DoughHandler(PriceManagerFactory.GetPriceManager(typeof(DoughHandler)));
var addsHandler = new AddsHandler(PriceManagerFactory.GetPriceManager(typeof(AddsHandler)));
//Definiamo la catena
pizzaHandler.SetSuccesor(doughHandler);
doughHandler.SetSuccesor(addsHandler);
//Fine creazione di Chain-Of-Responsibility

//Creiamo repository che se ne occuperà di persistenza. Lettura e scrittura dei dati sul filesystem
IOrderRepository orderRepository = new OrderRepository();
var directoryInfo = orderRepository.GetFileNames(OrdersDirectory);
var progressivo = 1;
var orderTotalsStringBuilder = new StringBuilder();
foreach(var file in directoryInfo)
{
    var orderId = $"Ordine { progressivo++ }";
    var orderLines = orderRepository.GetOrderLines(file);
    var orderItems = new List<Pizza>();
    foreach(var line in orderLines)
    {
        var pizzaComponents = line.Split(';');

        //usiamo il pattern Builder
        var pizza = PizzaBuilder
            .Create()
            .WithPizzaType(pizzaComponents[0])
            .WithDough(pizzaComponents[1])
            .WithComponents(pizzaComponents[2].Split(','))
            .Build();
        pizzaHandler.ProcessPrice(pizza);
        orderItems.Add(pizza);
    }
    orderTotalsStringBuilder.AppendLine(
        $"{orderId};{orderItems.Sum(x=>x.GetTotals())}");
}
orderRepository.SaveToFile(OutputFile, 
    orderTotalsStringBuilder.ToString());