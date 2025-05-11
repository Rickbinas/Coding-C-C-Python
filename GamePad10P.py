#--------------------- Software Camera-GroundSide Gamepad----3-23-2020----------------------------
#Compiler: Python 3.7.3
#Used with raspberry pi 4
#----------------------------------------------------------------------------------------
#pyinstaller
#https://www.youtube.com/watch?v=UZX5kH72Yx4
import inputs

pads = inputs.devices.gamepads
global TU,TD
TU="A"
TD="A"
#--------gamepad status when connected or disconnected on startup----------
'''
if len(pads) == 0:    
    #raise Exception("Couldn't find any Gamepads!")
    file7 = open(r"C:\CamGimbal\gpadstat.txt","w+")
    file7.write("0\n")
    file7.close()

else:
    file7 = open(r"C:\CamGimbal\gpadstat.txt","w+")
    file7.write("1\n")
    file7.close()
'''

    
while True:
    events = inputs.get_gamepad()
    for event in events:
        #print(event.ev_type, event.code, event.state)
        
#--------gamepad status when connected or disconnected ----------
        '''
        if len(pads) == 1:
          file7 = open(r"C:\CamGimbal\gpadstat.txt","w+")
          file7.write("1\n")
          file7.close()
        else:
          file7 = open(r"C:\CamGimbal\gpadstat.txt","w+")
          file7.write("0\n")
          file7.close() 
            #print("this")
        '''    
#-------------------------EO/IR---------------------------------             
        if event.code == 'BTN_TL' and event.state == 1:
             #print('left back upper btn_state 1')
             file3 = open(r"C:\CamVid\Gamepad10X\gpadEO.txt","w+")
             file3.write("1\n")
             file3.close()

        if event.code == 'BTN_TL' and event.state == 0:
             #print('left back upper btn_state 0')
             file3 = open(r"C:\CamVid\Gamepad10X\gpadEO.txt","w+")
             file3.write("0\n")
             file3.close()
             #print("left")
             
#---------------------IR BLK_WHT--------------------------------
             
        if event.code == 'BTN_TR' and event.state == 1:
             #print('Right back upper btn_state 1')
             file4 = open(r"C:\CamVid\Gamepad10X\gpadIR.txt","w+")
             file4.write("1\n")
             file4.close()
             #print("right")
             

        if event.code == 'BTN_TR' and event.state == 0:
             #print('Right back upper btn_state 0')
             file4 = open(r"C:\CamVid\Gamepad10X\gpadIR.txt","w+")
             file4.write("0\n")
             file4.close()
            
#---------------------HatNorth--Tilt up-------------------------
        if event.code == 'ABS_HAT0Y' and event.state == -1:
             #print('HATY North 1')
             TU="B"
             file1 = open(r"C:\CamVid\Gamepad10X\gpadU.txt","w+")
             file1.write("1\n")
             file1.close()

        if event.code == 'ABS_HAT0Y' and event.state == 0:
             #print('HATY North 0')
             TU="A"
             file1 = open(r"C:\CamVid\Gamepad10X\gpadU.txt","w+")
             file1.write("0\n")
             file1.close()     
#---------------------HatSouth--Tily Down-----------------------
             
        if event.code == 'ABS_HAT0Y' and event.state == 1:
             #print('HATY South')
             TD="C"
             file2 = open(r"C:\CamVid\Gamepad10X\gpadD.txt","w+")
             file2.write("1\n")
             file2.close()
             
        if event.code == 'ABS_HAT0Y' and event.state == 0:
             #print('HATY South')
             TD="A"
             file2 = open(r"C:\CamVid\Gamepad10X\gpadD.txt","w+")
             file2.write("0\n")
             file2.close()

#---------------------Hatwest--Pan left-------------------------
        if event.code == 'ABS_HAT0X' and event.state == -1:
             #print('HATX west -1')
             file9 = open(r"C:\CamVid\Gamepad10X\gpadlft.txt","w+")
             file9.write("1\n")
             file9.close()

        if event.code == 'ABS_HAT0X' and event.state == 0:
             #print('HATX west 0')
             file9 = open(r"C:\CamVid\Gamepad10X\gpadlft.txt","w+")
             file9.write("0\n")
             file9.close()     
#---------------------Hateast--Pan Right-----------------------
             
        if event.code == 'ABS_HAT0X' and event.state == 1:
             #print('HATX East 1')
             file10 = open(r"C:\CamVid\Gamepad10X\gpadrht.txt","w+")
             file10.write("1\n")
             file10.close()
             
        if event.code == 'ABS_HAT0X' and event.state == 0:
             #print('HATX East 0')
             file10 = open(r"C:\CamVid\Gamepad10X\gpadrht.txt","w+")
             file10.write("0\n")
             file10.close()
             
#-----------------------Reserved----------------------------------
        '''     
        if event.code == 'ABS_HAT0X' and event.state == -1:
            pass
             #print('HATX WEST')     
        if event.code == 'ABS_HAT0X' and event.state == 1:
            pass
             #print('HATY EAST')
        '''     
#----------------------Zoom Minus-----------------------------------             
        if event.code == 'ABS_Z' and event.state == 255:
            # print('left back lower btn_state 1')
             file5 = open(r"C:\CamVid\Gamepad10X\gpadZmm.txt","w+")
             file5.write("1\n")
             file5.close()
             
        if event.code == 'ABS_Z' and event.state == 0:
             #print('left back lower btn_state 0')
             file5 = open(r"C:\CamVid\Gamepad10X\gpadZmm.txt","w+")
             file5.write("0\n")
             file5.close()
#----------------------Zoom Plus------------------------------------             
        if event.code == 'ABS_RZ' and event.state == 255:
            # print('Right back lower btn_state 1')
             file6 = open(r"C:\CamVid\Gamepad10X\gpadZmP.txt","w+")
             file6.write("1\n")
             file6.close()
        if event.code == 'ABS_RZ' and event.state == 0:
            # print('Right back lower btn_state 0')
             file6 = open(r"C:\CamVid\Gamepad10X\gpadZmP.txt","w+")
             file6.write("0\n")
             file6.close()
#----------------------Escape---------------------------------------------
        '''     
        if event.code == 'BTN_SELECT' and event.state == 1:
             #print('HATY South')
             file8 = open(r"C:\CamGimbal\gEsc.txt","w+")
             file8.write("1\n")
             file8.close()
             
        if event.code == 'BTN_SELECT' and event.state == 0:
             #print('HATY South')
             file8 = open(r"C:\CamGimbal\gEsc.txt","w+")
             file8.write("0\n")
             file8.close()      
    '   '''
        
        file7 = open(r"C:\GimbalCam\Gamepd.txt","w+")
        file7.write(TU+TD)
        file7.close()
        
