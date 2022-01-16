using MorseCode.Chinese.Properties;
using System.Linq;
using System.Text;

namespace MorseCode.Chinese
{
    public static class CommercialCode
    {
        private static readonly string DATA = Encoding.Default.GetString(GZip.Decompress(Resources.CC));
        public static string FromCodeString(string codeText)
        {
            return FromCodes(codeText.Partition(4, s => short.Parse(s)));
        }

        public static string FromCodes(params short[] codes)
        {
            var strBuilder = new StringBuilder();
            foreach (var code in codes)
            {
                strBuilder.Append(DATA[code]);
            }
            return strBuilder.ToString();
        }

        public static short[] ToCodes(string text)
        {
            var codes = new short[text.Length];
            for (int i = 0; i < codes.Length; i++)
            {
                codes[i] = (short)DATA.IndexOf(text[i]);
                if (codes[i] == -1)
                    codes[i] = 0;
            }
            return codes;
        }

        public static string ToCodesString(string text)
        {
            var str = new StringBuilder(text.Length * 4);
            for (int i = 0; i < text.Length; i++)
            {
                var code = DATA.IndexOf(text[i]);
                if (code == -1)
                    code = 0;
                str.Append(code.ToString("D4"));
            }
            return str.ToString();
        }

        public static string ToMorse(string text)
        {
            return Morse.ShortDigit.ToMorse(ToCodesString(text));
        }

        public static string FromMorse(string morse)
        {
            return FromCodeString(Morse.ShortDigit.FromMorse(morse));
        }

        public static Task BeepTextAsync(string text)
        {
            return Morse.BeepAsync(ToMorse(text));
        }
    }
}