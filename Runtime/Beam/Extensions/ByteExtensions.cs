using System.Collections.Generic;
using System.Linq;
using Org.BouncyCastle.Math;

namespace Beam.Extensions
{
    public static class ByteExtensions
    {
        const string prefix = "0x";

        public static string ToHex(this IEnumerable<byte> obj) => ByteExtensions.prefix + string.Concat(obj.Select(x => x.ToString("x2")));

        public static string ToHex(this BigInteger obj) => ByteExtensions.prefix + obj.ToString(16);

        public static string TrimHexPrefix(this string s) => s.StartsWith("0x") ? s.Substring(2) : s;
    }
}