using Medella.Performance.Tester.Benchmark;

namespace Medella.Performance.Tester.Deserializer
{
	public class ParseIntClr : BenchmarkRunner
	{
		private readonly string _str;
		public decimal Dec;

		public ParseIntClr(string str)
		{
			_str = str;
		}

		public override void Run()
		{
			Dec = int.Parse(_str);
		}
	}
}