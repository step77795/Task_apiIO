using System.Net;

namespace Task_apiIO
{
    public class World_list_of_uni
    {
        public string link;
        
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
            World_list_of_uni WList_1 = new World_list_of_uni();
            WList_1.link = "http://universities.hipolabs.com/search?country=";

            Countries USA = new Countries();

            USA.Country_name = "United+States";
            
            Console.Write("Please enter country name: USA");
            string s = Console.ReadLine();

            switch (s)
            {
                case "usa": 
                    USA.GetList();
                    Console.WriteLine();
                    break;
                case "": 
                    Console.WriteLine();
                    break;
                case "1":
                    Console.WriteLine();
                    break;
                    default: 
                    Console.WriteLine();
                    break;
            }
        }
    }
}
