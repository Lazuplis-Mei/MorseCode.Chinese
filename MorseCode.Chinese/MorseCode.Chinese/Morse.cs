using System.Text;

namespace MorseCode
{
    public partial class Morse
    {
        public const char S = '.';
        public const char L = '-';
        private const string standardLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string[] standardCodes =
        {
            ".-",
            "-...",
            "-.-.",
            "-..",
            ".",
            "..-.",
            "--.",
            "....",
            "..",
            ".---",
            "-.-",
            ".-..",
            "--",
            "-.",
            "---",
            ".--.",
            "--.-",
            ".-.",
            "...",
            "-",
            "..-",
            "...-",
            ".--",
            "-..-",
            "-.--",
            "--..",
        };
        public static readonly Morse Standred = new(standardLetters, standardCodes);
        
        private const string extendedLetters = $"!\"#$'()*,-./0123456789:;=?@{standardLetters}[]_";
        private static readonly string[] extendedCodes =
        {
            "-.-.--",
            ".-..-.",
            "..--",
            "...-..-",
            ".----.",
            "-.--.",
            "-.--.-",
            "----",
            "--..--",
            "-....-",
            ".-.-.-",
            "-..-.",
            "-----",
            ".----",
            "..---",
            "...--",
            "....-",
            ".....",
            "-....",
            "--...",
            "---..",
            "----.",
            "---...",
            "-.-.-.",
            "-...-",
            "..--..",
            ".--.-.",
                ".-",
                "-...",
                "-.-.",
                "-..",
                ".",
                "..-.",
                "--.",
                "....",
                "..",
                ".---",
                "-.-",
                ".-..",
                "--",
                "-.",
                "---",
                ".--.",
                "--.-",
                ".-.",
                "...",
                "-",
                "..-",
                "...-",
                ".--",
                "-..-",
                "-.--",
                "--..",
            "-.-..",
            ".---.",
            "..--.-",
        };
        public static readonly Morse Extended = new(extendedLetters, extendedCodes);

        private const string shortDigits = "0123456789";
        private static readonly string[] shortDigitsCodes =
        {
            ".-",
            "..-",
            "...--",
            "....-",
            ".....",
            "-....",
            "--...",
            "-..",
            "-.",
            "-",
        };
        public static readonly Morse ShortDigit = new(shortDigits, shortDigitsCodes);

        private static void BeepS() => Console.Beep(800, 250);
        private static void BeepL() => Console.Beep(800, 500);

        public static Task BeepAsync(string morse, char separator = '/')
        {
            return Task.Run(() =>
            {
                foreach (var sig in morse)
                {
                    if (sig == S)
                        BeepS();
                    else if (sig == L)
                        BeepL();
                    else if (sig == separator)
                        Thread.Sleep(800);
                    else
                        throw new ArgumentOutOfRangeException(nameof(morse), $"invaild signal: {sig}");
                }
            });
        }

        private readonly string letters;
        private readonly string lowerLetters;
        private readonly string[] codes;

        private Morse(string letters, string[] codes)
        {
            this.letters = letters;
            this.lowerLetters = letters.ToLower();
            this.codes = codes;
        }

        private int GetLetterIndex(char letter)
        {
            int index = letters.IndexOf(letter);
            if (index == -1)
            {
                index = lowerLetters.IndexOf(letter);
                if (index == -1)
                    throw new ArgumentOutOfRangeException(nameof(letter));
            }
            return index;
        }

        public bool IsVaildLetter(char letter)
        {
            return letters.Contains(letter) || lowerLetters.Contains(letter);
        }

        private int GetCodeIndex(string code)
        {
            int index = Array.IndexOf(codes, code);
            if (index == -1)
                throw new ArgumentOutOfRangeException(nameof(code));
            return index;
        }

        public bool IsVaildCode(string code)
        {
            return Array.IndexOf(codes, code) != -1;
        }

        public string FromMorse(string morse, char separator = '/')
        {
            var strBuilder = new StringBuilder();
            foreach (var code in morse.Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                strBuilder.Append(letters[GetCodeIndex(code)]);
            }
            return strBuilder.ToString();
        }

        public string ToMorse(string text, char separator = '/')
        {
            var result = new string[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                result[i] = codes[GetLetterIndex(text[i])];
            }
            return string.Join(separator, result);
        }


        public Task BeepTextAsync(string text)
        {
            return BeepAsync(ToMorse(text));
        }

    }
}