using Medella.Performance.Tester.Benchmark;
using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Medella.Performance.Tester.Deserializer
{
	public class ParseXmlSafe : BenchmarkRunner
	{
		private static string _varchar;
		private readonly string _sidedata;
		private readonly string _str;
		public bool StringEqual;

		public ParseXmlSafe(string str, string sidedata)
		{
			_str = str;
			_sidedata = sidedata;
			var sb = new StringBuilder();
			for (ushort i = 0; i < 65535; i++)
			{
				var z = Encoding.ASCII.GetBytes(new[] { (char)i })[0];
				sb.Append(z);
			}
			_varchar = sb.ToString();
		}

		public override void Run()
		{
			var pos = 0;
			StringEqual = ParseXml(_str, ref pos, _sidedata);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool ParseXml(string xml, ref int pos, string sideData)
		{
			var extra = 0;
			var stringEqual = true;
			var ampStart = 0;
			var indexSidedata = 0;
			for (var i = pos; i < xml.Length; i++)
			{
				var chr = xml[i];
				if (chr == '"')
				{
					pos = i;
					return stringEqual;
				}
				if (chr == '&')
				{
					ampStart = i + 1;
					continue;
				}
				if (ampStart > 0)
				{
					if (chr != ';') continue;
					var length = i - ampStart;
					extra = extra - length;
					chr = GetAmpChar(xml, ampStart, length);
					ampStart = 0;
				}
				if (stringEqual && (sideData == null || sideData.Length <= indexSidedata || chr != sideData[indexSidedata] && ToVarchar(chr) != sideData[indexSidedata]))
					stringEqual = false;
				indexSidedata++;
				if (chr == '\'') extra++;
			}
			return stringEqual;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static char GetAmpChar(string xml, int start, int length)
		{
			if (length == 2 && xml[start] == 'l' && xml[start + 1] == 't') return '<';
			if (length == 2 && xml[start] == 'g' && xml[start + 1] == 't') return '>';
			if (length == 3 && xml[start] == 'a' && xml[start + 1] == 'm' && xml[start + 2] == 'p') return '&';
			if (length == 4 && xml[start] == 'a' && xml[start + 1] == 'p' && xml[start + 2] == 'o' && xml[start + 3] == 's') return '\'';
			if (length == 4 && xml[start] == 'q' && xml[start + 1] == 'u' && xml[start + 2] == 'o' && xml[start + 3] == 't') return '"';
			return SpecialChar(xml, start, length);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static char SpecialChar(string xml, int start, int length)
		{
			if (xml[start] == '#' && xml[start + 1] == 'x')
				return (char)ParseHexInt(xml, start + 2, length - 2);
			if (xml[start] == '#')
			{
				var s = xml.Substring(start + 1, length - 1);
				return (char)ushort.Parse(s);
			}
			throw new Exception("Unknown code attribute");
		}

		public static char ToVarchar(char chr)
		{
			return _varchar[chr];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ParseHexInt(string buffer, int pos, int lenght)
		{
			var number = 0;
			for (var i = pos; i < pos + lenght; i++)
			{
				var chr = buffer[i];
				if (chr >= '0' || chr <= '9')
				{
					number = number * 16 + (chr - '0');
					continue;
				}
				if (chr >= 'A' && chr <= 'F')
				{
					number = number * 16 + (chr - 'A') + 10;
					continue;
				}
				number = number * 16 + (chr - 'a') + 10;
			}
			//Debugger.Launch();
			return number;
		}
	}
}