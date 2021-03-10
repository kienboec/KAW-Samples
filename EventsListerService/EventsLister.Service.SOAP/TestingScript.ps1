$messageHasData = @"
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
    xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
    xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <HasData xmlns="http://tempuri.org/"></HasData>
  </soap:Body>
</soap:Envelope>
"@

$messageGetAllEvents = @"
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
    xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
    xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <GetAllEvents xmlns="http://tempuri.org/"></GetAllEvents>
  </soap:Body>
</soap:Envelope>
"@

cls

$result = Invoke-WebRequest -Uri "https://localhost:44313/Service.asmx" -Method Post -Body $messageHasData 
write-host $result
$result = Invoke-WebRequest -Uri "https://localhost:44313/Service.asmx" -Method Post -Body $messageGetAllEvents 
write-host $result
$result = Invoke-WebRequest -Uri "https://localhost:44313/Service.asmx" -Method Post -Body $messageHasData 
write-host $result