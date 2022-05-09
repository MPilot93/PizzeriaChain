namespace YI.ChainOfResponsibility
{
    public interface IOrderRepository 
    {
        IEnumerable<string> GetFileNames(string directoryPath);
        IEnumerable<string> GetOrderLines(string filePath);
        void SaveToFile(string fullFileName, string fileContent);
    }

    public class OrderRepository : IOrderRepository
    {
        public IEnumerable<string> GetFileNames(string directoryPath)
         => SafeExecute(() => Directory.GetFiles(directoryPath));

        public IEnumerable<string> GetOrderLines(string filePath)
            => SafeExecute(() => File.ReadAllLines(filePath));

        public void SaveToFile(string fullFileName, string fileContent)
            => SafeExecute(() => File.WriteAllText(fullFileName, fileContent));

        public void SafeExecute(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private IEnumerable<T> SafeExecute<T>(Func<IEnumerable<T>> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Enumerable.Empty<T>();
        }
    }
}
