using Medella.Performance.Tester.Benchmark;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Medella.Performance.Tester.Deserializer
{
	public class ParseXmlClr : BenchmarkRunner
	{
		private readonly string _sidedata;
		public bool StringEqual;
		private readonly XmlElement _elem;

		public ParseXmlClr(string xml, string sidedata)
		{
			_sidedata = sidedata;
			XmlDocument xmlDocument = new XmlDocument();

			var reader = new StringReader(xml);
			xmlDocument.XmlResolver = null;
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
			{
				IgnoreComments = false,
				IgnoreWhitespace = true,
				DtdProcessing = DtdProcessing.Prohibit,
				ValidationFlags = XmlSchemaValidationFlags.None,
			};
			using (XmlReader reader1 = XmlReader.Create(reader, xmlReaderSettings))
			{
				xmlDocument.Load(reader1);
			}
			_elem = xmlDocument.DocumentElement;
		}

		public override void Run()
		{
			var attr = _elem.Attributes["Tags"].Value;
			StringEqual = attr == _sidedata;

		}
	}
}