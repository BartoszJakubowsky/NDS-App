using System;
using Newtonsoft.Json;

public class Save : JsonSerializerClass
{
    public Save(string dataName) 
    {
        this.dataName = dataName;


        JsonFile newJsonData;
        //
        //
        //

        void createJsonFile()
        {
            newJsonData = new JsonFile()
            {
                HistoryInput = new List<JsonCommandsHistory>()
            };
            JsonSerializer(newJsonData);
        }


        if (File.Exists($@"{CurrentDirectory}/{jsonData}.json") == false)
        {
            createJsonFile();
        }
        else
        {
            if (File.ReadAllText($@"{CurrentDirectory}/{jsonData}.json").Contains("HistoryInput") == false)
            {
                createJsonFile();
            }
            else
            {
                newJsonData = JsonDeserialized();
            }
        }

        this.jsonFile = newJsonData; 
    }

    public string dataName;
    public JsonFile jsonFile;

    


    public void SaveInput(params string[] data)
    {
        //get text saved as json


        void addInputToMemory(params string[] data)
        {

            jsonFile.HistoryInput.Add(new JsonCommandsHistory()
            {
                ActionName = dataName,
                TypedInput = new List<JsonInput>()
            });


            for (int i = 1; i < data.Length; i++)
            {
                jsonFile.HistoryInput.Last().TypedInput.Add(new JsonInput() { Input = data[i] });

            }
        }

        if (File.ReadAllText($@"{CurrentDirectory}/{jsonData}.json").Contains(dataName))
        {
            jsonFile = JsonDeserialized();
            addInputToMemory(data);

        } 
        else
        {
            addInputToMemory(data);

        }


        JsonSerializer(jsonFile);
    }


}




