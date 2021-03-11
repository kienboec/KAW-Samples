@REM cls -> clear screen
cls 
@REM -> remark -> comment ... no @echo off

REM get all info from swear jar
curl -X GET "https://localhost:44331/api/SwearJar" -H  "accept: text/plain"

rem add 1 to swear jar for user daniel
curl -X POST "https://localhost:44331/api/SwearJar/daniel" -H  "accept: */*" -d ""

rem check if user daniel is in swear jar with 1 as balance
curl -X GET "https://localhost:44331/api/SwearJar" -H  "accept: text/plain"

rem check only daniel and if user daniel is in swear jar with 1 as balance
curl -X GET "https://localhost:44331/api/SwearJar/daniel" -H  "accept: text/plain"

rem add +3 to balance by calling post 3 times
curl -X POST "https://localhost:44331/api/SwearJar/daniel" -H  "accept: */*" -d ""
curl -X POST "https://localhost:44331/api/SwearJar/daniel" -H  "accept: */*" -d ""
curl -X POST "https://localhost:44331/api/SwearJar/daniel" -H  "accept: */*" -d ""

rem check swear jar for balance 4
curl -X GET "https://localhost:44331/api/SwearJar" -H  "accept: text/plain"

rem remove daniel from the swear jar ... balance is reset
curl -X DELETE "https://localhost:44331/api/SwearJar/daniel" -H  "accept: */*"

rem is swear jar empty?
curl -X GET "https://localhost:44331/api/SwearJar" -H  "accept: text/plain"

REM ---------------------------------------------------------------------------------

REM check for edge cases
curl -X GET "https://localhost:44331/api/SwearJar/benjamin" -H  "accept: text/plain" -i
curl -X DELETE "https://localhost:44331/api/SwearJar/benjamin" -H  "accept: text/plain" -i