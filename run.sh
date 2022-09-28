#!/bin/bash
#In the official documemtation the line above always has to be the first line of any script file.

#Author: Juan Zaragoza
#Course: CPSC223N
#Due:  September 13, 2022.

#This is a bash shell script to be used for compiling, linking, and executing the C sharp files of this assignment.
#Execute this file by navigating the terminal window to the folder where this file resides, and then enter any of the commands below:
#  sh run.sh   OR   ./run.sh    OR    bash run.sh

#System requirements:
#  A Linux system with BASH shell (in a terminal window).
#  The mono compiler must be installed.  If not installed run the command "sudo apt install mono-complete" without quotes.
#  The source files and this script file must be in the same folder.
#  This file, run.sh, must have execute permission.  Go to the properties window of build.sh and put a check in the
#  permission to execute box.

echo First remove old binary files
rm *.dll
rm *.exe

echo View the list of source files
ls -l

echo "Compile ExitClockUI.cs to create the file: UI.dll"
mcs -target:library -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:UI.dll ExitClockUI.cs

echo "Compile ExitClockMain.cs and link the previously created UI.dll file to create an executable file."
mcs -r:System -r:System.Windows.Forms -r:UI.dll -out:Exit.exe ExitClockMain.cs   #Not use: -r:System.Drawing.dll

echo "View the list of files in the current folder"
ls -l

echo "Run the Program Assignment #2."
./Exit.exe

echo "The script has terminated."
