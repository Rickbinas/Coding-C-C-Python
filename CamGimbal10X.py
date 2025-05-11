#wilburt
#--------------------EOIR CamGimbal with C#-----------------------------------
import base64
import numpy.core.multiarray
import cv2
import zmq
#import pyshine as ps
import tkinter as tk
from tkinter import ttk,Text #combobox
import cv2
from PIL import Image, ImageTk
import numpy as np
import socket
import time
import threading
from subprocess import*
from datetime import datetime
import subprocess
from subprocess import Popen
from videostab2A import VideoStabA

#host = '10.10.10.199' #your PC
host = '10.10.10.198' #your PC tcom2
#host = '192.168.168.99' #your PC
port = 6060

#server1 = ('10.10.10.100',port)# to MCU
server1 = ('10.10.10.98',port)# to MCU
#server1 = ('192.168.168.100',port)# to MCU
s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
s.bind((host, port))

context = zmq.Context()
footage_socket = context.socket(zmq.SUB)
footage_socket.bind('tcp://*:5555')
footage_socket.setsockopt_string(zmq.SUBSCRIBE, np.compat.unicode(''))


time.sleep(2)

global x,xx,yy,GCAM,TU,TD,PL,PR,zp,zn,EOCAM,GU,GD,GPWM,GPWMP,GB,GL,GR,BWise,clmp
global name,snap,vid,closepgm,source,cv2image,GamPd,frame,init,GCAM,BZ,GEO
global szH,szW,DL,SDat
global DLat,DLng,DHdg,DAlt

SDat="K"
DHdg="0"
DAlt="0"
DLat="0"
DLng="0"

DL=""
szW=640
szH=480

frame=0;GGCAM="G";GCAM="G";TU="A";TD="A";PL="A";PR="A";E = "U";GB="S";GU="0";GD="0";GL="0"
GR="0";GPWM="A";GPWMP="A";EOCAM=0;BZ=0;GZM="0";GZP="0";BWise="0";clmp="0";Tpic="0";GEO="0"

x =0
xx=0
yy=0
zp=0
zn=0
init=0
e=0
ee=0
snap=0
vid=0
closepgm=0
Bdd=0
Vadj=0
Vadj1=0
Vadj2=0
Vadj3=0
bb=0
pulse = "F"
finetune = "F"


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

def Videosoc():
    
    global source,cv2image,Vadj,bdd,Vadj1,Vadj3,bb
    
    frame = footage_socket.recv_string()
   
    img1= base64.b64decode(frame)    
    npimg = np.frombuffer(img1, dtype=np.uint8)       
    source = cv2.imdecode(npimg, 1)
    cv2.normalize(source, source, Vadj3, Vadj1, cv2.NORM_MINMAX)
    #kernel1  = np.array([[-1,-1,-1], [-1, 9,-1],[-1,-1,-1]])
    #source = cv2.filter2D(src=source, ddepth=-1, kernel=kernel1)
    #source = cv2.fastNlMeansDenoisingColored(source,None,10,10,7,21)
    #kernel = np.array([[-1,-1,-1], [-1,9,-1], [-1,-1,-1]])
    #source = cv2.filter2D(source, -1, kernel)
    cv2image = cv2.cvtColor(source, cv2.COLOR_BGR2RGBA)
    

