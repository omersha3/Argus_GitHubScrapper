using MongoDB.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;

using System;
using System.Collections.Generic;
using System.IO;

using System.Net;

namespace GitHubScraper
{
    public class TestContainer
    {
        IWebDriver Driver;
        private string c_repoList = "repo-list";
        private string c_title = "a";
        private string c_description = "p";
        private string c_tag = "topic-tag";
        private string c_time = "relative-time";
        private string c_lang = "text-gray";
        private string c_stars = "pl-2";
        private string c_nextPage = "next_page";

        public TestContainer()
        {
           

        }

        public void RunTest(string mongoIPandPort, string webServerIPandPort)
        {
            var options = new ChromeOptions();
            string urlString = string.Format("http://{0}/wd/hub", webServerIPandPort);
            Uri url = new Uri(urlString);
            Driver = new RemoteWebDriver(url, options);

            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            SearchRepoByName("selenium");
            ExtractAllReposirotiesDetails(mongoIPandPort);
            ClickOnNextPage();
            Driver.Quit();
            
        }


        public void SearchRepoByName(string searchString)
        {
            Actions builder = new Actions(Driver);

            Driver.Navigate().GoToUrl(@"https://github.com/");
            IWebElement Searchbox = Driver.FindElement(By.Name("q"));
            Searchbox.Click();
            Searchbox.SendKeys(searchString);
            Searchbox.SendKeys(Keys.Enter);

            var stopWatch = System.Diagnostics.Stopwatch.StartNew();
            //builder.SendKeys(Keys.Enter);
            

         IWebElement repoListElement = Driver.FindElement(By.ClassName(c_repoList));
            stopWatch.Stop();

            Console.WriteLine("Search result: " + stopWatch.ElapsedMilliseconds.ToString()+ " ms");
            
        }

        public void ExtractAllReposirotiesDetails(string mongoIPandPort)
        {

            

            IWebElement repoListElement = Driver.FindElement(By.ClassName(c_repoList));
            IList<IWebElement> allRepos = repoListElement.FindElements(By.XPath("div"));
            ResultHandler.InitMongo(mongoIPandPort);

            for (int i = 0; i < 5; i++)
            {
                
                string title = allRepos[i].FindElement(By.TagName(c_title)).Text;

                string url = allRepos[i].FindElement(By.TagName(c_title)).GetAttribute("href");


                bool isValidLink = isLinkExists(url);

                string description;
                try
                {
                    description = allRepos[i].FindElement(By.TagName(c_description)).Text;
                }
                catch
                {
                    description = "None";
                }

                IList<IWebElement> tags = allRepos[i].FindElements(By.ClassName(c_tag));
                String[] allTextTags = new String[tags.Count];

                               
               if (tags.Count>0)
                {
                    
                    int integer = 0;
                    try
                    {
                        foreach (IWebElement t in tags)
                        {
                            allTextTags[integer++] = t.Text;

                        }
                        
                    }
                    catch
                    {
                        
                    }
                }

                string time = allRepos[i].FindElement(By.TagName(c_time)).Text;

                IWebElement rightPane = allRepos[i].FindElement(By.XPath("div[2]"));

                string lang = rightPane.FindElement(By.ClassName(c_lang)).Text;
                string star = rightPane.FindElement(By.ClassName(c_stars)).Text;
                
                ResultHandler.WriteToMongo(mongoIPandPort, title, description,  allTextTags , time, lang, star, isValidLink);
            }

          

        }
        private bool isLinkExists(string url)
        {

            try
            {
                Uri uri = new Uri(url);
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                WebRequest http = HttpWebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)http.GetResponse();
                Stream stream = response.GetResponseStream();
                return true;
            }
            catch (UriFormatException e)
            {
                return false;
            }
            catch (IOException e)
            {
                return false;
            }

        }

        public void ClickOnNextPage()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();


            Driver.FindElement(By.ClassName(c_nextPage)).Click();

            IWebElement repoListElement = Driver.FindElement(By.ClassName(c_repoList));

            watch.Stop();
            Console.WriteLine("Next Page Timing " + watch.ElapsedMilliseconds.ToString() + " ms");
           
        }
    }
}
