using System;
using Newtonsoft.Json;

public class JsonSerializerClass
{
	public JsonSerializerClass()
    {
	}
	
	//pc
	//public string CurrentDirectory = @"C:\Users\Nowe Jakubki\OneDrive\Pan_Programista\C#\# Projekty\Projekt NDS v2 - PC\NDS-App-v2\cos";
	//laptop
	public string CurrentDirectory = @"C:\Users\barto\OneDrive\Pan_Programista\C#\# Projekty\Projekt NDS v2\cos";

	public string jsonData = "savedHistory";

	public JsonFile JsonDeserialized()
    {
		//get text saved as json
		string readedJsonFile = File.ReadAllText($@"{CurrentDirectory}/{jsonData}.json");

		JsonFile deserializedJson;

		//convert json to object
		return deserializedJson = JsonConvert.DeserializeObject<JsonFile>(readedJsonFile);
	}

	public void JsonSerializer(JsonFile data)
    {

		//convert to json data
		string dataSerialized = JsonConvert.SerializeObject(data);

		jsonData = @$"{CurrentDirectory}/{jsonData}.json";
		//save file as json 
		File.WriteAllText(jsonData, dataSerialized);

    }


}
