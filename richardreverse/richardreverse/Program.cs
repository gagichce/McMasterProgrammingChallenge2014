using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace richardreverse
{
    class Program
    {
        static string punctuation = ".,!?";
        //static string SplitAndJoin(string letter, string text, Func<string, string> action)
        //{
        //    return string.Join(letter, text.Split(letter).Select(c => action(c)));
        //}
        static string ReverseSentence(string input)
        {
            List<string> result = new List<string>();
            List<char> punc = new List<char>();
            string piece = "";
            foreach (var letter in input)
            {
                if (punctuation.Contains(letter))
                {
                    result.Add(piece);
                    piece = "";
                    punc.Add(letter);
                }
                else
                    piece += letter;
            }
            result.Add(piece);
            result = result.Select(w => string.Join(" ", w.Split(' ').Select(y => new string(y.Reverse().ToArray())))).ToList();
            string answer = "";
            foreach (var thing in result)
            {
                if (punc.Count == 0)
                    answer += thing;
                else
                {
                    answer += thing + punc.Last();
                    punc.RemoveAt(punc.Count - 1);
                }
            }
            return answer;
        }
        static void Main(string[] args)
        {
            string sentence = Console.ReadLine();
            //string[] pieces = sentence.Split(punctuation.ToCharArray());
            Console.WriteLine(ReverseSentence(sentence));
            //sentence = string.Join(" ", sentence.Split(' ').Select(s => ReverseWord(s)));
            string copy = sentence;

        }
    }
}
