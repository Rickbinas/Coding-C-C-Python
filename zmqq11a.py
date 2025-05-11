import base64
import cv2
import zmq

import numpy as np
import socket
import time

context = zmq.Context()
footage_socket = context.socket(zmq.SUB)
footage_socket.bind('tcp://*:5555')
#footage_socket.setsockopt_string(zmq.SUBSCRIBE, np.unicode(''))
footage_socket.setsockopt_string(zmq.SUBSCRIBE, np.compat.unicode(''))

while True:

    frame = footage_socket.recv_string()        
    img1= base64.b64decode(frame)   
    npimg = np.frombuffer(img1, dtype=np.uint8)       
    source = cv2.imdecode(npimg, 1)
    cv2.imshow("frame", source)
    key = cv2.waitKey(10)
    if key == 27:
        break

cap.release()
cv2.destroyAllWindows()
