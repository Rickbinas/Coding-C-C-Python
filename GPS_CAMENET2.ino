#include <UIPEthernet.h>

EthernetServer server = EthernetServer(5000);
boolean alreadyConnected = false;
//GPS PARSE WITH HOLYBRO COMPASS WITH ARDUINO MICRO
//--------------------------------------------
byte GPSBuffer[82];
byte c[82];
byte GPSIndex = 0;
int inByte;
int y1,y2,y3,y4,y5,y6,y7,y8,y9;
char NS,WE;
int x1,x2,x3,x4,x5,x6,x7,x8,x9,x10;
int sensorValue = 0;
char V; 
//----convert--------------
char yy1,yy2,yy3,yy4,yy5,yy6,yy7,yy8,yy9;
char xx1,xx2,xx3,xx4,xx5,xx6,xx7,xx8,xx9,xx10;
//-----------------gyro----------------------------
char gy;
char gp;
char gr;
#include "I2Cdev.h"
#include "MPU6050_6Axis_MotionApps20.h"
#if I2CDEV_IMPLEMENTATION == I2CDEV_ARDUINO_WIRE
#include "Wire.h"
#endif

MPU6050 mpu;
#define OUTPUT_READABLE_YAWPITCHROLL
#define INTERRUPT_PIN 2  // use pin 2 on Arduino Uno & most boards


uint8_t mpuIntStatus;   // holds actual interrupt status byte from MPU
uint8_t devStatus;      // return status after each device operation (0 = success, !0 = error)
uint16_t packetSize;    // expected DMP packet size (default is 42 bytes)
uint16_t fifoCount;     // count of all bytes currently in FIFO
uint8_t fifoBuffer[64]; // FIFO storage buffer

// orientation/motion vars
Quaternion q;           // [w, x, y, z]         quaternion container
VectorInt16 aaWorld;    // [x, y, z]            world-frame accel sensor measurements
VectorFloat gravity;    // [x, y, z]            gravity vector
float euler[3];         // [psi, theta, phi]    Euler angle container
float ypr[3];           // [yaw, pitch, roll]   yaw/pitch/roll container and gravity vector
float Gyp,Gypp,Gyppp;
float Gyr,Gyrr,Gyrrr;
float Gyy,Gyyy,Gyyyy;



void setup()
{
  
pinMode(8, OUTPUT);
digitalWrite(8, LOW);
  
uint8_t mac[1] = {0x00};
IPAddress myIP( 169,254,178,244);
Ethernet.begin(mac,myIP);
server.begin();

 // Serial.begin(115200); //GPS
  //while (!Serial);
  Serial1.begin(9600); //serial monitor
  while (!Serial1);
//-------------------------------------------
 // join I2C bus (I2Cdev library doesn't do this automatically)
    #if I2CDEV_IMPLEMENTATION == I2CDEV_ARDUINO_WIRE
        Wire.begin();
        Wire.setClock(400000); // 400kHz I2C clock. Comment this line if having compilation difficulties
    #elif I2CDEV_IMPLEMENTATION == I2CDEV_BUILTIN_FASTWIRE
        Fastwire::setup(400, true);
    #endif
  // Serial.begin(9600);
   while (!Serial);   
    mpu.initialize();devStatus = mpu.dmpInitialize();mpu.setXGyroOffset(220);mpu.setYGyroOffset(76); mpu.setZGyroOffset(-85);
    mpu.setZAccelOffset(1788); // 1688 factory default for my test chip
  
   if (devStatus == 0) 
   {      
       mpu.setDMPEnabled(true);      
       mpuIntStatus = mpu.getIntStatus();       
       packetSize = mpu.dmpGetFIFOPacketSize();
    }    
}

