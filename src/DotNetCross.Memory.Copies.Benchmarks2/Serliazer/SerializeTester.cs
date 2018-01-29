using System;
using System.Collections.Generic;
using Medella.Performance.Tester.Benchmark;

namespace Medella.Performance.Tester.Serliazer
{
	public class SerializeTester
	{
		public static void Run()
		{
			var loopOverhead = new LoopOverhead().Start();
			Console.WriteLine($"Loopoverhead:{loopOverhead}");
			new DecimalToStringUnsafe().Start();
			var results = new Dictionary<string, double>
			{
				{nameof(DecimalToStringUnsafe), new DecimalToStringUnsafe().Start() - loopOverhead},
				{nameof(DecimalToStringSafe), new DecimalToStringSafe().Start() - loopOverhead},
				{nameof(DecimalToStringClr), new DecimalToStringClr().Start() - loopOverhead},
				{nameof(UInt32ToDecCharsUnsafe), new UInt32ToDecCharsUnsafe().Start() - loopOverhead},
				{nameof(UInt32ToDecCharsSafe), new UInt32ToDecCharsSafe().Start() - loopOverhead},
				{nameof(UInt32ToDecCharsClr), new UInt32ToDecCharsClr().Start() - loopOverhead},
			};
			foreach (var result in results)
				Console.WriteLine($"{result.Key}{result.Value,8:0.00} ns");
		}
	}
}