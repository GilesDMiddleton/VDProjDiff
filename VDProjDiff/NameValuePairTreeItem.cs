using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VDProjDiff;

namespace VDProjDiff
{
    /// <summary>
    /// The name=value entities found at leaf levels in the tree of the VDProj file
    /// </summary>
    public class NameValuePairTreeItem : TreeItem
    {
        public string value;

        // match start of line"one or more characters" = "zero or more characters"end of line
        private const string regexMatch = "^\"(.+)\" = \"(.*)\"$";
        public static bool IsNVP(string text)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(text, regexMatch);
        }
        public NameValuePairTreeItem(TreeItem parentItem) : base(parentItem)
        {
        }

        public override string ToString()
        {
            return $"\"{this.Key}\" = \"{this.value}\"";
        }

        /// <summary>
        /// given a single line, read the name value pair
        /// </summary>
        /// <param name="line"></param>
        public override void Read(string[] text, ref int lineNumber)
        {
            MatchCollection col = Regex.Matches(text[lineNumber++], regexMatch);
            name = col[0].Groups[1].Value;
            value = col[0].Groups[2].Value;
        }
    }
}
