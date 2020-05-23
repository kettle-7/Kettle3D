REM Run this with 'cmd /c main.bat'
REM Try to update the launcher, if this fails load the backup.

set internet = %1
Ping https://raw.githubusercontent.com -n 1 -w 1000
cls
IF errorlevel 1 ( set internet = true ) ELSE  set internet = false

IF !internet! == true powershell "Invoke-WebRequest https://raw.githubusercontent.com/Kettle3D/Kettle3D/master/kettle3d-launcher.py -o %appdata%\Kettle3D\launcher.py"
copy %appdata%\Kettle3D\launcher.py "C:\Program Files\Kettle3D\pyscripts\launcher.py"

REM Launch the Launcher

SET PATH=%PATH%;"C:\Program Files\Kettle3D\runtime\panda\python";"C:\Program Files\Kettle3D\runtime\panda\bin";
ppython "C:\Program Files\Kettle3D\pyscripts\launcher.py"
