#wilburt
#--------------------EOIR CamGimbal with C#-----------------------------------
import base64
import numpy.core.multiarray
import cv2
import zmq

import numpy as np
import socket
import time
import threading
from subprocess import*
from datetime import datetime
import subprocess
from subprocess import Popen

global TCPip,UDPip

TCPip=""
UDPip=""

try:
    file1 = open(r"C:\GimbalCam\TCPIP.txt","r+")
    try:TCPip=file1.readline(15)#
    except:pass
    finally:file1.close()            
except:pass

try:
    file2 = open(r"C:\GimbalCam\UDPIP.txt","r+")
    try:UDPip=file2.readline(15)#
    except:pass
    finally:file2.close()            
except:pass

host = TCPip #your PC
#host = '10.10.10.199' #your PC
port = 6060

server1 = (UDPip,port)# to MCU
#server1 = ('10.10.10.100',port)# to MCU
s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
s.bind((host, port))

context = zmq.Context()
footage_socket = context.socket(zmq.SUB)
footage_socket.bind('tcp://*:5555')
footage_socket.setsockopt_string(zmq.SUBSCRIBE, np.compat.unicode(''))


time.sleep(2)

global x,xx,yy,GCAM,EOCAM,BWise,clmp,res
global name,snap,vid,closepgm,source,GCAM,BZ,GEO
global DLat,DLng,DHdg,DAlt,DFix,DSat
global Gpantilt,TILT,PAN,Dkit,header

DHdg="0";DAlt="0";DLat="0";DLng="0";DFix="0";DSat="0";Gpantilt="AAAA"

frame=0;GCAM="G";E = "U";EOCAM=0;BZ=0;BWise="0";clmp="0";Tpic="0";

x =0;xx=0;yy=0;e=0;ee=0;snap=0;vid=0;closepgm=0

TILT="A";PAN="A";Rate="1";Dkit=""
header=""
res="0"


#----------------Inialize Cameras----------------------------
def EOinitparameters():#reset EO cam to initial state
    
    global e,GCAM,EOCAM
    
    e=e +1
    if e <60:        
        EOCAM = 1   
    if e > 60:
         EOCAM = EOCAM
         e=e

def IRinitparameters():#reset IR cam to initial state
    
    global ee,BZ
    
    ee=ee +1
    if ee <60:        
        BZ = 1   
    if ee > 60:
         BZ = BZ
         ee=ee         
#-----------reserved for tracking----------------------------

def select_point(event, x, y, flags, params):        
    global point, point_selected, old_points,xx,yy,E
    
    if event == cv2.EVENT_LBUTTONDOWN:
        
        point = (x, y)
        xx='%04d'%(x)
        yy='%04d'%(y)
        point_selected = True       
        old_points = np.array([[x, y]], dtype=np.float32)
        E = "D"
        #print(xx,yy)
    if event == cv2.EVENT_LBUTTONUP:
        
        point = (10, 10)        
        point_selected = True        
        old_points = np.array([[x, y]], dtype=np.float32)
        E = "U"
        
cv2.namedWindow("Frame", cv2.WINDOW_NORMAL)
cv2.namedWindow("Frame")
cv2.setMouseCallback("Frame", select_point)

point_selected = False
point = ()
old_points = np.array([[]])

#--------------------Read Video------------------------------
def Videosoc():
    
    global source,cv2image
    
    frame = footage_socket.recv_string()   
    img1= base64.b64decode(frame)    
    npimg = np.frombuffer(img1, dtype=np.uint8)       
    source = cv2.imdecode(npimg, 1)
    cv2image = cv2.cvtColor(source, cv2.COLOR_BGR2RGBA)
    
