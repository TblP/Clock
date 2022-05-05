using System;
using System.Collections;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class getreqst: MonoBehaviour
{
    public TimeSpan curtime;
    private const string t1 = "https://time100.ru/api.php/";
    private const string t2 = "https://yandex.ru/time/sync.json";
    // Start is called before the first frame update
    void Awake()
    {
        /*if (connectcheck(t1) == 404)
        {
            if (connectcheck(t2) == 404)
            {
                DateTime localtime = new DateTime();
                curtime = localtime.TimeOfDay;
            }
        }*/
        StartCoroutine(GetText(t1));
    }
    IEnumerator GetText(string web) {
        UnityWebRequest www = UnityWebRequest.Get(web);
        yield return www.Send();
        
        if(www.isNetworkError) {
            if (web == t2)
            {
                DateTime localtime = new DateTime();
                curtime = localtime.TimeOfDay;
                
            }
            else
            {
               StartCoroutine(GetText(t2));  
            }
            
        }
        else {
            string responseFromServer;
            if (web == t2)
            {
                JObject parsed = JObject.Parse(www.downloadHandler.text);
                
                responseFromServer = (string)parsed["time"];
                curtime = ctime(long.Parse(responseFromServer.Substring(0,10)));
                
            }

            if (web == t1)
            {
                curtime = ctime(long.Parse(www.downloadHandler.text));
                
                
            }
        }
        StartCoroutine(GetText(t1));
    }
    /*public int connectcheck(string webt)
    {   
        // Create a request for the URL.
        WebRequest request = WebRequest.Create(webt);
        // If required by the server, set the credentials.
        request.Credentials = CredentialCache.DefaultCredentials;
        // Get the response.
        WebResponse response;
        try
        {
            response = request.GetResponse();
        }
        catch(WebException)
        {
            return 404;
        }
        
        // Display the status.
        if (((HttpWebResponse)response).StatusCode != HttpStatusCode.OK)
        {
            return 404;
        }
        string responseFromServer;
        if (webt == t2)
        {
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
                JObject parsed = JObject.Parse(responseFromServer);
                // Display the content.
                responseFromServer = (string)parsed["time"];
            }
            curtime = ctime(long.Parse(responseFromServer.Substring(0,10)));
            return 1;
        }
        
        using (Stream dataStream = response.GetResponseStream())
        {
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            responseFromServer = reader.ReadToEnd();
            // Display the content.
        }
        
        curtime = ctime(long.Parse(responseFromServer));
            
        // Close the response.
        response.Close();
        return 1;
        
    }*/
    public TimeSpan ctime(long t)
    {
        return DateTimeOffset.FromUnixTimeSeconds(t).LocalDateTime.TimeOfDay;
    }
}
