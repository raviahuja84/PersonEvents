using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PersonEvents_SignalR_Client
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Thread.Sleep(2000);

            return Task.Run(async () =>
            {
                return await ExecuteAsync();
            }).Result;
        }

        public static async Task<int> ExecuteAsync()
        {
            var baseUrl = "http://localhost:4235/personevents";

            Console.WriteLine("Connecting to {0}", baseUrl);
            var connection = new HubConnectionBuilder()
                .WithUrl(baseUrl)
                //.WithConsoleLogger()
                .Build();

            try
            {
                await connection.StartAsync();
                Console.WriteLine("Connected to {0}", baseUrl);

                var cts = new CancellationTokenSource();
                Console.CancelKeyPress += (sender, a) =>
                {
                    a.Cancel = true;
                    Console.WriteLine("Stopping loops...");
                    cts.Cancel();
                };

                // Setup Handler
                connection.On<List<dynamic>>("UpdatePersonEvents", data =>
                {
                    var products = data;
                    foreach (var item in products)
                    {
                        Console.WriteLine($"{item.name}: {item.quantity}");
                    }
                });

                while (!cts.Token.IsCancellationRequested)
                {
                    var personName = await Task.Run(() => ReadPersonName(), cts.Token);
                    var personDOB = await Task.Run(() => ReadPersonDOB(), cts.Token);
                    var eventData = await Task.Run(() => ReadEventData(), cts.Token);

                    if (!string.IsNullOrEmpty(personName) 
                    || personDOB == null
                    || (!string.IsNullOrEmpty(personName) ))
                    {
                        break;
                    }

                    await connection.InvokeAsync("RegisterEvent", personName, personDOB, eventData, cts.Token);
                }
            }
            catch (AggregateException aex) when (aex.InnerExceptions.All(e => e is OperationCanceledException))
            {
                Console.WriteLine($"Exception Caught - AggregateException: {aex.Message}");
            }
            catch (OperationCanceledException opcex)
            {
                Console.WriteLine($"Exception Caught - OperationCanceledException: {opcex.Message}");
            }
            finally
            {
                await connection.DisposeAsync();
            }
            return 0;
        }

        private static string ReadPersonName()
        {
            Console.WriteLine("Please enter the Person Name");
            var name = Console.ReadLine();
            return name;
        }

        private static string ReadEventData()
        {
            Console.WriteLine("Please enter the Event Data");
            var eventData = Console.ReadLine();
            return eventData;
        }

        private static DateTime ReadPersonDOB()
        {
            Console.WriteLine("Please enter the Person DOB in format (yyyy-mm-dd)");
            
            DateTime perDOB;
            var format = "yyyy-mm-dd";

            var strDOB = Console.ReadLine();
            if (DateTime.TryParseExact(strDOB, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out perDOB))
            {
                Console.WriteLine("Person DOB parsed ok");
            }
            else
            {
                Console.WriteLine("Person DOB failed to parse!!!");
            }

            return perDOB;
        }
    }
}
