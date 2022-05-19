using Newtonsoft.Json.Linq;
using System.Net;



namespace Task_apiIO
{
    public class World_list_of_uni
    {
        public string link= "http://universities.hipolabs.com/search?country=";
    }

    public class Countries: World_list_of_uni
    {
        public string Country_name;

        private void Create_link()
        {
            link = link + Country_name;
        }
        protected string Response { get; set; }

        protected void GetJson()
        {   
            HttpWebRequest request;
            Create_link();
            request = (HttpWebRequest)WebRequest.Create(link);
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream != null) Response = new StreamReader(stream).ReadToEnd();
            }
            catch (Exception) { }
        }
        public string short_list { get; set; }
        public void short_list_of_universities()
        {
            GetJson();
            var json = JArray.Parse(Response);
            string str2 = string.Empty;
            for (int i =0; i < json.Count; i++)
            { 
                char[] a = { '\n' };
                string str = Convert.ToString(json[i]);
                string[] Str = str.Split(a, StringSplitOptions.RemoveEmptyEntries);

                for (int j=0; j<Str.Length; j++)
                {
                    if (Str[j].Contains("  \"name\": "))
                    {
                        char[] b = { '"' };
                        string[] Str1 = Str[j].Split(b, StringSplitOptions.RemoveEmptyEntries);
                        short_list = short_list + Convert.ToString(Str1[3]) + "\n";
                    }
                } 
            }
        }
    }  

    class Program
    {
        private static void Main(string[] args)
        {
            Countries USA = new Countries();
            USA.Country_name = "United+States";

            Countries TR = new Countries();
            TR.Country_name = "Turkey";

            Console.Write("Please enter country name( USA or Turkey )  : ");
            string s = (Console.ReadLine()).ToLower();

            switch (s)
            {
                case "usa": 
                    USA.short_list_of_universities();
                    Console.WriteLine(USA.short_list);
                    break;
                case "turkey":
                    TR.short_list_of_universities();
                    Console.WriteLine(TR.short_list);
                    break;
                    default: 
                    Console.WriteLine("Unfortunately, we have not found such a country! Try again)");
                    break;
            }
        }
    }
}
