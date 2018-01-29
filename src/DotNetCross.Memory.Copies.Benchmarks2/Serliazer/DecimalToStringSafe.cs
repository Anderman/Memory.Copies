using Medella.Performance.Tester.Benchmark;
using System.Runtime.CompilerServices;

namespace Medella.Performance.Tester.Serliazer
{
	public unsafe class DecimalToStringSafe : BenchmarkRunner
	{
		private readonly char[] _buffer = new char[32];

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Run()
		{
			var pos = _buffer.Length;
			DecimalToString(decimal.MaxValue, _buffer, ref pos);
		}

		public static void DecimalToString(decimal value, char[] buffer, ref int i)
		{
			//int[] arr = decimal.GetBits(value);
			var z = (uint*)&value;
			var hi = *(z + 1);
			var mid = *(z + 3);
			var low = *(z + 2);
			while ((mid | hi) != 0)
				UInt32ToDecChars(buffer, ref i, DecDivMod1E9(ref hi, ref mid, ref low), 9);
			UInt32ToDecChars(buffer, ref i, low, 0);
			var scale = *((byte*)&value + 2);
			if (scale != 0)
			{
				for (var j = i; j < buffer.Length - scale; j++)
					buffer[j - 1] = buffer[j];
				buffer[i-- + scale] = '.';
			}
			if (*((byte*)&value + 3) != 0)
				buffer[i--] = '-';
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
	}
}