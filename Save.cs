using System;
using Newtonsoft.Json;

public class Save : JsonSerializer
{
	public Save(string dataName) : base()
	{
		this.dataName = dataName;
		this.jsonFile = 

	}


	public string dataName;
	public JsonSerializer jsonFile;

	public void SaveInit(params string[] data)
    {

    }



}
