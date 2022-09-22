using System;
using Newtonsoft.Json;

public class JsonSerializer
{
	public JsonSerializer(string jsonData = "jsonData")
	{
		this.jsonData = jsonData;
		if (File.Exists($"{Directory.GetCurrentDirectory()}/{jsonData}") == false)
        {
			File.Create($"{Directory.GetCurrentDirectory()}/{jsonData}.json");
        }
	}

	public string jsonData;

	public JsonSerializer JsonDeserialized(string jsonData)
    {

		stirng 

		JsonSerializer deserializedJson;
		return deserializedJson = JsonConvert.DeserializeObject<JsonSerializer>(jsonData);
	}

	



}
