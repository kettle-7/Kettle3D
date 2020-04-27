from urllib.request import urlopen

launcherprogramming = urlopen("https://Kettle3D.github.io/Kettle3D/kettle3D-versions.py").read().decode('utf-8')

launcher = open("C:\\Program Files\\Kettle3D\\Kettle3Dlaunch.py", 'w')
launcher.write(launcherprogramming)
launcher.close()

import Kettle3Dlaunch