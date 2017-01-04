class SipValues():
    AccountDefault =("username=",
                    "secret=",
                    "canreinvite=yes\r\n",
                    "type=friend\r\n",
                    "allow=gsm\r\n",
                    "context=",
                    "host=dynamic\r\n",
                    "bindport=5061\r\n"
                    )

#    Path = 'C:\\Users\\Jenky\\Desktop\\'
    Path = '/etc/asterisk/'
    sipFile = Path + 'sip.conf'
    extensionsFile = Path + 'extensions.conf'

    sipContentDefault = ("[general]",
                         "context=default",
                         "port=5060",
                         "bindaddr=0.0.0.0",
                         "srvlookup=yes\r\n")

    extensionsContentDefault = ("[macro-dialSIP]",
                                "exten => s,1,Dial(SIP/${ARG1})",
                                "exten => s,2,Goto(s-${DIALSTATUS},1)",
                                "exten => s-CANCEL,1,Hangup",
                                "exten => s-NOANSWER,1,Hangup",
                                "exten => s-BUSY,1,Busy(30)",
                                "exten => s-CONGESTION,1,Congestion(30)",
                                "exten => s-CHANUNAVAIL,1,playback(ss-noservice)",
                                "exten => s-CANCEL,1,Hangup")
