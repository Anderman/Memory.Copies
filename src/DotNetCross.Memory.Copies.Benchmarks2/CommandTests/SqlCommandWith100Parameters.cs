using Medella.Performance.Tester.Benchmark;
using System.Data.SqlClient;
using System.Linq;

namespace Medella.Performance.Tester.CommandTests
{
	public class SqlCommandWith100Parameters : BenchmarkRunner
	{
		private readonly SqlConnection _conn;
		private readonly SqlCommand _cmd;
		private readonly SqlParameter[] _parameters;
		private readonly string[] _columnNames;

		public SqlCommandWith100Parameters()
		{
			_conn = new SqlConnection("server=.;INTEGRATED SECURITY=SSPI;Database=loadpremium291");
			_conn.Open();
			_cmd = new SqlCommand("SELECT 1", _conn);
			_columnNames = Enumerable.Range(1, 100).Select(x => $"Column{x}").ToArray();
			_parameters = Enumerable.Range(1, 100).Select(x => new SqlParameter($"Column{x}", 0)).ToArray();
		}

		public override void Run()
		{
			_cmd.Parameters.Clear();
			for (int i = 0; i < _columnNames.Length; i++)
			{
				//_cmd.Parameters.AddWithValue(_columnNames[i], decimal.MaxValue);
				_parameters[i].Value = decimal.MaxValue;
				_cmd.Parameters.Add(_parameters[i]);
			}
		}

		public override void Finsch()
		{
			_conn.Close();
		}
	}
}
