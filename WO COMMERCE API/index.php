<?php

$curl = curl_init();

curl_setopt_array($curl, array(
  CURLOPT_URL => "https://cmvstore.voipportingservices.com/wp-json/wc/v2/products/323?oauth_consumer_key=ck_c7ed560b0d4fab584988731022e9a28732dd9c0e&oauth_signature_method=HMAC-SHA1&oauth_timestamp=1583866141&oauth_nonce=fZife8m30yS&oauth_version=1.0&oauth_signature=Y8vgMMN+tZt4HSbjl2VEWA/n+Cw=",
  CURLOPT_RETURNTRANSFER => true,
  CURLOPT_ENCODING => "",
  CURLOPT_MAXREDIRS => 10,
  CURLOPT_TIMEOUT => 5,
  CURLOPT_FOLLOWLOCATION => true,
  CURLOPT_HTTP_VERSION => CURL_HTTP_VERSION_1_1,
  CURLOPT_CUSTOMREQUEST => "GET",
));

$response = curl_exec($curl);

curl_close($curl);
print_r($response);

?>