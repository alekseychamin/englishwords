using ConsoleWorkFlow.Services;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ConsoleWorkFlow.WorkFlow
{
    public class GoodByeWorld : StepBody
    {
        private readonly IGoodByeService _goodByeService;

        public GoodByeWorld(IGoodByeService goodByeService)
        {
            _goodByeService = goodByeService;
        }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            _goodByeService.SayGoodBye();
            return ExecutionResult.Next();
        }
    }
}
