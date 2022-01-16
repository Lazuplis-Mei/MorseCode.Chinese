using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorseCode.Chinese
{
    internal static class StringExtension
    {
        public static string[] Partition(this string self, int length)
        {
            return self.Partition(length, s => s);
        }

        public static T[] Partition<T>(this string self, int length, Func<string, T> converter)
        {
            var result = new T[self.Length / length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = converter(self.Substring(i * length, length));
            }
            return result;
        }
    }

    internal static class GZip
    {
        public static byte[] Compress(byte[] bytes)
        {
            var memory = new MemoryStream();
            GZipStream gzip = new(memory, CompressionMode.Compress);
            gzip.Write(bytes, 0, bytes.Length);
            gzip.Close();
            return memory.ToArray();
        }

        public static byte[] Decompress(byte[] bytes)
        {
            var memory = new MemoryStream(bytes);
            GZipStream gzip = new(memory, CompressionMode.Decompress);
            memory = new MemoryStream();
            gzip.CopyTo(memory);
            gzip.Close();
            return memory.ToArray();
        }

    }
}
