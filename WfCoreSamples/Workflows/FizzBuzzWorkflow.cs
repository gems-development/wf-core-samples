using System;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WfCoreSamples.Workflows
{
	public class FizzBuzzWfData
	{
		public int Counter { get; set; } = 1;
		public StringBuilder Output { get; set; } = new StringBuilder();
	}

	public class FizzBuzzWorkflow : IWorkflow<FizzBuzzWfData>
	{
		public const string WorkflowDefinitionId = "FizzBuzz";

		public string Id => WorkflowDefinitionId;
		public int Version => 1;

		public void Build(IWorkflowBuilder<FizzBuzzWfData> builder)
		{
			builder
				.StartWith(context => ExecutionResult.Next())
				.While(data => data.Counter <= 100)
					.Do(a => a
						.StartWith(context => ExecutionResult.Next())
							.Output((step, data) => data.Output.Append(data.Counter))
						.If(data => data.Counter % 3 == 0 || data.Counter % 5 == 0)
							.Do(b => b
								.StartWith(context => ExecutionResult.Next())
									.Output((step, data) => data.Output.Clear())
								.If(data => data.Counter % 3 == 0)
									.Do(c => c
										.StartWith(context => ExecutionResult.Next())
											.Output((step, data) => data.Output.Append("Fizz")))
								.If(data => data.Counter % 5 == 0)
									.Do(c => c
										.StartWith(context => ExecutionResult.Next())
											.Output((step, data) => data.Output.Append("Buzz"))))
						.Then(context => ExecutionResult.Next())
							.Output((step, data) =>
							{
								Console.WriteLine(data.Output.ToString());
								data.Output.Clear();

								data.Counter++;
							}));
		}
	}
}
