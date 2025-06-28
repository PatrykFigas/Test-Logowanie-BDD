using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace TestProject1.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        private IWebDriver driver;

        [Given(@"Użytkownik znajduje się na stronie logowania")]
        public void GivenUzytkownikNaStronieLogowania()
        {
            // Inicjalizowanie przeglądarki
            driver = new ChromeDriver();

            // Przejście do strony logowania
            driver.Navigate().GoToUrl("https://demo.guru99.com/V4/");
            driver.Manage().Window.Maximize();
        }

        [When(@"Wprowadza poprawną nazwę użytkownika i hasło")]
        public void WhenPoprawneDane()
        {
            // Wprowadzanie poprawnego loginu i hasła
            driver.FindElement(By.Name("uid")).SendKeys("mngr621310");
            driver.FindElement(By.Name("password")).SendKeys("udubemU");
        }

        [When(@"Wprowadza poprawną nazwę użytkownika i niepoprawne hasło")]
        public void WhenNiepoprawneHaslo()
        {
            // Wprowadzanie poprawnego loginu i błędnego hasła
            driver.FindElement(By.Name("uid")).SendKeys("mngr621310");
            driver.FindElement(By.Name("password")).SendKeys("zlehaslo");
        }

        [When(@"Naciska przycisk ""Zaloguj""")]
        public void WhenKliknijZaloguj()
        {
            // Kliknięcie przycisku zaloguj
            driver.FindElement(By.Name("btnLogin")).Click();
        }

        [Then(@"Zostaje przekierowany na stronę główną")]
        public void ThenNaStronieGlownej()
        {
            
            // Poczekaj, aż tekst "Manger Id" pojawi się na stronie (do 5 sek.)
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.PageSource.Contains("Manger Id"));

            // Potwierdzenie logowania
            Assert.IsTrue(driver.PageSource.Contains("Manger Id"), "Nie znaleziono potwierdzenia logowania");
            driver.Quit();
        }

        [Then(@"Widzi komunikat ""(.*)""")]
        public void ThenWidziKomunikat(string expected)
        {
            try
            {
                var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;

                Assert.AreEqual(expected, alertText);
                alert.Accept();
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("Alert nie pojawił się w oczekiwanym czasie.");
            }
            finally
            {
                driver.Quit();
            }
        }

    }
}

