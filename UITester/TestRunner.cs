using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UITester
{
    class TestRunner
    {
        const string APPLICATION_ROOT = "http://google.com";

        public static void Main(string[] args)
        {

            // program runs from "/bin/Debug" or "/bin/Release",
            // so we go up 2 directories to get to our chromedriver.exe file
            var driver = new ChromeDriver("../../");

            while (true)
            {
                // instantiate a TestLogger to show our results as Console output
                TestLogger logger = new TestLogger();

                // go to the root url specified at the beginning of this file
                driver.Navigate().GoToUrl(APPLICATION_ROOT);

                // make our assertions, assigning a logger to track the results and print them as we go
                RunTest<bool>(logger, driver.FindElementById("hplogo").Displayed, true, "Google logo should be present");
                RunTest<string>(logger, driver.FindElementByName("btnK").GetCssValue("text-align"), "center", "Google Search button should have center-aligned text");

                // go to another Url within our application
                driver.Navigate().GoToUrl(APPLICATION_ROOT + "/images");

                // make more assertions
                RunTest<bool>(logger, driver.FindElementById("qbi").Displayed, true, "Google Images camera icon should be present");
                RunTest<int>(logger, driver.FindElementsByName("q").Count, 1, "Only 1 input box should be displayed on Image Search screen");

                // print the cumulative results tracked by the logger
                logger.PrintResults();

                Console.Write("Press any key to run tests again . . .");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public static void RunTest<T>(TestLogger TestLogger, T ActualValue, T ExpectedValue, string GoalStatement)
        {
            if (ActualValue.Equals(ExpectedValue))
            {
                TestLogger.Pass();
            }
            else
            {
                TestLogger.Fail();
                Console.WriteLine(" - Expected \"" + ExpectedValue + "\", but found \"" + ActualValue + "\"");
                Console.Write("    "); // spacing for next line, keeps things looking nice in console
            }

            Console.WriteLine(" - " + GoalStatement);

            
        }
    }

    class TestLogger
    {
        private int PassCount;
        private int FailCount;

        public enum ConsoleState
        {
            PASS,
            FAIL,
            DEFAULT
        }

        public TestLogger()
        {
            SetConsoleColors(ConsoleState.DEFAULT);
            Console.WriteLine("Running all tests...");
            this.PassCount = 0;
            this.FailCount = 0;
        }        

        public void Pass()
        {
            SetConsoleColors(ConsoleState.PASS);
            Console.Write("PASS");
            this.PassCount++;
            SetConsoleColors(ConsoleState.DEFAULT);
        }

        public void Fail()
        {
            SetConsoleColors(ConsoleState.FAIL);
            Console.Write("FAIL");
            this.FailCount++;
            SetConsoleColors(ConsoleState.DEFAULT);
        }

        public void PrintResults()
        {
            if (this.FailCount == 0)
                SetConsoleColors(ConsoleState.PASS);
            else
                SetConsoleColors(ConsoleState.FAIL);

            int TotalTests = this.PassCount + this.FailCount;
            double SuccessRate = ((double)this.PassCount / (double)TotalTests) * 100 ;

            Console.WriteLine();
            Console.WriteLine(TotalTests + " tests run, " 
                + this.PassCount + " tests passed, " 
                + this.FailCount + " failed " 
                + "(" + SuccessRate + "%)");

            SetConsoleColors(ConsoleState.DEFAULT);
        }

        public void SetConsoleColors(ConsoleState Status)
        {
            switch (Status)
            {
                case ConsoleState.PASS:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;

                case ConsoleState.FAIL:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.White; // reset to default
                    break;
            }
        }
    }
}
