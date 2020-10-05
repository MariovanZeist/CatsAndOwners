using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cats
{
	public class CatDataConverter : JsonConverter<CatData>
	{
		public override CatData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var catData = new CatData();

			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject)
				{
					//Now fixup all the references.
					foreach (var ownerCat in catData.OwnerCats)
					{
						ownerCat.Cat = catData.Cats.First(c => c.Id == ownerCat.CatId);
						ownerCat.Owner = catData.Owners.First(c => c.Id == ownerCat.OwnerId);
						ownerCat.Cat.OwnerCats.Add(ownerCat);
						ownerCat.Owner.OwnerCats.Add(ownerCat);
					}
					return catData;
				}

				// Get the property.
				if (reader.TokenType != JsonTokenType.PropertyName)
				{
					throw new JsonException();
				}

				switch (reader.GetString())
				{
					case nameof(CatData.Cats):
						catData.Cats = ReadArray<Cat>(ref reader, options);
						break;
					case nameof(CatData.Owners):
						catData.Owners = ReadArray<Owner>(ref reader, options);
						break;
					case nameof(CatData.OwnerCats):
						catData.OwnerCats = ReadArray<OwnerCat>(ref reader, options);
						break;
					default:
						break;
				}
			}
			throw new JsonException();
		}


		private IList<T> ReadArray<T>(ref Utf8JsonReader reader, JsonSerializerOptions options)
		{
			reader.Read();
			return JsonSerializer.Deserialize<List<T>>(ref reader, options);
		}

		public override void Write(Utf8JsonWriter writer, CatData value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName(nameof(CatData.Cats));
			JsonSerializer.Serialize(writer, value.Cats, options);
			writer.WritePropertyName(nameof(CatData.Owners));
			JsonSerializer.Serialize(writer, value.Owners, options);
			writer.WritePropertyName(nameof(CatData.OwnerCats));
			JsonSerializer.Serialize(writer, value.OwnerCats, options);
			writer.WriteEndObject();
		}
	}


	public class CatConverter : JsonConverter<Cat>
	{
		public override Cat Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var cat = new Cat();
			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject)
				{
					return cat;
				}

				// Get the key.
				if (reader.TokenType != JsonTokenType.PropertyName)
				{
					throw new JsonException();
				}

				switch (reader.GetString())
				{
					case nameof(Cat.Id):
						reader.Read();
						cat.Id = reader.GetInt32();
						break;
					case nameof(Cat.Name):
						reader.Read();
						cat.Name = reader.GetString();
						break;
					default:
						break;
				}
			}
			throw new JsonException();
		}

		public override void Write(Utf8JsonWriter writer, Cat value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WriteNumber(nameof(Cat.Id), value.Id);
			writer.WriteString(nameof(Cat.Name), value.Name);
			writer.WriteEndObject();
		}
	}

	public class OwnerConverter : JsonConverter<Owner>
	{
		public override Owner Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var owner = new Owner();
			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject)
				{
					return owner;
				}
				// Get the key.
				if (reader.TokenType != JsonTokenType.PropertyName)
				{
					throw new JsonException();
				}
				switch (reader.GetString())
				{
					case nameof(Owner.Id):
						reader.Read();
						owner.Id = reader.GetInt32();
						break;
					case nameof(Owner.Name):
						reader.Read();
						owner.Name = reader.GetString();
						break;
					default:
						break;
				}
			}
			throw new JsonException();
		}

		public override void Write(Utf8JsonWriter writer, Owner value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WriteNumber(nameof(Owner.Id), value.Id);
			writer.WriteString(nameof(Owner.Name), value.Name);
			writer.WriteEndObject();
		}
	}

	public class OwnerCatConverter : JsonConverter<OwnerCat>
	{
		public override OwnerCat Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var ownerCat = new OwnerCat();
			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject)
				{
					return ownerCat;
				}
				// Get the key.
				if (reader.TokenType != JsonTokenType.PropertyName)
				{
					throw new JsonException();
				}
				switch (reader.GetString())
				{
					case nameof(OwnerCat.CatId):
						reader.Read();
						ownerCat.CatId = reader.GetInt32();
						break;
					case nameof(OwnerCat.OwnerId):
						reader.Read();
						ownerCat.OwnerId = reader.GetInt32();
						break;
					default:
						break;
				}
			}
			throw new JsonException();
		}

		public override void Write(Utf8JsonWriter writer, OwnerCat value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WriteNumber(nameof(OwnerCat.CatId), value.CatId);
			writer.WriteNumber(nameof(OwnerCat.OwnerId), value.OwnerId);
			writer.WriteEndObject();
		}
	}
}
