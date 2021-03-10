REM REM -> Remark -> comment
REM clear screen
@cls

REM call the "GET ALL DATA" operation on resource SwearJar
curl -X GET "https://localhost:44341/api/SwearJar" -H  "accept: text/plain" -i

REM call the get my balance from the swear jar
curl -X GET "https://localhost:44341/api/SwearJar/daniel" -H  "accept: text/plain" -i

REM add 1 to the balance in the swear jar
curl -X POST "https://localhost:44341/api/SwearJar/daniel" -H  "accept: */*" -d "" -i

REM call the get my balance from the swear jar
curl -X GET "https://localhost:44341/api/SwearJar/daniel" -H  "accept: text/plain" -i

REM call the "GET ALL DATA" operation on resource SwearJar
curl -X GET "https://localhost:44341/api/SwearJar" -H  "accept: text/plain" -i

REM remove all of daniel's balance info of the swear jar
curl -X DELETE "https://localhost:44341/api/SwearJar/daniel" -H  "accept: */*" -i

REM check delete twice
echo check if call does not fail...
curl -X DELETE "https://localhost:44341/api/SwearJar/daniel" -H  "accept: */*" -i

REM call the get my balance from the swear jar
curl -X GET "https://localhost:44341/api/SwearJar/daniel" -H  "accept: text/plain" -i

REM call the "GET ALL DATA" operation on resource SwearJar
curl -X GET "https://localhost:44341/api/SwearJar" -H  "accept: text/plain" -i

@echo.
@echo.
@echo.
@echo.
@echo.
@echo.
echo check for errors
curl -X DELETE "https://localhost:44341/api/SwearJar/luca" -H  "accept: */*" -i
curl -X GET "https://localhost:44341/api/SwearJar/luca" -H  "accept: */*" -i

REM call the "GET ALL DATA" operation on resource SwearJar
curl -X GET "https://localhost:44341/api/SwearJar" -H  "accept: text/plain" -i