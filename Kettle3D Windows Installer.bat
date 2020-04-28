cd %USERPROFILE%\Downloads
powershell "Invoke-WebRequest https://www.panda3d.org/download/panda3d-1.10.6/Panda3D-SDK-1.10.6-x64.exe -o %USERPROFILE%\Downloads\panda_installer.exe"
panda_installer.exe

cd %USERPROFILE%\AppData\Roaming
md Kettle3D\assets\index
cd Kettle3D
md data
md lib
md versions
cd C:\Program Files
md Kettle3D
cd Kettle3D
powershell "Invoke-WebRequest https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/Kettle3D-updater-win10.py -O kettle3D-updater.py"
powershell "Invoke-WebRequest https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/windows.txt -O osname.txt"
ppython kettle3D-updater.py
