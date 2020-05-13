# Kettle3D
A 3D sandbox game I'm working on allowing you to design and construct skyscrapers.

# The Basis
Kettle3D is designed to allow you to construct the skyscraper of your dreams, then turn on options to calculate lateral and gravity loads. Later on I plan to add more features such as electricity, plumbing, internet and the ability to build a whole city.

# Where is Kettle3D at?
Although the game itself is in early stages of development, a Windows 10 64-bit installer and a OS X 32-bit/64-bit installer are available - this installs the 3D rendering engine and the launcher, which updates itself every time you use it.

# Installation Instructions

## For Windows 10
You can download the Windows 10 installer [here.](https://github.com/Kettle3D/Kettle3D/releases/download/v1.0.1/Kettle3D.Windows.Installer.bat)  
If you see this:  
![Image](https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/assets/chromes-security-policy.png "Chrome's Security Policy")  
then that means Chrome's security policy is working. What this means is that Google Chrome (if you're using it) has a policy that stops you from downloading apps that you might not know about. If you click 'Keep' then it'll go away.

Double-click on it in File Explorer. If it crashes then you'll have to right-click it and click 'Run as Administrator'. You might need the Admin password for this.

Kettle3D should be in your Start Menu. It will have a picture of two gears as its logo. If you like, you can download Kettle3D.ico and set it as the icon.
1) Right-click on Kettle3D in your Start Menu, and then click 'Open file location'
2) Right-click on Kettle3D (Kettle3D.lnk on older systems,) then click 'Properties'
3) Click 'Change Icon'
4) Browse to the folder where you downloaded Kettle3D.ico

## For Mac OS X
You can download the OS X installer [here.](https://github.com/Kettle3D/Kettle3D/releases/download/v1.0.1/Kettle3D.OS.X.Installer.sh)  
Double-click it in your Downloads folder. You'll probably need the Admin password. Yes, that means you'll need to see Andy.

The installer will download the Panda installer, but you'll have to manually install it.
There will be a file called Kettle3D.command on your desktop - double-click on it to open Kettle3D.

### How to add a desktop shortcut for Kettle3D (works on OS X only):
Follow these instructions if the desktop shortcut the installer made didn't work.
1) Click the **Spotlight** icon, the small magnifying glass at the top-right corner of the screen.
2) In the box that appears, enter *Automator*.
3) Click the app that looks like a robot when it appears in the menu. It will either be in the section labeled **Top Hit** or in **Applications**.
4) When Automator opens, select **Application**.
5) Click **Choose**.
6) Click **Actions** and scroll down to **Run Shell Script**. Tip: the list's in alphabetical order.
7) Drag it into the window off to the right.
8) Where it says *cat* off to the right, delete it and replace it with this:
`/bin/bash $HOME/Library/Application Support/Kettle3D/Kettle3D.sh` (all one line)
9) Click **File > Save** and enter *Kettle3D* as the name. When it asks you where you want to save it, choose either your Desktop or the Applications folder on your computer.

Hopefully this works - my computer's a Windows.

---

Please note that you'll need to have installed Panda for the game to work. The Panda installer is contained in the Kettle3D installer - it'll open when you install Kettle3D on Windows 10, and it'll download the Mac installer to HOME/Downloads on OS X.
  
When you launch Kettle3D, a black window with white text will appear. This is the command line. It will display the game's output log. It'll look a little like this:  
![Image](https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/assets/windows_command_line.png "Kettle3D Command Window")

## Note
Please note that Kettle3D's world generation system is very inefficient. I will eventually post the files for an empty world on GitHub, but at the moment I'm still finalising it.  
If it takes about half an hour for your computer to generate the world, your computer is fast. I'll try and fix this when I can.

# How did I make it?
Kettle3D was programmed using Batch, Python and Tcl. The 3D rendering engine uses Panda3D, OpenGL, OpenAL and C++.

# DISCLAIMER
Please note that while every effort has been put into this game to ensure that users get a good experience, Kettle3D will not accept any responsibility for buildings prototyped on this game and built without confirmation that building codes are met. While this game allows you to simulate gravity and lateral loads, they may not mimic reality and are purely for amusement. Basically, don't use this game as an architecture guide.
