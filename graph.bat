 @echo off
set gvddir=.\Hades\bin\Debug
set dotexe=.\dot\bin\dot.exe
set outdir=.\graphs

if not exist %outdir% MKDIR %outdir%
if not exist %gvddir%\*.gvd GOTO NOFILES

echo Generating graphs from gvd files ... 
for /f %%a IN ('dir /b %gvddir%\*.gvd') do (%dotexe% %gvddir%\%%a -Tjpeg -o %outdir%\%%~na.jpeg)

GOTO END
:NOFILES
echo There are no gvd files in \Hades\bin\Debug to generate images of.
PAUSE
:END
