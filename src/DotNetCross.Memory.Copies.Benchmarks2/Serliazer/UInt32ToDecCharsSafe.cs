using Medella.Performance.Tester.Benchmark;
using System;
using System.Runtime.CompilerServices;

namespace Medella.Performance.Tester.Serliazer
{
	public unsafe class UInt32ToDecCharsSafe : BenchmarkRunner
	{
		private readonly char[] _buffer = new char[32];
		private char* pEnd;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Run()
		{
			var pos = _buffer.Length;
			UInt32ToDecChars(_buffer, ref pos, uint.MaxValue, 0);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void UInt32ToDecChars(char[] buffer, ref int j, uint value, int digits)
		{
			while (--digits >= 0 || value > 0)
				if (value > 9)
				{
					var tmp = value * (ulong)0x0CCCCCCCD;
					var newValue = (uint)(tmp >> 35);
					buffer[--j] = (char)(value - newValue * 10 + '0');
					value = newValue;
				}
				else
				{
					buffer[--j] = (char)(value + '0');
					break;
				}
		}

		public override void Finsch()
		{
			Console.WriteLine($"*{new string(_buffer)}*");
		}
	}
}