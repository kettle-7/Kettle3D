@echo off
cd %appdata%
md Kettle3D
cd Kettle3D
md assets
md data
md lib
md version

cd %ProgramFiles%
md Kettle3D
cd Kettle3D
md runtime
md scripts
md pyscripts
cd runtime
md panda
md "Unity Hub"

cd %USERPROFILE%\Downloads
powershell "Invoke-WebRequest https://www.panda3d.org/download/panda3d-1.10.6/Panda3D-SDK-1.10.6-x64.exe -o %USERPROFILE%\Downloads\panda_installer.exe"
panda_installer.exe

powershell "Invoke-WebRequest https://github.com/Kettle3D/Kettle3D/raw/master/binaries/windows/Kettle3D.exe -o "C:\Program Files\Kettle3D\Kettle3D.exe""

set SCRIPT="%TEMP%\%RANDOM%-%RANDOM%-%RANDOM%-%RANDOM%.vbs"

echo Set oWS = WScript.CreateObject("WScript.Shell") >> %SCRIPT%
echo sLinkFile = "%USERPROFILE%\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Kettle3D.lnk" >> %SCRIPT%
echo Set oLink = oWS.CreateShortcut(sLinkFile) >> %SCRIPT%
echo oLink.TargetPath = "C:\Program Files\Kettle3D\Kettle3D.exe" >> %SCRIPT%
echo oLink.Save >> %SCRIPT%

cscript /nologo %SCRIPT%
del %SCRIPT%