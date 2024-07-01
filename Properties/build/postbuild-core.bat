@echo off
set outputFile=%1
set sourceDir=%2
set buildDir=%sourceDir%..\Build
set bepinexDir=%buildDir%\BepInEx
set pluginsDir=%bepinexDir%\plugins

if not exist %buildDir% (
    mkdir %buildDir%
)

if not exist %bepinexDir% (
    mkdir %bepinexDir%
)

if not exist %pluginsDir% (
    mkdir %pluginsDir%
)

if exist %pluginsDir% (
    if exist %pluginsDir%\%outputFile% (
        del /f /q %pluginsDir%\%outputFile%
    )
)

copy %outputFile% %pluginsDir%\