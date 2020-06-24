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
  CURLOPT_POSTFIELDS =>"<?xml version=\"1.0\" encoding=\"utf-8\"?> \r\n<soap12:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\"> \r\n <soap12:Body>\r\n <GetVendors xmlns=\"http://WebService.jenne.com\">\r\n <email>storeadmin@connectmevoice.com</email>\r\n <password>$3!!Sell@198</password>\r\n </GetVendors>\r\n </soap12:Body>\r\n</soap12:Envelope> ",
  CURLOPT_HTTPHEADER => array(
    "Content-Type: application/soap+xml"
  ),
));

$response = curl_exec($curl);

curl_close($curl);

$xml = simplexml_load_string($response);
$json = json_encode($response);

print_r ($response);
