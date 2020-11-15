using ConsoleWorkFlow.Services;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ConsoleWorkFlow.WorkFlow
{
    public class HelloWorld : StepBody
    {
        private readonly IHelloService _helloService;

        public HelloWorld(IHelloService helloService)
        {
            _helloService = helloService;
        }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            _helloService.SayHello();
            return ExecutionResult.Next();
        }
    }
}