def filedat():#------------File reading---------------------
    
    global x,xx,yy,GCAM,EOCAM,BWise,clmp,res
    global name,snap,vid,closepgm,source,GCAM,BZ,GEO,Rate
    global DLat,DLng,DHdg,DAlt,DFix,DSat
    global Gpantilt,TILT,PAN,Dkit,header  
    try:
        file24 = open(r"C:\GimbalCam\GGpantilt.txt","r+")
        try:           
            Gpantilt=file24.readline(9)         
            TILT=Gpantilt[0:1]
            PAN=Gpantilt[1:2]
            EOCAM=Gpantilt[2:3]
            BZ=Gpantilt[3:4]
            GCAM=Gpantilt[4:5]
            clmp=Gpantilt[5:6]
            snap=Gpantilt[6:7]
            vid=Gpantilt[7:8]
            Rate=Gpantilt[8:9]          
            
            if GCAM=="G":
                
                BWise = "0"                          
            else:
                BWise="1"
            
        except:pass
        finally:file24.close()
    except:pass
       
    try:
        file10 = open(r"C:\GimbalCam\CodeID.txt","r+")
        try:name=file10.readline(7)#
        except:pass
        finally:file10.close()            
    except:pass
 
    try:
        file13 = open(r"C:\GimbalCam\closepgm.txt","r+")
        try:closepgm=file13.readline(1)
        except:pass
        finally:file13.close()            
    except:pass
    
    try:
        file14 = open(r"C:\GimbalCam\DrKitData.txt","r+")
        try:
            Dkit=file14.readline(57)
            header=Dkit[14:15]
            if header=="*":
                DHdg=Dkit[15:18]
                DAlt=Dkit[19:24]
                DLat=Dkit[25:35]
                DLng=Dkit[36:47]
                DFix=Dkit[48:50]
                DSat=Dkit[51:53]
        except:pass
        finally:
            file14.close()            
    except:pass

while True:#----------------Main Loop--------------------------------------------------------
       
    if xx==0: xx="0000" #keeps protocol string constant char count, reserve for tracking
    if yy==0: yy="0000" #keeps protocol string constant char count, reserve for tracking
    
    EOinitparameters()
    IRinitparameters()

    message = str(E)+ str(xx) +"Y" + str(yy) + str(TILT)+ str(PAN) + str(EOCAM) + str(BZ) + str(GCAM)+ str(Rate)
    s.sendto(message.encode('utf-8'), server1) # send
    
    #print(TCPip,UDPip,Gpantilt)   
    t1= threading.Thread(target=Videosoc)
    t1.start()
    t1.join()

#----------Receive Data------------------------------------------------------------------------       
    data, addr = s.recvfrom(1024)# receive
    data = data.decode('utf-8')    
#-----------------Video Overlay----------------------------------------------------------------
      
    x, y, w, P = 5, 875, 1310,70
    sub_img = source[y:y+P, x:x+w]
    blk_rect = np.ones(sub_img.shape, dtype=np.uint8) * 0
    res = cv2.addWeighted(sub_img, 0.5, blk_rect, 0.5, 1.0)    
    source[y:y+P, x:x+w] = res # Putting the image back to its position
  
    font = cv2.FONT_HERSHEY_PLAIN
    dt=str(datetime.now().time())
    dt=dt[0:8]
    
    cv2.putText(source,'DATE: ' + str(datetime.now().date()),(1100,935),font,1.2,(0,255,0),2,cv2.LINE_4)    
    cv2.putText(source,'TIME: ' + dt,(820,935),font,1.2,(0,255,0),2,cv2.LINE_4)    
    cv2.putText(source,'LAT: '+ DLat, (20,935),font,1.2,(0,255,0),2,cv2.LINE_4)   
    cv2.putText(source,'LONG: '+ DLng,(250,935),font,1.2,(0,255,0),2,cv2.LINE_4)    
    cv2.putText(source,'HDG: '+ DHdg,(500,935),font,1.2,(0,255,0),2,cv2.LINE_4)    
    cv2.putText(source,'ALT: '+ DAlt + ' FT ',(620,935),font,1.2,(0,255,0),2,cv2.LINE_4)    
    cv2.putText(source,'3D FIX: '+ DFix ,(25,900),font,1.2,(0,255,0),2,cv2.LINE_4)    
    cv2.putText(source,'NO. OF SATS: '+ DSat ,(250,900),font,1.2,(0,255,0),2,cv2.LINE_4)
    
#-----------------------------------------------------------------------------------------------    
    t2= threading.Thread(target=filedat)
    t2.start()
    t2.join()
