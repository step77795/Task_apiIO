using System.Net;

namespace Task_apiIO
{
    public class World_list_of_uni
    {
        public string link;

        public void GetList()
        {
            
        }
    }

    public class Countries: World_list_of_uni
    {
        public string Country_name;

        private void Create_link()
        {
            link = link + Country_name;
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
