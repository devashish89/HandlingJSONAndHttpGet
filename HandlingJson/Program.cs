using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.IO;
using System.Security.Permissions;
using System.Security.Policy;

namespace HandlingJson
{
    public class Fruits
    {
        public string fruit { get; set; }
        public string size { get; set; }
        public string color { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            JavaScriptSerializer obj1 = new JavaScriptSerializer();
            var Fruits = (Fruits) obj1.Deserialize("{\"fruit\":\"Apple\",\"size\":\"Large\",\"color\":\"Red\"}", typeof(Fruits));

            Console.WriteLine(Fruits.color+"--"+Fruits.size+"--"+Fruits.fruit);

            string authOutput = GetRequest("http://bbdterminal.com/api/demoapi?e10adc3949ba59abbe56e057f20f883e&json=1");

            string key = string.Empty;
            foreach (string line in authOutput.Split(Environment.NewLine.ToCharArray()))
            {
                //Console.WriteLine(line);
                if (line.StartsWith("key:"))
                {
                    Console.WriteLine(new string('*', 50));
                    Console.WriteLine("Key -->" + line.Substring(4));
                    key = line.Substring(4);
                }
            }

            Console.WriteLine(new string('^', 50));

            //NSE data 
            string nseOutput = GetRequest("http://bbdterminal.com/api/"+key+"/scripmaster?nse");
            //Console.WriteLine(nseOutput);
            foreach (string line in nseOutput.Split(Environment.NewLine.ToCharArray()))
            {
                Console.WriteLine(line);
            }


        }

        static string GetRequest(string URL)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            WebResponse response =  request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());

            String json_text = sr.ReadToEnd();
            //Console.WriteLine(json_text);

            return json_text;

            //can not be json - validated in Postman also ---

            //dynamic stuff = JsonConvert.DeserializeObject(json_text); 

            //JavaScriptSerializer jsonObj = new JavaScriptSerializer();

            //var GetAuthClass = (GetAuthClass) jsonObj.Deserialize(stuff);
            //Console.WriteLine(GetAuthClass.key);
        }
        /*
        public class GetAuthClass
        {
            public string key { get; set; }
            public DateTime Time { get; set; }
            public string Your_IP { get; set; }
            public string OrdTypes { get; set; }
            public string kNSEFO_Validate_Lotsey { get; set; }
            public string MCX_Validate_Lots { get; set; }
            public string defScrips { get; set; }
            public string notices { get; set; }

        }
        */
    }


}
