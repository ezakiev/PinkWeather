using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Курсовая
{
    class Parser
    {
        public string parse(string text)
        {
            string lol = null;
            var matches = Regex.Matches(text, @"rgb\(102,102,255\)"">([[0-9]{2}\.[0-9]{2}\.[0-9]{4}|-[0-9]+\.[0-9]|[0-9]+\.[0-9]]|\d*)");
            foreach (Match mat in matches)
                lol += mat.Groups[1].Value + ",";
            return lol;
        }
    }
}
