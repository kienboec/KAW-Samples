
@echo off
cls

echo get question
curl -X GET "https://localhost:44318/api/Question" -H  "accept: text/plain"
echo.
echo.

echo get answers of users
curl -X GET "https://localhost:44318/api/Question/answers" -H  "accept: text/plain"
echo.
echo.

echo set answer to 4
curl -X POST "https://localhost:44318/api/Question/answers" -H  "accept: */*" -H  "Content-Type: application/json" -d "4"
echo.
echo.

echo get answers of users
curl -X GET "https://localhost:44318/api/Question/answers" -H  "accept: text/plain"
echo.
echo.

echo get result of question
curl -X GET "https://localhost:44318/api/Question/result" -H  "accept: text/plain"
echo.
echo.

echo remove answer counts
curl -X DELETE "https://localhost:44318/api/Question/answers" -H  "accept: */*"
echo.
echo.
