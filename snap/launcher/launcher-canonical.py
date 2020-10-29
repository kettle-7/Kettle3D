#!python3

# Kettle3D Launcher v1.0, Salvaged from https://github.com/CYBORG123456789/Kettle3D/blob/master/kettle3D-versions.py :~)

from urllib.request import urlopen
from urllib.error import URLError
from os.path import normpath
from sys import platform
from os import getenv
import pickle
import copy
import time
import sys
import os

directory = ""
args = copy.deepcopy(sys.argv)
mods = ['default']
version = "stable"
gui = True

true = [
    "true",
    "1",
    "True",
    "t",
    "T"
]

false = [
    "false",
    "0",
    "False",
    "f",
    "F"
]

del args[0]

# print("mods    = %s" % str(mods))
# print("version = %s" % version)
# print("gui     = %s" % str(gui))

IS_DEV_VERSION = False;
if (os.path.exists("/home/kettle/Programming/Kettle3D.dev")):
    IS_DEV_VERSION = True;
if IS_DEV_VERSION:
    sys.path.append("/usr/lib/python3.8/tkinter");
    
# tkinter = __import__("/usr/lib/python3.8/tkinter/__init__.py", fromlist="*")

from tkinter.ttk import *
from tkinter import *

os_id = 'windows'

# if True:
if platform.startswith('win32') or platform.startswith('cygwin'):  # Set the variables for Windows:
    directory = getenv("localappdata") + "Low\\Kettle3D\\Kettle3D"
    os_id = 'windows'
if platform.startswith('darwin'):  # Set the variables for macOS:
    directory = getenv("HOME") + "/Library/Application Support/Kettle3D/Kettle3D"
    os_id = 'macos'
if platform.startswith('linux'):  # Set the varialbes for Linux:
    directory = getenv('HOME') + "/.config/unity3d/Kettle3D/Kettle3D"
    os_id = 'linux'
else:  # OS isn't supported, show a message box, assume it's UNIX-based, because this thing picks up on Windows systems.
    tk = Tk();
    tk.title('Kettle3D');
    Label(tk, text="Sorry, but your operating system doesn't support Kettle3D.").pack();
    Label(tk, text="Please report this as a bug with the button below.").pack();
    Button(tk, text="Report a bug",
           command=lambda: os.system("xdg-open https://github.com/Kettle3D/Kettle3D/issues")).pack();
    while True:
        tk.update();

if os.path.exists(directory + normpath("/launcherprefs")):
    file = open(directory + normpath("/launcherprefs"), 'rb')
    prefs = pickle.load(file)
    file.close()
else:
    file = open(directory + normpath("/launcherprefs"), 'xb')
    prefs = {
        "stability-level": "stable",
        "version": version,
        "mods": mods
    }
    file.close()
    
mods = prefs['mods']
version = prefs['version']
    
argn = 0

while argn < len(args) - 1:
    arg = args[argn]
    if arg == "-m":
        mods.append(args[argn + 1])
        argn += 2
    elif arg == "-v":
        version = args[argn + 1]
        argn += 2
    elif arg == "-g":
        if args[argn + 1] in true:
            gui = True
        elif args[argn + 1] in false:
            gui = False
        else:
            gui = bool(args[argn + 1])
        argn += 2
    else:
        print("Invalid argument: %s" % arg)
        argn += 1

class file_dummy():
    def open(self, a=None, b=None, c=None):
        pass

    def close(self):
        pass

    def read(self):
        pass

    def write(self):
        pass


class txtfile():
    def __init__(self, path, version, newcontent=None):  # file for download
        self.path = path
        self.version = version
        self.winpath = normpath(self.path)
        self.newcontent = newcontent
        print("Looking for file %s..." % path)
        try:
            self.newcontent = open(directory + self.winpath, 'w')
            self.oldcontent = open(directory + self.winpath, 'r')
            print("File %s found successfully." % path)
            try:
                self.onlinecontent = urlopen(
                    "https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/" + path).read().decode('utf-8')
                if self.oldcontent != self.onlinecontent and self.onlinecontent != None and self.onlinecontent.strip() != '':
                    self.newcontent.write(self.onlinecontent)
                    print("Successfully updated file.")
                else:
                    print("File matches.")
            except URLError:
                print("Couldn't update file. Maybe try checking your internet connection?")
        except(FileNotFoundError, OSError):
            self.newcontent = open(directory + self.winpath, 'x')
            print("File %s created successfully." % path)
            try:
                self.onlinecontent = urlopen(
                    "https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/" + path).read().decode('utf-8')
                print("Successfully downloaded file.")
                fae = {
                    "path": self.path,
                    "version": self.version
                }
                files["txt"].append(fae)
                self.newcontent.write(self.onlinecontent)
            except URLError:
                print("Couldn't download file. Maybe try checking your internet connection?")
        finally:
            self.newcontent.close()


