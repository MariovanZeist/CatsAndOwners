using BenchmarkDotNet.Attributes;
using System;

namespace Cats
{
	public class BenchMarkJson
	{

		[Benchmark]
		public CatData DeSerializeNewtonSoft()
		{
			return NS.DeSerialize(Program.NSJson);
		}

		[Benchmark]
		public CatData DeSerializeSystemText()
		{
			return ST.DeSerialize(Program.STJson);
		}

		[Benchmark]
		public CatData DeSerializeSystemTextUtf8()
		{
			return ST.DeSerializeUtf8(Program.STUtf8Json.Span);
		}

		[Benchmark]
		public string SerializeNewtonSoft()
		{
			return NS.Serialize(Program.Source);
		}

		[Benchmark]
		public string SerializeSystemText()
		{
			return ST.Serialize(Program.Source);
		}

		[Benchmark]
		public ReadOnlyMemory<byte> SerializeSystemTextUtf8()
		{
			return ST.SerializeUtf8(Program.Source);
		}

	}
}
