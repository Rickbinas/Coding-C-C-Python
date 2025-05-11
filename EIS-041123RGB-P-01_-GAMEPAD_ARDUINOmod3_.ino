
//Equinox Joystick Controller 07-05-2023
//Adjusted for QGCS
//Joystick 3
//--------------------------------------------------------------------
#include <Joystick.h>
bool initialized = false;
int buttonState1 = 0;
int buttonState2 = 0;
int buttonState3 = 0;
int buttonState4 = 0;
int buttonState5 = 0;
int buttonState6 = 0;

int buttonState7 = 0;
int buttonState8 = 0;
int buttonState9 = 0;

int xValue = 0;
int yThrottle = 0;
int DZMThrottle = 10;//throttle center deadband
int xRudder = 0;
int DZMRudder = 8;//rudder(yaw) center deadband
int yPitch = 0;
int DZMPitch = 10;//pitch center deadband
int xRoll = 0;
int DZMRoll = 10;//roll center deadband

void setup() {
  
  Serial.begin(9600);  
  // Initialize Button Pins
  pinMode(1, INPUT_PULLUP);
  pinMode(2, INPUT_PULLUP);
  pinMode(3, INPUT_PULLUP);
  pinMode(4, INPUT_PULLUP);
  pinMode(5, INPUT_PULLUP);
  pinMode(6, INPUT_PULLUP);

  pinMode(7, INPUT_PULLUP);
  pinMode(8, INPUT_PULLUP);
  pinMode(9, INPUT_PULLUP);
  //------------analog joystick-------------
  pinMode(A0, INPUT);
  pinMode(A1, INPUT);
  pinMode(A2, INPUT);
  pinMode(A3, INPUT);
  
  //Initialize Joystick Library
  //Joystick.setButton(swbutton, state 1=high 0 = low);}
  Joystick.begin();
}
void loop() {

   if(initialized){
//---------------------------------Buttons--------------------------------------------
  //buttonState1 = digitalRead(1); //Loiter
   //if (buttonState1 == HIGH) {Joystick.setButton(1, 0);} else {Joystick.setButton(1, 1);}
  buttonState2 = digitalRead(2); //Aux
   if (buttonState2 == HIGH) {Joystick.setButton(2, 0);} else {Joystick.setButton(2, 1);}
  buttonState3 = digitalRead(3); //Land
   if (buttonState3 == HIGH) {Joystick.setButton(3, 0);} else {Joystick.setButton(3, 1);}
  buttonState4 = digitalRead(4); //Aux
   if (buttonState4 == HIGH) {Joystick.setButton(4, 0);} else {Joystick.setButton(4, 1);}
  buttonState5 = digitalRead(5); //Aux
   if (buttonState5 == HIGH) {Joystick.setButton(5, 0);} else {Joystick.setButton(5, 1);}
  buttonState6 = digitalRead(6); //Aux
   if (buttonState6 == HIGH) {Joystick.setButton(6, 0);} else {Joystick.setButton(6, 1);}

  buttonState9 = digitalRead(9); //Arm
  if (buttonState9 == HIGH) {Joystick.setButton(9, 0);} else {Joystick.setButton(9, 1);}

   
 //-------------------------------Arm_Disarm---------------------------------------------
 
  buttonState7 = digitalRead(7); //DisArm
   if (buttonState7 == HIGH) {Joystick.setButton(7, 0);} else {Joystick.setButton(7, 1);}
   
  buttonState8 = digitalRead(8); //Arm
  if (buttonState8 == HIGH) {Joystick.setButton(8, 0);} else {Joystick.setButton(8, 1);}
  
//----------------------------analog joystick--------------------------------------------

   xRudder  = analogRead(A0)/2;//----------------------------------------------------
  xRudder = map(xRudder,1023,0,280,930);//--------reverse--------------------------              
  if ((xRudder > (768 - DZMRudder)) && (xRudder < (768 + DZMRudder))) xRudder = 767; 
  //if (xRudder>772){xRudder = xRudder -10;}//upper scale
 // if ((xRudder > (895))) xRudder = 896;//high end
  //if ((xRudder < (644))) xRudder = 641;//low end                            
  Joystick.setYAxis(xRudder); 
  
 yThrottle = analogRead(A1);//------------------------------------------------------------ 
 yThrottle= map(yThrottle,0,1023,320,710); //reverse
  if ((yThrottle > (515 - DZMThrottle)) && (yThrottle < (515 + DZMThrottle))) yThrottle = 512;   
  //if (yThrottle>258 ){yThrottle = yThrottle ;}//upper scale 
  //if ((yThrottle > (380))) yThrottle = 384;//high end
  //if (yThrottle<258){yThrottle = 30 + yThrottle ;}//lower scale
  //if ((yThrottle < (132))) yThrottle = 129;//low end                                     
  Joystick.setXAxis(yThrottle); 
   
   xRoll = analogRead(A3); //------------------------------------------------------------            
  xRoll = map(xRoll,1023,0,290,760);//--------reverse-------------------------- 
   
  if ((xRoll > (542 - DZMRoll)) && (xRoll < (542 + DZMRoll))) xRoll = 539; 
  //if ((xRoll > (766))) xRoll = 767;//high end//
  //if ((xRoll < (514))) xRoll = 512;//low end//                                 
  //Joystick.setRudder(xRoll);
  
  Joystick.setXAxisRotation(xRoll);
 
 yPitch = analogRead(A2);  //pitch//------------------------------------------------          
 yPitch = map(yPitch,0,1023,60,420); 
 //if ((yPitch > (258 - DZMPitch)) && (yPitch < (258 + DZMPitch))) yPitch = 255;
// if ((yPitch > (276 + DZMPitch))) yPitch = yPitch - 20;
 //if ((yPitch > (383))) yPitch = 384;//high 
// if ((yPitch < (130))) yPitch=129;//low end                            
 Joystick.setZAxis(yPitch); 
  
 //-----------Serial Debug --------------------------------------------------------------------- 
  Serial.print("Throttle:");Serial.print(yThrottle);Serial.print(" Yaw:");Serial.print(xRudder);
  Serial.print(" Pitch:");Serial.print(yPitch);Serial.print(" Roll:");Serial.println(xRoll);
   } else {
    buttonState7 = digitalRead(7);
    if (buttonState7 == HIGH) {initialized = true;}
  }
}
