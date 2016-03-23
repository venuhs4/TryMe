using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TryMe
{
    internal class Program
    {
        static string Recipe = "";
        private static void Main(string[] args)
        {
            string r1 = "ABCDEF";
            string r2 = "D";

            AddRecipe(r1);
            AddRecipe(r2);

            Debug.WriteLine(Recipe);

        }
        public static void AddRecipe(string recipe)
        {
            if (!Recipe.Split('|').Any(r=> r.Trim() ==recipe.Trim()))
            {
                Recipe += string.IsNullOrEmpty(Recipe) ? recipe : " | " + recipe;
            }
        }
    }
}