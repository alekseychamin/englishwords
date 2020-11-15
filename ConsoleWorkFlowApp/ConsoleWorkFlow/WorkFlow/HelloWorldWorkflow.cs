using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;

namespace ConsoleWorkFlow.WorkFlow
{
    public class HelloWorldWorkflow : IWorkflow
    {
        public string Id => "HelloWorldWorkflow";

        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .StartWith<HelloWorld>()
                .Then<GoodByeWorld>();
        }
    }
}
