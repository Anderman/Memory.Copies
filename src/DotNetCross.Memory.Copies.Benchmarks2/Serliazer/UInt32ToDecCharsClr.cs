using Medella.Performance.Tester.Benchmark;
using System.Runtime.CompilerServices;

namespace Medella.Performance.Tester.Serliazer
{
	public unsafe class UInt32ToDecCharsClr : BenchmarkRunner
	{
		private string str;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Run()
		{
			str = uint.MaxValue.ToString();
		}
	}
}