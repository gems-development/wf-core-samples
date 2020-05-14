using Microsoft.Extensions.DependencyInjection;
using System;
using WorkflowCore.Interface;
using WfCoreSamples.Workflows;

namespace WfCoreSamples
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var serviceProvider = ConfigureServices();
			var host = serviceProvider.GetService<IWorkflowHost>();

			// Регистрируем бизнес-процесс с классом данных.
			host.RegisterWorkflow<FizzBuzzWorkflow, FizzBuzzWfData>();
			// Отмечаем процесс для запуска.
			host.StartWorkflow(FizzBuzzWorkflow.WorkflowDefinitionId);
			// Стартуем движок.
			host.Start();
			Console.ReadKey();
			host.Stop();
		}

		private static IServiceProvider ConfigureServices()
		{
			var services = new ServiceCollection();

			services.AddLogging();
			services.AddWorkflow();

			return services.BuildServiceProvider();
		}
	}
}
