import SipValues
import os
from SipValues import *

class ReadAFile:
    def __init__(self):
        try:
            self.Def = SipValues
            #self.string = 'hola'
            if not (os.path.isfile(self.Def.sipFile)):
                fil = open(self.Def.sipFile,'wb')
                for content in self.Def.sipContentDefault:
                    fil.write(content + '\r\n')
                fil.close()

            if not (os.path.isfile(self.Def.extensionsFile)):
                fil = open(self.Def.extensionsFile,'wb')
                for content in self.Def.extensionsContentDefault:
                    fil.write(content + '\r\n')
                fil.close()
        except:
            raise

    def EnumerateLines(self, filePath):
            try:
                Lines = open(filePath).readlines()
            except:
                return 'Error al abrir el archivo'
            finally:
                return Lines

    def FindLine(self, line, filePath):
        try:
            NumLine = None
            lines = open(filePath).readlines()
            for i, fline in enumerate(lines[:]):
                if fline.rstrip() == line:
                    #print i
                    NumLine = i
                    break
            return NumLine
        except:
            raise
        finally:
            return NumLine

    def getLine(self, name, path):
        try:
            ind = self.FindLine('[' + name + ']', path)
            #print isinstance(ind,int)
            if isinstance(ind,int):
                return open(path).readlines()[ind]
            else:
                return None
        except:
            raise

    def checkcontext(self, name, path):
        if self.getLine(name, path) is not None:
            return True
        else:
            return False

    def modifyuser(self, name, user, password, identifier):
        try:
            if self.checkcontext(name, self.Def.sipFile):
                lines = self.EnumerateLines(self.Def.sipFile)
                lin = self.getLine(name, self.Def.sipFile)
                startofcontext = lines.index(lin)
                #print startofcontext
                try:
                    if str(lines[startofcontext + 1]) is not '':
                        endofcontext = lines[startofcontext:].index('')
                        print endofcontext, 'try'#, str(lines[startofcontext + 1]), 'try'
                    else:
                        endofcontext = lines[startofcontext:].index('\r\n')
                        print endofcontext, 'else'
                except:
                    endofcontext = os.path.getsize(self.Def.sipFile)
                newcontext = list(self.Def.AccountDefault)
                newLines = lines[:startofcontext + 1] + [newcontext[0]+ user + \
                '\r\n'] + [newcontext[1] + password + '\r\n'] + \
                newcontext[2:5] + [newcontext[5] + name + '\r\n'] + \
                newcontext[6:] + lines[endofcontext:]
                fil = open(self.Def.sipFile,'wb')
                for line in newLines:
                    fil.write(line)
                fil.close()
        except:
            raise

    def addToContext(self, context, path, newline):
        try:
            lines = self.EnumerateLines(path)
            num = self.FindLine(context, path)
            num2 = lines[num:].index('\r\n')
            #print context
#            print newline in lines[num:num + num2 + 1]
            #print lines[num:num + num2 + 1]
            if newline not in lines[num:num + num2 + 1]:
                lines = lines[:num + 1] + [newline] + lines[num + 1:]
                #print lines
                fil = open(path,'wb')
                for line in lines:
                    fil.write(line)
                fil.close()
        except:
            raise

    def modifygroup(self, group, user, exten):
        try:
