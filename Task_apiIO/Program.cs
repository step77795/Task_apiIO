using Newtonsoft.Json.Linq;
using System.Net;


namespace Task_apiIO
{   //Родительский класс(ограничевается раздачей общей кастомной ссылки)
    public class World_list_of_uni
    {
        public string link= "http://universities.hipolabs.com/search?country=";
    }
    //Наследник
    public class Countries: World_list_of_uni
    {   //Задаёт необходимое название страны для запроса
        public string Country_name;
        //Создаёт итоговую ссылку для запроса
        private void Create_link()
        {
            link = link + Country_name;
        }
        //Автоматическое свойство. Служит "хабом" для результата выполнения парсинга.
        protected string Response { get; set; }
        //Метод создания запоса и получения ответа по нему
        protected void GetJson()
        {   
            Create_link();
            //fix12-> HttpWebRequest request;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link);
            //Попытка вытянуть данные по запросу
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream != null) Response = new StreamReader(stream).ReadToEnd();
            }
            catch (Exception) { Console.WriteLine("Something went wrong( API response = null"); }
        }
        //Автоматическое свойство. Служит "хабом" для списка университетов по странам.
        public string short_list { get; set; }
        //Метод обеспечивает выборку данных из ответа по запросу(метод GetJson)
        public void short_list_of_universities()
        {
            GetJson();

            /*Json файл приходит в формате 
            * Json
            *      0
            *          много всяких полей 
            *          в том числе и name <-нужно
            *      1  
            *          ...
            *     ...
            */
            //Обработка json
            var jarray = JArray.Parse(Response);
            for (int i=0; i< jarray.Count; i++)
            {
                var jarray_i = JObject.Parse(Convert.ToString(jarray[i]));
                var name = jarray_i["name"];
                Console.WriteLine(name);
            }
        }
    }  

    class Program
    {
        private static void Main(string[] args)
        {
            Countries USA = new Countries { Country_name = "United+States"};
            Countries TR = new Countries { Country_name = "Turkey" };

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
