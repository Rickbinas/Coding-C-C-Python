using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamGimbal
{
    internal class Encoderpoint
    {
       /* 
        public int cmprHeading = 0;
        public int cmprBearing = 0;
        public int cmpHB = 0;
        public double cmprHB1 = 0;
        public double cmprHB = 0;
        public int cmprResultVector = 0;

        public double cmprPitch = 0;
        public int enctiltdt = 0;
        public double cmprPitchResultVector = 0;
       */
       

        public void encodept(Form1 form) 
        {
           /*
            //-----------------Drone Heading Encoder Points-----------------------------
            if (cmprHB >= 0 && cmpHB <= 18 || cmprHB <= 359 && cmprHB >= 342) { cmprHeading = 14; }
            if (cmprHB > 18 && cmpHB <= 54) { cmprHeading = 15; }
            if (cmprHB > 54 && cmpHB <= 90) { cmprHeading = 16; }
            if (cmprHB > 90 && cmpHB <= 126) { cmprHeading = 17; }
            if (cmprHB > 126 && cmpHB <= 162) { cmprHeading = 18; }
            if (cmprHB > 162 && cmpHB <= 198) { cmprHeading = 19; }
            if (cmprHB > 198 && cmpHB <= 234) { cmprHeading = 10; }
            if (cmprHB > 234 && cmpHB <= 270) { cmprHeading = 11; }
            if (cmprHB > 270 && cmpHB <= 306) { cmprHeading = 12; }
            if (cmprHB > 306 && cmpHB < 342) { cmprHeading = 13; }

//-------------------Position Clicked on map, Encoder points----------------------------------
            if (cmprHB1 >= 0 && cmprHB1 <= 18 || cmprHB1 <= 359 && cmprHB1 >= 342) { cmprBearing = 14; }
            if (cmprHB1 > 18 && cmprHB1 <= 54) { cmprBearing = 15; }
            if (cmprHB1 > 54 && cmprHB1 <= 90) { cmprBearing = 16; }
            if (cmprHB1 > 90 && cmprHB1 <= 126) { cmprBearing = 17; }
            if (cmprHB1 > 126 && cmprHB1 <= 162) { cmprBearing = 18; }
            if (cmprHB1 > 162 && cmprHB1 <= 198) { cmprBearing = 19; }
            if (cmprHB1 > 198 && cmprHB1 <= 234) { cmprBearing = 10; }
            if (cmprHB1 > 234 && cmprHB1 <= 270) { cmprBearing = 11; }
            if (cmprHB1 > 270 && cmprHB1 <= 306) { cmprBearing = 12; }
            if (cmprHB1 > 306 && cmprHB1 < 342) { cmprBearing = 13; }

            cmprHB = Convert.ToInt32(textBox5.Text);//heading
            cmprHB1 = Convert.ToDouble(textBox8.Text); //clickbearing

            //------------------------------------------------------------------------------

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
            //---------------------------Pitch--------------------------------------------------------------
            //enctiltdt = Convert.ToInt32(textBox14.Text);
            //cmprPitch = Convert.ToDouble(textBox9.Text);
            //enctiltdt = Convert.ToInt32(textBox14);
           // cmprPitch = Convert.ToDouble(textBox9);

            if (cmprPitch >= 0 && cmprPitch <= 7)
            { cmprPitchResultVector = 51; }
            if (cmprPitch >= 8 && cmprPitch <= 14)
            { cmprPitchResultVector = 52; }
            if (cmprPitch >= 15 && cmprPitch <= 22)
            { cmprPitchResultVector = 53; }
            if (cmprPitch >= 23 && cmprPitch <= 29)
            { cmprPitchResultVector = 54; }
            if (cmprPitch >= 30 && cmprPitch <= 37)
            { cmprPitchResultVector = 55; }
            if (cmprPitch >= 38 && cmprPitch <= 44)
            { cmprPitchResultVector = 56; }
            if (cmprPitch >= 45)
            { cmprPitchResultVector = 57; }
            */
        }
    }
}
