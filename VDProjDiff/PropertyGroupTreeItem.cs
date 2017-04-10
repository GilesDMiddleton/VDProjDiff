using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VDProjDiff
{
    class PropertyGroupTreeItem : TreeItem
    {
        public override void Read(string[] text, ref int lineNum)
        {
            // my first line is my title
            name = text[lineNum++].Trim('"');
            if (text[lineNum++].Trim() != "{")
                throw new Exception("error expected token {");

            while (lineNum < text.Length && text[lineNum] != "}")
            {
                // read one or more property groups or nvp
                if (NameValuePairTreeItem.IsNVP(text[lineNum]))
                {
                    NameValuePairTreeItem nvp = new NameValuePairTreeItem(this);
                    nvp.Read(text, ref lineNum);
                    children.Add(nvp);
                    continue;
                }

                // group the first thing in quotes, there should be only one of those 
                // then exclude anything after that isn't whitespace, and definitely NOT = or "
                string l = text[lineNum];

                if (Regex.IsMatch(text[lineNum], "^\"{1}(.[^\"]+)\"{1}\\s*$"))
                {
                    PropertyGroupTreeItem group = new PropertyGroupTreeItem(this);
                    group.Read(text, ref lineNum);
                    // do not add the hierarchy stuff, it's mostly unhelpful, and contains lots of nasty duplicate group items - we could sort this out by making keys up later
                    // which comprise sub properties to uniquely identify them, or just take each entry as a whole string/key.
                    if (group.name != "Hierarchy")
                    {
                        children.Add(group);
                    }

                    // if the group contains a sourcepath, add that to the name
                    foreach (var child in group.children)
                    {
                        if (child is NameValuePairTreeItem)
                        {
                            var nvp = child as NameValuePairTreeItem;
                            if (nvp.name == "SourcePath")
                                group.name += $"({nvp.value})";
                        }
                    }
                    continue;
                }
            }
            lineNum++; // last }
        }

        public PropertyGroupTreeItem(TreeItem parentItem) : base(parentItem)
        {

        }
    }
}
