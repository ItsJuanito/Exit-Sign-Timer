//Author: Juan Zaragoza
//Specialty: Graphical UIs
//Mail: zaragoza_9@csu.fullerton.edu

//Program name: Assignment #2
//Programming language: C#
//Date project began: 8-31-2022
//Date of last update: 8-31-2022

//Purpose: An extension of Assignment #1 where we are supposed to create a simple UI containing an exit sign.
//And now we must use a timer to make our exit message flicker slowly or fast depending on the user's choice.
//Also the program uses three panels which completely cover the form behind the panels. Lastly the control
//panel contains 3 buttons, the start/pause/resume button, the fast/slow button, and the quit button.

//Files in this program: ExitClockMain.cs, ExitClockUI.cs, run.sh

//Status: Completed

//This file name: ExitClockUI.cs
//Compile this C# file:
//mcs -target:library -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:UI.dll ExitClockUI.cs

//System requirements: Linux system with Bash shell and package mono-complete installed.

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class ExitClockUI : Form
{  //Declare attributes of the whole form.
   private const int formwidth = 1000;  //Horizontal width of the user interface.
   private const int formheight = 800;  //Vertical height of the user interface.
   private Size ui_size = new Size(formwidth,formheight);

   //Declare attributes of Header label.
   private String header_message_text = "Welcome to Assignment #1";
   private Label header_message = new Label();
   private Point header_message_location = new Point(100,5);
   private Font  header_message_font = new Font("Arial",15,FontStyle.Bold);
   private Size  header_message_size = new Size(200,40);

   //Declare attributes of Exit label
   private String exit_message_text = "EXIT";
   private Label exit_message = new Label();
   private Point exit_message_location = new Point(0,0);
   private Font  exit_message_font = new Font("Arial",58,FontStyle.Bold);
   private Size  exit_message_size = new Size(200,10);

   //Declare attributes of Header panel
   private const int header_panel_height = 60;
   private Panel header_panel = new Panel();
   private Point header_panel_location = new Point(0,0);
   private Size  header_panel_size = new Size(formwidth,header_panel_height);
   private Color header_panel_color = Color.Orange;

   //Declare attributes of Graphic panel
   private const int graphic_panel_height = 600;
   private Graphic_panel drawingpane = new Graphic_panel();
   private Point graphic_panel_location = new Point(0,header_panel_height);
   private Size  graphic_panel_size = new Size(formwidth,graphic_panel_height);
   private Color graphic_panel_color = Color.Navy;

   //Declare attributes of the Control panel.
   private const int control_panel_height = formheight-header_panel_height-graphic_panel_height;
   private Panel control_panel = new Panel();
   private Point control_panel_location = new Point(0,header_panel_height+graphic_panel_height);
   private Size  control_panel_size = new Size(formwidth,control_panel_height);
   private Color control_panel_color = Color.Orange;   //Color.LightYellow;

   //Declare attributes common to all the buttons that will appear on the Control panel.
   private const int button_height = 40;
   private const int button_width  = 120;
   private Size  button_size = new Size(button_width,button_height);

   //Declare attributes of the start button that will appear on the Control panel.
   private Color start_button_color = Color.Navy;
   private Point start_button_location = new Point(100,20); //x, y
   private Button start_button = new Button();

   //Declare attributes of the speed button that will appear on the Control panel.
   private Color speed_button_color = Color.Navy;
   private Point speed_button_location = new Point(425,20); //x, y
   private Button speed_button = new Button();

   //Declare attributes of the exit button that will appear on the Control panel.
   private Color exit_button_color = Color.Navy;
   private Point exit_button_location = new Point(750,20); //x, y
   private Button exit_button = new Button();

   //Declare some mechanisms for managing the visibility of displayed geometric shapes.
   private static bool start_ellipse_visible = false;

   //Declare attributes for the clock speeds and intervals.
   private static System.Timers.Timer blinker = new System.Timers.Timer();
   private const double fast_clock = 9.0;  //Hz
   private const double slow_clock = 2.0;  //Hz
   //Set the fast and slow intervals
   private const double one_second = 1000.0;  //ms
   private const double fast_interval = one_second / fast_clock;
   private const double slow_interval = one_second / slow_clock;
   //Type case the fast and slow intervals
   private int fast_interval_int = (int)System.Math.Round(fast_interval);
   private int slow_interval_int = (int)System.Math.Round(slow_interval);

   //The constructor of this class
   public ExitClockUI()
   {//Set the attributes of this form.
    Text = "Exit Sign";
    Size = ui_size;
    MaximumSize = ui_size;
    MinimumSize = ui_size;

    //Construct the top panel
    header_message.Text = "Exit Sign by Juan Zaragoza";  //header_message_text;
    header_message.Font = header_message_font;
    header_message.TextAlign = ContentAlignment.MiddleCenter;
    header_message.Size = new Size(800,50);
    header_message.Location = header_message_location;
    header_message.ForeColor = Color.White;
    header_panel.BackColor = header_panel_color;
    header_panel.Size = header_panel_size;
    header_panel.Location = header_panel_location;
    header_panel.Controls.Add(header_message);

    //Construct the middle panel EXIT sign
    exit_message.Text = "Exit";
    exit_message.Font = exit_message_font;
    exit_message.ForeColor = Color.White;
    exit_message.TextAlign = ContentAlignment.MiddleCenter;
    exit_message.Size = new Size(1000, 200);
    exit_message.Location = exit_message_location;
    exit_message.BackColor = Color.Navy;

    //Construct the middle panel also called the "graphic panel".
    drawingpane.BackColor = graphic_panel_color;
    drawingpane.Size = graphic_panel_size;
    drawingpane.Location = graphic_panel_location;

    //Construct the bottom panel also called the "control panel".
    control_panel.BackColor = control_panel_color;
    control_panel.Size = control_panel_size;
    control_panel.Location = control_panel_location;

    //Construct the start button
    start_button.BackColor = start_button_color;
    start_button.Size = button_size;
    start_button.Location = start_button_location;
    start_button.Text = "Start";
    start_button.ForeColor = Color.White;
    start_button.TextAlign = ContentAlignment.MiddleCenter; //MiddleCenter
    start_button.Click += new EventHandler(start_ellipse);
    start_button.TabIndex = 3;
    start_button.TabStop = true;

    //Construct the speed button
    speed_button.BackColor = speed_button_color;
    speed_button.Size = button_size;
    speed_button.Location = speed_button_location;
    speed_button.Text = "Fast";
    speed_button.ForeColor = Color.White;
    speed_button.TextAlign = ContentAlignment.MiddleCenter; //MiddleCenter
    speed_button.Click += new EventHandler(speed);
    speed_button.TabIndex = 3;
    speed_button.TabStop = true;

    //Construct the exit button
    exit_button.BackColor = exit_button_color;
    exit_button.Size = button_size;
    exit_button.Location = exit_button_location;
    exit_button.Text = "Quit";
    exit_button.ForeColor = Color.White;
    exit_button.TextAlign = ContentAlignment.MiddleCenter;
    exit_button.Click += new EventHandler(terminate_execution);
    exit_button.TabIndex = 4;
    exit_button.TabStop = true;

    //Add Exit message to the middle panel
    drawingpane.Controls.Add(exit_message);

    //Add buttons to the control panel
    control_panel.Controls.Add(start_button);
    control_panel.Controls.Add(speed_button);
    control_panel.Controls.Add(exit_button);

    //Add panels to the UI form
    Controls.Add(header_panel);
    Controls.Add(drawingpane);
    Controls.Add(control_panel);

    //Initialize the blinker attributes and events
    blinker.Enabled = false;
    blinker.Elapsed += new ElapsedEventHandler(blinker_event);
    blinker.Interval = slow_interval_int;

   }//End of constructor

   //Method to execute when the start ellipse button receives a mouse click
   protected void start_ellipse(Object sender, EventArgs h) {
     //Check to see if the arrow is visible/present
     if(start_ellipse_visible) {
        //Change button text to "Resume"
        start_button.Text = "Resume";
        //Keeps the arrow from being visible
        start_ellipse_visible = false;
        //Makes the blinker stop
        blinker.Enabled = false;
    } else {
        //Change button text to "Pause"
        start_button.Text = "Pause";
        //Makes the arrow visible
        start_ellipse_visible = true;
        //Makes the blinker start
        blinker.Enabled = true;
       }
    drawingpane.Invalidate();
  }//End of method start_ellipse

  //Method to execute when the speed button receives a mouse click
  protected void speed(Object sender, EventArgs h) {
    //Check to see if the arrow is visible/present
    if(start_ellipse_visible) {
       //Change button text to "Fast"
       speed_button.Text = "Fast";
       //Makes the blinker go slower
       blinker.Interval = slow_interval_int;
    } else {
       //Change button text to "Slow"
       speed_button.Text = "Slow";
       //Makes the blinker go faster
       blinker.Interval = fast_interval_int;
      }
   drawingpane.Invalidate();
 }//End of method speed

  //Method to let the timer(blinker) start and end
  protected void blinker_event(System.Object sender, ElapsedEventArgs evt) {
    if(start_ellipse_visible) {
      //Removes the arrow
      start_ellipse_visible = false;
    } else {
      //Adds the arrow
      start_ellipse_visible = true;
    }
    drawingpane.Invalidate();
  } //End of method blinker_event

   //Method that terminates the program "Quit button"
   protected void terminate_execution(Object sender, EventArgs i)
   {System.Console.WriteLine("This program will end execution.");
    Close();
   }

   public class Graphic_panel: Panel            //Class Graphicpanel inherits from class Panel
   {public Graphic_panel()                      //Constructor for derived class Graphicpanel
    {Console.WriteLine("A graphic enabled panel was created");
    }//End of constructor

    //The next method is the OnPaint that belongs to the middle panel only.  The outputs from this
    //method are located according to the local Cartesian system of that middle panel.  The draw
    //methods called inside of this OnPaint can only output onto the middle panel alone.
    protected override void OnPaint(PaintEventArgs ee)
    {Graphics graph = ee.Graphics;

     // filled elllipse visible for the top of the arrow
     if(start_ellipse_visible) graph.FillEllipse(Brushes.Orange, 690, 200, 25, 25); // x, y, w, h
     if(start_ellipse_visible) graph.FillEllipse(Brushes.Orange, 750, 250, 25, 25); // x, y, w, h
     if(start_ellipse_visible) graph.FillEllipse(Brushes.Orange, 810, 300, 25, 25); // x, y, w, h
     // filled elllipse visible for the middle of the arrow
     if(start_ellipse_visible) graph.FillEllipse(Brushes.Orange, 120, 350, 25, 25); // x, y, w, h
     if(start_ellipse_visible) graph.FillEllipse(Brushes.Orange, 190, 350, 25, 25); // x, y, w, h
     if(start_ellipse_visible) graph.FillEllipse(Brushes.Orange, 270, 350, 25, 25); // x, y, w, h
     if(start_ellipse_visible) graph.FillEllipse(Brushes.Orange, 360, 350, 25, 25); // x, y, w, h
     if(start_ellipse_visible) graph.FillEllipse(Brushes.Orange, 440, 350, 25, 25); // x, y, w, h
     if(start_ellipse_visible) graph.FillEllipse(Brushes.Orange, 520, 350, 25, 25); // x, y, w, h
     if(start_ellipse_visible) graph.FillEllipse(Brushes.Orange, 600, 350, 25, 25); // x, y, w, h
     if(start_ellipse_visible) graph.FillEllipse(Brushes.Orange, 680, 350, 25, 25); // x, y, w, h
     if(start_ellipse_visible) graph.FillEllipse(Brushes.Orange, 760, 350, 25, 25); // x, y, w, h
     if(start_ellipse_visible) graph.FillEllipse(Brushes.Orange, 840, 350, 25, 25); // x, y, w, h
     // filled elllipse visible fro the bottom of the arrow
     if(start_ellipse_visible) graph.FillEllipse(Brushes.Orange, 810, 400, 25, 25); // x, y, w, h
     if(start_ellipse_visible) graph.FillEllipse(Brushes.Orange, 750, 440, 25, 25); // x, y, w, h
     if(start_ellipse_visible) graph.FillEllipse(Brushes.Orange, 690, 490, 25, 25); // x, y, w, h

     base.OnPaint(ee);
    }//End of OnPaint belonging only to Graph Panel class.
   }//End of derived class Graphicpanel
}//End of class ExitClockUI
