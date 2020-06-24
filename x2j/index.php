<!DOCTYPE html>

<html>

<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">

    <title>X2J</title>

    <link rel="stylesheet" href="./jjsonviewer.css">
    <meta name="robots" content="noindex, nofollow">
    <meta name="googlebot" content="noindex, nofollow">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <script type="text/javascript" src="./jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="./angular.js"></script>
    <script type="text/javascript" src="./json2xml.js"></script>
    <script type="text/javascript" src="./xml2json.js"></script>
    <script type="text/javascript" src="./jjsonviewer.js"></script>







</head>

<body>

<?php

$curl = curl_init();

$number='<tns>5103943687</tns>';

curl_setopt_array($curl, array(
  CURLOPT_URL => "https://aniweb02.peerlessnetwork.com:8181/animateapi/axis/APIService?wsdl",
  CURLOPT_RETURNTRANSFER => true,
  CURLOPT_ENCODING => "",
  CURLOPT_MAXREDIRS => 10,
  CURLOPT_TIMEOUT => 0,
  CURLOPT_FOLLOWLOCATION => true,
  CURLOPT_HTTP_VERSION => CURL_HTTP_VERSION_1_1,
  CURLOPT_CUSTOMREQUEST => "POST",
  CURLOPT_POSTFIELDS =>"<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:pub=\"http://publicapi.api.s2.peerless.com/\">\r\n  <soapenv:Header/>\r\n  <soapenv:Body>\r\n     <pub:portabilityCheck>\r\n        <!--Optional:-->\r\n        <authentication>\r\n           <!--Optional:-->\r\n             <customer>CONNECTM</customer>\r\n          <!--Optional:-->\r\n          <passCode>hwi7le2b1z9</passCode>\r\n          <!--Optional:-->\r\n          <userId>scott@connectmevoice.com</userId>\r\n        </authentication>\r\n        <!--Optional:-->\r\n        <portabilityCheckRequest>\r\n           <!--Optional:-->\r\n           <tns>\r\n              <!--Zero or more repetitions:-->\r\n              .$number.\r\n           </tns>\r\n        </portabilityCheckRequest>\r\n     </pub:portabilityCheck>\r\n  </soapenv:Body>\r\n</soapenv:Envelope> \r\n",
  CURLOPT_HTTPHEADER => array(
    "Content-Type: text/xml"
  ),
));

$response = curl_exec($curl);

$variable = json_encode($response);

curl_close($curl);


echo ("<script>localStorage.setItem('xml',$variable);</script>");?>

<script type="text/javascript">
        setTimeout(function(){ 

          window.location.reload()


        }, 5000);




        function MyController($scope) {

            function parseXml(xml) {
                var dom = null;
                if (window.DOMParser) {
                    try {
                        dom = (new DOMParser()).parseFromString(xml, "text/xml");
                    } catch (e) {
                        dom = null;
                    }
                } else if (window.ActiveXObject) {
                    try {
                        dom = new ActiveXObject('Microsoft.XMLDOM');
                        dom.async = false;
                        if (!dom.loadXML(xml)) // parse error ..
                            window.alert(dom.parseError.reason + dom.parseError.srcText);
                    } catch (e) {
                        dom = null;
                    }
                } else
                    alert("oops");
                return dom;
            }



            $scope.XmlInput = "";
            $scope.JsonOutput = '';
            $scope.XmlOutput = '';

            $scope.Convert = function() {
                var parsedXml = parseXml(localStorage.getItem('xml'));
                $scope.JsonOutput = xml2json(parsedXml, '\t');
                $scope.XmlOutput = json2xml(eval('json=' + $scope.JsonOutput));
                // $scope.finalres.jJsonViewer($scope.XmlOutput);  
			          $("#jjson").jJsonViewer( $scope.JsonOutput);
            };


            $scope.Convert();
        }
    </script>
    <div ng-app>
        <div ng-controller="MyController"  class="jjson">       
        <div id="jjson" class="jjson"></div>

        </div>
    </div>





</body>


</html>