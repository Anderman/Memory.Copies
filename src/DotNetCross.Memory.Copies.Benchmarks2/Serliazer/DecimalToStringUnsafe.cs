using Medella.Performance.Tester.Benchmark;
using System;
using System.Runtime.CompilerServices;

namespace Medella.Performance.Tester.Serliazer
{
	public unsafe class DecimalToStringUnsafe : BenchmarkRunner
	{
		private readonly char[] _buffer = new char[32];

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Run()
		{
			fixed (char* p = _buffer)
			{
				var p2 = p + _buffer.Length;
				DecimalToString(ref p2, decimal.MaxValue);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void DecimalToString(ref char* p2, decimal value)
		{
			//int[] arr = decimal.GetBits(value);
			*(--p2) = '\0';
			var pEnd = p2;
			var p = p2;
			var z = (uint*)&value;
			var hi = *(z + 1);
			var mid = *(z + 3);
			var low = *(z + 2);
			while ((mid | hi) != 0)
				p = UInt32ToDecChars(p, DecDivMod1E9(ref hi, ref mid, ref low), 9);
			p = UInt32ToDecChars(p, low, 0);
			var scale = *((byte*)&value + 2);
			var pStart = p;
			if (scale != 0)
			{
				pStart = --p;
				while (p < pEnd - scale)
				{
					*(p - 1) = *p;
					p++;
				}
				*(p - 1) = '.';
			}
			if (*((byte*)&value + 3) != 0)
				*(--pStart) = '-';
			p2 = pStart;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static char* UInt32ToDecChars(char* bufferEnd, uint value, int digits)
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
			//Debugger.Launch();
			return bufferEnd;
		}

		public static uint DecDivMod1E9(ref uint hi, ref uint mid, ref uint low)
		{
			long tmp = hi;
			hi = hi / 1000000000;
			tmp = ((tmp - hi * 1000000000) << 32) | mid;
			mid = (uint)(tmp / 1000000000);
			tmp = ((tmp - mid * 1000000000) << 32) | low;
			low = (uint)(tmp / 1000000000);
			return (uint)(tmp - low * 1000000000);
		}


		public override void Finsch()
		{
			Console.WriteLine($"*{new string(_buffer)}*");
		}
	}
}