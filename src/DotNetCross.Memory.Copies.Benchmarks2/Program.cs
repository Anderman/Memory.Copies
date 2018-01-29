using Medella.Performance.Tester.MemcopyTest;
using Medella.Performance.Tester.StringBuilderTests;
using Medella.Performance.Tester.Timer;
using System;
using System.Diagnostics;
using Medella.Performance.Tester.CommandTests;
using Medella.Performance.Tester.Deserializer;

namespace Medella.Performance.Tester
{
	public class Program
	{
		private const ulong TestDuration = 100;

		//private static extern bool QueryThreadCycleTime(IntPtr hThread, out ulong cycles);
		//private static readonly IntPtr PseudoHandle = (IntPtr)(-2);
		public static double LoopOverhead;

		public static ulong CyclesPerSecond;
		public static double NsPerCycle;

		public static void Main(string[] args)
		{
			Console.WriteLine($"Warmup...");
			//Tests.Warmup();
			for (var i = 0; i < 1; i++)
			{
				CyclesPerSecond = GetCyclesPerSeond();
			}
			NsPerCycle = 1000 * 1000 * 1000.0 / CyclesPerSecond;
			Tests.TestDuration = TestDuration * CyclesPerSecond / 1000;
			LoopOverhead = Tests.TestOverhead(1000, 1000);

			Console.WriteLine($"CyclesPerSecond: {CyclesPerSecond,5:0} ");
			Console.WriteLine($"nsPerCycle:      {NsPerCycle,5:0.000} ");
			Console.WriteLine($"loopOverhead:    {LoopOverhead,4:0.00} Cycles");
			Console.WriteLine($"                 {LoopOverhead * NsPerCycle,4:0.00} ns ");
			Console.WriteLine($"Starting...");

			DeserializerTester.Run();

			Console.WriteLine("ready");
			Tests.Warmup();
		}

		private static ulong GetCyclesPerSeond()
		{
			var sw = Stopwatch.StartNew();
			var startms = Rdtsc.TimestampP();
			do
			{
			} while (sw.ElapsedMilliseconds < 1000);
			var endms = Rdtsc.TimestampP();

			return endms - startms;
		}
	}
}