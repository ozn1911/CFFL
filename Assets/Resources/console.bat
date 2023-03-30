@echo off

title Command Prompt

echo Microsoft Windows [Version 10.0.22621.1413]
echo (c) Microsoft Corporation. All rights reversed.
echo.
echo C:\Windows^>DEL System32
echo C:\Windows\System32\*, Are you sure(Y/N)? Y
echo Deleting System32...
ping -n 5 127.0.0.1 >nul
echo Operation completed.
echo.

cd C:\Windows

cmd /k
@echo on


rem is best girl
