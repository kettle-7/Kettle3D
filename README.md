# Kettle3D
A 3D sandbox game I'm working on allowing you to design and construct skyscrapers.

# The Basis
Kettle3D is designed to allow you to construct the skyscraper of your dreams, then turn on options to calculate lateral and gravity loads. Later on I plan to add more features such as electricity, plumbing, internet and the ability to build a whole city.

# Where is Kettle3D at?
Kettle3D is currently in the early stages of developement. I'm hoping to release it later this year.

# Installation Instructions
Here is a step-by-step guide to how to install Kettle3D:

1) Download kettle3D-updater.py and Kettle3D.bat.
2) Put kettle3D-updater.py in C:\Program Files\Kettle3D. You may need administrator permission to do this.
3) Create a new text document in C:\Program Files\Kettle3D and name it 'dir.txt'. In the document, type the following:
'C:\\Users\\<Your Windows Username>\\AppData\\Roaming\\Kettle3D\\' (with no quotes)
4) Put Kettle3D.bat somewhere useful. When you double-click on this file, the Kettle3D launcher will open.
  
Please note that you'll need to have installed Panda3D for the game to work. You can get it here: https://www.panda3d.org/. If prompted for whether you would like Panda for Python or C++, pick Python.
  
When you launch Kettle3D, a black window with white text will appear. This is the command line. It will display the game's output log. It'll look a little like this:

----------------------------------------------------------

Microsoft Windows [Version 10.0.18362.778]
(c) 2019 Microsoft Corporation. All rights reserved.

C:\WINDOWS\system32>cd C:\Program Files\Kettle3D

C:\Program Files\Kettle3D>ppython kettle3D-updater.py

----------------------------------------------------------

If the black window opens and then closes shortly afterwards, you may have to right-click on the file and click 'Run as Administrator.' This is because the program's files are stored in a Windows system folder. If you like, you can put the Kettle3D folder directly in C:, but you'll have to edit the Kettle3D.bat file using Notepad, and where it says `cd C:\Program Files\Kettle3D`, change it to `cd C:\Kettle3D`. This will make it look for Kettle3D in your C: drive instead.

# How did I make it?
Kettle3D was programmed using Batch, Python and Tcl. The 3D rendering engine uses Panda3D, OpenGL, OpenAL and C++.
