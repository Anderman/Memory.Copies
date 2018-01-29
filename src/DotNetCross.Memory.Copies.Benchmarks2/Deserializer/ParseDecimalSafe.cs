using Medella.Performance.Tester.Benchmark;
using System;
using System.Runtime.CompilerServices;

namespace Medella.Performance.Tester.Deserializer
{
	public class ParseDecimalSafe : BenchmarkRunner
	{
		private string _str;
		private decimal _dec;
		private int _pos;

		public ParseDecimalSafe(string str)
		{
			_str = str;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Run()
		{
			_dec = ParseDecimal(ref _str, ref _pos);
		}
		public static decimal ParseDecimal(ref string buffer, ref int pos)
		{
			var neg = false;
			byte scale = 0;
			ulong midLow = 0;
			uint hi = 0;
			var dot = false;
			for (var i = pos; i < buffer.Length; i++)
			{
				var chr = buffer[i];
				if (chr == '"')
				{
					pos = i;
					break;
				}
				if (chr == '-')
				{
					neg = true;
					continue;
				}
				if (chr == '.' || chr == ',')
				{
					dot = true;
					continue;
				}
				DecAddDigit(ref midLow, ref hi, (uint)(chr - '0'));

				if (dot) scale++;
			}
			var dec = new decimal((int)midLow, (int)(midLow >> 32), (int)hi, neg, scale);
			return dec;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void DecAddDigit(ref ulong midLow, ref uint hi, uint digit)
		{
			DecMul10(ref midLow, ref hi);
			if (midLow > ulong.MaxValue - digit)
				hi++;
			midLow += digit;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void DecMul10(ref ulong midLow, ref uint hi)
		{
			if (hi == 0 && midLow < ulong.MaxValue / 10)
			{
				midLow = midLow * 10;
			}
			else
			{
				var midloAdd = midLow;
				var hiAdd = hi;
				DecShiftLeft(ref midLow, ref hi);
				DecShiftLeft(ref midLow, ref hi);
				DecAdd(ref midLow, ref hi, midloAdd, hiAdd);
				DecShiftLeft(ref midLow, ref hi);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void DecShiftLeft(ref ulong midLow, ref uint hi)
		{
			uint c0 = (midLow & 0x8000000000000000) != 0 ? 1u : 0u;
			midLow = midLow << 1;
			hi = (hi << 1) | c0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void DecAdd(ref ulong midLow, ref uint hi, ulong midloAdd, uint hiAdd)
		{
			if (midLow > ulong.MaxValue - midloAdd)
			{
				hi++;
			}
			midLow += midloAdd;
			hi += hiAdd;
		}

		public override void Finsch()
		{
			if (decimal.Parse(_str) != _dec)
				Console.Write($"*{_dec}!={_str}*");
		}
	}
}