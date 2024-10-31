using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using System.Net;
using System.Text.Json;
using System.Data;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Controls;
using Windows.Globalization;
using Windows.Networking.ServiceDiscovery.Dnssd;





namespace Stormwind
{
    public partial class MainPage : ContentPage
    {


/*********************
 * Klasa Currency 
 * przetrzymuje informacje o walucie
 * np jej kod, nazwę i tworzy liste podklasy Rate
 * parametry wejściowe : w tym przypadku pobiera informacje z bazy danych NBP o walucie
 * parametry wyjściowe : wszystkie informacje o walucie 
 * autor : klasa Xaml
 * 
 * 
 ********************/
        public class Currency
        {
            public string? table { get; set; }
            public string? currency { get; set; }
            public string? code { get; set; }
            public IList<Rate> rates { get; set; }
        }
        /**************
         * Klasa Rate
         * przetrzymuje informacje o walucie
         * np jej cene sprzedarzy i zakupu
         * parametry wejściowe : w tym przypadku pobiera informacje z bazy danych NBP o walucie
         * parametry wyjściowe : cena sprzedarzy i zakupu waluty
         * autor : klasa XAML
         * 
         * 
        ***************/
        public class Rate
        {
            public string? no { get; set; }
            public string? effectiveDate { get; set; }
            public double? bid { get; set; }
            public double? ask { get; set; }

        }
        /**********
         * Main Page i initializeComponent 
         * są klasami xaml bez których 
         * program by nie działał
         ************/
        public MainPage()
        {
            InitializeComponent();
            
            /*
             * dpData jest to nazwa kalendarza który umożliwia 
             * wybranie daty przez użytkownika  
             * poprzez to może wybrać dzień z którego będą podane mu informacje na temat waluty
             * Maksymalny dzień do wybrania jest dzisiaj bo nie potrafimy podróżować w czasie 
             */
            DateTime dzis = DateTime.Now;
            dpData.MaximumDate = dzis;

        }
        /*
         * Bcurrency1  
         * Funkcja wywoływana przez kliknięcie przycisku 
         * jest to funkcja która kompiluje w sobie całe działanie programu 
         * pobiera date, wybraną waluę 
         * wyłuskuje z bazy danych nbp wszystkie informacje na jej temat
         * które następnie przekazuje do stringa który następnie jest przesyłany 
         * do jednego z elementów odpowiadających za wyświetlenie tych danych 
         * ponadto porównuję cene skupu z poprzedniego dnia
         * i wyświetla odpowiadający im obraz 
         * jeżeli cena jest większa wyświatlana jest strzała w górę
         * jeżeli cena jest mniejsza wyświatlana jest strzała w dół
         */
        private void Bcurrency1(object sender, EventArgs e)
        {
            string data = dpData.Date.ToString("yyyy-MM-dd");
            int index = wybierzWalute.SelectedIndex;
            string cur1 = "";
            if (index != -1)
            {
                cur1 = (string)wybierzWalute.ItemsSource[index];
            }

            string json;
            string url1 = "https://api.nbp.pl/api/exchangerates/rates/c/" + cur1 + "/" + data + "/?format=json";


            using (var webClient = new WebClient())
            {
                json = webClient.DownloadString(url1);
            }

            Currency c = JsonSerializer.Deserialize<Currency>(json);
            string s = $"nazwa waluty : {c.currency}\n";
            s += $"kod waluty {c.code} \n";
            s += $"Data : {c.rates[0].effectiveDate} \n";
            s += $"Cena skupu : {c.rates[0].bid} \n";
            s += $"Cena sprzedazy : {c.rates[0].ask} \n ";
            textCurrency1.Text = s;




             string  days = dpData.Date.AddDays(-1).ToString("yyyy-MM-dd");
            string json1;
           
            string url2 = "https://api.nbp.pl/api/exchangerates/rates/c/" + cur1 + "/" + days + "/?format=json";
           
            using (var webClient = new WebClient())
            {
                json1 = webClient.DownloadString(url2);
            }
                
            Currency y = JsonSerializer.Deserialize<Currency>(json1);
            if (c.rates[0].ask > y.rates[0].ask)
            {
                strzala.Source = "strzalaup.png";
            }
            else
            {
                strzala.Source = "strzaladown.png";
            }
        }

    }

}