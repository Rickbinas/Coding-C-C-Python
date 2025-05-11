#include <I2C.h>
#include <Wire.h>
#include <Adafruit_BMP085.h>
Adafruit_BMP085 bmp;

//-----------------------------
 int sensorPin = A0;   
 int sensorValue = 0; 
 float KPA;
//------------battcurr
int BattValue = 0;        // value read from the pot
int BattoutputValue = 0;
int CurrentValue = 0;        // value read from the pot
int CurrentoutputValue = 0;
//------------------------------
#include "I2Cdev.h"
//#include "MPU6050.h"
#include "MPU6050_6Axis_MotionApps20.h"
#if I2CDEV_IMPLEMENTATION == I2CDEV_ARDUINO_WIRE
#include "Wire.h"
#endif

MPU6050 mpu;
//MPU6050 mpu(0x69); // <-- use for AD0 high
#define OUTPUT_READABLE_YAWPITCHROLL
#define INTERRUPT_PIN 2  // use pin 2 on Arduino Uno & most boards


// MPU control/status vars
bool dmpReady = false;  // set true if DMP init was successful
uint8_t mpuIntStatus;   // holds actual interrupt status byte from MPU
uint8_t devStatus;      // return status after each device operation (0 = success, !0 = error)
uint16_t packetSize;    // expected DMP packet size (default is 42 bytes)
uint16_t fifoCount;     // count of all bytes currently in FIFO
uint8_t fifoBuffer[64]; // FIFO storage buffer

// orientation/motion vars
Quaternion q;           // [w, x, y, z]         quaternion container
VectorInt16 aa;         // [x, y, z]            accel sensor measurements
VectorInt16 aaReal;     // [x, y, z]            gravity-free accel sensor measurements
VectorInt16 aaWorld;    // [x, y, z]            world-frame accel sensor measurements
VectorFloat gravity;    // [x, y, z]            gravity vector
float euler[3];         // [psi, theta, phi]    Euler angle container
float ypr[3];           // [yaw, pitch, roll]   yaw/pitch/roll container and gravity vector
float Gyp,Gypp,Gyppp;
float Gyr,Gyrr,Gyrrr;
float Gyy,Gyyy,Gyyyy;
volatile bool mpuInterrupt = false;     // indicates whether MPU interrupt pin has gone high
void dmpDataReady() {
    mpuInterrupt = true;
}
//MPU6050 accelgyro;
//int16_t ax, ay, az;
//int16_t gx, gy, gz;
//#define OUTPUT_READABLE_ACCELGYRO
//----------------------------
#include <HMC5983.h>
HMC5983 compass;

#define HMC5883L  0x1E 
int x = 0;
int y = 0;
int z = 0;
int xAx = 0;
int yAx= 0;
unsigned long hdg;

#include <TinyGPS++.h>
TinyGPSPlus gps;

int year;
byte month, day, hour, minute, second, hundredths;
unsigned long chars;
unsigned short sentences, failed_checksum;
unsigned long fix_age;
byte inBytes;
//byte GPSBuffer[82];
char GPSBuffer[80];
byte GPSIndex = 0;
int  cc;
#include <stdio.h> 
#include <Servo.h> 
Servo pitchservo;
Servo ailronservo;
Servo rudderservo;
Servo throttleservo;
Servo Aux1servo;
Servo Aux2servo;
Servo Aux3servo;

char Buffer[12];
byte c[82];
int Index = 0;
int pitch,p1,p2,p3,p4;
int aileron,a1,a2,a3,a4;
int rudder,r1,r2,r3,r4;
int throttle,t1,t2,t3,t4;
int aux1,ax1,ax2,ax3,ax4;
int aux2,aa1,aa2,aa3,aa4;
int aux3,ba1,ba2,ba3,ba4;