void loop()
{
 EthernetClient client = server.available();
 
while (client.available()) 
{
  char newChar = client.read();
  
if (newChar =='@') 
{ 
  V=sensorValue;
 sensorValue = analogRead(A0);
 sensorValue += 1000;

 //Serial.print(sensorValue);
 client.print(V);
 //Serial.print(y1); Serial.print(y2); Serial.print(y3);  Serial.print(y4);  Serial.print(y5);  Serial.print(y6);  Serial.print(y7);  Serial.print(y8);  Serial.print(y9); 
 //client.print(yy1); client.print(yy2); client.print(yy3);  client.print(yy4);  client.print(yy5);  client.print(yy6);  client.print(yy7);  client.print(yy8);  client.print(yy9); 
 
 //Serial.print(x1); Serial.print(x2); Serial.print(x3);  Serial.print(x4);  Serial.print(x5);  Serial.print(x6);  Serial.print(x7);  Serial.print(x8);  Serial.print(x9); Serial.print(x10);
 // client.print(xx1); client.print(xx2); client.print(xx3);  client.print(xx4);  client.print(xx5); client.print(xx6);  client.print(xx7);  client.print(xx8);  client.print(xx9); client.print(xx10);
 //Serial.print(NS); Serial.println(WE);
 //client.print(NS); client.println(WE);
 //---------------------------------------------
 
  mpuIntStatus = mpu.getIntStatus();    
    fifoCount = mpu.getFIFOCount();  
    if ((mpuIntStatus & 0x10) || fifoCount == 1024)
    {      
        mpu.resetFIFO();
       // Serial.println(F("FIFO overflow!"));  
    } 
    else if (mpuIntStatus & 0x02) 
    {     
        while (fifoCount < packetSize) fifoCount = mpu.getFIFOCount();      
        mpu.getFIFOBytes(fifoBuffer, packetSize);           
        fifoCount -= packetSize;            
   mpu.dmpGetQuaternion(&q, fifoBuffer); mpu.dmpGetGravity(&gravity, &q);mpu.dmpGetYawPitchRoll(ypr, &q, &gravity);//ypr
   // Serial.print(ypr[0] * 180/M_PI); Serial.print(ypr[1] * 180/M_PI);Serial.println(ypr[2] * 180/M_PI);
        Gyp=ypr[1] * 180/M_PI;
           if(Gyp>-90) {  Gypp=Gyp-(-90);  Gyppp=Gypp+100;}
        Gyr=ypr[2] * 180/M_PI;
           if(Gyr>-90){ Gyrr=Gyr-(-90); Gyrrr=Gyrr+100;}
        Gyy=ypr[0] * 180/M_PI;
          if(Gyy>-179){ Gyyy=Gyy-(-179); Gyyyy=Gyyy+1000;}  
         // Serial.println(Gyrrr);
          //Serial.println(Gyppp); 
         // Serial.println(Gyyyy); 
         //gr =Gyrrr;
        // gp =Gyppp;
        // gy =Gyyyy;
    }
 

  while (Serial1.available() > 0)
  {
    inByte = Serial1.read();

    //Serial.write(inByte); // Output exactly what we read from the GPS to debug

    if ((inByte == '$') || (GPSIndex >= 80)){ GPSIndex = 0;}
    if (inByte != '\r'){GPSBuffer[GPSIndex++] = inByte;}
    if (inByte == '\n')
    {     
      GPSIndex = 0;
   
    if ((GPSBuffer[1] == 'G') && (GPSBuffer[2] == 'N') && (GPSBuffer[3] == 'R') && (GPSBuffer[4] == 'M') && (GPSBuffer[5] == 'C'))
    {  
  //$GNRMC,125827.00,A,3908.85095,N,07647.80188,W
   
   y1=(GPSBuffer[19]-48); y2=(GPSBuffer[20]-48); y3=(GPSBuffer[21]-48); y4=(GPSBuffer[22]-48); y5=(GPSBuffer[24]-48); y6=(GPSBuffer[25]-48); y7=(GPSBuffer[26]-48); y8=(GPSBuffer[27]-48); y9=(GPSBuffer[28]-48);
   yy1=y1; yy2=y2; yy3=y3 ;yy4=y4; yy5=y5; yy6=y6; yy7=y7; yy8=y8; yy9=y9;
   x1=(GPSBuffer[32]-48);x2=(GPSBuffer[33]-48);x3=(GPSBuffer[34]-48);x4=(GPSBuffer[35]-48);x5=(GPSBuffer[36]-48); x6=(GPSBuffer[38]-48); x7=(GPSBuffer[39]-48); x8=(GPSBuffer[40]-48); x9=(GPSBuffer[41]-48);x10=(GPSBuffer[42]-48);
   xx1=x1; xx2=x2; xx3=x3; xx4=x4; xx5=x5; xx6=x6; xx7=x7; xx8=x8; xx9=x9; xx10=x10;
   NS = (GPSBuffer[30]); WE=GPSBuffer[44];
  
    }
    
 }  
}
//-------------------------
}
if (newChar =='U') {digitalWrite(8, HIGH);}
  
}


}//-----------------------------------------------------------


