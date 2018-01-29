using Medella.Performance.Tester.Benchmark;
using System.Runtime.CompilerServices;

namespace Medella.Performance.Tester.Serliazer
{
	public class DecimalToStringClr : BenchmarkRunner
	{
		public string Str;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Run()
		{
			Str = decimal.MaxValue.ToString();
		}
	}
}