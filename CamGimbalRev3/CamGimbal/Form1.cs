using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Xml.Linq;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using System.Threading;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Net.NetworkInformation;

using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using System.Reflection;


namespace CamGimbal
{
    public partial class Form1 : Form
    {

        GMarkerGoogle marker;
        GMarkerGoogle marker1;
        GMarkerGoogle marker2;
        GMapOverlay markerOverlay;

        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;

        [DllImport("User32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetWindowPos(IntPtr hwnd, long hWndInsertAfter, long x, long y, long cx, long cy, long wFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

        string SelectCam = "C:\\CamVid\\CamSel.txt";
        string Tiltup = "C:\\CamVid\\Tiltup.txt";
        string TiltDwn = "C:\\CamVid\\TiltDwn.txt";
        string PanLft = "C:\\CamVid\\PanLft.txt";
        string PanRht = "C:\\CamVid\\PanRht.txt";
        string ZoomPminus = "C:\\CamVid\\ZoomP.txt";
        string ZoomMplus = "C:\\CamVid\\ZoomM.txt";
        string Track = "C:\\CamVid\\Track.txt";
        string colormapselect= "C:\\CamVid\\Colormap.txt";
        string CodeID = "C:\\CamVid\\CodeID.txt";
        string Tpic = "C:\\CamVid\\Tpic.txt";
        string Vid = "C:\\CamVid\\VVid.txt";
        string closepgm = "C:\\CamVid\\closepgm.txt";
        string GamePad = "C:\\CamVid\\GmPd.txt";        
        string Datalink = "C:\\CamVid\\Datalink.txt";
        string cameraIp= "C:\\CamVid\\CamNet.txt";
        string PCip = "C:\\CamVid\\PcNet.txt";
        string USBDetectR = "C:\\CamVid\\USBDetectR.txt";
        string USBDetectT = "C:\\CamVid\\USBDetectT.txt";

        //string Networkdetect = "Network Equinox Cam";

        string Gamepadd = "GamePad10P";

        string USBDetectRR;
        string USBDetectRr;

        //-------------------gamepad-----------------------------
        string GpadSel = "C:\\CamVid\\Gamepad10X\\gpadEO.txt";
        string Gpadtiltup= "C:\\CamVid\\Gamepad10X\\gpadU.txt";      
        string GpadtiltDwn = "C:\\CamVid\\Gamepad10X\\gpadD.txt";       
        string GpadPanleft = "C:\\CamVid\\Gamepad10X\\gpadlft.txt";       
        string GpadPanrht = "C:\\CamVid\\Gamepad10X\\gpadrht.txt";         
        string GpadZoomminus = "C:\\CamVid\\Gamepad10X\\gpadZmm.txt";
        string DLat="C:\\CamVid\\DLat.txt";
        string DLng = "C:\\CamVid\\DLng.txt";
        string DHdg = "C:\\CamVid\\DHdg.txt";
        string DAlt= "C:\\CamVid\\DAlt.txt";
        string Bdd = "C:\\CamVid\\BD.txt";
        string Bb = "C:\\CamVid\\BB.txt";

        string plss = "C:\\CamVid\\pls.txt";




        string GpadZoomplus = "C:\\CamVid\\Gamepad10X\\gpadZmP.txt";
      
        string Gpadtiltupp = "0";
        string GpadtiltDwnn = "0";
        string GpadPanleftt = "0";
        string GpadPanrhtt = "0";

       
        int x=0;
        int xx = 0;
        int xA=0;
        int xB = 0;
        int xC=0;
        int xD = 0;
        Bitmap imagetwo;
        double lat1;
        double long1;
        int xrw = 0;
        double lat2;
        double lon2;
      
        int head = 0;
        IntPtr appWin1;
        IntPtr appWin2;
        // GMapOverlay markerOverlay;

        int i = 0;
        int PanEncVal = 0; //usthisfrom data protocol
      
        int updwm=0;       
        int Eozoom = 0;
        int BZzoom = 0;
        string EOIR="G";

        double hheight;
        double angleofdepression;
        double distanetofeet2;

        double distanetofeet;
        double distanetofeet1;

        // int cmprHeading = 0;
        //int cmprBearing=0;
        //int cmpHB = 0;
        // double cmprHB1 = 0;
        /*
                public int cmprHeading = 0;
                public int cmprBearing=0;
                public int cmpHB = 0;
                public  double cmprHB1 = 0;
                public int cmprResultVector = 0;
                public double cmprHB = 0;
        */
        // public double cmprPitch = 0;
        // public int enctiltdt = 0;
        // public double cmprPitchResultVector = 0;     

        int cmprHeading = 0;
        int cmprBearing = 0;
        int cmpHB = 0;
        double cmprHB1 = 0;
        int cmprResultVector = 0;
        double cmprHB = 0;
        double Midway = 0;

        int PanEncCurrentPosition = 0;
       
        int PDir = 0;
        int PitchPosition = 0;
        
        double cmprPitch = 0;      
        double cmprPitchResultVector = 0;

        string homelatitude;
        string homelongitude;
        double homelatitudeD;
        double homelongitudeD;

        double rlat1;
        double rlat2;
        double theta;
        double rtheta;
        double dist;


    string Pxdd;
       
       
        string EOIRSel;

        string swzN = "0";
        string swzP = "0";
        string UzP = "0";
        string UzN = "0";

        int xDD = 0;
        int XXDD = 0;

        double iLat;
        double iLong;
        int repoint=0;

        int flag = 0;
        int runPgm = 0;
        int sysReoot = 0;

        int Fwidth = 50;
        int Fheight = 50;

        int RunBtn = 0;

        int homeit = 0;
        int nudge = 0;

        int FOVMin = 0;
        int FOVMax = 0;
        int MidwayCalc = 0;

        double panFactor = 1;
        int BD = 0;
        int bb = 0;

        int pulse = 0;
        public Form1()
        {
            InitializeComponent();

            ProcessStartInfo ps2 = new ProcessStartInfo("C:\\CamVid\\Netwrk\\PingNetwork.exe");
            Process.Start(ps2);

            using (FileStream stream = File.Open(closepgm, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("0");
                }
            }//------

            using (FileStream stream = File.Open(SelectCam, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("G"); 
                }
            }//------

            using (FileStream stream = File.Open(Track, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("S"); 
                }
            }//------
            using (FileStream stream = File.Open(colormapselect, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("");
                }
            }//

            using (FileStream stream = File.Open(Tpic, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("0");

                }
            }//----
            using (FileStream stream = File.Open(Vid, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("4");//video release
                }
            }//-----------
            using (FileStream stream = File.Open(GamePad, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("0");
                                          
                }
            }//-------

            using (FileStream stream = File.Open(USBDetectR, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("00");

                }
            }//-------

            using (FileStream stream = File.Open(USBDetectT, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("0");

                }
            }//-------

            using (FileStream stream = File.Open(Datalink, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("$0000:0000*#000:00000<000.000000>-000.000000*");

                }
            }//-------

            using (FileStream stream = File.Open(plss, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))//finetune
                {
                    objWriter.Write("F");
                    button32.BackColor = Color.LightCoral;
                }
            }

            button32.BackColor = Color.LightCoral;

            stoppanmtrright();
            stopPanMtrLft();
            stopTiltmtrup();
            stopTiltmtrdwn();


        }//------------------------------------------------------------------------------------------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
           lat1 = 39.299236; //baltimore
           long1 = -76.609383;

           // lat1 = 0;
           // long1 = 0;

            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            //gMapControl1.MapProvider = GMapProviders.GoogleChinaSatelliteMap;
            gMapControl1.MapProvider = GMapProviders.GoogleKoreaSatelliteMap;
            gMapControl1.Position = new PointLatLng(lat1, long1);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 2;
            gMapControl1.AutoScroll = true;
            gMapControl1.ShowCenter = false;

           // gMapControl1.Location = new Point(-300, 100);

            markerOverlay = new GMapOverlay("Marker");
             //marker = new GMarkerGoogle(new PointLatLng(lat1, long1), GMarkerGoogleType.red_dot);

            PointLatLng point = new PointLatLng(lat1, long1);//set home position
            Bitmap bmpmarker = (Bitmap)Image.FromFile(@"C:\CamVid\Icon\home1.png");
            GMapMarker marker = new GMarkerGoogle(point, bmpmarker);
            markerOverlay.Markers.Add(marker);
            gMapControl1.Overlays.Add(markerOverlay);
            gMapControl1.UpdateMarkerLocalPosition(marker);

            imagetwo = new Bitmap(@"C:\CamVid\Icon\pln.png");
            pictureBox2.Width = 160;
            pictureBox2.Height = 155;
            pictureBox2.Parent = gMapControl1;
            pictureBox2.Location = new Point(10, 10);

            textBox5.Location = new Point(23, 235);//heading box
            label5.Location = new Point(23, 220);//heading text

            textBox6.Location = new Point(143, 235);//altitude box
            label6.Location = new Point(148, 220);//altitude text

            label12.Location = new Point(54, 238);//deg text
            label13.Location = new Point(125, 238);//fttext

            textBox3.Location = new Point(18, 100);//latitude box
            textBox4.Location = new Point(115, 100);//longitude box

            label3.Location = new Point(28, 125);//lat text
            label4.Location = new Point(146, 125);//lat text


           // tabControl1.Location = new Point(10, 10);//tab

            //richTextBox1.Location = new Point(25, 50);
            label31.Location = new Point(28, 35);//
            label33.Location = new Point(430, 35);//
            label34.Location = new Point(780, 35);//tab

            //textBox3.Parent= pictureBox2;   
            groupBox4.Location = new Point(25, 50);

            comboBox1.Items.Add("Black/White");
            comboBox1.Items.Add("Inverse");
            comboBox1.Items.Add("AUTUMN");
            comboBox1.Items.Add("BONE");
            comboBox1.Items.Add("JET");

            comboBox1.Items.Add("WINTER");
            comboBox1.Items.Add("RAINBOW");
            comboBox1.Items.Add("OCEAN");
            comboBox1.Items.Add("SUMMER");
            comboBox1.Items.Add("SPRING");

            comboBox1.Items.Add("COOL");
            comboBox1.Items.Add("HSV");
            comboBox1.Items.Add("PINK");
            comboBox1.Items.Add("HOT");
            comboBox1.Items.Add("PARULA");

            comboBox1.Items.Add("MAGMA");
            comboBox1.Items.Add("INFERNO");
            comboBox1.Items.Add("PLASMA");
            comboBox1.Items.Add("VIRIDIS");
            comboBox1.Items.Add("CIVIDIS");

            comboBox1.Items.Add("TWILIGHT");
            comboBox1.Items.Add("TWILIGHT_SHIFTED");
            comboBox1.Items.Add("TURBO");
            comboBox1.Items.Add("DEEPGREEN");
            //comboBox1.Items.Add("Black/White");
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersHeight = 20;
            dataGridView1.RowTemplate.Height = 20;
            dataGridView1.ColumnCount = 4;
            dataGridView1.RowCount = 50;
            //this.dataGridView1.Location = new Point(this.dataGridView1.Location.X, 200);
            //this.dataGridView1.Location = new Point(this.dataGridView1.Location.Y, 120);

            DataGridViewColumn column0 = dataGridView1.Columns[0];
            column0.Width = 100;
            DataGridViewColumn column1 = dataGridView1.Columns[1];
            column1.Width = 100;
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            column2.Width = 50;

            timer1.Enabled= false;
            timer2.Enabled= false;
            timer3.Enabled= false;
            timer4.Enabled= false;

            //groupBox7.Size = new Size(230, 80);
            //button14.Size = new Size(101, 23);
            //button16.Size = new Size(50, 23);
           // button17.Size = new Size(60, 23);
           // button17.Location = new Point(110, 45);
            this.Size = new Size(1275, 750);
            groupBox7.Size = new Size(175, 80);

            groupBox8.Size = new Size(600, 80);

            textBox10.Location = new Point(55, 30);
            textBox12.Location = new Point(75, 30);
            textBox15.Location = new Point(95, 30);
           
            textBox21.Location = new Point(115, 30);


            textBox13.Location = new Point(95, 52);
            // textBox16.Location = new Point(95, 55);
            // textBox22.Location = new Point(115, 55);
            //textBox14.Location = new Point(135, 55);

            // label2.Location = new Point(65, 60);
            button12.Size = new Size(50, 23);
            button12.Location = new Point(165, 55);
            label1.Location = new Point(25, 33);
            label20.Location = new Point(55, 15);
            //textBox19.Location = new Point(170, 30);
            //textBox20.Location = new Point(190, 30);
           // label21.Location = new Point(165, 15);

            textBox24.Location = new Point(75, 48);
            textBox24.Size = new Size(75, 18);

            textBox25.Location = new Point(75, 25);
            textBox25.Size = new Size(75, 18);

            button12.Enabled = false;//disable home  unless enable link button is pressed
            button16.Enabled = false;//disable reset unless enable link button is pressed

            pictureBox1.Size = new Size(925, 625);

         // pictureBox3.Size = new Size(1150, 625);
           // pictureBox3.Size = new Size(925, 625);
            comboBox1.SelectedIndex = 0;

            //button14.Enabled = false;
            button19.Enabled = false;

            //gMapControl1.Enabled = false;   
            textBox13.BackColor=Color.LightGreen;

        }
        //-----------------------------------------function prototypes-------------------------------------------------------------------
        void tiltadjustup()
        {
            using (FileStream stream = File.Open(Tiltup, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("B");//enable
                }
            }//---

        }
        void tiltadjustdwn()
        {
            using (FileStream stream = File.Open(TiltDwn, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("C");//enable
                }
            }//--

        }
        void Tiltmtrup()
        {
            using (FileStream stream = File.Open(Tiltup, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("B");//enable
                }
            }//-------------
        }
        void stopTiltmtrup()
        {
            using (FileStream stream = File.Open(Tiltup, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("A");//neutral
                }
            }//-
        }
        void Tiltmtrdwn()
        {
            using (FileStream stream = File.Open(TiltDwn, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("C");//enable
                }
            }//--
        }
        void stopTiltmtrdwn()
        {
            using (FileStream stream = File.Open(TiltDwn, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("A");//neutral
                }
            }//---
        }

