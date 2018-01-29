using Medella.Performance.Tester.Benchmark;

namespace Medella.Performance.Tester.Deserializer
{
	public class ParseIntHexClr : BenchmarkRunner
	{
		private readonly string _str;
		public decimal Dec;

		public ParseIntHexClr(string str)
		{
			_str = str;
		}

		public override void Run()
		{
			Dec = int.Parse(_str, System.Globalization.NumberStyles.AllowHexSpecifier);
		}
		public override void Finsch()
		{
		}
	}
}