@echo off
REM This script unregisters the documentation in target MS Help Viewer
REM help system and makes it context sensitive in Visual Studio.
REM You should call the following lines or this script during
REM installation of your product on the target machine.


REM - get processor type and get registry key
IF "%PROCESSOR_ARCHITECTURE%"=="AMD64" (
	REM Architecture type is AMD64
	set ROOT_KEY_NAME=HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Help
) else (
	REM Architecture type is x86
	set ROOT_KEY_NAME=HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Help
)
setlocal ENABLEEXTENSIONS


:MSHV1
REM ************* VS 2010
REM Determine whether MS Help Viewer 1.x is installed (both versions 1.0 and 1.1 use v1.0 registry key)
set KEY_NAME=%ROOT_KEY_NAME%\v1.0
set VALUE_NAME=AppRoot
for /F "usebackq tokens=2*" %%A IN (`reg query "%KEY_NAME%" /v "%VALUE_NAME%" 2^>nul ^| find "%VALUE_NAME%"`) do (
  set MSHV1_ROOT_PATH=%%B
)
if not defined MSHV1_ROOT_PATH (
	goto MSHV20
)
REM MS Help Viewer 1.x is present, unregister the doc from it
"%~dp0HelpLibManagerLauncher.exe" /product "VS" /version "100" /uninstall /silent /vendor "VendorName" /mediaBookList "SISPTD Reference" /productName "SISPTD Reference" > NUL


:MSHV20
REM ************* VS 2012
REM Determine whether MS Help Viewer 2.0 is installed
set KEY_NAME=%ROOT_KEY_NAME%\v2.0
set VALUE_NAME=AppRoot
for /F "usebackq tokens=2*" %%A IN (`reg query "%KEY_NAME%" /v "%VALUE_NAME%" 2^>nul ^| find "%VALUE_NAME%"`) do (
  set MSHV20_ROOT_PATH=%%B
)
if not defined MSHV20_ROOT_PATH (
	goto MSHV21
)
REM MS Help Viewer 2.0 is present, unregister the doc from it

REM Get installed locale of VisualStudio11 catalog
set KEY_NAME=%KEY_NAME%\Catalogs\VisualStudio11
REM Set SUB_KEYS to all lines with sub-keys (that have a locale at the end)
for /f "tokens=*" %%a in ('reg query "%KEY_NAME%" ^| find "%KEY_NAME%\"') do (set SUB_KEYS=%%a)
REM Extract the locale after the last \ character.
for %%a in (%SUB_KEYS%) do set locale=%%~nxa
IF NOT defined locale (
	SET locale=en-US
)	

"%MSHV20_ROOT_PATH%HlpCtntmgr.exe" /operation uninstall /catalogName VisualStudio11 /locale %locale% /vendor "VendorName"  /productName "SISPTD Reference" /bookList "SISPTD Reference" /wait 0
echo HlpCtntmgr.exe uninstall exit code=%ERRORLEVEL%


:MSHV21
REM ************* VS 2013
REM Determine whether MS Help Viewer 2.1 is installed
set KEY_NAME=%ROOT_KEY_NAME%\v2.1
set VALUE_NAME=AppRoot
for /F "usebackq tokens=2*" %%A IN (`reg query "%KEY_NAME%" /v "%VALUE_NAME%" 2^>nul ^| find "%VALUE_NAME%"`) do (
  set MSHV21_ROOT_PATH=%%B
)
if not defined MSHV21_ROOT_PATH (
	goto MSHV22
)
REM MS Help Viewer 2.1 is present, unregister the doc from it

REM Get installed locale of VisualStudio12 catalog
set KEY_NAME=%KEY_NAME%\Catalogs\VisualStudio12
REM Set SUB_KEYS to all lines with sub-keys (that have a locale at the end)
for /f "tokens=*" %%a in ('reg query "%KEY_NAME%" ^| find "%KEY_NAME%\"') do (set SUB_KEYS=%%a)
REM Extract the locale after the last \ character.
for %%a in (%SUB_KEYS%) do set locale=%%~nxa
IF NOT defined locale (
	SET locale=en-US
)	

"%MSHV21_ROOT_PATH%HlpCtntmgr.exe" /operation uninstall /catalogName VisualStudio12 /locale %locale% /vendor "VendorName"  /productName "SISPTD Reference" /bookList "SISPTD Reference" /wait 0
echo HlpCtntmgr.exe uninstall exit code=%ERRORLEVEL%


:MSHV22
REM ************* VS 2015
REM Determine whether MS Help Viewer 2.2 is installed
set KEY_NAME=%ROOT_KEY_NAME%\v2.2
set VALUE_NAME=AppRoot
for /F "usebackq tokens=2*" %%A IN (`reg query "%KEY_NAME%" /v "%VALUE_NAME%" 2^>nul ^| find "%VALUE_NAME%"`) do (
  set MSHV22_ROOT_PATH=%%B
)
if not defined MSHV22_ROOT_PATH (
	goto MSHV_CONTINUE
)
REM MS Help Viewer 2.2 is present, unregister the doc from it

REM Get installed locale of VisualStudio14 catalog
set KEY_NAME=%KEY_NAME%\Catalogs\VisualStudio14
REM Set SUB_KEYS to all lines with sub-keys (that have a locale at the end)
for /f "tokens=*" %%a in ('reg query "%KEY_NAME%" ^| find "%KEY_NAME%\"') do (set SUB_KEYS=%%a)
REM Extract the locale after the last \ character.
for %%a in (%SUB_KEYS%) do set locale=%%~nxa
IF NOT defined locale (
	SET locale=en-US
)	

"%MSHV22_ROOT_PATH%HlpCtntmgr.exe" /operation uninstall /catalogName VisualStudio14 /locale %locale% /vendor "VendorName"  /productName "SISPTD Reference" /bookList "SISPTD Reference" /wait 0
echo HlpCtntmgr.exe uninstall exit code=%ERRORLEVEL%


:MSHV_CONTINUE

