using System;

namespace dbl_boot
{
    class MyConfig
    {
        internal string DataFile = "";
        internal bool Monitor = false;
        internal string IPv4 = "";
        internal string IPv6 = "";
        internal string TXT = "";
        internal int TTL = 300;

        internal string Serialize()
        {
            return "2|" + PipeEncode(DataFile) + "|" + (Monitor ? "T" : "F") + "|" + PipeEncode(IPv4) + "|" + PipeEncode(IPv6) + "|" + PipeEncode(TXT) + "|" + TTL.ToString();
        }

        internal static MyConfig DeSerialize(string x)
        {
            var rv = new MyConfig();
            int v = int.Parse(PipeRead(ref x));
            if (v == 1)
            {
                rv.DataFile = PipeRead(ref x);
                rv.Monitor = false; //not part of v. 1 config
                rv.IPv4 = PipeRead(ref x);
                rv.IPv6 = PipeRead(ref x);
                rv.TXT = PipeRead(ref x);
                rv.TTL = int.Parse(PipeRead(ref x));
            }
            else if (v == 2)
            {
                rv.DataFile = PipeRead(ref x);
                rv.Monitor = (PipeRead(ref x) == "T");
                rv.IPv4 = PipeRead(ref x);
                rv.IPv6 = PipeRead(ref x);
                rv.TXT = PipeRead(ref x);
                rv.TTL = int.Parse(PipeRead(ref x));
            }
            else
            {
                throw new ArgumentException("Invalid configuration data version");
            }
            return rv;
        }

        internal static string PipeEncode(string x)
        {
            return x.Replace(@"\", @"\\").Replace(@"|", @"\|");
        }

        internal static string PipeRead(ref string fromStr)
        {
            int i, j;
            string rv = "";
            while (true)
            {
                i = fromStr.IndexOf('|');
                if (i < 0) i = fromStr.Length;
                j = fromStr.IndexOf('\\');
                if (j < 0) j = fromStr.Length;
                if (i <= j)
                {
                    rv += fromStr.Substring(0, i);
                    fromStr = i<fromStr.Length ? fromStr.Substring(i + 1) : "";
                    return rv;
                }
                rv += fromStr.Substring(0, j) + fromStr[j + 1];
                fromStr = j<fromStr.Length ? fromStr.Substring(j + 2) : "";
            };
        }
    }
}
