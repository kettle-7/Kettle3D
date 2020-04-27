from urllib.request import urlopen

launcherprogramming = urlopen("https://Kettle3D.github.io/Kettle3D/kettle3D-versions.py").read().decode('utf-8')

try:
  launcher = open("C:\\Program Files\\Kettle3D\\kettle3D-launcher.py", 'w')
except:
  launcher = open("C:\\Program Files\\Kettle3D\\kettle3D-launcher.py", 'x')

launcher.write(launcherprogramming)
launcher.close()

import kettle3D-launcher
