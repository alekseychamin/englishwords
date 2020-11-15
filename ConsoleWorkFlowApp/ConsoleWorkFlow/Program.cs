using ConsoleWorkFlow.Services;
using ConsoleWorkFlow.WorkFlow;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WorkflowCore.Interface;

namespace ConsoleWorkFlow
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup DI
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IHelloService, HelloService>();
                    services.AddSingleton<IGoodByeService, GoodByeService>();
                    services.AddTransient<GoodByeWorld>();
                    services.AddTransient<HelloWorld>();
                    services.AddWorkflow();
                    services.AddLogging();
                })
                .Build();

            var workflowHost = host.Services.GetService<IWorkflowHost>();
            workflowHost.RegisterWorkflow<HelloWorldWorkflow>();
            workflowHost.Start();

            workflowHost.StartWorkflow("HelloWorldWorkflow", 1, null);

            Console.ReadLine();
            workflowHost.Stop();
        }
    }
}