#--------------------Thermal-Color Maping--------------------------------------------------------                         
    if BWise=="0":
       cv2image1 = cv2.cvtColor(source, cv2.COLOR_BGR2RGBA)#convert
       cv2image = cv2.cvtColor(cv2image1, cv2.COLOR_BGR2RGBA)# normal/positve image
    if BWise=="1"and clmp=="0":
       cv2image = cv2.cvtColor(source, cv2.COLOR_BGR2RGB)
    if BWise=="1" and clmp=="1":
       cv2image = cv2.bitwise_not(source) # negative image       
    if BWise=="1" and clmp=="2":
       cv2image = cv2.applyColorMap(source, cv2.COLORMAP_AUTUMN)#
    if BWise=="1"and clmp=="3":
       cv2image = cv2.applyColorMap(source, cv2.COLORMAP_BONE)#
    if BWise=="1" and clmp=="4":
       cv2image = cv2.applyColorMap(source, cv2.COLORMAP_JET)#
    if BWise=="1" and clmp=="5":
       cv2image = cv2.applyColorMap(source, cv2.COLORMAP_WINTER)#
    if BWise=="1" and clmp=="6":
       cv2image = cv2.applyColorMap(source, cv2.COLORMAP_RAINBOW)#
    if BWise=="1" and clmp=="7":
       cv2image = cv2.applyColorMap(source, cv2.COLORMAP_OCEAN)#
    if BWise=="1" and clmp=="8":
       cv2image = cv2.applyColorMap(source, cv2.COLORMAP_SUMMER)#
    if BWise=="1" and clmp=="9":
       cv2image = cv2.applyColorMap(source, cv2.COLORMAP_SPRING)#
    if BWise=="1" and clmp=="A":
       cv2image = cv2.applyColorMap(source, cv2.COLORMAP_COOL)#
    if BWise=="1" and clmp=="B":
       cv2image = cv2.applyColorMap(source, cv2.COLORMAP_HSV)#

    if BWise=="1" and clmp=="C":
       cv2image = cv2.applyColorMap(source, cv2.COLORMAP_PINK)#
    if BWise=="1" and clmp=="D":
       cv2image = cv2.applyColorMap(source, cv2.COLORMAP_HOT)#
    if BWise=="1" and clmp=="E":
       cv2image = cv2.applyColorMap(source, cv2.COLORMAP_PARULA)#
    if BWise=="1" and clmp=="F":
       cv2image = cv2.applyColorMap(source, cv2.COLORMAP_MAGMA)#
    if BWise=="1" and clmp=="G":
       cv2image = cv2.applyColorMap(source, cv2.COLORMAP_INFERNO)#
    if BWise=="1" and clmp=="H":
       cv2image = cv2.applyColorMap(source, cv2.COLORMAP_PLASMA)#
    if BWise=="1" and clmp=="I":
       cv2image = cv2.applyColorMap(source, cv2.COLORMAP_VIRIDIS)#
    if BWise=="1" and clmp=="J":
       cv2image = cv2.applyColorMap(source, cv2.COLORMAP_CIVIDIS)#
    if BWise=="1" and clmp=="K":
       cv2image = cv2.applyColorMap(source, cv2.COLORMAP_TWILIGHT)#
    if BWise=="1" and clmp=="L":
       cv2image = cv2.applyColorMap(source, cv2.COLORMAP_TWILIGHT_SHIFTED)#
    if BWise=="1" and clmp=="M":
       cv2image = cv2.applyColorMap(source, cv2.COLORMAP_TURBO)#
    if BWise=="1"and clmp=="N":
       cv2image = cv2.applyColorMap(source, cv2.COLORMAP_DEEPGREEN)#
       
#-------------------------Take a picture-------------------------------------   
    if snap == "1":            
       file = 'c:/GimbalCam/Vid/' + str(name) + '.jpg'
       out = cv2.imwrite(file, cv2image)      
    if snap == "0":
       snap ="0"
#------------------------Record Video----------------------------------------       
    if vid == "1":
        
        fourcc = cv2.VideoWriter_fourcc(*'mp4v')
        file1 = 'c:/GimbalCam/Vid/' + str(name) + '.mp4'     
        #out = cv2.VideoWriter(file1,fourcc, 20.0, (int(1620),int(1080)))
        out = cv2.VideoWriter(file1,fourcc, 20.0, (int(1420),int(950))) 
    if vid =="2":
        
       if BWise=="0":
           out.write(source) # recording
       if BWise=="1":
           out.write(cv2image) # recording
           
    if vid=="3":      
       out.release() # stop recording
       
#----------------------Select Mat image-------------------------------------
    if BWise=="1":
        cv2.imshow("Frame", cv2image)
      
    if BWise=="0":   
       cv2.imshow("Frame", source)
#---------------------------------------------------------------------------       
    key = cv2.waitKey(1)
    if key == 27:
        break
#---------------------close program----------------------------------------
    
    if cv2.getWindowProperty('Frame',cv2.WND_PROP_VISIBLE) < 1:        
        break    
    if closepgm=="1":
        
       cv2.destroyAllWindows()
       break


