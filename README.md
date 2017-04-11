# VDProjDiff
Produces a summary of the differences between two VDProj files. It uses .Net 4.6 - which should be installed on your operating system by now, or you need to go back to 1995. You can find the pre-built EXE in the release folder, or build the project yourself.
There are other tools out there that also try to help in this arena if mine isn't what you want.
[Converting VDProj to XML](http://web2.codeproject.com/Articles/862952/Checking-Visual-Studio-Setup-Projects?display=Print) and 
[MSI Diff tool](https://www.codeproject.com/articles/9132/a-windows-installer-database-diff-tool)

# Licence
Read /VDProjDiff/Licence.txt
Do what you want with it! 

# Why does this tool exist?
Microsoft seem comfortable letting the installer component have multiple flaws, including the potential for you to be to put at risk... When the installer project decides to automatically include a 3rd party dependency which could violate it's licence agreement.

Thus you need to be able to see what changes are afoot. And, if you have tried to see the difference between minor changes in a .vdproj file it's often very confusing using standard windiff/winmerge tools, due to the massive shift in location of content or a magical re-guid of components. This tool tries to present a brief version of what's added, removed or changed between two versions of the file.

# Plans to improve?
I'd like to create a VS2015+ extension that integrates with TFS so you can compare between TFS and local workspace versions.
Maybe even produce a better summary by component, textualizing the folder GUIDs, and detect if an exclusion is married with an inclusion elsewhere - helping you save time hunting for false-positives.
I'd also like to use a proper BNF grammar/parser for the file. But in reality - due to the madness of auto-detection of dependencies, the only truly useful tool is the MSI Diff tool mentioned above. As you never know what the build machine will do during compilation.

# Follow me
I'm on twitter [@GilesDMiddleton](https://twitter.com/GilesDMiddleton)