        void Panmtrright()
        {
            using (FileStream stream = File.Open(PanRht, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("C");
                }
            }
        }
        void stoppanmtrright()
        {
            using (FileStream stream = File.Open(PanRht, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("A");//neutral
                }
            }//
        }
        void Panmtrleft()
        {
            using (FileStream stream = File.Open(PanLft, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("B");//Enable
                }
            }//
        }
        void stopPanMtrLft()
        {
            using (FileStream stream = File.Open(PanLft, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("A");//neutral
                }
            }//-
        }
        void ZoomPstrart()//button7dwn
        {
            if (EOIR == "G")
            {
                timer5.Enabled = true;//zoom count
            }
            if (EOIR == "R")
            {
                timer7.Enabled = true;//zoom count
            }

            using (FileStream stream = File.Open(ZoomMplus, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("2");//                    
                }
            }
        }
        void stopZoomPstrart()
        {
            if (EOIR == "G")
            {
                timer5.Enabled = false;//zoom count
            }

            if (EOIR == "R")
            {
                timer7.Enabled = false;//zoom count
            }
            using (FileStream stream = File.Open(ZoomMplus, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("0");//neutral                  
                }
            }//
        }
        void ZoomNstrart()
        {
            if (EOIR == "G")
            {
                timer6.Enabled = true;//zoom count
            }
            if (EOIR == "R")
            {
                timer8.Enabled = true;//zoom count
            }

            using (FileStream stream = File.Open(ZoomPminus, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("1");//enable
                }
            }//
        }
        void stopZoomNstrart()
        {
            if (EOIR == "G")
            {
                timer6.Enabled = false;//zoom count
            }
            if (EOIR == "R")
            {
                timer8.Enabled = false;//zoom count
            }

            using (FileStream stream = File.Open(ZoomPminus, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("0");//neutral
                }
            }//          
        }

        void fov()
        {
            FOVMin = Convert.ToInt32(textBox23.Text) - 25;
            FOVMax = Convert.ToInt32(textBox23.Text) + 25;
            textBox30.Text = Convert.ToString(FOVMin);
            textBox31.Text = Convert.ToString(FOVMax);
            MidwayCalc = Convert.ToInt32(textBox23.Text);
        }
        //------------------------------------------endfunction prototype--------------------------------------------------


        void sizewindow()
        {
            int ww = pictureBox1.Width +28 ;
            int hh = pictureBox1.Height + 34;
            MoveWindow(appWin1, -17, -28, ww, hh, true);
        }

        void sizewindow1()
        {
           // int ww = pictureBox3.Width + 28;
            //int hh = pictureBox3.Height + 34;
           // MoveWindow(appWin2, -20, -28, ww, hh, true);
        }
        /*
        void MNPythonPGM()
        {

            XXDD = XXDD + 1;

            if (XXDD > 2)

            { XXDD = 1; } //toggle back and forth

            if (XXDD == 1)
            {
                Thread.Sleep(500);
                ProcessStartInfo ps1 = new ProcessStartInfo("C:\\CamVid\\CamGimbalX\\dist\\CamGimbal10X\\CamGimbal10X.exe");
                ps1.WindowStyle = ProcessWindowStyle.Normal;
                Process p1 = Process.Start(ps1);
                p1.WaitForInputIdle();
                appWin1 = p1.MainWindowHandle;
                SetParent(appWin1, pictureBox1.Handle);
                sizewindow();
                textBox26.BackColor = Color.LightGreen;
            }

            if (XXDD == 2)
            {

                Process[] process5 = Process.GetProcessesByName("CamGimbal10X");
                foreach (Process pro5 in process5)
                    pro5.Kill();
            }

               
          
            
        }*/

        private void button1_Click(object sender, EventArgs e) //Camera select
        {
            x = x + 1;

            if (x >2)

            {x = 1;} //toggle back and forth

           if (x == 1)
            { 
            using (FileStream stream = File.Open(SelectCam, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("R"); //set snappic to zer0
                    if (x == 1) { button1.Image = null; button1.Image = Image.FromFile(@"C:\CamVid\Icon\IR.png"); }
                     EOIR = "R";
                    }
            }//-------
           }//------

            if (x == 2)
            {
                using (FileStream stream = File.Open(SelectCam, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter objWriter = new StreamWriter(stream))
                    {
                        objWriter.Write("G"); //set snappic to zero
                        if (x == 2) { button1.Image = null; button1.Image = Image.FromFile(@"C:\CamVid\Icon\EEEEO.png"); }
                        EOIR = "G";
                    }
                }//-------
            }//------
            
        }
//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
             stopTiltmtrup();
            //swtiltupp = "0";
        }
        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            textBox20.BackColor = Color.LightCoral;//since it was moved from home position set to red

