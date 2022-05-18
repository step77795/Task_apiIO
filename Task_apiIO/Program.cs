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
        public string Response { get; set; }

        HttpWebRequest request;
        public void GetList()
        {
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

    }  


    class Program
    {
        private static void Main(string[] args)
        {
            Countries USA = new Countries();
            USA.Country_name = "United+States";
            Countries TR = new Countries();
            TR.Country_name = "Turkey";

            Console.Write("Please enter country name: ");
            string s = Console.ReadLine();

            switch (s)
            {
                case "usa": 
                    USA.GetList();
                    Console.WriteLine(USA.Response);
                    //var response = .Response;
                    break;
                case "Turkey": 
                    TR.GetList();
                    Console.WriteLine(TR.Response);
                    break;
                case "":
                    Console.WriteLine();
                    break;
                    default: 
                    Console.WriteLine();
                    break;
            }
        }
    }
}
