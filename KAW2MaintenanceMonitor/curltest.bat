@echo off
cls

echo OK  expected
curl -X GET "https://localhost:44378/api/Maintenance" -H  "accept: text/plain"
echo.
echo.

curl -X POST "https://localhost:44378/api/Maintenance" -H  "accept: */*" -H  "Content-Type: application/json" -d "\"bad things happen ... #1\""

echo ERR expected #1
curl -X GET "https://localhost:44378/api/Maintenance" -H  "accept: text/plain"
echo.
echo.

curl -X POST "https://localhost:44378/api/Maintenance" -H  "accept: */*" -H  "Content-Type: application/json" -d "\"bad things happen ... #2\""

echo ERR expected #2
curl -X GET "https://localhost:44378/api/Maintenance" -H  "accept: text/plain"
echo.
echo.

curl -X DELETE "https://localhost:44378/api/Maintenance" -H  "accept: */*"

echo OK expected
curl -X GET "https://localhost:44378/api/Maintenance" -H  "accept: text/plain"
echo.
echo.
