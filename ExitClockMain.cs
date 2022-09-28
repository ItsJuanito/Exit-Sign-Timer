//Author: Juan Zaragoza
//Specialty: Graphical UIs
//Mail: zaragoza_9@csu.fullerton.edu

//Program name: Assignment #2
//Programming language: C#
//Date project began: 8-31-2022
//Date of last update: 8-31-2022

//Purpose: A continuation of Assignment #1 however this time the program will use a timer that allows the user
//to speed up or slow down the exit shape. This is the main driver file that runs the form class from ExitClockUI.cs

//Files in this program: ExitClockMain.cs, ExitClockUI.cs, run.sh

//Status: Completed

//This file name: ExitClockMain.cs
//Compile this C# file:
//mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:UI.dll -out:Exit.exe ExitClockMain.cs

//System requirements: Linux system with Bash shell and package mono-complete installed.

using System;
using System.Windows.Forms;

public class ExitClockMain {
  public static void Main() {
    System.Console.WriteLine("This is the main driver for Assignment #2.");
    ExitClockUI UI_form = new ExitClockUI();
    Application.Run(UI_form);
    System.Console.WriteLine("The Assignment #2 program has ended.");
  } //End of Main method
} //End of ExitClockMain class
