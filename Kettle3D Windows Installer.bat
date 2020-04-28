cd %USERPROFILE%\Downloads

cd %USERPROFILE%\AppData\Roaming
md Kettle3D\assets\index
md data
md lib
md versions
cd C:\Program Files
md Kettle3D
cd C:\Program Files\Kettle3D
python -m wget https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/Kettle3D-updater-win10.py
echo off
title Custom Text File
cls
set /p txt=%USERPROFILE%; 
echo %txt% > "C:\Program Files\Kettle3D\dir.txt"
exit
