
import os
#os.system('sudo chmod a+rw /dev/i2c-*') # enable i2c pins
#os.system('sudo chmod 666 /dev/ttyS4') # enable rt/tx pins
#os.system('sudo chmod 666 /dev/ttyACM0') #enable USB spare port
import cv2
import numpy as np
import base64
import zmq
import socket
import os
import struct
from subprocess import*
import subprocess
import serial
import threading

subprocess.Popen(['python3','Encoder.py'])
'''
os.system('sudo chmod 666 /dev/ttyS4') # enable rt/tx pins
os.system('sudo chmod 666 /dev/ttyS0') # enable rt/tx pins
os.system('sudo chmod 666 /dev/ttyS1') # enable rt/tx pins
os.system('sudo chmod 666 /dev/ttyS2') # enable rt/tx pins

os.system('sudo chmod 666 /dev/ttyS3') # enable rt/tx pins
os.system('sudo chmod 666 /dev/ttyS5') # enable rt/tx pins
os.system('sudo chmod 666 /dev/ttyS6') # enable rt/tx pins
os.system('sudo chmod 666 /dev/ttyS7') # enable rt/tx pins

os.system('sudo chmod 666 /dev/ttyS8') # enable rt/tx pins
os.system('sudo chmod 666 /dev/ttyS9') # enable rt/tx pins
os.system('sudo chmod 666 /dev/ttyS10') # enable rt/tx pins
os.system('sudo chmod 666 /dev/ttyS11') # enable rt/tx pins
os.system('sudo chmod 666 /dev/ttyUSB0') # enable rt/tx pins

os.system('sudo chmod 666 /dev/ttyACM0') #enable USB spare port
os.system('sudo chmod 666 /dev/ttyACM1') #enable USB spare port
os.system('sudo chmod 666 /dev/ttyACM2') #enable USB spare port
os.system('sudo chmod 666 /dev/ttyACM3') #enable USB spare port
os.system('sudo chmod 666 /dev/ttyACM4') #enable USB spare port
os.system('sudo chmod 666 /dev/ttyACM5') #enable USB spare port
'''
global x,y,Ev,Gyrox_,Gr
TB = "";x = 0;y = 0;CameraSelect = "";TILT = 0;PAN= 0
TILTSTATUS = 0;TBB = 3;Fcam = 1;Ev = "";SO = "";CM= ""
'''
Xval=0
Yval=0
Gyrox_= 0
Gyroz_= 0
Gyroy_= 0
acelx = 0
acelz = 0
w = 640
h = 512
dst=0
Gr=0
comp=0
XX=0
YY=0
XC=360
YC=240
'''
port = '/dev/ttyACM1'

TCP=""
UDP=""
ser = serial.Serial (port,baudrate = 115200,timeout = 1)


try:
    file1 = open('/home/ric/tcpp.txt',"r+")                
    try:TCP=file1.readline(15)#
    except:pass
    finally:file1.close()
except:pass

try:
    file2 = open('/home/ric/udpp.txt',"r+")                
    try:UDP=file2.readline(15)#
    except:pass
    finally:file2.close()
except:pass

context = zmq.Context()
footage_socket = context.socket(zmq.PUB)
#footage_socket.connect('tcp://10.10.10.199:5555')
footage_socket.connect('tcp://' + TCP +':5555')


#host1 = "10.10.10.100"  #MiniMCU IPPort
host1 = UDP  #MiniMCU IPPort
port1 = 6060

#addr=("10.10.10.199",port1)#to PC
addr=(TCP,port1)#to PC

mySocket1 = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
mySocket1.bind((host1,port1))

'''
port2 = 6062
mySocket2= socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
mySocket2.bind((host1,port2))
'''


cap = cv2.VideoCapture(2,cv2.CAP_V4L)
cap.set(cv2.CAP_PROP_FPS,15) #EO


cap.set(cv2.CAP_PROP_FRAME_WIDTH,1620)
cap.set(cv2.CAP_PROP_FRAME_HEIGHT,1080)

cap1 = cv2.VideoCapture(0,cv2.CAP_V4L)
cap1.set(cv2.CAP_PROP_FPS,5)

#stabilizer1 = VideoStabA(cap1)

cap1.set(cv2.CAP_PROP_FRAME_WIDTH,640)
cap1.set(cv2.CAP_PROP_FRAME_HEIGHT,512)

Fcam=0

#---------------------------------------------------------------------------

_, frame = cap.read()

_, frame = cap1.read()




def dat():
    message =''
    #message = str(xx)+ str(yy)
    mySocket1.sendto(message.encode(),addr)
    

