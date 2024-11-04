using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStatus
{
    internal class HttpHelper
    {
        public string getjson(string url)
        {
           HttpWebRequest request=HttpWebRequest.CreateHttp(url);
            var resopnes=request.GetResponse();
            StreamReader stream=new StreamReader(resopnes.GetResponseStream()); 
            string json = stream.ReadToEnd();   
            return json;


        }
    }
}
