using Newtonsoft.Json.Linq;
using System.Net;


namespace Task_apiIO
{

    //Родительский класс(ограничевается раздачей общей кастомной ссылки)
    public class World_list_of_uni
    {
        public string link= "http://universities.hipolabs.com/search?country=";
    }
    //Наследник
    public class Countries: World_list_of_uni
    {
        //Автоматическое свойство. Служит "хабом" для результата выполнения парсинга.
        public string Response { get; set; }
        //Задаёт необходимое название страны для запроса
        public string country_name;
        
        //Метод создания запоса и получения ответа по нему
        private void GetJson()
        {
            //Создаёт итоговую ссылку для запроса
            link = link + country_name;
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

        //Метод обеспечивает выборку данных из ответа по запросу(метод GetJson)
        public void Short_list_of_universities()
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
            //Обработка json и вывод name
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
            Countries USA = new Countries { country_name = "United+States"};
            Countries TR = new Countries { country_name = "Turkey" };

            Console.Write("Please enter country name( USA or Turkey )  : ");
            string str =  Console.ReadLine();
            if (str != null) 
            {
                str = str.ToLower();
                switch (str)
                {
                    case "usa":
                        USA.Short_list_of_universities();
                        break;
                    case "turkey":
                        TR.Short_list_of_universities();
                        break;
                    default:
                        Console.WriteLine("Unfortunately, we have not found such a country! Try again)");
                        break;
                }
            }
        }
    }
}
