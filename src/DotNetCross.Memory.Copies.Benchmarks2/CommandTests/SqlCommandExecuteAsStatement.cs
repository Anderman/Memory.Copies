using Medella.Performance.Tester.Benchmark;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace Medella.Performance.Tester.CommandTests
{
	public class SqlCommandExecuteAsStatement : BenchmarkRunner
	{
		private const int Count = 1000;
		private readonly SqlConnection _conn;
		private readonly SqlCommand _cmd;
		private readonly SqlParameter[] _parameters;
		private readonly string[] _columnNames;

		public SqlCommandExecuteAsStatement()
		{
			_conn = new SqlConnection("server=.;INTEGRATED SECURITY=SSPI;Database=loadpremium291");
			_conn.Open();
			var z = (long.MaxValue / Count).ToString();
			var statement = string.Join("+", Enumerable.Range(1, Count).Select(x => $"{x}").ToArray());
			_cmd = new SqlCommand($"SELECT {statement}", _conn);
			_columnNames = Enumerable.Range(1, Count).Select(x => $"Column{x}").ToArray();
			_parameters = Enumerable.Range(1, Count).Select(x => new SqlParameter($"@P{x}", 0)).ToArray();
			_cmd.ExecuteNonQuery();
			_conn.StatisticsEnabled = true;

		}

		public override void Run()
		{
			_conn.ResetStatistics();
			_cmd.ExecuteNonQuery();
		}

		public override void Finsch()
		{
			var z = _conn.RetrieveStatistics();
			Console.WriteLine($"Bytessent:{z["BytesSent"]} BytesReceived:{z["BytesReceived"]} UnpreparedExecs:{z["UnpreparedExecs"]}");
			_conn.Close();
		}
	}
	public class SqlCommandUpdateStatement : BenchmarkRunner
	{
		private const int Count = 100000;
		private readonly SqlConnection _conn;
		private readonly SqlCommand _cmd;
		private readonly SqlParameter[] _parameters;
		private readonly string[] _columnNames;

		public SqlCommandUpdateStatement()
		{
			_conn = new SqlConnection("server=.;INTEGRATED SECURITY=SSPI;Database=loadpremium291");
			_conn.Open();
			var z = (long.MaxValue / Count).ToString();
			var a = "UPDATE [BaseEHR11_ViewAccessAudit_sidedata] SET [IsPatientAuthor]=1 WHERE iid=";

			var statement = string.Join("\r\n", Enumerable.Range(1, Count).Select((x) => $"{a}{x}").ToArray());
			_cmd = new SqlCommand($"SET NOCOUNT ON;BEGIN TRANSACTION;{statement};COMMIT;", _conn);
			_cmd.CommandTimeout = int.MaxValue;
			_columnNames = Enumerable.Range(1, Count).Select(x => $"Column{x}").ToArray();
			_parameters = Enumerable.Range(1, Count).Select(x => new SqlParameter($"@P{x}", 0)).ToArray();
			_cmd.ExecuteNonQuery();
			//_conn.StatisticsEnabled = true;

		}

		public override void Run()
		{
			_conn.ResetStatistics();
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