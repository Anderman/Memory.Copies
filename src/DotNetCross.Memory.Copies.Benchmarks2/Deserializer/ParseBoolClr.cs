using Medella.Performance.Tester.Benchmark;

namespace Medella.Performance.Tester.Deserializer
{
	public class ParseBoolClr : BenchmarkRunner
	{
		private readonly string _str;
		public bool Result;

		public ParseBoolClr(string str)
		{
			_str = "dummy" + str;
		}

		public override void Run()
		{
			var pos = "dummy".Length;
			Result = ParseBool(_str, ref pos, _str.Length - pos);
		}

		public bool ParseBool(string value, ref int pos, int length)
		{
			var str = _str.Substring(5, length);
			var z = (char)(str[0]);
			if (string.IsNullOrEmpty(str)) return false;
			str = str.ToLower();
			if (str == "true" || str == "yes" || str == "1") return true;
			if (str == "false" || str == "no" || str == "0") return false;
			if (int.TryParse(str, out var i))
				return i > 0;
			return false;
		}
	}
}