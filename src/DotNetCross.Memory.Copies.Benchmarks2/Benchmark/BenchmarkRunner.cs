using Medella.Performance.Tester.Timer;
using System;

namespace Medella.Performance.Tester.Benchmark
{
	public abstract class BenchmarkRunner
	{
		public static ulong TestDuration = 100000000;
		public double Start()
		{
			var mincycles = ulong.MaxValue;
			var startTest = Rdtsc.TimestampP();
			var minIterations = GetMinIterations();
			var testCycles = 0UL;
			do
			{
				var start = Rdtsc.TimestampP();
				for (var j = 0; j < minIterations; j++)
				{
					Run();
				}
				var end = Rdtsc.TimestampP();
				var cycles = end - start;
				if (cycles <= mincycles)
				{
					mincycles = cycles;
				}
				testCycles = Rdtsc.TimestampP() - startTest;
			} while (testCycles < TestDuration);
			double CyclesPerIteration = mincycles / (double)minIterations;
			double CyclesPerMSecond = Program.CyclesPerSecond / 1000.0;
			if (CyclesPerIteration > CyclesPerMSecond)
				Console.Write($":{CyclesPerIteration / CyclesPerMSecond,6:0.0} ms {CyclesPerIteration} Cycles");
			else
				Console.Write($":{CyclesPerIteration,6:0} Cycles");
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Finsch();
			Console.ResetColor();
			Console.WriteLine();
			return mincycles / (double)minIterations;
		}

		public abstract void Run();
		public virtual void Finsch() { }

		public int GetMinIterations()
		{
			var i = 0;
			var minIteration = 1000;
			var mincycles = ulong.MaxValue;
			var start = Rdtsc.TimestampP();
			for (var j = 0; j < minIteration; j++)
			{
				i++;
			}
			var end = Rdtsc.TimestampP();
			var cycles = end - start;
			if (cycles <= mincycles)
			{
				mincycles = cycles;
			}
			var cycleOverhead = mincycles / (double)minIteration;
			//compile portalble code to asm
			start = Rdtsc.TimestampP();
			Run();
			end = Rdtsc.TimestampP();
			var cyclesCode = 0UL;
			if (end - start > cycleOverhead * 1000000)
			{
				Console.Write($"{this.GetType().Name.PadRight(35)} ");
				return 1;
			}
			start = Rdtsc.TimestampP();
			for (var j = 0; j < 10; j++)
			{
				Run();
			}
			end = Rdtsc.TimestampP();
			cyclesCode = (end - start) / 10;
			int iteration = (int)(1000 * cycleOverhead / cyclesCode);
			iteration = iteration < 10 ? 10 : iteration;

			Console.Write($"{this.GetType().Name.PadRight(35)} ");
			//Console.Write($"Iteration:{iteration,3:0}: Overhead:{cycleOverhead,5:0.000} Cycles FirstGuess:{cyclesCode,6:0.0} Cycles: ");


			return iteration;
		}
	}
}