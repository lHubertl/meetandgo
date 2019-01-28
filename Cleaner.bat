rem Clearing obj and bin folders
for /d /r . %%d in (bin,obj) do @if exist "%%d" rd /s/q "%%d"

rem Clearing *.suo, *.bak, *.orig files
rem del /S /F /AH *.suo
del /S /F /AH *.bak
rem del /S /F /AH *.orig