import socket
import os
import threading
import time
import asteriskconffile
import reloadAsterisk
from reloadAsterisk import *
from asteriskconffile import *

class ReceiveData(threading.Thread):
    def __init__(self, socket):
        self.reload = ReloadAsterisk()
        threading.Thread.__init__(self)
        self.conn, self.addr = socket
        print 'connected: ', self.addr
        self.runable = True
        self.raf = ReadAFile()
    def run(self):
        try:
            while self.runable:
                data = self.conn.recv(8192).splitlines()
                print data[0],data[1:]
                if data[0].strip() == 'creategroup':
                    self.raf.creategroup(data[1])
                elif data[0].strip() == 'createuser':
                    self.raf.createuser(data[1],data[2],data[3],data[4])
#                    self.reload.reload(0.12)
                elif data[0].strip() == 'deletegroup':
                    self.raf.deletegroup(data[1])
#                    self.reload.reload(0.12)
                elif data[0].strip() == 'deleteuser':
                    self.raf.deleteuser(data[1])
#                    self.reload.reload(0.12)
                elif data[0].strip() == 'modifyuser':
                    self.raf.modifyuser(data[1],data[2],data[3],data[4])
#                    self.reload.reload(0.12)
                elif data[0].strip() == 'modifygroup':
                    self.raf.modifygroup(data[1],data[2],data[3])
#                    self.reload.reload(0.12)
                elif data[0].strip() == 'deleteusergroup':
                    self.raf.deleteusergroup(data[1],data[2],data[3])
#                    self.reload.reload(0.12)
                elif data[0].strip() == 'cleardata':
                    self.raf.cleardata()
                elif data[0].strip() == 'reload':
                    self.reload.reload(0.15)
        except:
            print 'client ' + str(self.addr[0]) +':' +  str(self.addr[1]) + ' disconnected'
            raise
    def stop(self):
        self.runable = False

if __name__ == '__main__':
    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    addr = ("10.0.0.23",2577)
    s.bind(addr)
    s.listen(4)
    print 'server started'
    while True:
        try:
            thread = ReceiveData(s.accept())
            thread.daemon = True
            thread.start()
            thread.join()
        except:
            print 'thread error'
            raise
#    c = asteriskTelnet("manager","1234")
#    while True:
#        os.system('cls')
#        print ' PScript Menu Command \r\n reload - Reload Asterisk\r\n peers'\
#        + ' - Show SIP Peers\r\n restart - Restart Server\r\n start - Start Server'\
#        + '\r\n exit - close Server'
#        command = raw_input(' PScript Menu Command ')
#        if command.tolower() is 'reload':
#            c.reload(0.15)
