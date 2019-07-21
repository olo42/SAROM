sc create SAROM binPath=%~dp0SAROM.exe
sc failure SAROM actions=restart/60000/restart/60000/""/60000 reset=86400
sc start SAROM
sc config SAROM start=auto