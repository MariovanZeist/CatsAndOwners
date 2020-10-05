using System;
using System.Buffers;
using System.Linq;
using System.Text.Json;
using System.Xml.Serialization;

namespace Cats
{
	public static class ST
	{
		//S.T.Json

		static JsonSerializerOptions STSettings = new JsonSerializerOptions
		{
			WriteIndented = true,
		};

		static ST()
		{
//			STSettings.Converters.Add(new CatDataConverter());
		}


		public static string Serialize(CatData data)
		{
			return JsonSerializer.Serialize(data, STSettings);
		}

		public static CatData DeSerialize(string json)
		{
			var catData = JsonSerializer.Deserialize<CatData>(json, STSettings);
			Fixup(catData);
			return catData;
		}

		public static ReadOnlyMemory<byte> SerializeUtf8(CatData data)
		{
			var buffer = new ArrayBufferWriter<byte>();
			using var wr = new Utf8JsonWriter(buffer);
			JsonSerializer.Serialize(wr, data, STSettings);
			return buffer.WrittenMemory;
		}

		public static CatData DeSerializeUtf8(ReadOnlySpan<byte> utf8Json)
		{
			var catData = JsonSerializer.Deserialize<CatData>(utf8Json, STSettings);
			Fixup(catData);
			return catData;
		}


		static void Fixup(CatData catData)
		{
			foreach (var ownerCat in catData.OwnerCats)
			{
				ownerCat.Cat = catData.Cats.First(c => c.Id == ownerCat.CatId);
				ownerCat.Owner = catData.Owners.First(c => c.Id == ownerCat.OwnerId);
				ownerCat.Cat.OwnerCats.Add(ownerCat);
				ownerCat.Owner.OwnerCats.Add(ownerCat);
			}
		}
	}
}