def Videosoc():

     
   

    for rec in range(1):
        
        rec = cv2.resize(frame, (1620, 1080))
        '''
        rec = cv2.resize(frame, (1280, 720))  # resize for recording       
        cv2.line(rec,(540 , 358),(740,358),(0,0,255),1)#vertical line
        cv2.line(rec,(638, 260),(638,460),(0,0,255),1)#horizontalline
        cv2.circle(rec, (638, 358), 30, (0, 0, 255), 1)
        '''
        x = 810
        y=540
        a = int(x)
        b= int(y)
        aa=int(x)
        bb=int(y)

        e=0 # magnification factor

        c=40+e
        d=20+e
        
        cv2.line(rec,(aa+20, bb),(a+60,b),(0,255,0),1)
        cv2.line(rec,(aa-20, bb),(a-60,b),(0,255,0),1)
        cv2.line(rec,(aa, bb+20),(a,b+60),(0,255,0),1)
        cv2.line(rec,(aa, bb-20),(a,b-60),(0,255,0),1)


        #quadrant 2     
        cv2.line(rec,(a-c, b-c),(a-d,b-c),(0,0,255),2)#horizontalline        
        cv2.line(rec,(a-c, b-c),(a-c,b-d),(0,0,255),2)#vericaline
        #quadrant 3
        cv2.line(rec,(a-c, b+c),(a-d,b+c),(0,0,255),2)#horizontalline       
        cv2.line(rec,(a-c, bb+c),(a-c,b+d),(0,0,255),2)#verticaline
        
        #quadrant 1
        cv2.line(rec,(a+c, b-c),(a+d,b-c),(0,0,255),2)#horizontaline        
        cv2.line(rec,(a+c, b-c),(a+c,b-d),(0,0,255),2)#verticalline

        #quadrant4
        cv2.line(rec,(a+c, b+c),(a+d,b+c),(0,0,255),2)#horizontaline
        cv2.line(rec,(a+c, b+c),(a+c,b+d),(0,0,255),2)#vericalline
        '''
        #quadrant 2     
        cv2.line(rec,(a-40, b-40),(a-20,b-40),(0,0,255),2)#horizontalline        
        cv2.line(rec,(a-40, b-40),(a-40,b-20),(0,0,255),2)#vericaline
        #quadrant 3
        cv2.line(rec,(a-40, b+40),(a-20,b+40),(0,0,255),2)#horizontalline       
        cv2.line(rec,(a-40, bb+40),(a-40,b+20),(0,0,255),2)#verticaline
        
        #quadrant 1
        cv2.line(rec,(a+40, b-40),(a+20,b-40),(0,0,255),2)#horizontaline        
        cv2.line(rec,(a+40, b-40),(a+40,b-20),(0,0,255),2)#verticalline

        #quadrant4
        cv2.line(rec,(a+40, b+40),(a+20,b+40),(0,0,255),2)#horizontaline
        cv2.line(rec,(a+40, b+40),(a+40,b+20),(0,0,255),2)#vericalline 
        '''
        '''
        
        cv2.line(rec,(600, 378),(600,398),(0,0,255),2)#horizontalline
        cv2.line(rec,(600, 398),(620,398),(0,0,255),2)#horizontalline
        
        cv2.line(rec,(675, 320),(675,340),(0,0,255),2)#horizontalline
        cv2.line(rec,(675, 320),(655,320),(0,0,255),2)#horizontalline
        
        
        cv2.line(rec,(675, 398),(675,378),(0,0,255),2)#horizontalline        
        cv2.line(rec,(675, 398),(655,398),(0,0,255),2)#horizontalline
        
        '''
    
    encoded, buffer = cv2.imencode('.jpg', rec)    
    jpg_as_text = base64.b64encode(buffer)
    footage_socket.send(jpg_as_text)
   
    
    #global Gyrox_,Gr,comp,dst,x,y
    
    '''
    matrix=np.float32([[1,0, Gyroz_],[0,1, Gyroy_]])
    M=cv2.getRotationMatrix2D((w/2,h/2),0 + int(Gyrox_)/2,1.15)
    tran=cv2.warpAffine(frame,matrix,(w,h))
    dst=cv2.warpAffine(tran,M,(w,h))
    
    
    dst1 = cv2.resize(dst, (720, 480))  # resize the frame  
    encoded, buffer = cv2.imencode('.jpg', dst1)
    jpg_as_text = base64.b64encode(buffer)
    footage_socket.send( jpg_as_text)
    
    '''

    
while True:
    

    Videosoc()
      
         
    if Fcam == 0:
                
        success,frame = cap.read()
       
        
        for _ in range(2):
           
           _,frame = cap.read()
           
        
    if Fcam == 1:

        success,frame = cap1.read()
        
        for _ in range(2):
            
           _,frame = cap1.read()
   
    if success:

        
        
        data, addr = mySocket1.recvfrom(1024)#Receivedata
        data = data.decode('utf-8')
        print(data)   
        
        Ev = data[0:1]
        DataX = data[1:5] #mouse position x
        DataY = data[6:10] #mouse position y
        StripDXLzero = DataX.lstrip("0")#strip off leading zero
        XC =StripDXLzero
        StripDYLzero = DataY.lstrip("0")#strip off leading zero
        YC =StripDYLzero
        

        
        TILT = data[10:11] #
        PAN= data[11:12]
        EOCAM= data[12:13]
        BZ= data[13:14]
        CameraSelect = data[14:15] # cameraselect

        
        
        ser.write('$'.encode() + TILT.encode() + PAN.encode() + EOCAM.encode() + BZ.encode())
        
    if  CameraSelect  == 'G':
        Fcam = 0
    else:
        Fcam = 1 #reserved

    xx='%04d'%(x)
    yy='%04d'%(y)
    
    t2= threading.Thread(target=dat)
    t2.start()
    t2.join()
    
    '''
    t3= threading.Thread(target=dat1)
    t3.start()
    t3.join()
    
    data1, addr = mySocket2.recvfrom(1024)#Receivedata
    data1 = data1.decode('utf-8')
    print(data1)
    '''
    
    
    if Ev =="D":
        #select_point1()
        pass

    key = cv2.waitKey(1)
    if key == 27:
        break

cap.release()
cv2.destroyAllWindows()