char inByte;
char gpps;
//char gga;
int zz=0;
float magx,magxx;
int magxxx; 
float magy,magyy;
int magyyy;
float axx;
void setup() 
{
  Serial.begin(9600); //monitorpc
  while (!Serial);
  Serial1.begin(9600); //serial pc
  while (!Serial1);
  Serial2.begin(9600); //serial GPS
  while (!Serial2);

  I2c.begin();
  I2c.write(HMC5883L,0x02,0x00); //configure device for continuous mode  
  compass.begin(); // use "true" if you need some debug information
  //---------------------------------------------------------
  #if I2CDEV_IMPLEMENTATION == I2CDEV_ARDUINO_WIRE
        Wire.begin();
        Wire.setClock(400000); // 400kHz I2C clock. Comment this line if having compilation difficulties
    #elif I2CDEV_IMPLEMENTATION == I2CDEV_BUILTIN_FASTWIRE
        Fastwire::setup(400, true);
    #endif
      mpu.initialize();
    pinMode(INTERRUPT_PIN, INPUT);
 
  while (Serial1.available() && Serial1.read()); // empty buffer again  
    devStatus = mpu.dmpInitialize();
   // supply your own gyro offsets here, scaled for min sensitivity
    mpu.setXGyroOffset(220);
    mpu.setYGyroOffset(76);
    mpu.setZGyroOffset(-85);
    mpu.setZAccelOffset(1788); // 1688 factory default for my test chip

   
   if (devStatus == 0) {
      
        mpu.setDMPEnabled(true);
   
        attachInterrupt(digitalPinToInterrupt(INTERRUPT_PIN), dmpDataReady, RISING);
        mpuIntStatus = mpu.getIntStatus();   
        dmpReady = true;
       
        packetSize = mpu.dmpGetFIFOPacketSize();
    } 
    //---------------------------------------------     
  pitchservo.attach(9);
  ailronservo.attach(10);
  rudderservo.attach(8);
  throttleservo.attach(7);
  Aux1servo.attach(6);
  Aux2servo.attach(5);
  Aux3servo.attach(11);   
}

void loop() 
{
 I2c.read(HMC5883L,0x03,6); //read 6 bytes (x,y,z) from the device
  x = I2c.receive() << 8;
  x |= I2c.receive();
  y = I2c.receive() << 8;
  y |= I2c.receive();
  z = I2c.receive() << 8;
  z |= I2c.receive(); 
 float c = -999;
  c = compass.read();
  if (c == -999) {
  //  Serial.println("Reading error, discarded");
    
  } else {
  c= c+100;
  
   Serial1.print("?");Serial1.print(magxxx);Serial1.print(",");Serial1.print(magyyy);Serial1.print(",");Serial1.print(c);Serial1.print(","); Serial1.print(Gyppp);
   Serial1.print(","); Serial1.print(Gyrrr); Serial1.print(",");Serial1.println(Gyyyy);
   magx=x;
   if(magx>-600)
          { 
           magxx=magx-(-600); 
           magxxx=magxx+1000;
          } 
   magy=y;
   if(magy>-600)
          { 
           magyy=magy-(-600); 
           magyyy=magyy+1000;
          }         
//Serial1.print("test");Serial1.println(x);
  }
  delay(5);
 if (bmp.begin()) {

   Serial1.print(";"); Serial1.print(bmp.readAltitude(101500));Serial1.print("0000");Serial1.println("");//barometric altitude
   Serial1.print("}"); Serial1.print(KPA);Serial1.print("0000"); Serial1.println(""); //True airspeed
  
  }
   Serial1.print("<");Serial1.print(BattoutputValue+1000);Serial1.println("");
   Serial1.print(">");Serial1.print(CurrentoutputValue+1000);Serial1.println("");
   //-------------------------------
   mpuInterrupt = false;
    mpuIntStatus = mpu.getIntStatus();   
    fifoCount = mpu.getFIFOCount(); 
    if ((mpuIntStatus & 0x10) || fifoCount == 1024) {      
        mpu.resetFIFO();     
    } else if (mpuIntStatus & 0x02) {      
        while (fifoCount < packetSize) fifoCount = mpu.getFIFOCount();     
        mpu.getFIFOBytes(fifoBuffer, packetSize);            
        fifoCount -= packetSize;
        #ifdef OUTPUT_READABLE_YAWPITCHROLL
            // display Euler angles in degrees
            mpu.dmpGetQuaternion(&q, fifoBuffer);
            mpu.dmpGetGravity(&gravity, &q);
            mpu.dmpGetYawPitchRoll(ypr, &q, &gravity);
           // Serial1.print("t"); Serial1.print(ypr[0] * 180/M_PI);Serial1.print(",");Serial1.print(ypr[1] * 180/M_PI);Serial1.print(","); Serial1.println(ypr[2] * 180/M_PI);
           
           Gyp=ypr[1] * 180/M_PI;
           if(Gyp>-90)
          { 
           Gypp=Gyp-(-90); 
           Gyppp=Gypp+100;
          }
           Gyr=ypr[2] * 180/M_PI;
           if(Gyr>-90)
          { 
           Gyrr=Gyr-(-90); 
           Gyrrr=Gyrr+100;
          }
          Gyy=ypr[0] * 180/M_PI;
          if(Gyy>-179)
          { 
           Gyyy=Gyy-(-179); 
           Gyyyy=Gyyy+1000;
          }
        #endif    
    }
  
  CheckDataPCReceive(); 
  CheckGPS();

 //gps = Serial2.read();
// Serial.print(gps); 
}
void CheckDataPCReceive()
{

    
 while (Serial1.available() > 0)
   {
    inByte = Serial1.read();
  // Serial.print(inByte);
 // Serial.print( Serial2);
   if ((inByte == '$') || (Index >= 11)) {Index = 0;}        
   if (inByte != '\r') {Buffer[Index++] = inByte;}       
   if (inByte == '\n')
    {
      Index = 0;
      parsedata(); 
               
    }                    
  }
}

