using System;

public class JsonFile
{
    public List<JsonCommandsHistory> HistoryInput { get; set; }
}

public class JsonCommandsHistory
{
    public string ActionName { get; set; }
    public List<JsonInput> TypedInput { get; set; }

}

public class JsonInput
{
    public string Input { get; set; }
}