def filedat():

    global x,xx,yy,GCAM,TU,TD,PL,PR,zp,zn,EOCAM,GU,GD,GPWM,GPWMP,GB,GL,GR,BWise,clmp
    global name,snap,vid,closepgm,source,cv2image,GamPd,frame,init,GCAM,BZ,GEO
    global szH,szW,DL,SDat  
    global DLat,DLng,DHdg,DAlt,Vadj,bdd,Vadj1,Vadj2,Vadj3,bb,pulse,finetune

    try:
        file2 = open(r"C:\CamVid\CamSel.txt","r+")
        try:   
            GCAM=file2.readline(1)
            if GCAM=="G":
                BWise = "0"                          
            else:
                BWise="1"
        except:pass
        finally:           
            file2.close()
    except:pass
    
    try:
        file1 = open(r"C:\CamVid\Tiltup.txt","r+")
        try:TU=file1.readline(1)
        except:pass
        finally:file1.close()
    except:pass

    try:
        file3 = open(r"C:\CamVid\TiltDwn.txt","r+")
        try:TD=file3.readline(1)           
        except:pass
        finally:file3.close()
    except:pass

    try:
        file4 = open(r"C:\CamVid\PanLft.txt","r+")
        try:PL=file4.readline(1)           
        except:pass
        finally:file4.close()
    except:pass

    try:
        file5 = open(r"C:\CamVid\PanRht.txt","r+")
        try:PR=file5.readline(1)           
        except:pass
        finally:file5.close()
    except:pass

    try:
        file6 = open(r"C:\CamVid\ZoomP.txt","r+")
        try:zp=file6.readline(1)#
        except:pass
        finally:file6.close()
    except:pass

    try:
        file7 = open(r"C:\CamVid\ZoomM.txt","r+")
        try:zn=file7.readline(1)#
        except:pass
        finally:file7.close()            
    except:pass

    try:
        file8 = open(r"C:\CamVid\Track.txt","r+")
        try:GB=file8.readline(1)#
        except:pass
        finally:file8.close()            
    except:pass

    try:
        file9 = open(r"C:\CamVid\Colormap.txt","r+")
        try:clmp=file9.readline(1)#
        except:pass
        finally:file9.close()            
    except:pass

    try:
        file10 = open(r"C:\CamVid\CodeID.txt","r+")
        try:name=file10.readline(7)#
        except:pass
        finally:file10.close()            
    except:pass
    
    try:
        file11 = open(r"C:\CamVid\Tpic.txt","r+")
        try:snap=file11.readline(1)#
        except:pass
        finally:file11.close()            
    except:pass

    try:
        file12 = open(r"C:\CamVid\VVid.txt","r+")
        try:vid=file12.readline(1)#
        except:pass
        finally:file12.close()            
    except:pass
    
    try:
        file13 = open(r"C:\CamVid\closepgm.txt","r+")
        try:closepgm=file13.readline(1)#
        except:pass
        finally:file13.close()            
    except:pass

    try:
        file14 = open(r"C:\CamVid\GmPd.txt","r+")
        try:   
            GamPd=file14.readline(1)
            if GamPd=="1":
               pass                         
            else:
               pass
        except:pass
        finally:           
            file14.close()
    except:pass

    try:
        file23 = open(r"C:\CamVid\pls.txt","r+")
        try:   
            pulse=file23.readline(1)
            if pulse=="F":
                finetune="F"                          
            if pulse=="E":
                finetune="E" #onstate
        except:pass
        finally:           
            file23.close()
    except:pass
  
#------------------Overlay------------------------------     
    try:
        file16 = open(r"C:\CamVid\DLat.txt","r+")
        try:
            DLat=file16.readline(10)
        except:
            pass
    finally:
        file16.close()

    try:
        file17 = open(r"C:\CamVid\DLng.txt","r+")
        try:
            DLng=file17.readline(10)
        except:
            pass
    finally:
        file17.close()   
    
    try:
        file18 = open(r"C:\CamVid\DHdg.txt","r+")
        try:
            DHdg=file18.readline(3)
        except:
            pass
    finally:
        file18.close()

    try:
        file19 = open(r"C:\CamVid\DAlt.txt","r+")
        try:
            DAlt=file19.readline(5)
        except:
            pass
    finally:
        file19.close()      
    
    try:
        file11 = open(r"C:\CamVid\RDat.txt","w+")
        try:
             file11.write(data)               
        except:
            pass
        finally:                
            file11.close()
    except:
        pass
    
    try:
        file13 = open(r"C:\CamVid\SDat.txt","r+")
        try:SDat=file13.readline(1)#
        except:pass
        finally:file13.close()            
    except:pass

    try:
        file22 = open(r"C:\CamVid\BD.txt","r+")#darkness
        try:
            bdd=file22.readline(3)
            Vadj=int(bdd)-100
            Vadj1=int(Vadj)
        except:
            pass
    finally:
        file22.close()

    try:
        file23 = open(r"C:\CamVid\BB.txt","r+")#darkness
        try:
            bb=file23.readline(3)
            Vadj2=int(bb)-100
            Vadj3=int(Vadj2)
        except:
            pass
    finally:
        file23.close()     
#----------------GamePad--------------------------
#---------------TiltUp-----------------------------
    
    try:
        file1 = open(r"C:\CamVid\Gamepad10X\gpadU.txt","r+")
        try:
            GU=file1.readline(1)    
            if GU==1:
                GU == 1                          
            else:
                GU==0
        except:
            pass
        finally:               
            file1.close()
    except:
        pass
    