            Tiltmtrup();
            //swtiltupp = "1";
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            Tiltmtrdwn();
            //swtiltDwnn = "1";
        }
        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            textBox20.BackColor = Color.LightCoral;//since it was moved from home position set to red
            stopTiltmtrdwn();
           // swtiltDwnn = "0";
        }
        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            textBox19.BackColor = Color.LightCoral;//since it was moved from home position set to red

            Panmtrleft();
            //swPanleftt = "1";
        }
        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            stopPanMtrLft();
            //swPanleftt = "0";
        }
        private void button5_MouseDown(object sender, MouseEventArgs e)
        {
           textBox19.BackColor = Color.LightCoral;//since it was moved from home position set to red
           Panmtrright();
            //swPanrhtt = "1";
           // textBox27.Text = textBox13.Text;
        }
        private void button5_MouseUp(object sender, MouseEventArgs e)
        {
            stoppanmtrright();
            //swPanrhtt = "0";
        }
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void button6_MouseDown(object sender, MouseEventArgs e)
        {
            swzN = "1";
          
        }
        private void button6_MouseUp(object sender, MouseEventArgs e)
        {
            swzN = "0";          
        }
        private void button7_MouseUp(object sender, MouseEventArgs e)
        {
            swzP = "0";        
        }
        private void button7_MouseDown(object sender, MouseEventArgs e)
        {
            swzP = "1";         
        }
        private void button9_Click(object sender, EventArgs e)
        {
            xx = xx + 1;

            if (xx > 2)

            { xx = 1; } //toggle back and forth

            if (EOIR == "G" && xx == 1 && Eozoom > 24)//set motor track limit
            {
                button18.BackColor = Color.LightCoral;
                button9.BackColor = Color.LightGreen;
                using (FileStream stream = File.Open(Track, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter objWriter = new StreamWriter(stream))
                    {
                        objWriter.Write("G");//enable
                    }
                }//--
            }

            if (EOIR == "G" && xx == 1 && Eozoom < 24)//set motor track limit
            {
                button18.BackColor = Color.LightGreen;
                button9.BackColor = Color.LightGreen;

                using (FileStream stream = File.Open(Track, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter objWriter = new StreamWriter(stream))
                    {
                        objWriter.Write("T");//enable
                    }
                }//---    
            }
            
            if (EOIR == "R" && xx == 1 && BZzoom > 12)//set motor track limit
            {
                button18.BackColor = Color.LightCoral;
                button9.BackColor = Color.LightGreen;
                using (FileStream stream = File.Open(Track, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter objWriter = new StreamWriter(stream))
                    {
                        objWriter.Write("G");//enable
                    }
                }//--
            }
            if (EOIR == "R" && xx == 1 && BZzoom < 12)//set motor track limit
            {
                button18.BackColor = Color.LightGreen;
                button9.BackColor = Color.LightGreen;
                using (FileStream stream = File.Open(Track, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter objWriter = new StreamWriter(stream))
                    {
                        objWriter.Write("T");//enable
                    }
                }//--
            }
            if (xx == 2)
            {
                using (FileStream stream = File.Open(Track, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter objWriter = new StreamWriter(stream))
                    {
                        objWriter.Write("S"); //set snappic to zero
                        button9.BackColor = Color.LightCoral;
                        button18.BackColor = Color.Transparent;
                    }
                }//-------
            }//------

        }
        private void button10_MouseDown(object sender, MouseEventArgs e)
        {

            xA = xA + 1;

            using (FileStream stream = File.Open(CodeID, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write(textBox11.Text + xA);                  
                }
            }//-----------

            using (FileStream stream = File.Open(Tpic, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("1");
                }
            }//-----------

        }
        private void button10_MouseUp(object sender, MouseEventArgs e)
        {
            using (FileStream stream = File.Open(Tpic, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("0");
                }
            }//-----------
        }
        private void button11_MouseDown(object sender, MouseEventArgs e)
        {
            xB = xB + 1;

            using (FileStream stream = File.Open(CodeID, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write(textBox11.Text + xB);  
                }
            }//-----------

            xC = xC + 1;
            if (xC > 4)

            { xC = 1; } //toggle back and forth

            using (FileStream stream = File.Open(Vid, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write(xC);
                }
                if (xC == 1) { button11.BackgroundImage = null; button11.BackgroundImage = Image.FromFile(@"C:\CamVid\Icon\recon.png"); }
                if (xC == 3) { button11.BackgroundImage = null; button11.BackgroundImage = Image.FromFile(@"C:\CamVid\Icon\recoff.png"); }
            }//-----------           
           
        }
        private void button11_MouseUp(object sender, MouseEventArgs e)
        {
            xC = xC + 1;
            using (FileStream stream = File.Open(Vid, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write(xC);
                }
            }//-----------
        }

        private void button8_Click(object sender, EventArgs e)
        {

            ProcessStartInfo gamepadon = new ProcessStartInfo("C:\\CamVid\\Gamepad10X\\GamePad10P.exe");

            xD = xD + 1;

            if (xD > 2)

            { xD = 1; } //toggle back and forth

            if (xD == 1)
            {              
                Process Gpad = Process.Start(gamepadon);
                button8.BackColor = Color.LightGreen;              
            }//------

            if (xD == 2)
            {
                Process[] processes = Process.GetProcessesByName(Gamepadd);               
                foreach (Process pro in processes)
                    pro.Kill();
                button8.BackColor = Color.LightCoral;              
            }//------            
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(80, 78);//position inside picturebox2
            e.Graphics.RotateTransform(head);
            e.Graphics.DrawImage(imagetwo, -20, -20, 40, 40);//-20-20 pivot point 40 40 bitmap size
            e.Graphics.ResetTransform();
        }

        private void gMapControl1_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);

            double X = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;
            X = Math.Round(X, 8);


            double Y = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
            Y = Math.Round(Y, 8);

            string longitude = X.ToString();
            string latitude = Y.ToString();           

            lat1 = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
            long1 = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;

            textBox1.Text = Convert.ToString(Math.Round(lat1, 8));
            textBox2.Text = Convert.ToString(Math.Round(long1, 8));
                      
        }       
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cm = Convert.ToString(comboBox1.SelectedIndex);

            using (FileStream stream = File.Open(colormapselect, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    if (cm == "10") { cm = "A";}
                    if (cm == "11") { cm = "B";}
                    if (cm == "12") { cm = "C";}
                    if (cm == "13") { cm = "D";}
                    if (cm == "14") { cm = "E";}
                    if (cm == "15") { cm = "F";}
                    if (cm == "16") { cm = "G";}
                    if (cm == "17") { cm = "H";}
                    if (cm == "18") { cm = "I";}
                    if (cm == "19") { cm = "J";}
                    if (cm == "20") { cm = "K";}
                    if (cm == "21") { cm = "L";}
                    if (cm == "22") { cm = "M";}
                    if (cm == "23") { cm = "N";}
                    //if (cm == "24") { cm = "P";}

                    objWriter.Write(cm);//index number
                }
            }//-
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process[] processes = Process.GetProcessesByName(Gamepadd);
            foreach (Process pro in processes)
                pro.Kill();

            Process[] process2 = Process.GetProcessesByName("PingNetwork");
            foreach (Process pro1 in process2)
                pro1.Kill();

            Process[] processes2 = Process.GetProcessesByName("DKitCam");
            foreach (Process pro2 in processes2)
                pro2.Kill();

            Process[] processe3 = Process.GetProcessesByName("USBDetect");
            foreach (Process pro3 in processe3)
                pro3.Kill();

            //  Process[] processe4 = Process.GetProcessesByName("framecopy");
            //   foreach (Process pro4 in processe4)
            //       pro4.Kill();
            using (FileStream stream = File.Open(SelectCam, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("G");
                }
            }//---

            using (FileStream stream = File.Open(closepgm, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("1");
                }
            }//------
        }
        private void pointCameraHereToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            xrw = xrw + 1;

            lat2 = Convert.ToDouble(textBox1.Text);
            lon2 = Convert.ToDouble(textBox2.Text);

            lat1 = Convert.ToDouble(textBox3.Text);
            long1 = Convert.ToDouble(textBox4.Text);


            /*double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = long1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            */

             rlat1 = Math.PI * lat1 / 180;
             rlat2 = Math.PI * lat2 / 180;
             theta = long1 - lon2;
             rtheta = Math.PI * theta / 180;
             dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);

            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515; //miles
            dist = Math.Round(dist, 8);
            //textBox7.Text = dist.ToString(); //miles
           // double distanetofeet;
           // double distanetofeet1;
            distanetofeet = dist;
            distanetofeet1 = distanetofeet * 5280;//convertofeet
            //textBox7.Text = dist.ToString();
            distanetofeet1 = Math.Round(distanetofeet1, 3);

            textBox7.Text = distanetofeet1.ToString();

            //double hheight;
           // double angleofdepression;
           // double distanetofeet2;
            hheight = Convert.ToDouble(textBox6.Text);
            distanetofeet2 = distanetofeet1;
            angleofdepression = Math.Atan2(hheight, distanetofeet2) * (180 / Math.PI);
            angleofdepression = Math.Round(angleofdepression, 3);

            textBox9.Text = angleofdepression.ToString();
            Thread.Sleep(100);
            //--------------------------get bearing--------------------------------------------
            double degToRad = Math.PI / 180.0;
            double phi1 = Convert.ToDouble(textBox3.Text) * degToRad;
            double lam1 = Convert.ToDouble(textBox4.Text) * degToRad;
            double phi2 = Convert.ToDouble(textBox1.Text) * degToRad;
            double lam2 = Convert.ToDouble(textBox2.Text) * degToRad;
            double bearing = Math.Atan2(Math.Sin(lam2 - lam1) * Math.Cos(phi2),
            Math.Cos(phi1) * Math.Sin(phi2) - Math.Sin(phi1) * Math.Cos(phi2) * Math.Cos(lam2 - lam1)) * 180 / Math.PI;
            if (bearing < 0)
            {
                double Cdegrees = bearing - -179;
                double degree = 180 + Cdegrees;
                degree = Math.Round(degree, 3);
                string degree1 = degree.ToString();

                textBox8.Text = degree1;
            }

            else
            {
                bearing = Math.Round(bearing, 3);
                string bear1 = bearing.ToString();

                textBox8.Text = bear1;
            }
            
            dataGridView1.CurrentCell = dataGridView1.Rows[xrw].Cells[0];
            dataGridView1.Rows[1].Selected = true;
            dataGridView1.Rows.Add();
            dataGridView1.Rows[xrw].Cells[0].Value = lat2;
            dataGridView1.Rows[xrw].Cells[1].Value = lon2;
            dataGridView1.Rows[xrw].Cells[2].Value = xrw;

            marker = new GMarkerGoogle(new PointLatLng(lat2, lon2), GMarkerGoogleType.green);
            marker.ToolTipMode = MarkerTooltipMode.Always;
            marker.ToolTipText = string.Format("{0}", xrw);
            markerOverlay.Markers.Add(marker);
