using NuGet.Frameworks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using System.Threading;

namespace CoelhodaFonseca.Test
{
    public class FinanciamentoImobiliario
    {
        ChromeDriver driver = null;


        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();

            driver.Url = "https://www.coelhodafonseca.com.br/institucional/financiamento";
            //driver.Navigate();
            driver.Manage().Window.FullScreen();

            var valorImovel = 200_000;


            var txtValorImovel = driver.FindElementByName("property_value_label");
            txtValorImovel.SendKeys(valorImovel.ToString());

            var txtValorEntrada = driver.FindElementByName("entry_value_label");
            txtValorEntrada.SendKeys((valorImovel * 0.2).ToString());
        }



        [Test]
        public void Criticar_Prazo_Financiamento_Maior_Que_30_Anos()
        {
            var txtPrazoEmAnos = driver.FindElementByName("years");
            txtPrazoEmAnos.SendKeys("31");

            var btn = driver.FindElementById("simular-financiamento");
            btn.Click();

            Thread.Sleep(2_000);

            var textoCritica = driver.FindElements(By.XPath("//*[@id=\"email-form\"]/div[2]/div[2]/div[1]/div[3]"));
            var nomeTest = $@"C:\temp\teste-selenium\{DateTime.Now.ToString("mm")}{nameof(Criticar_Prazo_Financiamento_Maior_Que_30_Anos)}.png";


            if (textoCritica == null)
            {
                SalvarEvidencia(nomeTest);
                Assert.Fail();
            }


            if (textoCritica.Any(x => x.Text == "O prazo máximo de financiamento é 30 anos."))
            {
                SalvarEvidencia(nomeTest);
                Assert.Pass();
            }
            else
            {
                SalvarEvidencia(nomeTest);
                Assert.Fail();
            }
        }


        private void SalvarEvidencia(string nomeTeste)
        {
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            ss.SaveAsFile(nomeTeste, ScreenshotImageFormat.Png);

            driver.Close();
        }

        [Test]
        public void Criticar_Soma_Idade_Financiamento_Maior_Que_80_Anos()
        {
            Assert.Pass();
        }
    }
}