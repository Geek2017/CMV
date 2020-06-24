<?php

$curl = curl_init();

curl_setopt_array($curl, array(
  CURLOPT_URL => "https://webservice.jenne.com/",
  CURLOPT_RETURNTRANSFER => true,
  CURLOPT_ENCODING => "",
  CURLOPT_MAXREDIRS => 10,
  CURLOPT_TIMEOUT => 0,
  CURLOPT_FOLLOWLOCATION => true,
  CURLOPT_HTTP_VERSION => CURL_HTTP_VERSION_1_1,
  CURLOPT_CUSTOMREQUEST => "POST",
  CURLOPT_POSTFIELDS =>"<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<soap12:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\r\nxmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"\r\nxmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\">\r\n <soap12:Body>\r\n <FindProducts xmlns=\"http://WebService.jenne.com\">\r\n <email>storeadmin@connectmevoice.com</email>\r\n <password>$3!!Sell@198</password>\r\n <vendorCd>PHCP</vendorCd>\r\n <typeCd></typeCd>\r\n <findString>Panasonic Business Systems</findString>\r\n <pageSize>100</pageSize>\r\n <page>100</page>\r\n </FindProducts>\r\n </soap12:Body>\r\n</soap12:Envelope> ",
  CURLOPT_HTTPHEADER => array(
    "Content-Type: application/soap+xml"
  ),
));
   
    curl_setopt ($curl, CURLOPT_POST, TRUE);


    curl_setopt($curl, CURLOPT_USERAGENT, 'api');

    curl_setopt($curl, CURLOPT_TIMEOUT, 1); 
    curl_setopt($curl, CURLOPT_HEADER, 0);
    curl_setopt($curl,  CURLOPT_RETURNTRANSFER, false);
    curl_setopt($curl, CURLOPT_FORBID_REUSE, true);
    curl_setopt($curl, CURLOPT_CONNECTTIMEOUT, 1);
    curl_setopt($curl, CURLOPT_DNS_CACHE_TIMEOUT, 10); 

    curl_setopt($curl, CURLOPT_FRESH_CONNECT, true);

    


$response = curl_exec($curl);
curl_close($curl);
echo ('1234.....');

print_r ($response);
