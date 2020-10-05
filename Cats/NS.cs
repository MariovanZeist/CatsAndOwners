using Newtonsoft.Json;

namespace Cats
{
	public static class NS
	{
		// NewtonSoft

		static JsonSerializerSettings NSSettings = new JsonSerializerSettings
		{
			PreserveReferencesHandling = PreserveReferencesHandling.All,
			Formatting = Formatting.Indented
		};

		public static string Serialize(CatData data)
		{
			return JsonConvert.SerializeObject(data, NSSettings);
		}

		public static CatData DeSerialize(string json)
		{
			return JsonConvert.DeserializeObject<CatData>(json, NSSettings);
		}
	}
}