#            lines = self.EnumerateLines(self.Def.extensionsFile)
#            line = lines.index('[' + group + ']\r\n')
#            print line
#            if '[' + group + ']' in lines:
#                lines = self.EnumerateLines(self.Def.sipFile)
#                if '[' + user + ']' in lines:
            if self.checkcontext(group,self.Def.extensionsFile):
                if self.checkcontext(user,self.Def.sipFile):
                    self.addToContext('[' + user + ']',self.Def.extensionsFile,
                            'include => '+ group + '\r\n')
                    self.addToContext('[' + group + ']',self.Def.extensionsFile,
                        'exten => ' + exten + ',1,Macro(dialSIP,' + user + \
                        ')\r\n')
            else:
                print 'el group no existe'
                raise
        except:
            raise

    def creategroup(self, name):
        if not self.checkcontext(name, self.Def.extensionsFile):
            try:
                lines = ['[' + name + ']\r\n\r\n'] + self.EnumerateLines(\
                self.Def.extensionsFile)
                fil = open(self.Def.extensionsFile,'wb')
                for line in lines:
                    fil.write(line)
                fil.close()
            except:
                raise

    def createuser(self, name, user, password, identifier):
        if not self.checkcontext(name, self.Def.sipFile):
            try:
                lines = self.EnumerateLines(self.Def.sipFile) + \
                        ['\r\n[' + name + ']\r\n']
                fil = open(self.Def.sipFile,'wb')
                for line in lines:
                    fil.write(line)
                fil.close()
                self.modifyuser(name, user, password, identifier)
                self.creategroup(name)
            except:
                raise

    def deleteuser(self, name):
        if self.checkcontext(name, self.Def.sipFile):
            try:
                line = self.getLine(name,self.Def.sipFile)
                lines = self.EnumerateLines(self.Def.sipFile)
                num = lines.index(line)
                exten = lines[num+1][9:]
                lines = lines[:num-1] + lines[num + 9:]
                fil = open(self.Def.sipFile, 'wb')
                for lin in lines:
                    fil.write(lin)
                fil.close()
                #----------------
                lines = self.EnumerateLines(self.Def.extensionsFile)
                num = lines.index(line)
                endofcontext = lines[num:].index('\r\n')
                lines = lines[:num-1] + lines[num + endofcontext:]
                fil = open(self.Def.extensionsFile, 'wb')
                for lin in lines:
                    if 'exten => ' + exten not in lin and name not in lin:
                        print lin
                        fil.write(lin)
                fil.close()
            except:
                raise

    def deletegroup(self, name):
        if self.checkcontext(name,self.Def.extensionsFile):
            try:
                line = self.getLine(name,self.Def.extensionsFile)
                lines = self.EnumerateLines(self.Def.extensionsFile)
                num = lines.index(line)
                endofcontext = lines[num:].index('\r\n')
                lines = lines[:num] + lines[num + endofcontext + 1:]
                fil = open(self.Def.extensionsFile, 'wb')
                for lin in lines:
                    fil.write(lin)
                fil.close()
                #------------------
                lines = self.EnumerateLines(self.Def.extensionsFile)
                fil = open(self.Def.extensionsFile,'wb')
                for lin in lines:
                    if lin != 'include => '+ name + '\r\n':
                        fil.write(lin)
                fil.close()
            except:
                raise
    def deleteusergroup(self,group,user,exten):
        try:
            lines = self.EnumerateLines(self.Def.extensionsFile)
            groupline = lines.index('[' + group + ']\r\n')
            try:
                groupend = lines[groupline:].index('\r\n')
            except:
                groupend = len(lines)
            gline = lines[groupline:groupend + groupline].index('exten => ' +\
            exten + ',1,Macro(diaSIP,' + user + ')\r\n')
            lines = lines[:groupline + 1] + lines[groupline + 1:gline] + \
            lines[gline + 1:]
            userline = lines.index('[' + user + ']\r\n')
            try:
                userend = lines[userline:].index('\r\n')
            except:
                userend = len(lines)
            uline = lines[userline:userend + userline].index('include => ' +\
            group + '\r\n')
            print userline,userend, uline
            lines = lines[:userline + 1] + lines[userline + 1:uline] + \
            lines[uline + 1:]
            print lines
            fil = open(self.Def.extensionsFile,'wb')
            for lin in lines:
                fil.write(lin)
            fil.close()
        except:
            raise
    def cleardata(self):
        try:
            if (os.path.isfile(self.Def.sipFile)):
                fil = open(self.Def.sipFile,'wb')
                for content in self.Def.sipContentDefault:
                    fil.write(content + '\r\n')
                fil.close()
            if (os.path.isfile(self.Def.extensionsFile)):
                fil = open(self.Def.extensionsFile,'wb')
                for content in self.Def.extensionsContentDefault:
                    fil.write(content + '\r\n')
                fil.close()
        except:
            raise
    def group(self, name):
        pass
    def user(self, name):
        pass
"""
    options = {'[CHECKGROUP]' : checkcontext,\
               '[CHECKUSER]' : checkcontext,\
               '[CREATEUSER]' : createuser,\
               '[CREATEGROUP]' : creategroup,\
               '[DELETEUSER]' : deleteuser,\
               '[DELETEGROUP]' : deletegroup,\
               '[MODIFYUSER]' : modifyuser,\
               '[MODIFYGROUP]' : modifygroup,\
               '[GROUP]' : group,\
               '[USER]' : user}
"""
#-------------------------------------
#if __name__ == '__main__':
#    c = ReadAFile()
#    c.deleteusergroup('Epsoon','JN1','6010')
#    print 'end'
"""
    c.creategroup('Call Center')
    c.createuser('juan','2000','2412','juan')
    c.createuser('camile','2400','212','abc')
    c.modifygroup('Call Center','juan', '4000')
    """
    #c.modifygroup('Call Center','camile', '2400')

    #c.createuser('juan2','2000','2412','juan')
    #c.createuser('juan22','2000','2412','juan')
    #c.createuser('juan3','2000','2412','juan')
    #c.createuser('juan1','2000','2412','juan')
    #c.modifyuser('juan45','3000','631', 'julian')
    #c.createuser('juan11','2000','2412','juan')
    #c.modifyuser('camile','2033','blahblah','hey!')
    #c.modifygroup('Call Center','juan', '5000')
    #c.modifygroup('Call Center','juan', '1000')
    #c.modifygroup('Call Center','juan2', '8000')
    #c.modifygroup('Call Center','juan4', '3000')
    #c.deleteuser('juan')
    #c.deletegroup('Call Center')
    #c.deleteusergroup('Call Center','camile','2400')

