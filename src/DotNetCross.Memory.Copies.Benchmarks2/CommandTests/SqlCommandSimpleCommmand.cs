using System.Data.SqlClient;
using System.Linq;
using Medella.Performance.Tester.Benchmark;

namespace Medella.Performance.Tester.CommandTests
{
	public class SqlCommandSimpleCommmand : BenchmarkRunner
	{
		private readonly SqlConnection _conn;
		private readonly SqlCommand _cmd;
		private readonly SqlParameter[] _parameters;
		private readonly string[] _columnNames;

		public SqlCommandSimpleCommmand()
		{
			_conn = new SqlConnection("server=.;INTEGRATED SECURITY=SSPI;Database=loadpremium291");
			_conn.Open();
			var statement = string.Join("+", Enumerable.Range(1, 100).Select(x => $"@P{x}").ToArray());
			_cmd = new SqlCommand($"SELECT 1", _conn);
			_columnNames = Enumerable.Range(1, 100).Select(x => $"Column{x}").ToArray();
			_parameters = Enumerable.Range(1, 100).Select(x => new SqlParameter($"@P{x}", 0)).ToArray();
			Run();
		}

		public override void Run()
		{
			for (int j = 0; j < 2; j++)
			{
				_cmd.ExecuteNonQuery();
			}

		}
	}
}