import sys
import telnetlib
import time

class ReloadAsterisk():
    def __init__(self):
        pass
    def reload(self,tim):
        HOST="localhost"
        tn = telnetlib.Telnet(HOST,5038)
        time.sleep(tim)
        tn.write("Action: login\r\nUsername:  manager\r\nSecret: 1234\r\n\r\n")
        time.sleep(tim)
        tn.write("Action: command\r\nCommand: reload\r\n\r\n")
        time.sleep(tim)
        tn.write("Action: logoff\r\n\r\n")
        time.sleep(tim)
        tn.close()
