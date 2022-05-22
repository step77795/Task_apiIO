
namespace Task_apiIO
{
    
    class Program
    {
        enum NOfC
        {
            USA,
            Turkey,
            Germany
        }
        private static void Main(string[] args)
        {
            //Countries USA = new Countries { country_name = "United+States"};
            //Countries TR = new Countries { country_name = "Turkey" };

            Console.Write("Please enter country name( USA or Turkey )  : ");
            string str =  Console.ReadLine();
            NOfC nOfC = (NOfC)Enum.Parse(typeof(NOfC), str, ignoreCase: true);
            if (str != null) 
            {
                str = str.ToLower();

                switch (nOfC)
                {
                    case NOfC.USA:
                        Countries USA = new Countries { country_name = "United+States" };
                        USA.Short_list_of_universities();
                        break;
                    case NOfC.Turkey:
                        Countries TR = new Countries { country_name = "Turkey" };
                        TR.Short_list_of_universities();
                        break;
                    case NOfC.Germany:
                        Countries Germany = new Countries { country_name = "Germany" };
                        Germany.Short_list_of_universities();
                        break;
                    default:
                        Console.WriteLine("Unfortunately, we have not found such a country! Try again)");
                        break;
                }
            }
        }
    }
}