class binaryfile():
    def __init__(self, path, version):  # file for download
        self.path = path
        self.version = version
        self.winpath = normpath(self.path)
        print("Looking for file %s..." % path)
        try:
            self.newcontent = open(directory + self.winpath, 'wb')
            self.oldcontent = open(directory + self.winpath, 'rb')
            print("File %s found successfully." % path)
            try:
                self.onlinecontent = urlopen(
                    "https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/" + path).read()
                if self.oldcontent != self.onlinecontent:
                    self.newcontent.write(self.onlinecontent)
                    print("Successfully updated file.")
                else:
                    print("File matches.")
            except URLError:
                print("Couldn't update file. Maybe try checking your internet connection?")
        except(FileNotFoundError, OSError):
            self.newcontent = open(directory + self.winpath, 'xb')
            print("File %s created successfully." % path)
            try:
                self.onlinecontent = urlopen(
                    "https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/" + path).read()
                print("Successfully downloaded file.")
                fae = {
                    "path": self.path,
                    "version": self.version
                }
                files["binary"].append(fae)
                self.newcontent.write(self.onlinecontent)
            except URLError:
                print("Couldn't download file. Maybe try checking your internet connection?")
        finally:
            self.newcontent.close()


# noinspection PyPep8Naming
class imagefile:
    def __init__(self, path, localpath=""):  # Image for download
        if localpath == "":
            localpath = path;
        self.path = path
        self.version = version
        localpath = normpath(localpath)
        self.localpath = localpath
        print("Looking for file %s" % path)
        try:
            img_data = urlopen("https://github.com/Kettle3D/Kettle3D/raw/C%23/" + path).read()
            with open(directory + localpath, 'wb') as handler:
                handler.write(img_data)
                handler.close()
        except URLError:
            print("Couldn't download file. Maybe try checking your internet connection?")


class tkdummy:
    def __init__(self):
        pass

    def destroy(self):
        pass

    def update(self):
        pass

    def update_idletasks(self):
        pass

    pass


# This is where we need to do all the updating stuff:

internet = True
try:
    versions_txt = urlopen("https://github.com/Kettle3D/Kettle3D/raw/C%23/version-data/versions").read().decode('utf-8')
except URLError:
    internet = False
version_url_map  = {}
version_list = []

def visitwebsite():
    if os_id == 'macos':
        os.system("open https://kettle3d.github.io/download")
    elif os_id == 'windows':
        os.system("https://kettle3d.github.io/download")
    elif os_id == 'linux':
        os.system("xdg-open https://kettle3d.github.io/download")
    else:
        'Your OS does not support Kettle3D.'
        complain

if internet:
    if urlopen("https://github.com/Kettle3D/Kettle3D/raw/C%23/version-data/mslv").read().decode('utf-8').strip() != '1.0-bugfix1':
        vns_tk = Tk()
        vns_tk.title("Kettle3D Launcher")
        vns_tk.wm_attributes('-topmost', 1)
        Label(vns_tk, text="Your version of Kettle3D is no longer supported.").pack()
        Label(vns_tk, text="Click the button below to go to the downloads page.").pack()
        Button(vns_tk, text="Update", command=visitwebsite).pack()
        vns_tk.update()
        vns_tk.wm_attributes('-topmost', 0)
        while True:
            try:
                vns_tk.update()
            except:
                break

# w - windows, l - linux, m - macos, v - stable version, d - deprecated version, s - stable version, a - alpha, b - beta

