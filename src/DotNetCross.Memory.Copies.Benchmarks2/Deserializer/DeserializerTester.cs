using Medella.Performance.Tester.Benchmark;

namespace Medella.Performance.Tester.Deserializer
{
	public class DeserializerTester

	{
		private static string xml2 = "<SMF.MedFacts.MedFacts iid=\"20725\" Identifier=\"f1173adf-f92f-4356-a63c-d02eb4089757\" Author=\"123\" AuthorName=\"Jen Jensen007\" DateTimeContact=\"2016/10/11 14:21:38\" DateTimeCreation=\"2016/10/11 14:21:38\" DateTimeModification=\"2016/10/11 12:24:23\" FrontendViewName=\"ProceduresObjectView\" Patient=\"13\" PerformerNames=\"Jen Jensen007\" Performers=\"123\" Session=\"8d9b99ce-c336-4120-9c35-a416c15167d6\" Status01=\"Unsigned\" Status02=\"0\" Root=\"000000\" SourceBackendTypeName=\"Procedures\" SourceBackendTypeIID=\"195\" IsRoot=\"1\" MainMedFactIdentifier=\"f1173adf-f92f-4356-a63c-d02eb4089757\" RecordType=\"MedFact\" DateTimeExpiry=\"9999/12/31 23:59:59\" RuntimeVersion=\"SolutionA-1.0-SolutionA 1.0\" SourceType=\"CForm\" SourceName=\"Procedures\" SourceFieldNames=\"CodeCS&#x2551;Code&#x2551;CodeDesc\" SourceFieldValues=\"&lt;Root CodeCS=&quot;CPT-4 &#xAE;&quot; Code=&quot;00211&quot; CodeDesc=&quot;Anes Intracranial Craniotomy/Craniectomy Hmtma&quot; /&gt;\" SourceFieldSetId=\"1\" Archetype=\"ProcedureCode\" MetaDescription=\"Procedure Code\" CFormArchetype=\"ProcedureCode\" SOAPCode=\"NotApplicable\" ArchetypeRevision=\"2501\" SourceRevision=\"2623\" Tags=\"Co&#xFFFD;Procedures\" CodeSystem=\"CPT-4 &#xAE;\" Code=\"00211\" CodeDescription=\"Anes Intracranial Craniotomy/Craniectomy Hmtma\" IsNumeric=\"0\" Gender=\"Male\" Age=\"P0060Y09M03DT14H24M23\" SourceData=\"8QQAAB+LCAAAAAAABACNU9tS2zAQ/RWN3kUsX3JhMDOJoW1KgTAJ8CxLciKIJVdWEsJH9SP6ZV3lTiAzfbL27OronN31xcAaLsXMyhopJVJMOwlGA+aU1A6iCKPMCJkNU5wNRiRGf/+skRQHQUjpOriSNU9xVwNJXzvLuGVasSnK/Nc4Uy4bq6Pk/ox+lK5kGPlbVlVOGZ3inY5D2Nc4pqb1x/wKwujOOHmYuDMAYNQXIF0VStoUF3HOkmYrJkErTEichxFh7aJFiiBnMmCtME84RkNZ1ysRUUhj794CwzEobWFs6UlpCF3pztzEbIMr5uRIlTKzkq3thAFtNmjQoBTR+Dyk51H7oMxox7g7UXW/0EM11kqPh465GXgPwFQNgbVSrKKBlZWpZlPgWwPfLFBKLZ6UXNyx8rAt9X3+An33GYyejX0tpmbx/bF/leJ93OXuCygzZTWV2zdGy0oec2PUY/wVHv46uTX80WnondIOOLVjWIr3Tctgl458P+oaACn2RLdG+MmKE4S/WO22Jb3lZjibQR6uRVt08k6HS8KjqEliGgakw6OEsJg2OU1osyWaYFiW1efG7sVcv1XKLk9ImUtb7zaB0IBQOtpmz6OEnsVxK1hxMeC8vOD+nTdHlC4M4n6PpCA5sL+8SF1LHQQtvMOPN2z36rbAgTzybjTofT5D1zNrKomgrVowK5AXj1HN5lBZWFN6U/uIcBg7XPX/P0blpp2ftOwSJ8XsKv5HTePyQkEHxla5JZqwekLYdGwgmoC8esLCBCbi8RTbh5t+PM+XcUIXVfzQLN/vb7pMZddPxbyX/QzFuKcmN78n3XE7XRE39jt5+Q/brhFs8QQAAA==\" MedFactNr=\"20725\" RecordStatus=\"Active\" version=\"2016-10-11T12:24:23:351.2491\"><integrity hash-algorithm=\"sha256\" hash=\"ehwjn8OaYncBdaTt4f0q8VFq2F8=\" /><context-info created-by=\"jjensen007\" created-on=\"2016/10/11 12:24:23\" created-time-zone=\"W. Europe Standard Time\" saved-from=\"StartCreateMedFactsJobWorker\" saved-from-component=\"rule\" modified-by=\"jjensen007\" modified-on=\"2016/10/11 12:24:23\" modified-time-zone=\"W. Europe Standard Time\" /></SMF.MedFacts.MedFacts>"
			;
		public static void Run()
		{
			//var numberstr = decimal.MaxValue.ToString();
			var decStr = "-49823174320.9293800";
			var intStr = int.MinValue.ToString();
			var intHexStr = ushort.MaxValue.ToString("X");
			//var intStr = "32";
			var xml = "Co&#xFFFD;Procedures";
			var sidedata = "Co3Procedures";
			new LoopOverhead().Start();
			new ParseDecimalClr(decStr).Start();
			new ParseDecimalSafe(decStr).Start();

			new ParseIntClr(intStr).Start();
			new ParseIntSafe(intStr).Start();

			new ParseIntHexClr(intHexStr).Start();
			new ParseIntHexSafe(intHexStr).Start();

			new ParseXmlClr(xml2, "Co�Procedures").Start();
			new ParseXmlSafe(xml, sidedata).Start();

			new ParseBoolClr("_".Replace('_', '0')).Start();
			new ParseBoolSafe("false").Start();
		}
	}
}