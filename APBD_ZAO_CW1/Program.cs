using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
/**
 * 
 * Jakub Szymczuk s20289
 * */
namespace APBD_ZAO_CW1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string websiteUrl = args[0];
            if (websiteUrl == null)
            {
                throw new ArgumentNullException("Argument jest nullem");
            }

            using (HttpClient httpClient = new HttpClient()) //using uzyte zamiast metody dispose
            {
            
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(websiteUrl);
            string content = await httpResponseMessage.Content.ReadAsStringAsync();
            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
           
                MatchCollection matchCollection = regex.Matches(content);
                if (matchCollection.Count > 0)
            {

                    var uniqueMatches = matchCollection.OfType<Match>().Select(m => m.Value).Distinct();

                    uniqueMatches.ToList().ForEach(Console.WriteLine);

                }
            else
            {
                Console.WriteLine("Brak adresow email na stronie");

            }


            }

        }
    }
}