for line in versions_txt.splitlines():
    if line.startswith("#") or line.strip() == "":
        continue
    words = line.split(' ')
    if len(words) < 3:
        continue
    word1 = words[0]
    word2 = words[1]
    
    del words[0:2]
    
    word3 = ""
    
    for i in range(0, len(words)):
        word = words[i]
        word3 += word.replace("{os}", os_id)
    
    p_sl = prefs['stability-level']
    
    # Check if the version is stable enough for the user, and if not, throw it out:
    if  ('d' in word1 and p_sl == 'stable') or \
        ('a' in word1 and p_sl == 'stable') or \
        ('a' in word1 and p_sl == 'safe') or \
        ('b' in word1 and p_sl == 'stable'):
        continue
        
    if os_id == 'windows':
        olosid = 'w'
    elif os_id == 'macos':
        olosid = 'm'
    elif os_id == 'linux':
        olosid = 'l'
    else:
        raise Exception("Your OS does not support Kettle3D.")
    # Check if have a copy of the version for the user's OS and if not, throw it out:
    if  ('m' in word1 or 'l' in word1 or 'w' in word1):
        if not (olosid in word1):
            continue
    version_url_map[word2] = word3
    version_list.append(word2)
            
    if 'v' in word1:
        if not 'latest' in version_url_map:
            version_url_map['stable'] = word3
            
    if 's' in word1:
        version_url_map['stable'] = word3
        if not 'latest' in version_url_map:
            version_url_map['latest'] = word3
        
    if 'b' in word1:
        version_url_map['beta'] = word3
        if not 'latest' in version_url_map:
            version_url_map['latest'] = word3
        else:
            if version_url_map['latest'] == version_url_map['stable']:
                version_url_map['latest'] = word3
    
    if 'a' in word1:
        version_url_map['alpha'] = word3
        version_url_map['latest'] = word3
        
if not 'latest' in version_url_map:
    version_url_map['latest'] = version_url_map[max(list(version_url_map.keys()))]
    
if 'latest' in version_url_map:
    version_list.append('latest')
if 'stable' in version_url_map:
    version_list.append('stable')
if 'beta' in version_url_map:
    version_list.append('beta')
if 'alpha' in version_url_map:
    version_list.append('alpha')
        
if not version in version_url_map:
    print("Warning: The version ('%s') you picked isn't available for your platform." % version)
    version = 'stable'
    
display_version = copy.deepcopy(version)
d_version_list = copy.deepcopy(version_list)

for c in range(0, len(version_list)):
    i = version_list[c]
    if i == "stable" or i == "beta" or i == "latest" or i == "alpha":
        for q in version_list:
            if q in ('latest','stable','beta','alpha'):
                continue
            if version_url_map[i] == version_url_map[q]:
                if i == "stable":
                    i = "Latest Release (%s)" % q
                    if version == 'stable':
                        display_version = "Latest Release (%s)" % q
                    break
                if i == "latest":
                    i = "Latest Snapshot (%s)" % q
                    if version == 'latest':
                        display_version = "Latest Snapshot (%s)" % q
                    break
                if i == "alpha":
                    i = "Latest Unstable (%s)" % q
                    if version == 'alpha':
                        display_version = "Latest Snapshot (%s)" % q
                    break
                if i == "beta":
                    i = "Public Beta (%s)" % q
                    if version == 'beta':
                        display_version = "Public Beta (%s)" % q
                    break
    d_version_list[c] = i

from download_version import *

global play_tk
play_tk = tkdummy()
tk = tkdummy()
if gui:
    tk = Tk()
    tk.title("Kettle3D Launcher")
    tk.wm_attributes("-topmost", 1)
    tk.configure(bg='#47ad73')
    canvas = Canvas(tk, width=500, height=500, bd=0, highlightthickness=0)
    canvas.pack()
    tk.update()
    tk.wm_attributes("-topmost", 0)
    launcherbackground = PhotoImage(file=os.getenv("SNAP") + "/logo_k3d.png")
else:
    print("Checking for updates and launching version %s..." % version)

def check_diff(vsn='latest'):
    if internet:
        diff = urlopen("https://github.com/Kettle3D/Kettle3D/raw/C%23/version-data/core-updates").read().decode('utf-8')
    else:
        diff = ""
    versions = ['0']
    for v in version_list:
        if v == vsn:
            break
        versions.append(v)
    return find_patch(versions, diff)

def launch(vsn):
    if os_id == "macos":
        os.system("chmod +x " + directory + "/versions/" + vsn + "/MacOS/Kettle3D")
        os.system(directory + "/versions/" + vsn + "/MacOS/Kettle3D")
        pass

    elif os_id == "windows":
        os.system(directory + "\\versions\\" + vsn + "\\Kettle3D.exe")
        pass

    elif os_id == "linux":
        os.system("chmod +x " + directory + "/versions/" + vsn + "/Kettle3D-Linux.x86_64")
        os.system(directory + "/versions/" + vsn + "/Kettle3D-Linux.x86_64")
        pass

    else:
        print("Your OS isn't supported.")
        complain

