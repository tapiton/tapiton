using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EricProject.Site
{
    public class ClassAuto
    {
//         public function __doRequest($request, $location, $action, $version, $one_way = NULL) {
//    global $context;
//    $hmackey = "vjviT1g085PaaPzAwqU8QA8lwLFz9sz1"; // <-- Insert your HMAC key here
//    $keyid = "36488"; // <-- Insert the Key ID here
//    $hashtime = date("c");
//    $hashstr = "POST\ntext/xml; charset=utf-8\n" . sha1($request) . "\n" . $hashtime . "\n" . parse_url($location,PHP_URL_PATH);
//    $authstr = base64_encode(hash_hmac("sha1",$hashstr,$hmackey,TRUE));
//    if (version_compare(PHP_VERSION, '5.3.11') == -1) {
//        ini_set("user_agent", "PHP-SOAP/" . PHP_VERSION . "\r\nAuthorization: GGE4_API " . $keyid . ":" . $authstr . "\r\nx-gge4-date: " . $hashtime . "\r\nx-gge4-content-sha1: " . sha1($request));
//    } else {
//        stream_context_set_option($context,array("http" => array("header" => "authorization: GGE4_API " . $keyid . ":" . $authstr . "\r\nx-gge4-date: " . $hashtime . "\r\nx-gge4-content-sha1: " . sha1($request))));
//    }
//    return parent::__doRequest($request, $location, $action, $version, $one_way);
//  }

//  public function SoapClientHMAC($wsdl, $options = NULL) {
//    global $context;
//    $context = stream_context_create();
//    $options['stream_context'] = $context;
//    return parent::SoapClient($wsdl, $options);
//  }
//}

//$trxnProperties = array(
//  "User_Name"=>"testing196",
//  "Secure_AuthResult"=>"",
//  "Ecommerce_Flag"=>"",
//  "XID"=>"",
//  "ExactID"=>"AD8667-05",				    //Payment Gateway
//  "CAVV"=>"",
//  "Password"=>"g5sdv1vt",					                //Gateway Password
//  "CAVV_Algorithm"=>"",
//  "Transaction_Type"=>"00",//Transaction Code I.E. Purchase="00" Pre-Authorization="01" etc.
//  "Reference_No"=>"",
//  "Customer_Ref"=>"",
//  "Reference_3"=>"",
//  "Client_IP"=>"",					                    //This value is only used for fraud investigation.
//  "Client_Email"=>"pradeep_choudhry@seologistics.com",			//This value is only used for fraud investigation.
//  "Language"=>"en",				//English="en" French="fr"
//  "Card_Number"=>"",		    //For Testing, Use Test#s VISA="4111111111111111" MasterCard="5500000000000004" etc.
//  "Expiry_Date"=>"0414",//This value should be in the format MM/YY.
//  "CardHoldersName"=>"Pradeep",
//  "CardType"=>"MasterCard",
//  "Track1"=>"",
//  "Track2"=>"",
//  "Authorization_Num"=>"",
//  "Transaction_Tag"=>"",
//  "DollarAmount"=>"11.01",
//  "VerificationStr1"=>"",
//  "VerificationStr2"=>"",
//  "CVD_Presence_Ind"=>"",
//  "Secure_AuthRequired"=>"",
//  "Currency"=>"USD",
//  "PartialRedemption"=>"",
//  "TransarmorToken"=>"",

//  // Level 2 fields
//  "ZipCode"=>"",
//  "Tax1Amount"=>"",
//  "Tax1Number"=>"",
//  "Tax2Amount"=>"",
//  "Tax2Number"=>"",

//  //"SurchargeAmount"=>$_POST["tbPOS_SurchargeAmount"],	//Used for debit transactions only
//  //"PAN"=>$_POST["tbPOS_PAN"]							//Used for debit transactions only
//  );


//$client = new SoapClientHMAC("https://api.demo.globalgatewaye4.firstdata.com/transaction/v12/wsdl");
//$trxnResult = $client->SendAndCommit($trxnProperties);


//if(@$client->fault){
//    // there was a fault, inform
//    print "<B>FAULT:  Code: {$client->faultcode} <BR />";
//    print "String: {$client->faultstring} </B>";
//    $trxnResult["CTR"] = "There was an error while processing. No TRANSACTION DATA IN CTR!";
//}
////Uncomment the following commented code to display the full results.

//echo "<H3><U>Transaction Properties BEFORE Processing</U></H3>";
//echo "<TABLE border='0'>\n";
//echo " <TR><TD><B>Property</B></TD><TD><B>Value</B></TD></TR>\n";
//foreach($trxnProperties as $key=>$value){
//    echo " <TR><TD>$key</TD><TD>:$value</TD></TR>\n";
//}
//echo "</TABLE>\n";

//echo "<H3><U>Transaction Properties AFTER Processing</U></H3>";
//echo "<TABLE border='0'>\n";
//echo " <TR><TD><B>Property</B></TD><TD><B>Value</B></TD></TR>\n";
//foreach($trxnResult as $key=>$value){
//    $value = nl2br($value);
//    echo " <TR><TD valign='top'>$key</TD><TD>:$value</TD></TR>\n";
//}
//echo "</TABLE>\n";


//// kill object
//unset($client);
//?>
    }
}