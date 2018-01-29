using Medella.Performance.Tester.Benchmark;

namespace Medella.Performance.Tester.CommandTests
{
	public class CommandTester

	{
		public static void Run()
		{
			var loopOverhead = new LoopOverhead().Start();
			new SqlNewConnection().Start();
			new SqlOpenNewConnection().Start();
			new SqlOpenExistingConnection().Start();
			new SqlNewCommand().Start();
			new SqlCommandWith100Parameters().Start();
			new SqlCommandSimpleCommmand().Start();
			new SqlCommandExecuteWithParameters().Start();
			new SqlCommandExecuteAsStatement().Start();
			new SqlCommandUpdateStatement().Start();
		}
	}
}