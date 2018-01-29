using Medella.Performance.Tester.Benchmark;
using System.Data.SqlClient;
using System.Text;

namespace Medella.Performance.Tester.CommandTests
{
	public class SqlNewCommand : BenchmarkRunner
	{
		private readonly string _str;
		public StringBuilder Sb;
		private SqlConnection _conn;

		public SqlNewCommand()
		{
			_conn = new SqlConnection("server=.;INTEGRATED SECURITY=SSPI;Database=loadpremium291");
			_conn.Open();
		}

		public override void Run()
		{
			using (var cmd = new SqlCommand("SELECT 1", _conn))
				;

		}

		public override void Finsch()
		{
			_conn.Close();
		}
	}
}