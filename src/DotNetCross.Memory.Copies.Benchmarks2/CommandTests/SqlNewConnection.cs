using Medella.Performance.Tester.Benchmark;
using System.Data.SqlClient;
using System.Text;

namespace Medella.Performance.Tester.CommandTests
{
	public class SqlNewConnection : BenchmarkRunner
	{
		private readonly string _str;
		public StringBuilder Sb;

		public SqlNewConnection()
		{
			Sb = new StringBuilder(1000);
		}

		public override void Run()
		{
			using (var conn = new SqlConnection("server=.;INTEGRATED SECURITY=SSPI;Database=loadpremium291"))
			{
				//conn.Open();
				//using (var cmd = new SqlCommand("SELECT datarecs=MAX(\"iid\") FROM \"BaseEHR11_SMF_MedFacts_MedFacts_data\"", conn))
				//{
				//	cmd.CommandTimeout = int.MaxValue;
				//	//using (var r = cmd.ExecuteReader())
				//	//{
				//	//	while (r.Read())
				//	//	{
				//	//		var datarecs = r.GetInt32(0);
				//	//	}
				//	//}
				//}
				//conn.Close();
			}
		}
	}
}