def play():
    special_versions = [
        'latest',
        'stable',
        'beta',
        'alpha'
    ]
    
    m_version = version
    if version in special_versions:
        for v in version_list:
            if version_url_map[v] == version_url_map[version]:
                if not v in special_versions:
                    m_version = v
                    break
    
    nr_tk = Tk()
    nr_tk.title("Downloading Kettle3D...")
    nr_tk.wm_attributes('-topmost', 1)
    label = Label(nr_tk, text="Finding out what we need to download...")
    print("Finding out what we need to download...")
    label.pack()
    #nr_tk.update()
    nr_tk.wm_attributes('-topmost', 0)
    nr_tk.update()
    patch = check_diff(m_version)
    label.destroy()
    Canvas(nr_tk, width=500, height=0).pack()
    label = Label(nr_tk, text="Downloading some files...")
    print("Downloading some files...")
    label.pack()
    if not os.path.exists(directory + "/versions/" + m_version + "/confirmed_complete"):
        g_apply_patch(patch, nr_tk, version_url_map[m_version], directory + "/versions/" + m_version)
        f = open(directory + "/versions/" + m_version + "/confirmed_complete", 'w+t')
        f.write("This version of Kettle3D was installed authentically. Do not delete this file, or Kettle3D will re-install the version.")
        f.close()
    label.destroy()
    label = Label(nr_tk, text="Installing mods...")
    label.pack()
    nr_tk.update()
    print("Installing mods...")
    get_mods(mods, directory + normpath("/modules"))
    label.destroy()
    label = Label(nr_tk, text="Launching Kettle3D...")
    print("Launching Kettle3D...")
    label.pack()
    nr_tk.update()
    launch(m_version)
    nr_tk.destroy()

def options():
    if gui:
        global tk, s_version, s_stability_level
        tk.destroy()
        tk = Tk()
        tk.title('Kettle3D Launcher')
        tk.wm_attributes("-topmost", 1)
        tk.configure(bg='#47ad73')
        Label(tk, text='Settings', font=('sans-serif', 18), foreground='#ffffff', background='#47ad73').pack()
        s_version = StringVar()
        s_version.set(display_version)
        Label(tk, text='Version:', font=('sans-serif', 14), foreground='#ffffff', background='#47ad73').pack()
        Combobox(tk, textvariable=s_version, values=d_version_list, state='readonly').pack()
        s_stability_level = StringVar()
        s_stability_level.set(prefs['stability-level'])
        Checkbutton(tk, text="Enable Snapshots", variable=s_stability_level, onvalue='safe', offvalue='stable').pack()
        Label(tk, text="Snapshots are pre-released versions that are being bug-tested, and therefore are not guaranteed to be stable.",
            font=('sans-serif', 12), foreground='#ffffff', background='#47ad73').pack()
#        Canvas(tk, height=400, width=0, background='#47ad73',).pack()
        
        Button(tk, text="Done", command=unoptions).pack()
        tk.update()
        tk.wm_attributes("-topmost", 0)
        
def unoptions():
    if gui:
        # Globalise the variables
        
        # Save the settings
        vim = version
        v = s_version.get()
        for i in range(0, len(version_list)):
            if v == d_version_list[i]:
                vim = version_list[i]
        prefs['stability-level'] = s_stability_level.get()
        prefs['verison'] = vim
        version = vim
        
        # Do the GUI stuff
        global tk, launcherbackground
        tk.destroy()
        tk = Tk()
        tk.title("Kettle3D Launcher")
        tk.wm_attributes("-topmost", 1)
        tk.configure(bg='#47ad73')
        canvas = Canvas(tk, width=500, height=500, bd=0, highlightthickness=0)
        canvas.pack()
        tk.update()
        tk.wm_attributes("-topmost", 0)
        launcherbackground = PhotoImage(file=directory + "/modules/default/textures/k3d.png")
        Button(tk, text="PLAY", command=play).pack()
        Button(tk, text="Settings", command=options).pack()
        backgroundImage = canvas.create_image(0, 0, image=launcherbackground, anchor=NW)

if gui:
    Button(tk, text="PLAY", command=play).pack()
    Button(tk, text="Settings", command=options).pack()
    backgroundImage = canvas.create_image(0, 0, image=launcherbackground, anchor=NW)
    
    while True:
        try:
            tk.update_idletasks()
            tk.update()
        except:
            break
        time.sleep(0.01)
        
prefs['version'] = version
prefs['mods'] = mods
        
file = open(directory + normpath("/launcherprefs"), 'wb')
pickle.dump(prefs, file)
file.close()
