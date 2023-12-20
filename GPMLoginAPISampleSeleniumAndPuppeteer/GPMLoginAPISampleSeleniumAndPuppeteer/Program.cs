using GPMLoginAPISampleSeleniumAndPuppeteer.Libs;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace GPMLoginAPISampleSeleniumAndPuppeteer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GPMLoginApiV3 api = new GPMLoginApiV3("http://127.0.0.1:19995");

            JObject startResultObj = api.StartProfileAsync("17169ef5-761a-4fc4-9fba-2b634424c8c9").Result;
            FileInfo gpmDriverFileInfo = new FileInfo(Convert.ToString(startResultObj["data"]["driver_path"]));
            string remoteAddress = Convert.ToString(startResultObj["data"]["remote_debugging_address"]);

            // Selenium
            ChromeDriverService seleWebDriver = ChromeDriverService.CreateDefaultService(gpmDriverFileInfo.DirectoryName, gpmDriverFileInfo.Name);
            ChromeOptions options = new ChromeOptions();
            options.DebuggerAddress = remoteAddress;

            ChromeDriver driver = new ChromeDriver(seleWebDriver, options);
            driver.Navigate().GoToUrl("https://giaiphapmmo.vn");

            Console.ReadLine();

            // Puppeter
            string remoteBrowserEndpoint = "http://" + remoteAddress;
            ConnectOptions pupLaunchOptions = new ConnectOptions()
            {
                BrowserURL = remoteBrowserEndpoint
            };
            var browser = Puppeteer.ConnectAsync(pupLaunchOptions).Result;

            using (var page = browser.NewPageAsync().Result)
            {
                page.GoToAsync("https://giaiphapmmo.vn").Wait();
            }
            Console.ReadLine();
        }

    }
}
