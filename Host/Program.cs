using System;
using System.ServiceModel;
using ClassLibrary1;

namespace Host
{
    internal class Program 
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(Service1)))
            {
                host.Open();
                Console.WriteLine("Поехали!");
                Console.ReadLine();

            }
        }
    }
}