//-------------------------------Encoder Position calc----------------------------------------------
            //button16.PerformClick(); //reset timers

            cmprHB = Convert.ToInt32(textBox5.Text);//heading
            cmprHB1 = Convert.ToDouble(textBox8.Text); //bearingresult

            // Midway= Convert.ToInt32(textBox8.Text); //encodersmidpoint
            //textBox23.Text = Convert.ToString(Midway); //encodersmidpoint
            MidwayCalc = 0;
            nudge = 0;
          
            textBox29.Text = "0";
            //-----------------Drone Heading Encoder Points-----------------------------

            if (cmprHB >= 0 && cmpHB <= 18 || cmprHB <= 359 && cmprHB >= 342) { cmprHeading = 14;}
            if (cmprHB > 18 && cmpHB <= 54){cmprHeading = 15; }
            if (cmprHB > 54 && cmpHB <= 90){cmprHeading = 16; }
            if (cmprHB > 90 && cmpHB <= 126){cmprHeading = 17; }
            if (cmprHB > 126 && cmpHB <= 162){cmprHeading = 18;}
            if (cmprHB > 162 && cmpHB <= 198){cmprHeading = 19;}
            if (cmprHB > 198 && cmpHB <= 234){cmprHeading = 10;}
            if (cmprHB > 234 && cmpHB <= 270){cmprHeading = 11;}
            if (cmprHB > 270 && cmpHB <= 306){cmprHeading = 12;}
            if (cmprHB > 306 && cmpHB < 342){cmprHeading = 13;}

            textBox10.Text = Convert.ToString(cmprHeading);

//-------------------Position Clicked on map, Encoder points----------------------------------
            if (cmprHB1 >= 0 && cmprHB1 <= 18 || cmprHB1 <= 359 && cmprHB1 >= 342) { cmprBearing = 14; }
            if (cmprHB1 > 18 && cmprHB1 <= 54) { cmprBearing = 15;}
            if (cmprHB1 > 54 && cmprHB1 <= 90) { cmprBearing = 16; }
            if (cmprHB1 > 90 && cmprHB1 <= 126) { cmprBearing = 17; }
            if (cmprHB1 > 126 && cmprHB1 <= 162) { cmprBearing = 18; }
            if (cmprHB1 > 162 && cmprHB1 <= 198) { cmprBearing = 19; }
            if (cmprHB1 > 198 && cmprHB1 <= 234) { cmprBearing = 10; }
            if (cmprHB1 > 234 && cmprHB1 <= 270) { cmprBearing = 11; }
            if (cmprHB1 > 270 && cmprHB1 <= 306) { cmprBearing = 12; }
            if (cmprHB1 > 306 && cmprHB1 < 342) { cmprBearing = 13; }

            textBox12.Text = Convert.ToString(cmprBearing);
            MidwayCalc= Convert.ToInt32(textBox23.Text);
           
            //Encoderpoint pt= new Encoderpoint();
            //-----------------------------------Encoder's Actual sitting position----------------------------------------------------------------
            Midway = Convert.ToDouble(textBox8.Text); //encodersmidpoint

            if (Midway >= 0 && Midway <= 18) { textBox23.Text = "0";} //encoder is sitting at 14 move forward

            textBox30.Text = "335";
            textBox31.Text = "25";
            MidwayCalc = Convert.ToInt32(textBox23.Text);
            if (Midway <= 359 && Midway >= 342) { textBox23.Text = "360";} //move back
            
            textBox30.Text = "335";
            textBox31.Text = "25";
            MidwayCalc = Convert.ToInt32(textBox23.Text);

            if (Midway >= 18 && Midway <= 54) {textBox23.Text ="36";fov();}//compare to actual bearing value
            if (Midway >= 54 && Midway <= 90) { textBox23.Text = "72";fov(); }
            if (Midway >= 90 && Midway <= 126) { textBox23.Text = "108";fov();}
            if (Midway >= 126 && Midway <= 162) { textBox23.Text = "144";fov();}
            if (Midway >= 162 && Midway <= 198) { textBox23.Text = "180";fov();}
            if (Midway >= 198 && Midway <= 234) { textBox23.Text = "216";fov();}
            if (Midway >= 234 && Midway <= 270) { textBox23.Text = "252";fov();}
            if (Midway >= 270 && Midway <= 306) { textBox23.Text = "288";fov();}
            if (Midway >= 306 && Midway <= 342) { textBox23.Text = "324";fov();}

            //textBox30.Text = Convert.ToString(MidwayCalc);

            // pt.encodept();---------------------------------------------------------------------------------------------------------------------

            if (cmprHeading == 14 && cmprBearing == 14) { cmprResultVector = 14; }  
            if (cmprHeading == 14 && cmprBearing == 15) { cmprResultVector = 15; }
            if (cmprHeading == 14 && cmprBearing == 16) { cmprResultVector = 16; }
            if (cmprHeading == 14 && cmprBearing == 17) { cmprResultVector = 17; }
            if (cmprHeading == 14 && cmprBearing == 18) { cmprResultVector = 18; }
            if (cmprHeading == 14 && cmprBearing == 19) { cmprResultVector = 19; }
            if (cmprHeading == 14 && cmprBearing == 10) { cmprResultVector = 10; }
            if (cmprHeading == 14 && cmprBearing == 11) { cmprResultVector = 11; }
            if (cmprHeading == 14 && cmprBearing == 12) { cmprResultVector = 12; }
            if (cmprHeading == 14 && cmprBearing == 13) { cmprResultVector = 13; }

            if (cmprHeading == 15 && cmprBearing == 15) { cmprResultVector = 14; }
            if (cmprHeading == 15 && cmprBearing == 16) { cmprResultVector = 15; }
            if (cmprHeading == 15 && cmprBearing == 17) { cmprResultVector = 16; }
            if (cmprHeading == 15 && cmprBearing == 18) { cmprResultVector = 17; }
            if (cmprHeading == 15 && cmprBearing == 19) { cmprResultVector = 18; }
            if (cmprHeading == 15 && cmprBearing == 10) { cmprResultVector = 19; }
            if (cmprHeading == 15 && cmprBearing == 11) { cmprResultVector = 10; }
            if (cmprHeading == 15 && cmprBearing == 12) { cmprResultVector = 11; }
            if (cmprHeading == 15 && cmprBearing == 13) { cmprResultVector = 12; }
            if (cmprHeading == 15 && cmprBearing == 14) { cmprResultVector = 13; }

            if (cmprHeading == 16 && cmprBearing == 16) { cmprResultVector = 14; }
            if (cmprHeading == 16 && cmprBearing == 17) { cmprResultVector = 15; }
            if (cmprHeading == 16 && cmprBearing == 18) { cmprResultVector = 16; }
            if (cmprHeading == 16 && cmprBearing == 19) { cmprResultVector = 17; }
            if (cmprHeading == 16 && cmprBearing == 10) { cmprResultVector = 18; }
            if (cmprHeading == 16 && cmprBearing == 11) { cmprResultVector = 19; }
            if (cmprHeading == 16 && cmprBearing == 12) { cmprResultVector = 10; }
            if (cmprHeading == 16 && cmprBearing == 13) { cmprResultVector = 11; }
            if (cmprHeading == 16 && cmprBearing == 14) { cmprResultVector = 12; }
            if (cmprHeading == 16 && cmprBearing == 15) { cmprResultVector = 13; }

            if (cmprHeading == 17 && cmprBearing == 17) { cmprResultVector = 14; }
            if (cmprHeading == 17 && cmprBearing == 18) { cmprResultVector = 15; }
            if (cmprHeading == 17 && cmprBearing == 19) { cmprResultVector = 16; }
            if (cmprHeading == 17 && cmprBearing == 10) { cmprResultVector = 17; }
            if (cmprHeading == 17 && cmprBearing == 11) { cmprResultVector = 18; }
            if (cmprHeading == 17 && cmprBearing == 12) { cmprResultVector = 19; }
            if (cmprHeading == 17 && cmprBearing == 13) { cmprResultVector = 10; }
            if (cmprHeading == 17 && cmprBearing == 14) { cmprResultVector = 11; }
            if (cmprHeading == 17 && cmprBearing == 15) { cmprResultVector = 12; }
            if (cmprHeading == 17 && cmprBearing == 16) { cmprResultVector = 13; }

            if (cmprHeading == 18 && cmprBearing == 18) { cmprResultVector = 14; }
            if (cmprHeading == 18 && cmprBearing == 19) { cmprResultVector = 15; }
            if (cmprHeading == 18 && cmprBearing == 10) { cmprResultVector = 16; }
            if (cmprHeading == 18 && cmprBearing == 11) { cmprResultVector = 17; }
            if (cmprHeading == 18 && cmprBearing == 12) { cmprResultVector = 18; }
            if (cmprHeading == 18 && cmprBearing == 13) { cmprResultVector = 19; }
            if (cmprHeading == 18 && cmprBearing == 14) { cmprResultVector = 10; }
            if (cmprHeading == 18 && cmprBearing == 15) { cmprResultVector = 11; }
            if (cmprHeading == 18 && cmprBearing == 16) { cmprResultVector = 12; }
            if (cmprHeading == 18 && cmprBearing == 17) { cmprResultVector = 13; }

            if (cmprHeading == 19 && cmprBearing == 19) { cmprResultVector = 14; }
            if (cmprHeading == 19 && cmprBearing == 10) { cmprResultVector = 15; }
            if (cmprHeading == 19 && cmprBearing == 11) { cmprResultVector = 16; }
            if (cmprHeading == 19 && cmprBearing == 12) { cmprResultVector = 17; }
            if (cmprHeading == 19 && cmprBearing == 13) { cmprResultVector = 18; }
            if (cmprHeading == 19 && cmprBearing == 14) { cmprResultVector = 19; }
            if (cmprHeading == 19 && cmprBearing == 15) { cmprResultVector = 10; }
            if (cmprHeading == 19 && cmprBearing == 16) { cmprResultVector = 11; }
            if (cmprHeading == 19 && cmprBearing == 17) { cmprResultVector = 12; }
            if (cmprHeading == 19 && cmprBearing == 18) { cmprResultVector = 13; }
