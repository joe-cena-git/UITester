# UITester

## Overview
A simple, console-based UI Tester written in C# and driven by Selenium &amp; ChromeDriver.

## Usage
1. Clone the project into the directory of your choice.
2. Open the project in Visual Studio.
3. Open TestRunner.cs for editing.
4. Edit the APPLICATION_ROOT url at the top of the Main() method to be the same as the root Url of the application you are testing.
5. Add or edit the RunTest() methods in the Main() method to test the specific things you'd like to test.

## RunTest() Methods
The RunTest() method is the heart of this application, and can be used like this:
```C#
RunTest<type>(logger, <test value>, <expected value>, "Description of this test case");
```
Keep an eye on that <type> definition at the beginning of the method. It must be set to the type of variable you're comparing in order to work.

For example, you can test for the presence of certain elements like so (using boolean logic):
```C#
RunTest<bool>(logger, driver.FindElementById("qbi").Displayed, true, "Google Images camera icon should be present");
RunTest<bool>(logger, driver.FindElementById("hplogo").Displayed, true, "Google logo should be present");
```

You can also test for virtually any CSS value like this (using string comparison):
```C#
RunTest<string>(logger, driver.FindElementByName("btnK").GetCssValue("text-align"), "center", "Google Search button should have center-aligned text");
```

One more example, attempting to count the elements on screen (using integer comparison):
```C#
RunTest<int>(logger, driver.FindElementsByName("q").Count, 1, "Only 1 input box should be displayed on Image Search screen");
```

## To Do
Need to add error handling. If an element is not found by Selenium on a page, it throws an exception and stops. It would be  better if it printed a nice console warning/failure message instead and kept going.

