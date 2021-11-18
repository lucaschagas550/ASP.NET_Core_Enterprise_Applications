using System.Linq;

namespace NSE.Core.Utils
{
    public static class StringUtils
    {
        //Verica se é apenas números
        public static string ApenasNumeros(this string str, string input)
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }
    }
}