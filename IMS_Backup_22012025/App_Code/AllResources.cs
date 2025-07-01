using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Resource
/// </summary>
public class AllResources
{
    public static double member_income = 0;
    public static string member_id = "";
    public static string from_date = "";
    public static string to_date = "";
	public AllResources()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Function to get the Indian Time using the GMT+5.30
    //Author: Sreya
    public static DateTime CurrentTime()
    {
        TimeSpan ts = new TimeSpan(5, 30, 0);
        DateTime dtgmt = DateTime.UtcNow;
        DateTime dt = dtgmt.Add(ts);
        return dt;
    }
    #endregion
    public static DateTime timeNow()
    {
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        return indianTime;
    }
    public static string dateNow()
    {
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        string datee = string.Format("{0:d}", indianTime);
        return datee;
    }
    public static string onlytimeNow()
    {
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        string timee = string.Format("{0:t}", indianTime);
        return timee;
    }

    public static String NumWordsWrapper(double n)
    {
        string words = "";
        double intPart;
        double decPart = 0;
        if (n == 0)
            return "zero";
        try
        {
            string[] splitter = n.ToString().Split('.');
            intPart = double.Parse(splitter[0]);
            decPart = double.Parse(splitter[1]);
        }
        catch
        {
            intPart = n;
        }

        words = NumWords(intPart);

        if (decPart > 0)
        {
            if (words != "")
                words += " and ";
            int counter = decPart.ToString().Length;
            //if (counter==1)
            //{
            //    decPart = decPart * 10;
            //    counter = decPart.ToString().Length;
            //}
            switch (counter)
            {
                case 1: words += DecWords(decPart) + " Paisa"; break;
                case 2: words += DecWords(decPart) + " Paisa"; break;
                case 3: words += DecWords(decPart) + " thousandths"; break;
                case 4: words += DecWords(decPart) + " ten-thousandths"; break;
                case 5: words += DecWords(decPart) + " hundred-thousandths"; break;
                case 6: words += DecWords(decPart) + " millionths"; break;
                case 7: words += DecWords(decPart) + " ten-millionths"; break;
            }
        }
        return words;
    }

    static String NumWords(double n) //converts double to words
    {
        string[] numbersArr = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        string[] tensArr = new string[] { "twenty", "thirty", "fourty", "fifty", "sixty", "seventy", "eighty", "ninty" };
        string[] suffixesArr = new string[] { "thousand", "million", "billion", "trillion", "quadrillion", "quintillion", "sextillion", "septillion", "octillion", "nonillion", "decillion", "undecillion", "duodecillion", "tredecillion", "Quattuordecillion", "Quindecillion", "Sexdecillion", "Septdecillion", "Octodecillion", "Novemdecillion", "Vigintillion" };
        string words = "";

        bool tens = false;

        if (n < 0)
        {
            words += "negative ";
            n *= -1;
        }

        int power = (suffixesArr.Length + 1) * 3;

        while (power > 3)
        {
            double pow = Math.Pow(10, power);
            if (n > pow)
            {
                if (n % Math.Pow(10, power) > 0)
                {
                    words += NumWords(Math.Floor(n / pow)) + " " + suffixesArr[(power / 3) - 1] + ", ";
                }
                else if (n % pow > 0)
                {
                    words += NumWords(Math.Floor(n / pow)) + " " + suffixesArr[(power / 3) - 1];
                }
                n %= pow;
            }
            power -= 3;
        }
        if (n >= 1000)
        {
            if (n % 1000 > 0) words += NumWords(Math.Floor(n / 1000)) + " thousand, ";
            else words += NumWords(Math.Floor(n / 1000)) + " thousand";
            n %= 1000;
        }
        if (0 <= n && n <= 999)
        {
            if ((int)n / 100 > 0)
            {
                words += NumWords(Math.Floor(n / 100)) + " hundred";
                n %= 100;
            }
            if ((int)n / 10 > 1)
            {
                if (words != "")
                    words += " ";
                words += tensArr[(int)n / 10 - 2];
                tens = true;
                n %= 10;
            }

            if (n < 20)
            {
                if (words != "" && tens == false)
                    words += " ";
                words += (tens ? "-" + numbersArr[(int)n - 1] : numbersArr[(int)n - 1]);
                n -= Math.Floor(n);
            }
        }

        return words;

    }
    static String DecWords(double n) //converts double to words
    {
        string[] numbersArr = new string[] { "ten", "twenty", "thirty", "fourty", "fifty", "sixty", "seventy", "eighty", "ninty", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        string[] tensArr = new string[] { "twenty", "thirty", "fourty", "fifty", "sixty", "seventy", "eighty", "ninty" };
        string[] suffixesArr = new string[] { "thousand", "million", "billion", "trillion", "quadrillion", "quintillion", "sextillion", "septillion", "octillion", "nonillion", "decillion", "undecillion", "duodecillion", "tredecillion", "Quattuordecillion", "Quindecillion", "Sexdecillion", "Septdecillion", "Octodecillion", "Novemdecillion", "Vigintillion" };
        string words = "";

        bool tens = false;

        if (n < 0)
        {
            words += "negative ";
            n *= -1;
        }

        int power = (suffixesArr.Length + 1) * 3;

        while (power > 3)
        {
            double pow = Math.Pow(10, power);
            if (n > pow)
            {
                if (n % Math.Pow(10, power) > 0)
                {
                    words += NumWords(Math.Floor(n / pow)) + " " + suffixesArr[(power / 3) - 1] + ", ";
                }
                else if (n % pow > 0)
                {
                    words += NumWords(Math.Floor(n / pow)) + " " + suffixesArr[(power / 3) - 1];
                }
                n %= pow;
            }
            power -= 3;
        }
        if (n >= 1000)
        {
            if (n % 1000 > 0) words += NumWords(Math.Floor(n / 1000)) + " thousand, ";
            else words += NumWords(Math.Floor(n / 1000)) + " thousand";
            n %= 1000;
        }
        if (0 <= n && n <= 999)
        {
            if ((int)n / 100 > 0)
            {
                words += NumWords(Math.Floor(n / 100)) + " hundred";
                n %= 100;
            }
            if ((int)n / 10 > 1)
            {
                if (words != "")
                    words += " ";
                words += tensArr[(int)n / 10 - 2];
                tens = true;
                n %= 10;
            }

            if (n < 20)
            {
                if (words != "" && tens == false)
                    words += " ";
                words += (tens ? "-" + numbersArr[(int)n - 1] : numbersArr[(int)n - 1]);
                n -= Math.Floor(n);
            }
        }

        return words;

    }
}