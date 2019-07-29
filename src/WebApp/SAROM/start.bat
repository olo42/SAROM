@Echo Off
START "SAROM" .\SAROM.exe --console
TIMEOUT 10
START http://localhost:5000
