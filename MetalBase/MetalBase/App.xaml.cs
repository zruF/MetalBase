using MetalBase.Services;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Net.Http;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace MetalBase
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected async override void OnStart()
        {
            var currentUser = Preferences.Get("userId", null);
            if(currentUser == null)
            {
                var newUserId = Guid.NewGuid();

                HttpClientHandler handler = new HttpClientHandler()
                {
                    UseDefaultCredentials = true
                };
                handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                using (HttpClient client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri("https://10.0.2.2:44357");
                    var request = new UserRequest
                    {
                        Id = newUserId,
                        AndroidVersion = "test1",
                        MacAddress = "test2",
                        Smartphone = "test3"
                    };
                    var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                    var result = await client.PostAsync("/User", content);
                    Console.WriteLine(result.Content.ReadAsStringAsync().Result);
                    string resultContent = result.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(resultContent);
                }

                Preferences.Set("userId", newUserId.ToString());
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public class UserRequest
        {
            public Guid Id { get; set; }
            public string MacAddress { get; set; }
            public string Smartphone { get; set; }
            public string AndroidVersion { get; set; }
        }
    }
}
