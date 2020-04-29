# Install Panda

cd $HOME/Downloads
curl -o panda_installer.dmg https://www.panda3d.org/download/panda3d-1.10.6/Panda3D-SDK-1.10.6-MacOSX10.6.dmg
hdiutil attach panda_installer.dmg

# Install Kettle3D
cd $HOME
mkdir Library
cd Library
mkdir "Application Support"
cd "Application Support"
mkdir Kettle3D
cd Kettle3D
mkdir assets
mkdir data
mkdir lib
mkdir versions
curl -o kettle3D-updater.py https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/Kettle3D-updater-win10.py
cd $HOME/Desktop
curl -o Kettle3D.command https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/Kettle3D.sh
