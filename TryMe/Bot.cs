using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TryMe
{
    class Bot
    {
        static botDataContext Db = new botDataContext();
        static bool botAnswered = false;
        static Phrase LastAskedPhrase;
        internal static void StartBot()
        {
            string input = "";
            while (input != "exit")
            {
                WriteYou("");
                input = Console.ReadLine();

                if (LastAskedPhrase != null && input.ToLower() == "your answer is wrong")
                {
                    WriteBot("Can you answer for the same question then?");
                    var answer = Console.ReadLine();
                    LastAskedPhrase.output = answer;
                    LastAskedPhrase.hitCount = LastAskedPhrase.hitCount + 1;
                    LastAskedPhrase.lastHitDateTime = DateTime.Now;
                    Db.SubmitChanges();
                }
                else if (GetMatchingPharase(input) != null)
                {
                    Phrase phraseMatch = GetMatchingPharase(input);
                    LastAskedPhrase = phraseMatch;
                    if (phraseMatch.output != "")
                    {

                        WriteBot(phraseMatch.output);

                    }
                    else
                    {
                        WriteBot("Can you answer the same question? coz I don't know anything about this.");
                        var answer = Console.ReadLine();
                        phraseMatch.output = answer;
                    }
                    phraseMatch.hitCount = phraseMatch.hitCount + 1;
                    phraseMatch.lastHitDateTime = DateTime.Now;
                    Db.SubmitChanges();
                }
                else
                {
                    WriteBot("Can you answer the same question? coz I don't know anything about this.");
                    var answer = Console.ReadLine();
                    if (!new string[] { "i don't know", "i dont know", "i do not know" }.Contains(answer.ToLower()))
                    {
                        Db.Phrases.InsertOnSubmit(new Phrase()
                        {
                            input = input,
                            hitCount = 1,
                            createdDateTime = DateTime.Now,
                            lastHitDateTime = DateTime.Now,
                            output = answer,
                            priority = 0
                        });
                        Db.SubmitChanges();
                        WriteBot("Your answer saved. Thanks.");
                    }
                    else
                    {
                        Db.Phrases.InsertOnSubmit(new Phrase()
                        {
                            input = input,
                            hitCount = 1,
                            createdDateTime = DateTime.Now,
                            lastHitDateTime = DateTime.Now,
                            output = "",
                            priority = 0
                        });
                        Db.SubmitChanges();
                        WriteBot("No Problem. Thanks for the question");
                    }
                }
            }
        }

        private static Phrase GetMatchingPharase(string input)
        {
            var result = Db.Phrases.Where(p => input.ToLower() == p.input.ToLower());

            if (result != null && result.Count() > 0)
                return result.First();
            else
                return null;
        }

        private static bool IsMatched(string input, Phrase p)
        {
            return Regex.Replace(p.input.ToLower(), @"[^\w\d\s]", "") == Regex.Replace(input.ToLower(), @"[^\w\d\s]", "");
        }

        private static void WriteYou(string text)
        {
            botAnswered = false;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("You: {0}", text);
        }

        private static void WriteBot(string text)
        {
            botAnswered = true;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Bot: {0}", text);
        }
    }
}
