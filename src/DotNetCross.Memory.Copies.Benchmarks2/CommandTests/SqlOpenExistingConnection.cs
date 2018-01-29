using Medella.Performance.Tester.Benchmark;
using System.Data.SqlClient;
using System.Text;

namespace Medella.Performance.Tester.CommandTests
{
	public class SqlOpenExistingConnection : BenchmarkRunner
	{
		public StringBuilder Sb;
		private readonly SqlConnection _conn;

		public SqlOpenExistingConnection()
		{
			_conn = new SqlConnection("server=.;INTEGRATED SECURITY=SSPI;Database=loadpremium291");
			_conn.Open();
			_conn.Close();
		}

		public override void Run()
		{
			_conn.Open();
			_conn.Close();
		}
	}
}