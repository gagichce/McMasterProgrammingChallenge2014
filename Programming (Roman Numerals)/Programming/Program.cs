using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming
{
    class Program
    {
        //static string ReverseSentence(string input)
        //{
        //    string result = "";
        //    foreach (var letter in input)
        //    {
        //        if ("?>!")
        //    }
        //}
        static string numerals = "IVXLCDM";
        static int[] numbers = { 1, 5, 10, 50, 100, 500, 1000 };
        static int RomanToInt(string roman)
        {
            roman = roman.Trim();
            int result = 0;
            foreach (var letter in roman)
                result += numbers[numerals.IndexOf(letter)];
            return result;
        }
        static string IntToRoman(int number)
        {
            string result = "";
            while (number > 0)
            {
                for (int i = numerals.Length-1; i >=0 ; i--)
                {
                    if (numbers[i] <= number)
                    {
                        result += numerals[i];
                        number -= numbers[i];
                        break;
                    }
                }
            }
            return result;
        }
        static void Main(string[] args)
        {
            string operations = "*/+-%";
            var numTimes = int.Parse(Console.ReadLine());
            for (int iter = 0; iter < numTimes; iter++)
            {
                string sentence = Console.ReadLine();
                string[] operands = sentence.Split(operations.ToCharArray());
                int num = RomanToInt(operands[0]);
                int num2 = RomanToInt(operands[1]);
                int result = 0;
                switch (operations.Where(c => sentence.Contains(c)).First())
                {
                    case '+':
                        result = num + num2;
                        break;

                    case '-':
                        result = num - num2;
                        break;

                    case '/':
                        result = num / num2;
                        break;

                    case '*':
                        result = num * num2;
                        break;
                    case '%':
                        result = num % num2;
                        break;
                }
                Console.WriteLine(IntToRoman(result));
            }
        }
    }
}
