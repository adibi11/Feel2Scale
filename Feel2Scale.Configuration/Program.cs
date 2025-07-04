namespace Feel2Scale.Configuration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Feel2Scale Configuration Tool  " + Config.GetConnectionString("PostgresDB"));
        }
    }
}
