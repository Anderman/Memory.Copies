using Medella.Performance.Tester.Benchmark;
using System.Runtime.CompilerServices;

namespace Medella.Performance.Tester.Deserializer
{
	public class ParseIntSafe : BenchmarkRunner
	{
		private readonly string _str;
		public int Dec;

		public ParseIntSafe(string str)
		{
			_str = str;
		}

		public override void Run()
		{
			var pos = 0;
			Dec = ParseInt(_str, ref pos);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ParseInt(string buffer, ref int pos)
		{
			var neg = false;
			var number = 0;
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
				number = number * 10 + (chr - '0');
			}
			//Debugger.Launch();
			return neg ? -number : number;
		}
	}
}