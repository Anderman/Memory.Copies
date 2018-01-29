using Medella.Performance.Tester.Benchmark;
using System.Runtime.CompilerServices;

namespace Medella.Performance.Tester.Deserializer
{
	public class ParseIntHexSafe : BenchmarkRunner
	{
		private readonly string _str;
		public uint Dec;

		public ParseIntHexSafe(string str)
		{
			_str = str;
		}

		public override void Run()
		{
			var pos = 0;
			Dec = HexNumberToUInt32(_str, pos, _str.Length);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint HexNumberToUInt32(string buffer, int pos, int lenght)
		{
			uint number = 0;
			for (var i = pos; i < pos + lenght; i++)
			{
				var chr = buffer[i];
				if (chr >= '0' || chr <= '9')
				{
					number = number * 16 + (uint)(chr - '0');
					continue;
				}
				if (chr >= 'A' && chr <= 'F')
				{
					number = number * 16 + (uint)(chr - 'A' + 10);
					continue;
				}
				number = number * 16 + (uint)(chr - 'a' + 10);
			}
			//Debugger.Launch();
			return number;
		}
	}
}