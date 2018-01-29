using Medella.Performance.Tester.Benchmark;

namespace Medella.Performance.Tester.Deserializer
{
	public class ParseDecimalClr : BenchmarkRunner
	{
		private string _str;
		public decimal Dec;

		public ParseDecimalClr(string str)
		{
			_str = str;
		}

		public override void Run()
		{
			Dec = decimal.Parse(_str);
		}
	}
}