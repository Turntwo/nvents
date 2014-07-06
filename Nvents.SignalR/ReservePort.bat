echo Run as Admin
echo Change port to port setup in Vehicle Tracker config, and user to your user name 
netsh http add urlacl url=http://+:8080/ user=JEFBAR\jason.christian
pause
netsh http delete urlacl url=http://+:8080/
pause