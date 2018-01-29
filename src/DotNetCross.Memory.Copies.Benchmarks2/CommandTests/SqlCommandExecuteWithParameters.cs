using System;
using Medella.Performance.Tester.Benchmark;
using System.Data.SqlClient;
using System.Linq;

namespace Medella.Performance.Tester.CommandTests
{
	public class SqlCommandExecuteWithParameters : BenchmarkRunner
	{
		private const int Count = 1000;
		private readonly SqlCommand _cmd;
		private readonly string[] _columnNames;
		private readonly SqlConnection _conn;
		private readonly SqlParameter[] _parameters;

		public SqlCommandExecuteWithParameters()
		{
			_conn = new SqlConnection("server=.;INTEGRATED SECURITY=SSPI;Database=loadpremium291");
			_conn.Open();
			var statement = string.Join("+", Enumerable.Range(1, Count).Select(x => $"@P{x}").ToArray());
			_cmd = new SqlCommand($"SELECT {statement}", _conn);
			_columnNames = Enumerable.Range(1, Count).Select(x => $"Column{x}").ToArray();
			_parameters = Enumerable.Range(1, Count).Select(x => new SqlParameter($"@P{x}", 0)).ToArray();
			Run();
			_conn.StatisticsEnabled = true;
		}

		public override void Run()
		{
			_conn.ResetStatistics();
			_cmd.Parameters.Clear();
			for (var i = 0; i < _columnNames.Length; i++)
			{
				_parameters[i].Value = long.MaxValue / Count;
				_cmd.Parameters.Add(_parameters[i]);
			}
			_cmd.ExecuteNonQuery();
		}

		public override void Finsch()
		{
			var z = _conn.RetrieveStatistics();
			Console.WriteLine($"Bytessent:{z["BytesSent"]} BytesReceived:{z["BytesReceived"]} UnpreparedExecs:{z["UnpreparedExecs"]}");
			_conn.Close();
		}
	}
}