#---------------TiltDown--------------------------
    
    try:
        file2 = open(r"C:\CamVid\Gamepad10X\gpadD.txt","r+")
        try:
            GD=file2.readline(1)
            if GD==1:
                GD == 1          
            else:
                GD==0
        except:
            pass
        finally:                
            file2.close()
    except:
        pass
       
#---------------PanLeft-----------------------------
    try:
        file9 = open(r"C:\CamVid\Gamepad10X\gpadlft.txt","r+")
        try:
            GL=file9.readline(1)          
            if GL==1:
                GL == 1                          
            else:
                GL==0                           
        except:
                pass
        finally:                 
                file9.close()
    except:
        pass        
#---------------PanRht--------------------------
    try:
        file10 = open(r"C:\CamVid\Gamepad10X\gpadrht.txt","r+")
        try:
            GR=file10.readline(1)
            if GR==1:
                GR == 1          
            else:
                GR==0
        except:
            pass
        finally:               
            file10.close()
    except:
        pass   

#--------------EOIRSelect/Zoom Select-see c#----------------------------------------

#--------------------GamepadZoom with software EO--------------------------           
    if GCAM == "G" and zn == "0" and zp == "0" and GZM == "0" and GZP== "0": EOCAM=0 
    if GCAM == "G" and zn == "0" and zp == "1" and GZM == "0" and GZP== "0": EOCAM = 1 
    if GCAM == "G" and zn == "2" and zp == "0" and GZM == "0" and GZP== "0": EOCAM = 2       
#---------------------Hardware---------------------------       
    if GCAM == "G" and zn == "0" and zp == "0" and GZM == "0" and GZP== "1": EOCAM = 1 
    if GCAM == "G" and zn == "0" and zp == "0" and GZM == "1" and GZP== "0": EOCAM = 2     
#--------------------GamepadZoom with software IR--------------------------    
    if GCAM == "R" and zn == "0" and zp == "0" and GZM == "0" and GZP== "0": BZ = 0
    if GCAM == "R" and zn == "0" and zp == "1" and GZM == "0" and GZP== "0": BZ = 1
    if GCAM == "R" and zn == "2" and zp == "0" and GZM == "0" and GZP== "0": BZ = 2    
#--------------------Hardware---------------------------       
    if GCAM == "R" and zn == "0" and zp == "0" and GZM == "0" and GZP== "1": BZ = 1
    if GCAM == "R" and zn == "0" and zp == "0" and GZM == "1" and GZP== "0": BZ = 2
    
#--------------------GamepadTilt----------------------------    
    if TU == "A" and TD == "A" and GU == "0" and GD == "0" and GB=="S": GPWM = "A"
    if TU == "B" and TD == "A" and GU == "0" and GD == "0" and GB=="S": GPWM = "B"
    if TU == "A" and TD == "C" and GU == "0" and GD == "0" and GB=="S": GPWM = "C"
#--------------------GamepadTiltlimit----------------------------    
    if TU == "A" and TD == "A" and GU == "0" and GD == "0" and GB=="G": GPWM = "A"
    if TU == "B" and TD == "A" and GU == "0" and GD == "0" and GB=="G": GPWM = "B"
    if TU == "A" and TD == "C" and GU == "0" and GD == "0" and GB=="G": GPWM = "C"  
#--------------------HardwareTilt----------------------------
    if TU == "A" and TD == "A" and GU == "1" and GD == "0" and GB=="S": GPWM = "B"
    if TU == "A" and TD == "A" and GU == "0" and GD == "1" and GB=="S": GPWM = "C"
#--------------------HardwareTiltlimit----------------------------
    if TU == "A" and TD == "A" and GU == "1" and GD == "0" and GB=="G": GPWM = "B"
    if TU == "A" and TD == "A" and GU == "0" and GD == "1" and GB=="G": GPWM = "C"    
#--------------------GamepadPan----------------------------
    if PL == "A" and PR == "A" and GL == "0" and GR == "0" and GB=="S": GPWMP = "A"
    if PL == "B" and PR == "A" and GL == "0" and GR == "0" and GB=="S": GPWMP = "B"
    if PL == "A" and PR == "C" and GL == "0" and GR == "0" and GB=="S": GPWMP = "C"
