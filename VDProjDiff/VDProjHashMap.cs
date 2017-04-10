using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDProjDiff
{
    // creates a dictionary of the key to TreeItem
    class VDProjHashMap
    {
        // Installers I've worked with have ranged from 3 to 20,000 capacity. So giving this a healthy starting point.
        public Dictionary<string, TreeItem> mapOfTreeItems = new Dictionary<string, TreeItem>(2000);

        public VDProjHashMap(PropertyGroupTreeItem rootGroup)
        {
            // first iterate all property items recursively and create a dictionary
            // ignore the root group, we don't need to add that to the dictionary/list
            RecurseItems(rootGroup);
        }

        private void RecurseItems(TreeItem item)
        {
            foreach (TreeItem child in item.children)
            {
                mapOfTreeItems.Add(child.Key, child);
                RecurseItems(child);
            }
        }
    }
}
