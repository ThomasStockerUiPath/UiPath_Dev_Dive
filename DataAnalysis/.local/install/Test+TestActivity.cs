using System.Activities;
using UiPath.CodedWorkflows;
using UiPath.CodedWorkflows.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using OpenAIWrapper;
using UiPath.Core;
using UiPath.Core.Activities.Storage;
using UiPath.Orchestrator.Client.Models;
using UiPath.Testing;
using UiPath.Testing.Activities.TestData;
using UiPath.Testing.Activities.TestDataQueues.Enums;
using UiPath.Testing.Enums;
using UiPath.UIAutomationNext.API.Contracts;
using UiPath.UIAutomationNext.API.Models;
using UiPath.UIAutomationNext.Enums;

namespace DataAnalysis
{
    public class TestActivity : Activity
    {
        public TestActivity()
        {
            this.Implementation = () =>
            {
                return new TestActivityChild()
                {};
            };
        }
    }

    internal class TestActivityChild : CodeActivity
    {
        public TestActivityChild()
        {
            DisplayName = "Test";
        }

        protected override void Execute(CodeActivityContext context)
        {
            var codedWorkflow = new DataAnalysis.Test();
            CodedWorkflowHelper.Initialize(codedWorkflow, context);
            CodedWorkflowHelper.RunWithExceptionHandling(() =>
            {
                codedWorkflow.Before(new BeforeRunContext()
                {RelativeFilePath = "Test.cs"});
            }, () =>
            {
                codedWorkflow.Execute();
                return null;
            }, (exception, outArgs) =>
            {
                codedWorkflow.After(new AfterRunContext()
                {RelativeFilePath = "Test.cs", Exception = exception});
            });
        }
    }
}