void  parsedata()
{
   
  if ((Buffer[3] == 'P') && (Buffer[4] == 'C') && (Buffer[5] == 'H'))
 {  
    
 p1=(Buffer[7]-48)*1000; p2=(Buffer[8]-48)*100;p3=(Buffer[9]-48)*10;p4=(Buffer[10]-48)*1;
 pitch = p1+p2+p3+p4; pitch = pitch - 1000;
//Serial.println(pitch);
 }
  if ((Buffer[3] == 'A') && (Buffer[4] == 'I') && (Buffer[5] == 'L'))
 {  
    
 a1=(Buffer[7]-48)*1000; a2=(Buffer[8]-48)*100;a3=(Buffer[9]-48)*10;a4=(Buffer[10]-48)*1;
 aileron = a1+a2+a3+a4; aileron = aileron - 1000;
//Serial.println(aileron);
 }
  if ((Buffer[3] == 'R') && (Buffer[4] == 'U') && (Buffer[5] == 'D'))
 {  
    
r1=(Buffer[7]-48)*1000; r2=(Buffer[8]-48)*100;r3=(Buffer[9]-48)*10;r4=(Buffer[10]-48)*1;
rudder = r1+r2+r3+r4; rudder = rudder - 1000;
//Serial.println(rudder);
 }

   if ((Buffer[3] == 'T') && (Buffer[4] == 'H') && (Buffer[5] == 'L'))
 {  
    
t1=(Buffer[7]-48)*1000; t2=(Buffer[8]-48)*100;t3=(Buffer[9]-48)*10;t4=(Buffer[10]-48)*1;
throttle = t1+t2+t3+t4; throttle = throttle - 1000;
//Serial.println(throttle);
 }

   if ((Buffer[3] == 'B') && (Buffer[4] == 'X') && (Buffer[5] == 'Y'))
 {  
    
ax1=(Buffer[7]-48)*1000; ax2=(Buffer[8]-48)*100;ax3=(Buffer[9]-48)*10;ax4=(Buffer[10]-48)*1;
aux1 = ax1+ax2+ax3+ax4; aux1 = aux1 - 1000;
//Serial.println(aux1);
 }
   if ((Buffer[3] == 'C') && (Buffer[4] == 'G') && (Buffer[5] == 'Z'))
 {  
    
aa1=(Buffer[7]-48)*1000; aa2=(Buffer[8]-48)*100;aa3=(Buffer[9]-48)*10;aa4=(Buffer[10]-48)*1;
aux2 = aa1+aa2+aa3+aa4; aux2 = aux2 - 1000;
//Serial.println(aux2);
 }

    if ((Buffer[3] == 'Z') && (Buffer[4] == 'A') && (Buffer[5] == 'X'))
 {  
    
ba1=(Buffer[7]-48)*1000; ba2=(Buffer[8]-48)*100;ba3=(Buffer[9]-48)*10;ba4=(Buffer[10]-48)*1;
aux3 = ba1+ba2+ba3+ba4; aux3 = aux3 - 1000;
//Serial.println(aux3);
 }

pitchservo.writeMicroseconds(pitch);
ailronservo.writeMicroseconds(aileron);
rudderservo.writeMicroseconds(rudder);
throttleservo.writeMicroseconds(throttle);
Aux1servo.writeMicroseconds(aux1);
Aux2servo.writeMicroseconds(aux2);
Aux3servo.writeMicroseconds(aux3);
 
}
void CheckGPS()
{
 
float hdg;
float spd; 
float alt;

  while (Serial2.available() > 0)
  {
  
    inBytes = Serial2.read();
    if (gps.encode(inBytes))
 {
 
//   Serial.write(inBytes); // Output exactly what we read from the GPS to debug
 hdg=gps.course.deg();
 spd=gps.speed.knots();
 if (gps.location.isValid())
  { 
//  Serial1.print("&");Serial1.print(gps.location.lat(),6);Serial1.println("");//latutude
//  Serial1.print("/");Serial1.print(gps.location.lng(),6);Serial1.println("");//longitude 
  Serial1.print("%");Serial1.print(hdg);Serial1.print("00");Serial1.println("");//course 
  Serial1.print("#");Serial1.print(spd);Serial1.print("000");Serial1.println("");//spd
  Serial1.print("@");Serial1.print(gps.altitude.meters());Serial1.print("00000");Serial1.println("");//alt
 // Serial1.print("'");Serial1.print(gps.satellites.value());Serial1.println("");//SIV
//  Serial1.print("~");Serial1.print(gps.hdop.value());Serial1.println("");//HDOP
  Serial1.print("]");Serial1.print(gps.date.day()); Serial1.print("/");Serial1.print(gps.date.month());
  Serial1.print("/");Serial1.print(gps.date.year());Serial1.println("");//date  
  
  }
 
  
 }
    if ((inBytes == '$') || (GPSIndex >= 80))
    {
    
      GPSIndex = 0;
    }

    if (inBytes != '\r')
    {
      GPSBuffer[GPSIndex++] = inBytes;
    //  GPSLine();
    }

    if (inBytes == '\n')
    {
    GPSLine();
     
      GPSIndex = 0;
    }
  }
}
void GPSLine()
{
  
 
 //int z=0;
//if ((GPSBuffer[1] == 'G') && (GPSBuffer[2] == 'P') && (GPSBuffer[3] == 'R') && (GPSBuffer[4] == 'M') && (GPSBuffer[5] == 'C'))
//  {
//  for(z=0; z<75;z++)
//   {  
 //   Serial1.print (GPSBuffer[z]); 
//     if (GPSBuffer[z] == '\n'){break;}   
//  } 
 // }
  if ((GPSBuffer[1] == 'G') && (GPSBuffer[2] == 'P') && (GPSBuffer[3] == 'G') && (GPSBuffer[4] == 'G') && (GPSBuffer[5] == 'A'))
 {
   for(zz=0; zz<79;zz++)
   {  
     Serial1.print (GPSBuffer[zz]); 

      if (GPSBuffer[zz] == '\n'){break; }
     
    } 
  
 
   } 
 //---------------------------speed sensor---------------------------------   
 int t=0;
 int sum=0;
 int offset=0;
 float Vout=0;
 float P=0;

 
    for(t=0;t<10;t++)
    {
         sensorValue = analogRead(sensorPin)-512;
         sum+=sensorValue;
    }
     offset=sum/10.0;
    
     sensorValue = analogRead(sensorPin)-offset; 
       Vout=(5*sensorValue)/1024.0;
       P=Vout-2.5;           
     //  Serial.print("Presure = " ); 
     KPA = P*1000;                
      
  //-------------------batterycurrent--------------    
    BattValue = analogRead(A1);
  // map it to the range of the analog out:
    BattoutputValue = map(BattValue, 0, 255, 0, 1023); 
   //  analogWrite(BattoutputValue);
       delay(2);
       CurrentValue = analogRead(A2);      
  // map it to the range of the analog out:
    CurrentoutputValue = map(CurrentValue, 0, 255, 0, 1023); 
     
}

/*lat & long /gpsspd #gpshdg %gpsalt @siv 'f  software hdop~valid software utc ] software

magx ?
magy !
cmpass ^
ax =
ay _
az [
grox (
groy) }
groz }
batt <
curr>
barometric ;
TASspd {
$GPGGA sentence
*/





