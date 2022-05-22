using System.Net;
using Newtonsoft.Json.Linq;

namespace Task_apiIO
{
    public class Countries
    {
        private const string link = "http://universities.hipolabs.com/search?country=";
        //Автоматическое свойство. Служит "хабом" для результата выполнения парсинга.
        private string Response { get; set; }
        //Задаёт необходимое название страны для запроса
        public string country_name;

        //Метод создания запоса и получения ответа по нему
        private void GetJson()
        {
            //Создаёт итоговую ссылку для запроса
            //link = link + country_name;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link + country_name);
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
            for (int i = 0; i < jarray.Count; i++)
            {
                var jarray_i = JObject.Parse(Convert.ToString(jarray[i]));
                var name = jarray_i["name"];
                Console.WriteLine(name);
            }
        }
    }

}
