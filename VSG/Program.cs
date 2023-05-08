namespace VSG
{
    internal class Program
    {
        private const string HELP_SHORT = "Graag een temperatuurbestand als arg meegeven.";

        static void Main(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine(HELP_SHORT);
            }
            else
            {
                FileReader reader = new FileReader(true);
                reader.ReadFile(args[0]);
            }                
        }
    }
}