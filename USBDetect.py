#Detect if USB Ports are enabled on Camera

import socket
import time
global USBD

USBD="0"

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


#host = '10.10.10.199' #your PC
host = TCPip #your PC tcom2
port = 6062
#server = ('10.10.10.100',port)#to MCU
server = (UDPip,port)#to MCU tcom2
ss = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
ss.bind((host, port))

while True:
     
     try:
        file3 = open(r"C:\GimbalCam\USBDetectT.txt","r+")
        try:USBD=file3.readline(1)           
        except:pass
        finally:file3.close()
     except:pass
     
     message1=USBD
     ss.sendto(message1.encode('utf-8'), server) # send
     data, addr = ss.recvfrom(1024)#receive
     data = data.decode('utf-8')
     
     USBD[0:1]
     #print(data)
     print(USBD)
         
     try:
        file4 = open(r"C:\GimbalCam\USBDetectR.txt","w+")
        try:file4.write(data) #send to c:\ drive for file reading                          
        except:pass            
        finally:file4.close()                           
     except:pass
      
