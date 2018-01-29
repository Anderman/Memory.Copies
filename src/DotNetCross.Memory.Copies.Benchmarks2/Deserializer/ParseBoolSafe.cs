using Medella.Performance.Tester.Benchmark;
using System.Runtime.CompilerServices;

namespace Medella.Performance.Tester.Deserializer
{
	public class ParseBoolSafe : BenchmarkRunner
	{
		private readonly string _str;
		public bool Result;

		public ParseBoolSafe(string str)
		{
			_str = "dummy" + str;
		}

		public override void Run()
		{
			var pos = "dummy".Length;
			Result = ParseBool(_str, ref pos, _str.Length - pos);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool ParseBool(string s, ref int i, int length)
		{
			if (length == 0) return false;
			if (length == 1 && s[i] == '0') return false;
			if (length == 1 && s[i] == '1') return true;
			if (length == 2 && IsStr(s, i, "no")) return false;
			if (length == 3 && IsStr(s, i, "yes")) return true;
			if (length == 4 && IsStr(s, i, "true")) return true;
			if (length == 5 && IsStr(s, i, "false")) return false;
			if (int.TryParse(s, out var value))
				return value > 0;
			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsStr(string str, int pos, string cmp)
		{
			for (int i = 0; i < cmp.Length; i++)
				if (cmp[i] != (str[pos + i] | 0x20)) return false;
			return true;
		}
	}
}