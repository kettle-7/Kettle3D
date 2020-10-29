# A utility for finding out what files to download and then downloading them

from urllib.request import urlopen
from os.path import normpath
from urllib.error import *
from sys import platform
from tkinter import *
import os.path
import copy
import time

false = False
true = True

variables = {}

if platform.startswith('win32') or platform.startswith('cygwin'):
    os_id = 'windows'
elif platform.startswith('darwin'):
    os_id = 'macos'
elif platform.startswith('linux'):
    os_id = 'linux'
else:
    "Your OS is not supported."
    complain

def IndexOf(array, item):
    for i in range(0, len(array)):
        if array[i] == item:
            return i
    return -1
    
def parse(string):
    if string in variables:
        return variables[string]
    else:
        return string

def removeTrailingBackslash(string):
    out = ""
    for i in range(0, len(string) - 1):
        out += string[i]
    return out

def find_patch(versions, diff):
    lines = diff.splitlines()
    version = "0"
    ignored = False
    files = []
    for line in lines:
        if line.startswith('#'):
            continue
            
        if not version in versions:
            break
        
        elif line.startswith('linux since'):
            if os_id == 'linux':
                ignored = False
            else:
                ignored = True
            version = ""
            for i in range(12, len(line)):
                version += line[i]
        
        elif line.startswith('macos since'):
            if os_id == 'macos':
                ignored = False
            else:
                ignored = True
            version = ""
            for i in range(12, len(line)):
                version += line[i]
        
        elif line.startswith('windows since'):
            if os_id == 'windows':
                ignored = False
            else:
                ignored = True
            version = ""
            for i in range(14, len(line)):
                version += line[i]
        
        elif not ignored:
            args = line.strip().split(' ')
            command = copy.deepcopy(args[0])
            del args[0]
            
            if command == "add":
                sl = ""
                for word in args:
                    if sl == "":
                        sl = parse(word)
                    elif sl.endswith("\\"):
                        sl = removeTrailingBackslash(sl) + " " + parse(word)
                    else:
                        sl += parse(word)
                if not sl in files:
                    files.append(sl)
                
            elif command == "remove" or command == "rm" or command == "del":
                sl = ""
                for word in args:
                    if sl == "":
                        sl = parse(word)
                    elif sl.endswith("\\"):
                        sl = removeTrailingBackslash(sl) + " " + parse(word)
                    else:
                        sl = removeTrailingBackslash(sl) + parse(word)
                if sl in files:
                    del files[IndexOf(files, sl)]
                    
            elif command == "set":
                if len(args) == 0:
                    print(variables)
                elif len(args) == 1:
                    variables[args[0]] = None
                elif len(args) == 2:
                    variables[args[0]] = ""
                else:
                    name = args[0]
                    del args[0:2]
                    sl = ""
                    for word in args:
                        if sl == "":
                            sl = parse(word)
                        elif sl.endswith("\\"):
                            sl = removeTrailingBackslash(sl) + " " + parse(word)
                        else:
                            sl += parse(word)
                    variables[name] = sl
            elif command == "unset":
                sl = ""
                for word in args:
                    if sl == "":
                        sl = parse(word)
                    elif sl.endswith("\\"):
                        sl = removeTrailingBackslash(sl) + " " + parse(word)
                    else:
                        sl += parse(word)
                if sl in variables:
                    del variables[sl]
                        
    return files
    
def md(folder):
    if os.path.exists(folder):
        return
    if not os.path.exists(os.path.dirname(folder)):
        md(os.path.dirname(folder))
    os.mkdir(folder)
    
def downloadfile(path, localpath=""):
    if localpath == "":
        localpath = path;
    localpath = normpath(localpath)
    path = path.replace(' ', '%20')
    p = os.path.dirname(localpath)
    md(p)
    
    try:
        data = urlopen(path).read()
        with open(localpath, 'w+b') as handler:
            handler.write(data)
            handler.close()
    except URLError: # No Internet or HTTP 404
        return
    except Exception as error:
        print("Exception:")
        print(error)
        
def getmodmap(repo_data, debug=False):
    modmap = {}
    repos  = {}
    for line in repo_data.splitlines():
        if line.startswith("#"):
            continue
        else:
            args = line.split(' ')
            command = copy.deepcopy(args[0])
            del args[0]
            
            if command == 'repo':
                if len(args) < 2:
                    if debug:
                        'Ignoring...'
                    continue
                url = ""
                name = ""
                for word in range(1, len(args)):
                    if name == "":
                        name = args[word]
                    else:
                        name += " " + args[word]
                url = args[0]
                repos[name] = url
                if debug:
                    print("There is a repo called %s at the URL %s." % (name, url))
            elif command == 'mod':
                if len(args) < 3:
                    continue
                repo = ""
                name = ""
                mode = False
                for word in args:
                    if word == 'in':
                        mode = not mode
                    elif mode == False:
                        if name == "":
                            name = word
                        else:
                            name += " " + word
                    else:
                        if repo == "":
                            repo = word
                        else:
                            repo += " " + word
                if repo in repos:
                    modmap[name] = repos[repo] + "/" + name
                else:
                    print("Module repository %s isn't in our list. Make sure to define your repos at the top." % repo)
    return modmap

def download_mod(url, directory):
    downloadfile(url + "/version", directory + "/version")
    downloadfile(url + "/manifest", directory + "/manifest")
    downloadfile(url + "/assets", directory + "/assets")
    
    f = open(directory + "/assets")
    assets = f.read()
    f.close()
    
    for line in assets.splitlines():
        if line.strip() == "":
            continue
        if line.startswith("#"):
            continue
        downloadfile(url + "/" + line.strip(), directory + "/" + line.strip())

def get_mods(mods, mod_directory):
    repo_data = urlopen("https://github.com/Kettle3D/Kettle3D/raw/C%23/version-data/modules").read().decode('utf-8')
    modmap = getmodmap(repo_data)
    for mod in mods:
        if os.path.exists(os.path.normpath(mod_directory + "/" + mod + "/version")):
            f = open(os.path.normpath(mod_directory + "/" + mod + "/version"))
            vsn = f.read()
            f.close()
            if mod in modmap:
                if vsn == urlopen(modmap[mod] + "/version").read().decode('utf-8').strip():
                    continue
                else:
                    download_mod(modmap[mod], mod_directory + normpath("/" + mod))
            else:
                print("We couldn't find a mod called %s." % mod)
        
        else:
            if mod in modmap:
                download_mod(modmap[mod], mod_directory + normpath("/" + mod))
            else:
                print("We couldn't find a mod called %s." % mod)

def g_apply_patch(patch, tk, version_url, directory):
    for cf in patch:
        l = Label(tk, text="Downloading %s..." % cf)
        l.pack()
        print("Downloading %s..." % cf)
        tk.update()
        downloadfile(version_url + "/" + cf, normpath(directory + "/" + cf))
        l.destroy()
