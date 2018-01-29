using Medella.Performance.Tester.Benchmark;
using System;
using System.Runtime.CompilerServices;

namespace Medella.Performance.Tester.Serliazer
{
	public unsafe class UInt32ToDecCharsUnsafe : BenchmarkRunner
	{
		private readonly char[] _buffer = new char[32];
		public char* pEnd;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Run()
		{
			fixed (char* p = &_buffer[31])
			{
				pEnd = UInt32ToDecChars(p, uint.MaxValue, 0);
			}

			char* UInt32ToDecChars(char* bufferEnd, uint value, int digits)
			{
				while (--digits >= 0 || value > 0)
					if (value > 9)
					{
						var tmp = value * (ulong)0x0CCCCCCCD;
						var newValue = (uint)(tmp >> 35);
						*(--bufferEnd) = (char)(value - newValue * 10 + '0');
						value = newValue;
					}
					else
					{
						*(--bufferEnd) = (char)(value + '0');
						break;
					}
				return bufferEnd;
			}
		}

		public override void Finsch()
		{
			Console.WriteLine($"*{new string(_buffer)}*");
		}
	}
}