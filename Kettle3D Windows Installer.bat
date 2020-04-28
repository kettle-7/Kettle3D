cd %USERPROFILE%\AppData\Roaming
md Kettle3D\assets\index
md data
md lib
md versions
cd C:\Program Files
md Kettle3D
cd C:\Program Files\Kettle3D
python -m wget
echo off
title Custom Text File
cls
set /p txt=%USERPROFILE%; 
echo %txt% > "C:\Program Files\Kettle3D\dir.txt"
exit
