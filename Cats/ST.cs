using System;
using System.Buffers;
using System.Text.Json;

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
			STSettings.Converters.Add(new CatDataConverter());
			STSettings.Converters.Add(new CatConverter());
			STSettings.Converters.Add(new OwnerConverter());
			STSettings.Converters.Add(new OwnerCatConverter());
		}


		public static string Serialize(CatData data)
		{
			return JsonSerializer.Serialize(data, STSettings);
		}

		public static CatData DeSerialize(string json)
		{
			return JsonSerializer.Deserialize<CatData>(json, STSettings);
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
			return JsonSerializer.Deserialize<CatData>(utf8Json, STSettings);
		}
	}
}