#--------------------GamepadPanlimit----------------------------
    if PL == "A" and PR == "A" and GL == "0" and GR == "0" and GB=="G": GPWMP = "A"
    if PL == "B" and PR == "A" and GL == "0" and GR == "0" and GB=="G": GPWMP = "B"
    if PL == "A" and PR == "C" and GL == "0" and GR == "0" and GB=="G": GPWMP = "C"    
#--------------------HardwarePan----------------------------
    if PL == "A" and PR == "A" and GL == "1" and GR == "0" and GB=="S": GPWMP = "B"
    if PL == "A" and PR == "A" and GL == "0" and GR == "1" and GB=="S": GPWMP = "C"
#--------------------HardwarePanlimit----------------------------
    if PL == "A" and PR == "A" and GL == "1" and GR == "0" and GB=="G": GPWMP = "B"
    if PL == "A" and PR == "A" and GL == "0" and GR == "1" and GB=="G": GPWMP = "C"      
#---------------------------------------------------------------------       
    
while True:
        
    if xx==0: xx="0000" #keeps protocol string constant char count
    if yy==0: yy="0000"
    
    EOinitparameters()
    IRinitparameters()
    
    message = str(E)+ str(xx) +"Y" + str(yy) + str(GCAM)+ str(GPWM) + str(GPWMP) + str(EOCAM) + str(BZ)+ str(GB)+ str(finetune)
    s.sendto(message.encode('utf-8'), server1) # send


       
    t1= threading.Thread(target=Videosoc)
    t1.start()
    t1.join()

         
    data, addr = s.recvfrom(1024)# receive
    data = data.decode('utf-8')
    
    x, y, w, P = 0, 690, 1130,25
    sub_img = source[y:y+P, x:x+w]
    blk_rect = np.ones(sub_img.shape, dtype=np.uint8) * 0
    res = cv2.addWeighted(sub_img, 0.5, blk_rect, 0.5, 1.0)    
    source[y:y+P, x:x+w] = res # Putting the image back to its position
        
    font = cv2.FONT_HERSHEY_PLAIN
    dt=str(datetime.now().time())
    dt=dt[0:8]

    cv2.putText(source,'DATE: ' + str(datetime.now().date()),(650,710),font,.8,(0,255,255),1,cv2.LINE_4)
    cv2.putText(source,'TIME: ' + dt,(800,710),font,.8,(0,255,255),1,cv2.LINE_4)
    cv2.putText(source,'LAT: '+ DLat, (10,710),font,.8,(0,255,255),1,cv2.LINE_4)
    cv2.putText(source,'LONG: '+ DLng,(125,710),font,.8,(0,255,255),1,cv2.LINE_4)
    cv2.putText(source,'HDG: '+ DHdg,(255,710),font,.8,(0,255,255),1,cv2.LINE_4)
    cv2.putText(source,'ALT: '+ DAlt + ' FT ',(320,710),font,.8,(0,255,255),1,cv2.LINE_4)
    
    t2= threading.Thread(target=filedat)
    t2.start()
    t2.join()

#----------------------Color Maing----------------------------                         
    if BWise=="0":
        #cv2image = cv2.cvtColor(source, cv2.COLOR_BGR2RGBA)# normal/positve image
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
#--------------------------------------------------    
    if snap == "1":            
       file = 'c:/CamVid/Vid/' + str(name) + '.jpg'
       out = cv2.imwrite(file, cv2image)      
    if snap == "0":
       snap ="0"
#------------------------------       
    if vid == "1":
        
        fourcc = cv2.VideoWriter_fourcc(*'mp4v')
        file1 = 'c:/CamVid/Vid/' + str(name) + '.mp4'     
        out = cv2.VideoWriter(file1,fourcc, 20.0, (int(1280),int(720)))                        
    if vid =="2":
       if BWise=="0":
           out.write(source) # recording
       if BWise=="1":
           out.write(cv2image) # recording 
    if vid=="3":      
       out.release() # stop recording
#----------------------------------------
    if BWise=="1":
        cv2.imshow("Frame", cv2image)

       
    if BWise=="0":   
       cv2.imshow("Frame", source)

        
    key = cv2.waitKey(1)
    if key == 27:
        break
    if cv2.getWindowProperty('Frame',cv2.WND_PROP_VISIBLE) < 1:        
        break
    
    if closepgm=="1":
        
       cv2.destroyAllWindows()
       break


