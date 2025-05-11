import cv2
import zmq
import base64
import numpy as np

context = zmq.Context()
footage_socket = context.socket(zmq.SUB)
footage_socket.bind('tcp://*:5555')
#footage_socket.setsockopt_string(zmq.SUBSCRIBE, np.unicode(''))
footage_socket.setsockopt_string(zmq.SUBSCRIBE, np.compat.unicode(''))
while True:
    try:
        frame = footage_socket.recv_string()   
        img1= base64.b64decode(frame)    
        npimg = np.frombuffer(img1, dtype=np.uint8)       
        source = cv2.imdecode(npimg, 1)
        cv2.imshow("frame", source)
        cv2.waitKey(1)                              # Display it at least one ms
        #                                           # before going to the next frame
    except KeyboardInterrupt:
        cv2.destroyAllWindows()
        break
