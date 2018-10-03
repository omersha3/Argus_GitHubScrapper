

using System;

namespace GitHubScraper
{
    class Program
    {

        static void Main(string[] args)
        {
            TestContainer test = new TestContainer();
            ResultHandler.Create();
            Console.Write("Enter the MonogoDB IP and port you created, for example - 192.168.99.100:27017: ");
            string mongoIP = Console.ReadLine();
            Console.Write("Enter the Selenium Web Server IP and port you created:, for example - 192.168.99.100:4444: ");
            string webServerIP = Console.ReadLine();
            test.RunTest(mongoIP, webServerIP);
            Console.WriteLine("Go to Robo 3T, connect to the mongoDB you started and check the entieties");
            Console.WriteLine("Tnx. Press any key to exit");
            Console.ReadKey();

        }

    }

}
