
# VDProjDiff
Produces a summary of the differences between two VDProj files. It uses .Net 4.6 - which should be installed on your operating system by now, or you need to go back to 1995.

# Licence
Read /VDProjDiff/Licence.txt
Do what you want with it! 

# Why does this tool exist
Microsoft seem comfortable letting the installer component have multiple flaws, including the potential for you to be to put at risk... When the installer project decides to automatically include a 3rd party dependency which could violate it's licence agreement.

Thus you need to be able to see what changes are afoot. And, if you have tried to see the difference between minor changes in a .vdproj file it's often very confusing using standard windiff/winmerge tools, due to the massive shift in location of content or a magical re-guid of components. This tool tries to present a brief version of what's added, removed or changed between two versions of the file.

# Plans to improve?
I'd like to create a VS2015+ extension that integrates with TFS so you can compare between TFS and local workspace versions.
Maybe even produce a better summary by component, textualizing the folder GUIDs, and detect if an exclusion is married with an inclusion elsewhere - helping you save time hunting for false-positives.

# Follow me
I'm on twitter [@GilesDMiddleton](https://twitter.com/GilesDMiddleton)
