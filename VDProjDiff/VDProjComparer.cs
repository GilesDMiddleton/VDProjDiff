using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDProjdiff;

namespace VDProjDiff
{


    class VDProjComparer
    {
        private VDProjHashMap oldItems;
        private VDProjHashMap newItems;

        public StringBuilder removed = new StringBuilder();
        public StringBuilder added = new StringBuilder();
        public StringBuilder changed = new StringBuilder();

        private IEnumerable<string> intersection = null;

        public VDProjComparer(string oldFile, string newFile)
        {
            VDProjLoader oldfile = new VDProjLoader(oldFile);
            VDProjLoader newfile = new VDProjLoader(newFile);

            oldItems = new VDProjHashMap(oldfile.props);
            newItems = new VDProjHashMap(newfile.props);

            intersection = oldItems.mapOfTreeItems.Keys.Intersect(newItems.mapOfTreeItems.Keys);
            GetThingsAdded();
            GetThingsRemoved();
            GetThingsChanged();
        }

        public override string ToString()
        {
            StringBuilder displayString = new StringBuilder();

            if (added.Length == 0 && removed.Length == 0 && changed.Length == 0)
            {
                displayString.AppendLine($"Files are identical comparing {oldItems.mapOfTreeItems.Count}x2 items");
            }
            else
            {
                displayString.AppendLine($"Files are different comparing {oldItems.mapOfTreeItems.Count + newItems.mapOfTreeItems.Count} items (total in both files)");
                if (added.Length != 0)
                {
                    displayString.AppendLine("\n\n**********************************************************************************************\nAdded:\n**********************************************************************************************\n");
                    displayString.Append(added.ToString());
                    displayString.AppendLine();
                    displayString.AppendLine();
                }
                if (removed.Length != 0)
                {
                    displayString.AppendLine("\n\n**********************************************************************************************\nRemoved:\n**********************************************************************************************\n");
                    displayString.AppendLine(removed.ToString());
                    displayString.AppendLine();
                    displayString.AppendLine();
                }
                if (changed.Length != 0)
                {
                    displayString.AppendLine("\n\n**********************************************************************************************\nChanged:\n**********************************************************************************************\n");
                    displayString.AppendLine(changed.ToString());
                    displayString.AppendLine();
                    displayString.AppendLine();
                }
            }
            return displayString.ToString();
        }

        private void GetThingsAdded()
        {
            // find things not in the old file
            var extraKeys = newItems.mapOfTreeItems.Keys.Except(intersection);

            foreach (string key in extraKeys)
            {
                TreeItem item = newItems.mapOfTreeItems[key];
                added.AppendLine(item.ToString());
            }
        }

        private void GetThingsRemoved()
        {
            // find things not in the new file
            var extraKeys = oldItems.mapOfTreeItems.Keys.Except(intersection);
            foreach (string key in extraKeys)
            {
                TreeItem item = oldItems.mapOfTreeItems[key];
                added.AppendLine(item.ToString());
            }
        }

        private void GetThingsChanged()
        {
            // for those matching items - which are key/value pairs report those whose values have changed.
            var intersection = oldItems.mapOfTreeItems.Keys.Intersect(newItems.mapOfTreeItems.Keys);
            foreach (var key in intersection)
            {
                TreeItem itemold = oldItems.mapOfTreeItems[key];
                TreeItem itemnew = newItems.mapOfTreeItems[key];
                if (itemold is NameValuePairTreeItem)
                {
                    NameValuePairTreeItem nvpOld = itemold as NameValuePairTreeItem;
                    NameValuePairTreeItem nvpNew = itemnew as NameValuePairTreeItem;
                    if (nvpOld.value != nvpNew.value)
                    {
                        changed.AppendLine(nvpOld.Key);
                        changed.AppendLine($"old: {nvpOld.value}\nnew: {nvpNew.value}\n");
                    }
                }
                // else - shouldn't actually have any diffs on anything other than NVPs.
            }
        }
    }
}
