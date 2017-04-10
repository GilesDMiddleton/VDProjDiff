using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using VDProjDiff;

namespace VDProjdiff
{
    // token            
    // property group title           "x"  
    // name value pair                "y" = "z"
    // property group start           {
    // property group end             }
    
    // note, now { } is repetition. [ ] is option | is or
    // property group                 propertygrouptitle propertygroupstart { [ namevaluepair | propertygroup ] }

    // ignore property group "Hierarchy", it's just rubbish, full of guids.
    // "Configurations" contains the debug/release settings. 
    // property group is always the main key, comprised of {ancestors key}.mykey.
    // I've not used a proper parser - just POCO and regex.

    class VDProjLoader
    {
        private string[] rawdata;
        public PropertyGroupTreeItem props = new PropertyGroupTreeItem(null);
        public VDProjLoader(string fileName)
        {
            LoadFile(fileName);

            ReadIntoProps();
        }

        private void LoadFile(string filename)
        {
            rawdata = System.IO.File.ReadAllLines(filename);
            for( int i=0; i<rawdata.GetLength(0); i++ )
            {
                rawdata[i] = rawdata[i].Trim();
            }
        }

        private void ReadIntoProps()
        {
            int lineNum = 0;
            props.Read(rawdata,ref lineNum);

        }
    }
}