//-------------------------------------------------------------------------------------
            if (cmprHeading == 10 && cmprBearing == 10) { cmprResultVector = 14; }
            if (cmprHeading == 10 && cmprBearing == 11) { cmprResultVector = 15; }
            if (cmprHeading == 10 && cmprBearing == 12) { cmprResultVector = 16; }
            if (cmprHeading == 10 && cmprBearing == 13) { cmprResultVector = 17; }
            if (cmprHeading == 10 && cmprBearing == 14) { cmprResultVector = 18; }
            if (cmprHeading == 10 && cmprBearing == 15) { cmprResultVector = 19; }
            if (cmprHeading == 10 && cmprBearing == 16) { cmprResultVector = 10; }
            if (cmprHeading == 10 && cmprBearing == 17) { cmprResultVector = 11; }
            if (cmprHeading == 10 && cmprBearing == 18) { cmprResultVector = 12; }
            if (cmprHeading == 10 && cmprBearing == 19) { cmprResultVector = 13; }

            if (cmprHeading == 11 && cmprBearing == 11) { cmprResultVector = 14; }
            if (cmprHeading == 11 && cmprBearing == 12) { cmprResultVector = 15; }
            if (cmprHeading == 11 && cmprBearing == 13) { cmprResultVector = 16; }
            if (cmprHeading == 11 && cmprBearing == 14) { cmprResultVector = 17; }
            if (cmprHeading == 11 && cmprBearing == 15) { cmprResultVector = 18; }
            if (cmprHeading == 11 && cmprBearing == 16) { cmprResultVector = 19; }
            if (cmprHeading == 11 && cmprBearing == 17) { cmprResultVector = 10; }
            if (cmprHeading == 11 && cmprBearing == 18) { cmprResultVector = 11; }
            if (cmprHeading == 11 && cmprBearing == 19) { cmprResultVector = 12; }
            if (cmprHeading == 11 && cmprBearing == 10) { cmprResultVector = 13; }

            if (cmprHeading == 12 && cmprBearing == 12) { cmprResultVector = 14; }
            if (cmprHeading == 12 && cmprBearing == 13) { cmprResultVector = 15; }
            if (cmprHeading == 12 && cmprBearing == 14) { cmprResultVector = 16; }
            if (cmprHeading == 12 && cmprBearing == 15) { cmprResultVector = 17; }
            if (cmprHeading == 12 && cmprBearing == 16) { cmprResultVector = 18; }
            if (cmprHeading == 12 && cmprBearing == 17) { cmprResultVector = 19; }
            if (cmprHeading == 12 && cmprBearing == 18) { cmprResultVector = 10; }
            if (cmprHeading == 12 && cmprBearing == 19) { cmprResultVector = 11; }
            if (cmprHeading == 12 && cmprBearing == 10) { cmprResultVector = 12; }
            if (cmprHeading == 12 && cmprBearing == 11) { cmprResultVector = 13; }

            if (cmprHeading == 13 && cmprBearing == 13) { cmprResultVector = 14; }
            if (cmprHeading == 13 && cmprBearing == 14) { cmprResultVector = 15; }
            if (cmprHeading == 13 && cmprBearing == 15) { cmprResultVector = 16; }
            if (cmprHeading == 13 && cmprBearing == 16) { cmprResultVector = 17; }
            if (cmprHeading == 13 && cmprBearing == 17) { cmprResultVector = 18; }
            if (cmprHeading == 13 && cmprBearing == 18) { cmprResultVector = 19; }
            if (cmprHeading == 13 && cmprBearing == 19) { cmprResultVector = 10; }
            if (cmprHeading == 13 && cmprBearing == 10) { cmprResultVector = 11; }
            if (cmprHeading == 13 && cmprBearing == 11) { cmprResultVector = 12; }
            if (cmprHeading == 13 && cmprBearing == 12) { cmprResultVector = 13; }
             
            textBox15.Text = Convert.ToString(cmprResultVector);
           
            //---------------------pitch-----------------------------------
            cmprPitch = Convert.ToDouble(textBox9.Text);
           

            //timer3.Enabled = true;//search home pitch disable tilt
            timer15.Enabled = true;//search home pitch disable tilt

            if (flag == 1) //allow motor to move only when dronekit data is available
            {

                timer1.Enabled = true;//search home yaw
               
            }
            textBox15.BackColor = Color.White;
            textBox16.BackColor = Color.White;

            textBox13.BackColor = Color.LightCoral;
            textBox22.BackColor = Color.LightCoral;

            textBox19.BackColor = Color.LightCoral;
            textBox20.BackColor = Color.LightCoral;

           // button14.Enabled = true;

        }
       

        private void timer1_Tick(object sender, EventArgs e)//set yaw encoder location
        {
                
            PanEncCurrentPosition = Convert.ToInt32(textBox13.Text);

            if (PanEncCurrentPosition <=19 && PanEncCurrentPosition >= 14 && PanEncCurrentPosition > cmprResultVector)
            {               
                PDir = 0; //go countrclockwise
            }
            if (PanEncCurrentPosition <= 19 && PanEncCurrentPosition >= 14 && PanEncCurrentPosition < cmprResultVector)
            {                
                PDir = 1; //go clockwise
            }

            if (PanEncCurrentPosition <= 14 && PanEncCurrentPosition >= 10 && PanEncCurrentPosition > cmprResultVector)
            {
                PDir = 2; //go countrclockwise
            }

            if (PanEncCurrentPosition <= 14 && PanEncCurrentPosition >= 10 && PanEncCurrentPosition < cmprResultVector)
            {
                PDir = 3; //go clockwise
            }

            textBox21.Text = Convert.ToString(PDir);

//-------------------------------------------------------------------------------
            if (PanEncCurrentPosition != cmprResultVector && PDir == 1)           
            {
                Panmtrright();
            }

            if (PanEncCurrentPosition != cmprResultVector && PDir == 0)
            {
                Panmtrleft();
            }
            //------------------------------------------------------------------------------
            if (PanEncCurrentPosition != cmprResultVector && PDir == 3)
            {
                Panmtrright();
            }

            if (PanEncCurrentPosition != cmprResultVector && PDir == 2)
            {
                Panmtrleft();
            }
//------------------------------------------------------------------------
            if (PanEncCurrentPosition == cmprResultVector )//stay
                                                           
             {
                stoppanmtrright();
                timer1.Enabled = false;
                textBox13.BackColor = Color.LightGreen;

            }//--------------

            if (PanEncCurrentPosition == cmprResultVector)//stay
            {
                stopPanMtrLft();
                timer1.Enabled = false;
                textBox13.BackColor = Color.LightGreen;
            }
        }
        private void button15_Click(object sender, EventArgs e)
        {
                      
            ProcessStartInfo Dkit = new ProcessStartInfo("C:\\CamVid\\DKitCam\\DKitCam.exe");

            xDD = xDD + 1;

            if (xDD > 2)

            { xDD = 1; } //toggle back and forth

            if (xDD == 1)
            {
                flag = 1;
                Process.Start(Dkit);
                button15.BackColor = Color.LightGreen;
                timer9.Enabled = true;
                button12.Enabled = true;
                button16.Enabled = true;    

            }//------

            if (xDD == 2)
            {
                flag = 0;
                timer9.Enabled = false;
                Process[] processes2 = Process.GetProcessesByName("DKitCam");
                foreach (Process pro2 in processes2)
                    pro2.Kill();
                button15.BackColor = Color.LightCoral;

                button12.Enabled = false;
                button16.Enabled = false;
            }
            
        }

        private void timer2_Tick(object sender, EventArgs e)//set yaw home
        {
           
            PanEncVal = Convert.ToInt32(textBox13.Text);

            if (PanEncVal != 14)//encoders home position points to drones heading
            {
                Panmtrright();
            }
            if (PanEncVal == 14)
            {
                stoppanmtrright();
                timer2.Enabled = false;
                textBox19.BackColor = Color.LightGreen;
            }//--------
           // textBox27.Text = textBox13.Text;
        }

        private void button16_Click(object sender, EventArgs e)//reset
        {
            timer1.Enabled = false;//pan
            timer2.Enabled = false;//home pan
            timer3.Enabled = false;//tilt
            timer4.Enabled = false;//home tilt
            timer15.Enabled = false;

            textBox15.BackColor=Color.White;
            textBox16.BackColor = Color.White;

            textBox13.BackColor = Color.LightCoral;
            textBox22.BackColor = Color.LightCoral;

            //--------------------------resetpan---------------------------------------------------------------
            stoppanmtrright();
            stopPanMtrLft();
            //-----------------------resettilt-----------------------------------------
            stopTiltmtrup();
            stopTiltmtrdwn();
        }

        private void timer15_Tick(object sender, EventArgs e)
        {
            Tiltmtrup();

            if((textBox22.Text == "19") || (textBox22.Text == "18"))

            {
                stopTiltmtrup();
                timer15.Enabled= false;
                Thread.Sleep(100);
                timer3.Enabled = true;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)//------find pitch location
        {
           
            cmprPitch = Convert.ToDouble(textBox9.Text);
            
             if (cmprPitch >= 0 && cmprPitch <= 1)
              { cmprPitchResultVector = 20; }
              if (cmprPitch >= 2 && cmprPitch <= 26)
              { cmprPitchResultVector = 21; }
              if (cmprPitch >= 27 && cmprPitch <= 39)
              { cmprPitchResultVector = 22; }
              if (cmprPitch >= 40)
              {cmprPitchResultVector = 23; }

             // if (cmprPitch >= 51 )
             // { cmprPitchResultVector = 24; }

            textBox16.Text = Convert.ToString(cmprPitchResultVector);
            PitchPosition = Convert.ToInt32(textBox22.Text);

            if (PitchPosition < cmprPitchResultVector)
            {
                updwm = 0; //go down
            }
            if (PitchPosition > cmprPitchResultVector)
            {
                updwm = 1; //go up
            }
             textBox14.Text = Convert.ToString(updwm);

            //if (PitchPosition != cmprPitchResultVector && updwm == 1) //up direction

            if (PitchPosition> cmprPitchResultVector && updwm == 1) //up direction
            {
                Tiltmtrup();
            }
            if (PitchPosition < cmprPitchResultVector && updwm == 0) //down direction
                //if (PitchPosition != cmprPitchResultVector && updwm == 0) //down direction
            {
                Tiltmtrdwn();
            }
            //--------------------------------------
          
            if (PitchPosition == cmprPitchResultVector && updwm == 1) //stay
                                                      
            {                          
                tiltadjustup();
                stopTiltmtrup();
                textBox22.BackColor = Color.LightGreen;
                timer3.Enabled = false;

            }

            if (PitchPosition == cmprPitchResultVector && updwm == 0)

            {                           
                tiltadjustdwn();
                stopTiltmtrdwn();
                timer3.Enabled = false;
                textBox22.BackColor = Color.LightGreen;
            }         
        }    
        private void timer4_Tick(object sender, EventArgs e)//set tilt home
        {
          
            int Tilt_current_position = Convert.ToInt32(textBox22.Text);
            int Tilt_Goto_position = Convert.ToInt32(textBox16.Text);

            if (Tilt_current_position > 19)
            {
                Tiltmtrup();
            }

            if (Tilt_current_position < 19) 
            {
                Tiltmtrdwn();
            }

            if (Tilt_current_position == 20)
            {               
                stopTiltmtrup();
                Thread.Sleep(10);
                timer4.Enabled = false;
                textBox20.BackColor = Color.LightGreen;
            }
         
            }
//---------------------------------------------------zoom---------------------------------------------
        private void timer5_Tick(object sender, EventArgs e)
        {
            
            Eozoom = Eozoom + 1;
            if (Eozoom >57)
            {
                Eozoom = 57;
            }
            textBox17.Text = Convert.ToString(Eozoom);

            if(EOIR=="G" && xx == 1 && Eozoom > 24)//set motor track limit
            {
                button18.BackColor=Color.LightCoral;

                using (FileStream stream = File.Open(Track, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter objWriter = new StreamWriter(stream))
                    {
                        objWriter.Write("G");//enable
                    }
                }//--
            }

        }
        private void timer6_Tick(object sender, EventArgs e)
        {
            Eozoom = Eozoom - 1;
            if(Eozoom < 0)
            {
                Eozoom = 0;
            }
            textBox17.Text = Convert.ToString(Eozoom);

            if (EOIR == "G" && xx == 1 && Eozoom < 24)//set motor track limit
            {
                button18.BackColor = Color.LightGreen;

                using (FileStream stream = File.Open(Track, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter objWriter = new StreamWriter(stream))
                    {
                        objWriter.Write("T");//enable
                    }
                }//---    
            }
        }

        private void timer7_Tick(object sender, EventArgs e)
        {
            BZzoom=BZzoom + 1;
            if(BZzoom>28)
            {
                BZzoom = 28;
            }
            textBox18.Text = Convert.ToString(BZzoom);

            if (EOIR == "R" && xx == 1 && BZzoom > 12)//set motor track limit
            {
                button18.BackColor = Color.LightCoral;

                using (FileStream stream = File.Open(Track, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter objWriter = new StreamWriter(stream))
                    {
                        objWriter.Write("G");//enable
                    }
                }//--
            }

        }
        private void timer8_Tick(object sender, EventArgs e)
        {
            BZzoom = BZzoom - 1;
            if(BZzoom<0)
            {
                BZzoom = 0;
            }
            textBox18.Text = Convert.ToString(BZzoom);

            if (EOIR == "R" && xx == 1 && BZzoom < 12)//set motor track limit
            {
                button18.BackColor = Color.LightGreen;

                using (FileStream stream = File.Open(Track, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter objWriter = new StreamWriter(stream))
                    {
                        objWriter.Write("T");//enable
                    }
                }//--
            }
        }
//-----------------------------------------------------------------------------------------------------------------------------------------
        private void timer9_Tick(object sender, EventArgs e)
        {
            using (FileStream stream = File.Open(Datalink, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader objRead = new StreamReader(stream))
                {
                    Pxdd = objRead.ReadToEnd();
                    
                    try
                    {

                        if ((Pxdd.Substring(11, 1) == "#") && (Pxdd.Substring(21, 1) == "<"))
                        {
                          
                            PxD.Text = "DroneKit Protocol:" + Pxdd.Substring(11, 32);
                            textBox5.Text = Pxdd.Substring(12, 3);//heading
                            head = Convert.ToInt32(textBox5.Text);
                            Refresh();

                           textBox6.Text = Pxdd.Substring(16, 5);//altitude---------------------uncommecnt here
                            textBox3.Text = Pxdd.Substring(22, 9);//latitude
                            textBox4.Text = Pxdd.Substring(33, 10);//longitude

                            homelatitude = Pxdd.Substring(22, 10);//latitude
                            homelatitudeD = Convert.ToDouble(homelatitude);

                            homelongitude = Pxdd.Substring(33, 11);//longitude
                            homelongitudeD = Convert.ToDouble(homelongitude);


                            PointLatLng point = new PointLatLng(homelatitudeD, homelongitudeD);//set home position
                            Bitmap bmpmarker = (Bitmap)Image.FromFile(@"C:\CamVid\Icon\home.png");
                            GMapMarker marker1 = new GMarkerGoogle(point, bmpmarker);
                            markerOverlay.Markers.Add(marker1);
                            gMapControl1.UpdateMarkerLocalPosition(marker1);

                        }
                        if (((Pxdd.Substring(0, 1) == "$") && (Pxdd.Substring(10, 1) == "*")))
                        {
                            
                            Enc.Text = "Encoder Protocol:Pan:" + Pxdd.Substring(3, 2) + " Tilt:" + Pxdd.Substring(8, 2);
                           

                            string adjustT = Pxdd.Substring(8, 2);//tiltencoder
                            string adjustP = Pxdd.Substring(3, 2);//pan encoder
                            
                                                        if (adjustT =="38") 
                                                        {
                                                            adjustT = "19";
                                                        }
                                                        if (adjustT == "37")
                                                        {
                                                            adjustT = "19";
                                                        }
                                                        
                           // if ((adjustT == "20") || (adjustT == "20") || (adjustT == "21") || (adjustT == "22") || (adjustT == "23") || (adjustT == "24") || (adjustT == "25"))
                            if ((adjustT == "18") || (adjustT == "19") || (adjustT == "20") || (adjustT == "21") || (adjustT == "22") || (adjustT == "23"))
                            {
                                
                                textBox22.Text = adjustT;
                                //textBox22.Text = "19";
                            }
                            else
                            {
                                adjustT = "19";
                            }
                            //-----------------------------------
                            if ((adjustP == "10") || (adjustP == "11") || (adjustP == "12") ||
                                (adjustP == "13") || (adjustP == "14") || (adjustP == "15")||
                                (adjustP == "16") || (adjustP == "17") || (adjustP == "18") || (adjustP == "19"))
                            {

                                textBox13.Text = adjustP;
                            }
                            else
                            {
                                adjustP = "19";
                            }
                         
                        }
                    }
                    catch (Exception) {
                       
                        Pxdd = "000000000000000000000000000000000000000000";
                    }
                }
            }

            //richTextBox1.Text = Pxdd;
            richTextBox1.Text += Environment.NewLine + Pxdd;
            if (richTextBox1.Text.Length > 5000)
            {
                richTextBox1.Clear();
            }
           //find encodershomeposition
            textBox27.Text = textBox13.Text;
            if(textBox27.Text=="14")
            {
                homeit = homeit + 1;
                textBox28.Text = Convert.ToString(homeit);
            }

            if (textBox27.Text == "15")
            {
               // homeit = homeit;
                textBox28.Text = Convert.ToString(homeit);
            }

        }

        private void button14_Click(object sender, EventArgs e)
        {

           // textBox23.Text = Convert.ToString(xrw);
            markerOverlay.Markers.Clear();
            repoint = 0;
            timer12.Enabled = true;
        }

      
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
           
            sizewindow();
            //sizewindow1();
            //this.MaximumSize = new Size(1080, int.MaxValue);
            this.MaximumSize = new Size(1920, 1080);

            //pictureBox3.MaximumSize = new Size(1920, 1080);

            //Fw.Text = Convert.ToString(this.Size.Width);
            //Fh.Text = Convert.ToString(this.Size.Height);
            // textBox23.Text = Convert.ToString(this.Size.Width);
            // textBox27.Text = Convert.ToString(this.Size.Height);
        }
        private void textBox5_TextChanged(object sender, EventArgs e)//data filer
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox5.Text, "[^0-9,.,-]")) //accept these characters only
            {

                textBox5.Text = textBox5.Text.Remove(textBox5.Text.Length - 1);//exlse strip off alien character
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(textBox5.Text, "[^ ]"))//if nothing add zeros
            {

                textBox5.Text = "000";
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)//data filer
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox6.Text, "[^0-9,.,-]")) //accept these characters only
            {

                textBox6.Text = textBox6.Text.Remove(textBox6.Text.Length - 1);//exlse strip off alien character
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(textBox6.Text, "[^ ]"))//if nothing add zeros
            {

                textBox6.Text = "000";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, "[^0-9,.,-]")) //accept these characters only
            {

                textBox3.Text = textBox3.Text.Remove(textBox3.Text.Length - 1);//exlse strip off alien character
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, "[^ ]"))//if nothing add zeros
            {

                textBox3.Text = "000";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)//data filter
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^0-9,.,-]")) //accept these characters only
            {

                textBox4.Text = textBox4.Text.Remove(textBox4.Text.Length - 1);//exlse strip off alien character
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, "[^ ]"))//if nothing add zeros
            {

                textBox4.Text = "000";
            }
        }

        private void timer10_Tick(object sender, EventArgs e)//dronekit
        {
          
            using (FileStream stream = File.Open(GpadSel, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader objRead = new StreamReader(stream))
                {
                    EOIRSel = objRead.ReadToEnd();
                    try
                    {
                      //  if ((EOIRSel.Substring(0, 1) == "1") && (button1.Image == Image.FromFile(@"C:\CamVid\Icon\IR.png")))
                     if (EOIRSel.Substring(0, 1)=="1" )
                        {
                            button1.PerformClick(); //click EOIR button
                            Thread.Sleep(1000);
                        }                         
                    }
                    catch (Exception)
                    {
                        EOIRSel = "0000000"; //send something
                    }                  
                }
            }

            using (FileStream stream = File.Open(GpadZoomplus, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader objRead = new StreamReader(stream))
                {
                    UzP = objRead.ReadToEnd();
                    try
                    {
                        if (UzP.Substring(0, 1) == "1")
                        {
                            
                             UzP="1";
                        }
                        else
                        {
                            UzP = "0";
                        }
                    }
                    catch (Exception)
                    {
                        UzP = "000000000"; //send something
                    }
                }
 //-------------------------------avoid zoom button conflict between User sw and gamepad--------------------------------
 
                if ((swzP == "0")&& (swzN == "0") && (UzP == "0") && (UzN == "0"))//allbuttonsup
                   {stopZoomPstrart();stopZoomNstrart();}
                if ((swzP == "1") && (swzN == "0") && (UzP == "0") && (UzN == "0"))
                    { ZoomPstrart();}
                if ((swzP == "0") && (swzN == "1") && (UzP == "0") && (UzN == "0"))
                    {ZoomNstrart();}
                if ((swzP == "0") && (swzN == "0") && (UzP == "1") && (UzN == "0"))
                    {ZoomPstrart();}
                if ((swzP == "0") && (swzN == "0") && (UzP == "0") && (UzN == "1"))
                   {ZoomNstrart();}
            }

            using (FileStream stream = File.Open(GpadZoomminus, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader objRead = new StreamReader(stream))
                {
                    UzN = objRead.ReadToEnd();
                    try
                    {
                        if (UzN.Substring(0, 1) == "1")
                        {                           
                            UzN = "1";
                        }
                        else
                        {
                            UzN = "0";
                        }
                    }
                    catch (Exception)
                    {
                        UzN = "000000000"; //send something
                    }                   
                }
            }
       //------------------------check network connection   
            using (FileStream stream = File.Open(cameraIp, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader objRead = new StreamReader(stream))
                {
                    string cameraIpp = objRead.ReadToEnd();
                    try
                    {
                        if (cameraIpp.Substring(0, 1) == "1")
                        {
                            cameraIpp = "1";
                            textBox24.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            cameraIpp = "0";
                            textBox24.BackColor = Color.LightCoral;
                        }
                    }
                    catch (Exception)
                    {
                        cameraIpp = "000000000"; //send something
                    }                   
                }

            }

            using (FileStream stream = File.Open(PCip, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader objRead = new StreamReader(stream))
                {
                    string PCipp = objRead.ReadToEnd();
                    try
                    {
                        if (PCipp.Substring(0, 1) == "1")
                        {
                            PCipp = "1";
                            textBox25.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            PCipp = "0";
                            textBox25.BackColor = Color.LightCoral;
                        }
                    }
                    catch (Exception)
                    {
                        PCipp = "000000000"; //send something
                    }                 
                }

                if (textBox24.BackColor == Color.LightGreen && textBox24.BackColor == Color.LightGreen)
                {
                    //Thread.Sleep(1);
                    //button19.Enabled = true;
                    timer14.Enabled = true; //allow user to click run button
                    
                }
                else
                {
                    button19.Enabled = false;
                    timer14.Enabled = false;

                }

                if (textBox24.BackColor == Color.LightGreen && textBox24.BackColor == Color.LightGreen && USBDetectRR=="1")
                {
                    //Thread.Sleep(1);
                    timer13.Enabled = true;//run main python program
                  //  button13.Enabled = true;                   
                    textBox26.BackColor = Color.LightGreen;
                }
                else
                {
                    timer13.Enabled = false;
                   // button13.Enabled = false;
                    textBox26.BackColor = Color.LightCoral;

                }
            }
 //----------------------------------PanTilt--------------------------------------------------------------------------

            using (FileStream stream = File.Open(Gpadtiltup, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader objRead = new StreamReader(stream))
                {
                    Gpadtiltupp = objRead.ReadToEnd();
                    try
                    {
                        if (Gpadtiltupp.Substring(0, 1) == "1")
                        {

                            Gpadtiltupp = "1";
                        }
                        else
                        {
                            Gpadtiltupp = "0";
                        }
                    }
                    catch (Exception)
                    {
                        Gpadtiltupp = "000000000"; //send something
                    }
                }
            }

            using (FileStream stream = File.Open(GpadtiltDwn, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader objRead = new StreamReader(stream))
                {
                    GpadtiltDwnn = objRead.ReadToEnd();
                    try
                    {
                        if (GpadtiltDwnn.Substring(0, 1) == "1")
                        {

                            GpadtiltDwnn = "1";
                        }
                        else
                        {
                            GpadtiltDwnn = "0";
                        }
                    }
                    catch (Exception)
                    {
                        GpadtiltDwnn = "000000000"; //send something
                    }
                }
            }

            using (FileStream stream = File.Open(GpadPanleft, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader objRead = new StreamReader(stream))
                {
                    GpadPanleftt = objRead.ReadToEnd();
                    try
                    {
                        if (GpadPanleftt.Substring(0, 1) == "1")
                        {

                            GpadPanleftt = "1";
                        }
                        else
                        {
                            GpadPanleftt = "0";
                        }
                    }
                    catch (Exception)
                    {
                        GpadPanleftt = "000000000"; //send something
                    }
                }
            }
            using (FileStream stream = File.Open(GpadPanrht, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader objRead = new StreamReader(stream))
                {
                    GpadPanrhtt = objRead.ReadToEnd();
                    try
                    {
                        if (GpadPanrhtt.Substring(0, 1) == "1")
                        {

                            GpadPanrhtt = "1";
                        }
                        else
                        {
                            GpadPanrhtt = "0";
                        }
                    }
                    catch (Exception)
                    {
                        GpadPanrhtt = "000000000"; //send something
                    }
                }
            }
          

            using (FileStream stream = File.Open(DLat, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    string DLatt;
                    DLatt = textBox3.Text;
                    objWriter.Write(DLatt);
                }
            }//---

            using (FileStream stream = File.Open(DLng, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    string DLngg;
                    DLngg = textBox4.Text;
                    objWriter.Write(DLngg);
                }
            }//---
            using (FileStream stream = File.Open(DHdg, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    string DHdgg;
                    DHdgg = textBox5.Text;
                    objWriter.Write(DHdgg);
                }
            }//---
            using (FileStream stream = File.Open(DAlt, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    string DAltt;
                    DAltt = textBox6.Text;
                    objWriter.Write(DAltt);
                }
            }//--
        }
        private void button12_Click(object sender, EventArgs e)
        {
            
            if (textBox22.Text=="18" || textBox22.Text=="19")
            {
                textBox22.BackColor = Color.LightGreen; 
            }

            timer1.Enabled = false;//pan
            timer2.Enabled = false;//home pan
            timer3.Enabled = false;//tilt
            timer4.Enabled = false;//home tilt

            //--------------------------resetpan-------------------------------------
            stoppanmtrright();
            stopPanMtrLft();
            //-----------------------resettilt---------------------------------------
            stopTiltmtrup();
            stopTiltmtrdwn();
            Thread.Sleep(1000);
            timer2.Enabled = true;
            timer4.Enabled = true;
          
        }
      
        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
           // if(tabControl1.SelectedTab==tabPage1)
           // {
                //textBox26.Text = "this";
                //timer10.Enabled = true;
          ////  }
            //if (tabControl1.SelectedTab == tabPage2)
          //  {
                //textBox26.Text = "what";
                //timer10.Enabled = false;
           // }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            
            ProcessStartInfo Networkping = new ProcessStartInfo("NCPA.cpl");           
            Process.Start(Networkping);          
        }

        private void timer11_Tick(object sender, EventArgs e)
        {
            using (FileStream stream = File.Open(USBDetectR, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader objRead = new StreamReader(stream))
                {
                    USBDetectRr = objRead.ReadToEnd();
                     USBDetectRR = USBDetectRr;
                    try
                    {
                        if (USBDetectRR.Substring(1, 1) == "1")
                        {
                            USBDetectRR = "1";
                           // textBox26.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            USBDetectRR = "0";
                            //textBox26.BackColor = Color.LightCoral;
                        }
                    }
                    catch (Exception)
                    {
                        USBDetectRR = "000000000"; //send something
                    }
                }
            }
            richTextBox2.Text += Environment.NewLine + USBDetectRr;
            if (richTextBox2.Text.Length > 3000)
            {
                richTextBox2.Clear();
            }

        }

        private void button19_Click(object sender, EventArgs e)
        {
                      
            ProcessStartInfo USBDT = new ProcessStartInfo("C:\\CamVid\\USBDetect\\USBDetect.exe");

            runPgm = runPgm + 1;

            if (runPgm > 2)

            { runPgm = 1; } //toggle back and forth

            if (runPgm == 1)
            {
                Process.Start(USBDT);
                Thread.Sleep(2000);
                timer11.Enabled = true;
                button19.Text = "Run Program";
            }

            if (runPgm == 2)
            {
                Process[] processe3 = Process.GetProcessesByName("USBDetect");
                foreach (Process pro3 in processe3)
                    pro3.Kill();
                timer11.Enabled = false;
                button19.Text = "Retry Run";

                Process[] process5 = Process.GetProcessesByName("CamGimbal10X");//stop main program if its still running
                 foreach (Process pro5 in process5)
                 pro5.Kill();

                Process[] processe4 = Process.GetProcessesByName("framecopy");
                foreach (Process pro4 in processe4)
                    pro4.Kill();

                XXDD = 0;
            }
            
           
        }

        private void button20_Click(object sender, EventArgs e)
        {
            /*
             Process[] process5 = Process.GetProcessesByName("CamGimbal10X");
             foreach (Process pro5 in process5)
                 pro5.Kill();

             Thread.Sleep(1000);

             using (FileStream stream = File.Open(USBDetectT, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
             {
                 using (StreamWriter objWriter = new StreamWriter(stream))
                 {
                     objWriter.Write("2");

                 }
             }*/

            Process[] process5 = Process.GetProcessesByName("CamGimbal10X");
            foreach (Process pro5 in process5)
                pro5.Kill();
            Thread.Sleep(1000);
            Process[] processes = Process.GetProcessesByName(Gamepadd);
            foreach (Process pro in processes)
                pro.Kill();
            Thread.Sleep(1000);
               Process[] processes2 = Process.GetProcessesByName("DKitCam");
            foreach (Process pro2 in processes2)
                pro2.Kill();
            Thread.Sleep(1000);

            using (FileStream stream = File.Open(USBDetectT, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write("2");

                }
            }//------ 
            string message = "Close down main form while system reboots, shutdown camera gimbal";
            string title = "System Reboot";
            MessageBox.Show(message, title);

        }

        private void tabPage1_SizeChanged(object sender, EventArgs e)
        {
            sizewindow();
        }
     
        private void timer12_Tick(object sender, EventArgs e)
        {
            if (repoint != xrw)
            {
                repoint = repoint + 1;
            }
            if (repoint == xrw)
            {
                // textBox27.Text = "this"; stop timer here
                timer12.Enabled = false;
            }

            iLat = Convert.ToDouble(dataGridView1.Rows[repoint].Cells[0].Value);
            iLong = Convert.ToDouble(dataGridView1.Rows[repoint].Cells[1].Value);

            marker = new GMarkerGoogle(new PointLatLng(iLat, iLong), GMarkerGoogleType.green);
            marker.ToolTipMode = MarkerTooltipMode.Always;
            marker.ToolTipText = string.Format("{0}", repoint);
            markerOverlay.Markers.Add(marker);
            gMapControl1.Overlays.Add(markerOverlay);
            //gMapControl1.Refresh();
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            this.Size = new Size(1156, 670);
            
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            this.Size = new Size(1325, 779);
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            this.Size = new Size(1474, 885);
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            this.Size = new Size(1725, 1028);
        }

        private void timer13_Tick(object sender, EventArgs e)
        {
            XXDD = XXDD + 1;

            if (XXDD >= 2)

            { XXDD = 2; } //toggle back and forth

            if (XXDD == 1)
            {
                
                Thread.Sleep(500);
                ProcessStartInfo ps1 = new ProcessStartInfo("C:\\CamVid\\CamGimbalX\\dist\\CamGimbal10X\\CamGimbal10X.exe");
                ps1.WindowStyle = ProcessWindowStyle.Normal;
                Process p1 = Process.Start(ps1);
                p1.WaitForInputIdle();
                appWin1 = p1.MainWindowHandle;
                SetParent(appWin1, pictureBox1.Handle);
                sizewindow();
                textBox26.BackColor = Color.LightGreen;
                
                /*
                Thread.Sleep(500);
                ProcessStartInfo ps13 = new ProcessStartInfo("C:\\CamVid\\Framecpy\\dist\\framecopy\\framecopy.exe");
                ps13.WindowStyle = ProcessWindowStyle.Normal;
                Process p13 = Process.Start(ps13);
                p13.WaitForInputIdle();
                appWin2 = p13.MainWindowHandle;
                sizewindow1();
                SetParent(appWin2, pictureBox3.Handle);

                */





            }

            if (XXDD == 2)
            {

                timer13.Enabled = false;
                //Process[] process5 = Process.GetProcessesByName("CamGimbal10X");
                // foreach (Process pro5 in process5)
                // pro5.Kill();
            }

        }

        private void timer14_Tick(object sender, EventArgs e)
        {
            RunBtn = RunBtn + 1;

            if (RunBtn >= 2)

            { RunBtn = 2; }

            if (RunBtn == 1)
            {
                button19.Enabled = true;//enable user to click the run button
            }

            if (RunBtn == 2)
            {

                timer14.Enabled = false;

            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            /*
            Thread.Sleep(500);
            ProcessStartInfo ps13 = new ProcessStartInfo("C:\\CamVid\\Framecpy\\dist\\framecopy\\framecopy.exe");
            ps13.WindowStyle = ProcessWindowStyle.Normal;
            Process p13 = Process.Start(ps13);
            p13.WaitForInputIdle();
            appWin2 = p13.MainWindowHandle;
            SetParent(appWin2, pictureBox3.Handle);
           // sizewindow();
            */
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            gMapControl1.Zoom = gMapControl1.Zoom + 1;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            gMapControl1.Zoom = gMapControl1.Zoom - 1;
        }

        private void button22_Click(object sender, EventArgs e)//clockwise
        {
            nudge = nudge + 4;
            MidwayCalc = MidwayCalc + 4;
            textBox23.Text = Convert.ToString(MidwayCalc);
            textBox29.Text = Convert.ToString(nudge);

            Panmtrright();
            Thread.Sleep(250);
            stoppanmtrright();
        }

        private void button23_Click(object sender, EventArgs e)//counterclockwise
        {
           

            nudge = nudge - 4;
            MidwayCalc = MidwayCalc - 4;
            textBox23.Text = Convert.ToString(MidwayCalc);
            textBox29.Text = Convert.ToString(nudge);

            Panmtrleft();
            Thread.Sleep(250);
            stopPanMtrLft();

        }

        private void button24_Click(object sender, EventArgs e)
        {
           
            gMapControl1.Position = new PointLatLng(gMapControl1.Position.Lat + panFactor, gMapControl1.Position.Lng);//drag map down
        }

        private void button25_Click(object sender, EventArgs e)
        {
            gMapControl1.Position = new PointLatLng(gMapControl1.Position.Lat - panFactor, gMapControl1.Position.Lng);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            gMapControl1.Position = new PointLatLng(gMapControl1.Position.Lat, gMapControl1.Position.Lng + panFactor);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            gMapControl1.Position = new PointLatLng(gMapControl1.Position.Lat, gMapControl1.Position.Lng - panFactor);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleTerrainMap;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleKoreaSatelliteMap;
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            
            hScrollBar1.Value = hScrollBar1.Value + 1;
            BD = hScrollBar1.Value + 100;
            
            label42.Text=hScrollBar1.Value.ToString();

            using (FileStream stream = File.Open(Bdd, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write(BD);
                }
            }//------
        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            hScrollBar2.Value = hScrollBar2.Value + 1;
            bb = hScrollBar2.Value + 100;

            label43.Text = hScrollBar2.Value.ToString();

            using (FileStream stream = File.Open(Bb, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write(bb);
                }
            }//------
        }

        private void button31_Click(object sender, EventArgs e)
        {
            hScrollBar1.Value = 255;
            BD = hScrollBar1.Value + 100;
            label42.Text = hScrollBar1.Value.ToString();
           
            using (FileStream stream = File.Open(Bdd, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write(BD);
                }
            }//------

            Thread.Sleep(500);
            hScrollBar2.Value = 1;
            label43.Text = hScrollBar2.Value.ToString();
            bb = hScrollBar2.Value + 100;
            using (FileStream stream = File.Open(Bb, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter objWriter = new StreamWriter(stream))
                {
                    objWriter.Write(bb);
                }
            }//------

        }

      

        private void button32_Click(object sender, EventArgs e)
        {
            pulse = pulse + 1;

            if (pulse > 2)

            { pulse = 1;  } //toggle back and forth

            if (pulse == 1) //finetune off
            {

                using (FileStream stream = File.Open(plss, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter objWriter = new StreamWriter(stream))
                    {
                        objWriter.Write("E");
                    }
                }


                button32.BackColor = Color.LightGreen;
               

            }
            if (pulse == 2)//finetune on
            {

                using (FileStream stream = File.Open(plss, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter objWriter = new StreamWriter(stream))
                    {
                        objWriter.Write("F");
                    }
                }

                button32.BackColor = Color.LightCoral;

            }
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        //-------------------------------------------------------------------------------------------------------------------------------
    }//----------
}//--------------------------
 