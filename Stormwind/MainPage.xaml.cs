﻿using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using System.Net;
using System.Text.Json;
using System.Data;
namespace Stormwind
{
    public partial class MainPage : ContentPage
    {


        public class Currency
        {
            public string? table { get; set; }
            public string? currency { get; set; }
            public string? code { get; set; }
            public IList<Rate> rates { get; set; }
        }
        public class Rate
        {
            public string? no { get; set; }
            public string? effectiveDate { get; set; }
            public double? bid { get; set; }
            public double? ask { get; set; }

        }

        public MainPage()
        {
            InitializeComponent();
            DateTime dzis = DateTime.Now;
            dpData.MaximumDate = dzis;

        }

        private void Bcurrency1(object sender, EventArgs e)
        {
            string data = dpData.Date.ToString("yyyy-MM-dd");
            string cur1 = Waluta1.Text;
            string json;
            string url1 = "https://api.nbp.pl/api/exchangerates/rates/c/"+cur1+ "/" + data + "/?format=json";


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

        }

        private void Bcurrency2(object sender, EventArgs e)
        {
            string data = dpData.Date.ToString("yyyy-MM-dd");
            string cur2 = Waluta2.Text;
            string json;
            string url2 = "https://api.nbp.pl/api/exchangerates/rates/c/"+cur2 +"/" + data + "/?format=json";


            using (var webClient = new WebClient())
            {
                json = webClient.DownloadString(url2);
            }

            Currency c = JsonSerializer.Deserialize<Currency>(json);
            string s = $"nazwa waluty : {c.currency}\n";
            s += $"kod waluty {c.code} \n";
            s += $"Data : {c.rates[0].effectiveDate} \n";
            s += $"Cena skupu : {c.rates[0].bid} \n";
            s += $"Cena sprzedazy : {c.rates[0].ask} \n ";
            textCurrency2.Text = s;

        }
    }

}