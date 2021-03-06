using Medella.Performance.Tester.Benchmark;
using System;
using System.Text;

namespace Medella.Performance.Tester.StringBuilderTests
{
	public class StringBuilderAppend : BenchmarkRunner
	{
		private string _str;
		public StringBuilder Sb;
		private string _tableName;
		private string[] _columnNames;
		private string _columnValues;

		public StringBuilderAppend()
		{
			Sb = new StringBuilder(10000000);
			_tableName = "sqltablename".Replace('s', 'S');
			_columnNames = new[] { "Columns1", "Column2" };
			_columnValues = "jkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhfjkfhakjlafdshkjdshfakjfdsakjhf";
		}

		public override void Run()
		{
			Sb.Clear();
			Sb.Append("UPDATE [");
			Sb.Append(_tableName);
			Sb.Append("_sidedata] SET ");
			for (int i = 0; i < _columnNames.Length; i++)
			{
				Sb.Append(_columnNames[i]);
				Sb.Append('=');
				Sb.Append(_columnValues, 10, 100);
				if (_columnValues.Length - 1 != i) Sb.Append(',');
			}
			Sb.Append(" WHERE id=");
			Sb.Append("231765");
			_str = Sb.ToString();
		}

		public override void Finsch()
		{
			Console.Write((int)Sb.ToString().Length);
		}

	}
}