using Dotmim.Sync;
using Dotmim.Sync.Sqlite;
using Dotmim.Sync.Web.Client;
using System;
using System.Threading.Tasks;

namespace DBSync.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Be sure the web api has started. Then click enter..");
            Console.ReadLine();
            await SynchronizeAsync();
        }

        private static async Task SynchronizeAsync()
        {
            var clientConnectionString = @"Data Source=C:\DBName.db;Version=3;";
            var clientProvider = new SqliteSyncProvider(clientConnectionString);

            var serverOrchestrator = new WebClientOrchestrator("https://localhost:44374/api/sync");
          

            var agent = new SyncAgent(clientProvider, serverOrchestrator);

            do
            {
                try
                {
                    var progress = new SynchronousProgress<ProgressArgs>(args => Console.WriteLine($"{args.PogressPercentageString}:\t{args.Message}"));

                    var result = await agent.SynchronizeAsync(progress);

                    Console.WriteLine(result);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            } while (Console.ReadKey().Key != ConsoleKey.Escape);

            Console.WriteLine("End");
        }
    }
}
