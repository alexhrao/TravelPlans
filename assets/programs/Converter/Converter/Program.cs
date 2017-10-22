using System;
using Converter.Data;
using Converter.Utility;
using System.IO;
using System.Threading;

namespace Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Process(args);
            }
            catch (Exception e)
            {
                Console.WriteLine("An Error Occured during the last operation - details below");
                Console.WriteLine(e.ToString());
            }
            while (Worker.Workers != 0)
            {
                Thread.Sleep(1000);
            }
            Console.WriteLine("Finished. Press any key to exit...");
            Console.ReadKey();
        }

        public class Worker
        {
            // Given a single path, will Parse/Write file
            public string WorkerPath;
            public static int Workers = 0;
            public Worker(string path)
            {
                WorkerPath = path;
                Workers++;
            }

            public void Process(Object stateInfo)
            {
                Console.WriteLine("\tParsing File " + Path.GetFullPath(WorkerPath).Replace(@"\", "/") + "...");
                Parser parser = new Parser(WorkerPath);
                Console.WriteLine("\tWriting File " + Path.GetFullPath(WorkerPath).Replace(@"\", "/") + "...");
                Writer writer = new Writer(WorkerPath, parser);
                Workers--;
            }
        }

        static void Process(string[] args)
        {
            foreach (string path in args)
            {
                FileAttributes attributes = File.GetAttributes(path.Replace(@"\", "/"));
                if (attributes.HasFlag(FileAttributes.Directory))
                {
                    // Search directory for all files, call ourself with these.
                    args = Directory.GetFiles(path, "Details.txt", SearchOption.AllDirectories);
                    Process(args);
                }
                else
                {
                    // Create worker
                    Worker worker = new Worker(path);
                    ThreadPool.QueueUserWorkItem(worker.Process);
                }
            }
        }
    }
}
