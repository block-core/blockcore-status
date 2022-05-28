using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace blockcore.status.Common.WebToolkit;
public class JsonToObjects<T>
{
    public T ConverToObject(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }

    public async Task<T> DownloadAndConverToObjectAsync(string Url)
    {
        using (var httpClient = new HttpClient())
        {
            Uri uri = new Uri(Url);
            var json = await httpClient.GetStringAsync(uri);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
