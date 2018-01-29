using Medella.Performance.Tester.Benchmark;
using System.Text;

namespace Medella.Performance.Tester.StringBuilderTests
{
	public class StringBuilderClear : BenchmarkRunner
	{
		private readonly string _str;
		public StringBuilder Sb;

		public StringBuilderClear()
		{
			Sb = new StringBuilder(1000);
		}

		public override void Run()
		{
			Sb.Clear();
		}
	}
}