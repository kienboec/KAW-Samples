cls
@echo off

echo output should be "OK"
curl -X GET "https://localhost:44328/api/Maintenance" -H  "accept: text/plain"
echo.
echo.

echo set error; output should be "ERROR"
curl -X POST "https://localhost:44328/api/Maintenance" -H  "accept: text/plain" -H  "Content-Type: application/json" -d "\"bad troubles with internet connection! d'oh\""
echo.
echo.

echo output should be "ERROR"
curl -X GET "https://localhost:44328/api/Maintenance" -H  "accept: text/plain"
echo.
echo.

echo set error; output should be "ERROR"
curl -X POST "https://localhost:44328/api/Maintenance" -H  "accept: text/plain" -H  "Content-Type: application/json" -d "\"bad errors are bad\""
echo.
echo.

echo output should be "ERROR"
curl -X GET "https://localhost:44328/api/Maintenance" -H  "accept: text/plain"
echo.
echo.


echo reset error; output should be "OK" again
curl -X DELETE "https://localhost:44328/api/Maintenance" -H  "accept: text/plain"
echo.
echo.

echo output should be "OK"
curl -X GET "https://localhost:44328/api/Maintenance" -H  "accept: text/plain"
echo.
echo.
