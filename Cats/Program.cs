using BenchmarkDotNet.Running;
using System;
using System.IO;

namespace Cats
{
	class Program
	{
		public static readonly CatData Source;
		public static string NSJson;
		public static string STJson;
		public static ReadOnlyMemory<byte> STUtf8Json;

		static Program()
		{
			var c1 = new Cat { Id = 1, Name = "Cat1" };
			var c2 = new Cat { Id = 2, Name = "Cat2" };
			var c3 = new Cat { Id = 3, Name = "Cat3" };
			var c4 = new Cat { Id = 4, Name = "Cat4" };
			var c5 = new Cat { Id = 5, Name = "Cat5" };

			var o1 = new Owner { Id = 100, Name = "Owner1" };
			var o2 = new Owner { Id = 101, Name = "Owner2" };
			var o3 = new Owner { Id = 102, Name = "Owner3" };

			Source = new CatData
			{
				Cats = new[] { c1, c2, c3, c4, c5 },
				Owners = new[] { o1, o2, o3 },
				OwnerCats = new[] {
					OwnerCat.CreateFrom(c1, o1),
					OwnerCat.CreateFrom(c2, o1),
					OwnerCat.CreateFrom(c3, o1),
					OwnerCat.CreateFrom(c4, o1),
					OwnerCat.CreateFrom(c5, o1),
					OwnerCat.CreateFrom(c1, o2),
					OwnerCat.CreateFrom(c2, o2),
					OwnerCat.CreateFrom(c4, o2),
					OwnerCat.CreateFrom(c5, o2),
					OwnerCat.CreateFrom(c3, o3),
				}
			};

			NSJson = NS.Serialize(Source);
			STJson = ST.Serialize(Source);
			STUtf8Json = ST.SerializeUtf8(Source);
		}

		static void Main(string[] args)
		{
			//Run test
			Test();
			//Run Benchmark
			var summary = BenchmarkRunner.Run<BenchMarkJson>();
		}
	
		public static void Test()
		{
			var s1 = NS.Serialize(Source);
			WriteFile("D:\\NS.json", s1);
			var cd1 = NS.DeSerialize(s1);
			var s1t = NS.Serialize(cd1);
			Console.WriteLine($"NewtonSoft:{s1 == s1t}");

			var s2 = ST.Serialize(Source);
			WriteFile("D:\\ST.json", s2);
			var cd2 = ST.DeSerialize(s2);
			var s2t = ST.Serialize(cd2);
			Console.WriteLine($"System.Text:{s2 == s2t}");

			static void WriteFile(string filename, string json)
			{
				using var sw = new StreamWriter(filename);
				sw.Write(json);
			}
		}

	}
}
