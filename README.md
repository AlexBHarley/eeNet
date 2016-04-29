## eeNet 
![Build Status](https://ci.appveyor.com/api/projects/status/github/alexbharley/eeNet?branch=master&svg=true)

#### Node.js style events in C\# 

```C#
//API Overview

EventEmitter ee;

ee.On("eventName", Method);
ee.Emit("eventName", "some_data");
ee.EmitAsync("eventName", new List<string> {"data1", "data2"});
ee.RemoveListener("eventName", Method);
ee.RemoveAllListeners("eventName");
```

```C#
//Example

class Client
{
    private EventEmitter _ee;

    public Client()
    {
        this._ee = new EventEmitter();
    }

    public void PushDataToServer(string data)
    {
        this._ee.On("data_received", LogData);
        this._ee.On("data_received", SendToBus);
    }
    
    public void ReceiveData(List<string> dataList)
    {
        this._ee.Emit("data_received", dataList);
        this._ee.RemoveListener("data_received", LogData);
    }

    public void LogData(object data)
    {
        //Log and store data
    }

    public void SendToBus(object data)
    {
        //Send data somewhere interesting
    }
}
```


