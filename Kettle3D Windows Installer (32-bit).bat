cd %USERPROFILE%\Downloads
powershell "Invoke-WebRequest https://www.panda3d.org/download/panda3d-1.10.6/Panda3D-SDK-1.10.6.exe -o %USERPROFILE%\Downloads\panda_installer.exe"
panda_installer.exe

cd %USERPROFILE%\AppData\Roaming
md Kettle3D\assets\index
cd Kettle3D
md data
md lib
md versions
powershell "Invoke-WebRequest https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/Kettle3D-updater-win10.py -O kettle3D-updater.py"
powershell "Invoke-WebRequest https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/windows.txt -O osname.txt"
powershell "Invoke-WebRequest https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/Kettle3D.bat -O Kettle3D.bat"
@echo off

set SCRIPT="%TEMP%\%RANDOM%-%RANDOM%-%RANDOM%-%RANDOM%.vbs"

echo Set oWS = WScript.CreateObject("WScript.Shell") >> %SCRIPT%
echo sLinkFile = "%USERPROFILE%\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Kettle3D.lnk" >> %SCRIPT%
echo Set oLink = oWS.CreateShortcut(sLinkFile) >> %SCRIPT%
echo oLink.TargetPath = "%APPDATA%\Kettle3D\Kettle3D.bat" >> %SCRIPT%
echo oLink.Save >> %SCRIPT%

cscript /nologo %SCRIPT%
del %SCRIPT%
ppython kettle3D-updater.py
