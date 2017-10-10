using System;
using Converter.Data;
using Converter.Utility;

namespace Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new string[1];
            args[0] = "C:/Users/alexhrao/Documents/TravelPlans/trips/07/Details.txt";
            // Arguments will be file paths.
            foreach (string path in args)
            {
                Parser parser = new Parser(path);
                Writer writer = new Writer(path, parser);
                int tripInd = 1;
                foreach (Trip trip in parser.Transportation)
                {
                    Console.WriteLine(tripInd + ". " + trip.Name);
                    int transInd = 1;
                    foreach (Transport trans in trip.Transports)
                    {
                        Console.WriteLine("\t" + transInd + ". " + trans.DepartStation + " (" + trans.DepartTime.ToShortTimeString() + ")" +
                            " -> " + trans.ArriveStation + " (" + trans.ArriveTime.ToShortTimeString() + ")" + 
                            (trans.IsReservation ? " RESERVATION" : ""));
                        transInd++;
                    }
                    tripInd++;
                }
            }
            Console.ReadKey();
        }
    }
}
