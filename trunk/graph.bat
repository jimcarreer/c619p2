@echo off
set gvddir=.\Hades\bin\Debug
set dotexe=.\dot\bin\dot.exe
set outdir=.\graphs
set CPGVD=1;
set RMGVD=1;

if not exist %outdir% MKDIR %outdir%
if not exist %gvddir%\*.gvd GOTO NOFILES

if DEFINED CPGVD echo Copying gvd files from %gvddir%\ to %outdir%\ ...
if DEFINED CPGVD copy %gvddir%\*.gvd %outdir%\

echo Generating graphs from gvd files ... 
for /f %%a IN ('dir /b %gvddir%\*.gvd') do (%dotexe% %gvddir%\%%a -Tjpeg -o %outdir%\%%~na.jpeg)

GOTO DELFILES
:NOFILES
echo There are no gvd files in %gvddir% to generate images of.
PAUSE
GOTO END
:DELFILES
if DEFINED RMGVD echo Deleting gvd files from %gvddir% ...
if DEFINED RMGVD del %gvddir%\*.gvd
:END

