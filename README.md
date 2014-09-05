simple_remove_duplicates_csharp
===============================

This is a simple application that takes input as a Directory, and finds the latest 
duplicate file using the Window syntax placing it in the output folder.

What I mean:

Given directory,

data/files.txt
data/files (2).txt
data/files (3).txt
data/files (4).txt

gives,

data/files.txt
data/files (2).txt
data/files (3).txt
data/files (4).txt
data/output/files.txt <- copy of data/files (4).txt

I quickly put it together to help a friend out at work.
Uses Regex a bit. Could've been better!
