#include <UIPEthernet.h>
#include <Servo.h>

#include "ENCODERDATA.h"
#include "GPSLATLONG.h"

EthernetServer server = EthernetServer(5000);
boolean alreadyConnected = false;
//GPS PARSE WITH HOLYBRO COMPASS WITH ARDUINO MICRO
//--------------------------------------------

int VoltageLevel = 0;
int TempLevel = 0;
int Spare = 0;

Servo pitch;
int val;
//----------------------------------------------
void setup()
{
pinMode(9, OUTPUT);//pwm
//digitalWrite(9, LOW);
pinMode(7, OUTPUT);
digitalWrite(7, LOW);

pinMode(11, OUTPUT);
digitalWrite(11, LOW);
pinMode(12, OUTPUT);
digitalWrite(12, LOW);
//------------------Ethernet-------------------  
uint8_t mac[1] = {0x00};
//IPAddress myIP( 169,254,178,244);
IPAddress myIP( 192,168,168,46);
Ethernet.begin(mac,myIP);
server.begin();
//---------------------------------------------
Serial.begin(9600); //Monitor
  while (!Serial);
Serial1.begin(9600); //GPS RX IN
  while (!Serial1);
Serial2.begin(9600); //Encoder
  while (!Serial2);
//-------------------------------------------    
}
void loop()
{ 
EthernetClient client = server.available();
 
while (client.available()) 
{
  char newChar = client.read();
  
if (newChar =='@') 
{ 
 //-------------------------SendData-------------------
TempLevel = analogRead(A0);
TempLevel += 1000;
client.print("B");client.print(TempLevel);
client.print("G");client.print("0000"); 
/*
VoltageLevel  = analogRead(A1);
VoltageLevel  += 1000;
client.print("G");client.print(VoltageLevel); 

Spare = analogRead(A2);
Spare += 1000;
client.print("M");client.print(Spare);*/
client.print("M");client.print("0000");
client.print("X");client.print(EncAresult);client.print(EncA5);client.print(EncA6);//Encoder A
client.print(EncBresult);client.print(EncB5);client.print(EncB6);
//client.print(LatLong); //gps
client.println("K");//Encoder B
//--------------------------------------------------------
//CallGPS(); //GPS
ReadEncoder(); //Encoder A and B
//--------------------------------------------------------
}
if (newChar =='U')
{
 // Serial.println("U");
   pitch.attach(9); 
   pitch.writeMicroseconds(1700);
   digitalWrite(9, HIGH);
  
}
if (newChar =='D') 
{
   pitch.attach(9); 
   pitch.writeMicroseconds(1300);
   delay(1);
 }
if (newChar =='L') {digitalWrite(11, HIGH);}
if (newChar =='R') {digitalWrite(12, HIGH);}

if (newChar =='S') 
{
   pitch.attach(9);  
   pitch.writeMicroseconds(1500);
   delay(5);
}
//if (newChar =='F')
//{
// pitch.attach(9);  
 //  pitch.writeMicroseconds(1500);
//}
if (newChar =='K') {delay(1);digitalWrite(11, LOW);}
if (newChar =='C') {delay(1);digitalWrite(12, LOW);}  
}
// pitch.writeMicroseconds(1500);
   delay(1);

//client.print("K");
client.flush();
client.stop();

 Serial.print("B"); Serial.print(VoltageLevel); 
 Serial.print("G"); Serial.print(TempLevel);
 Serial.print("M"); Serial.print(Spare);
 Serial.print("X"); Serial.print(EncAresult); Serial.print(EncA5); Serial.print(EncA6);//Encoder A
 Serial.print(EncBresult); Serial.print(EncB5); Serial.print(EncB6);
// Serial.print(LatLong); //gps
 Serial.println("K");//Encoder B
  
}//----------------------------------------Notes-------------------

//B1575G1483M1417Q1362V1322T1318Z000000000Z0000000000Z00X1652132447K
//B1552G0000M0000X1939539039K
