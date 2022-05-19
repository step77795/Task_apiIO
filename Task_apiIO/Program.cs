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
            HttpWebRequest request;            
            request = (HttpWebRequest)WebRequest.Create(link);
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

            /*Разбиваю блоками. в json будет храниться "массив" в каждом аргументе, которого
            *храниться (много всяких полей в том числе и name(ИСКОМОЕ) см.выше
            */
            var json = JArray.Parse(Response);

            //Грубо разбиваю блоками. в Str будет храниться массив полей, в том числе и name(ИСКОМОЕ)
            for (int i =0; i < json.Count; i++)
            { 
                char[] a = { '\n' };
                string str = Convert.ToString(json[i]);
                string[] Str = str.Split(a, StringSplitOptions.RemoveEmptyEntries);
                //Нахожу поле name и убираю шелуху 
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
