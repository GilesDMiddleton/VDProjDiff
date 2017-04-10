using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDProjDiff
{
    /// <summary>
    /// Think of each item in the VDProj file as part of a tree, with name value pairs typically found at the leaf, and group items at the root and branches level.
    /// </summary>
    public class TreeItem
    {
        public string name;
        public TreeItem parent;
        public List<TreeItem> children = new List<TreeItem>();

        public TreeItem(TreeItem parentItem)
        {
            parent = parentItem;
        }

        private string _key;

        public string Key
        {
            get
            {
                if (String.IsNullOrEmpty(_key))
                {
                    _key = (parent != null ? $@"{parent.Key}\{name}" : name);
                }

                return _key;
            }
        }

        public override string ToString()
        {
            return this.Key;
        }

        public virtual void Read(string[] text, ref int lineNumber) {  }
    }
}
