using Medella.Performance.Tester.Benchmark;

namespace Medella.Performance.Tester.StringBuilderTests
{
	public class StringbuidlerTester

	{
		public static void Run()
		{
			var loopOverhead = new LoopOverhead().Start();
			new StringBuilderClear().Start();
			new StringBuilderAppend().Start();
		}
	}
}