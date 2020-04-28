# Kettle3D
A 3D sandbox game I'm working on allowing you to design and construct skyscrapers.

# The Basis
Kettle3D is designed to allow you to construct the skyscraper of your dreams, then turn on options to calculate lateral and gravity loads. Later on I plan to add more features such as electricity, plumbing, internet and the ability to build a whole city.

# Where is Kettle3D at?
Although the game itself is in early stages of development, a Windows 10 64-bit installer is available - this installs the 3D rendering engine and the launcher, which updates itself every time you use it.

# Installation Instructions
Even though I haven't finished the first version of Kettle3D yet, you can still get the installer and launcher so that when I do release it, you can get it right away. You can download the Windows 10 installer [here.](https://github.com/Kettle3D/Kettle3D/releases/download/v1.0/Kettle3D.Windows.10.Installer.64bit.bat)
  
Please note that you'll need to have installed Panda for the game to work. The Panda installer is contained in the Kettle3D installer - it'll open when you install Kettle3D.
  
When you launch Kettle3D, a black window with white text will appear. This is the command line. It will display the game's output log. It'll look a little like this:

----------------------------------------------------------

Microsoft Windows [Version 10.0.18362.778]
(c) 2019 Microsoft Corporation. All rights reserved.

C:\WINDOWS\system32>cd C:\Program Files\Kettle3D

C:\Program Files\Kettle3D>ppython kettle3D-updater.py

----------------------------------------------------------

If the black window opens and then closes shortly afterwards, you may have to right-click on the file and click 'Run as Administrator.' This is because the program's files are stored in a Windows system folder. If you like, you can put the Kettle3D folder directly in C:, but you'll have to edit the Kettle3D.bat file using Notepad, and where it says `cd C:\Program Files\Kettle3D`, change it to `cd C:\Kettle3D`. You'll have to still keep 'osname.txt' in Program Files\Kettle3D so that it can be found by the launcher.

# How did I make it?
Kettle3D was programmed using Batch, Python and Tcl. The 3D rendering engine uses Panda3D, OpenGL, OpenAL and C++.
