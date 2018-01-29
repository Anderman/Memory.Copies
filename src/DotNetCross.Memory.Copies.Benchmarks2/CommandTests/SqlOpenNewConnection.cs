using System.Data.SqlClient;
using System.Text;
using Medella.Performance.Tester.Benchmark;

namespace Medella.Performance.Tester.CommandTests
{
	public class SqlOpenNewConnection : BenchmarkRunner
	{
		private readonly string _str;
		public StringBuilder Sb;
		private SqlConnection _conn;

		public SqlOpenNewConnection()
		{
		}

		public override void Run()
		{
			_conn = new SqlConnection("server=.;INTEGRATED SECURITY=SSPI;Database=loadpremium291");
			_conn.Open();
			_conn.Close();
		}
	}
}