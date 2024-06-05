using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.Tools
{
    public static class UidGenerator
    {
        private static readonly Random random = new();
        public static string RandomUidGenerator(int length)
        {
            const string chars = "ABCDEF0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
