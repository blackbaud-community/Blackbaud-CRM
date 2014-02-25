<?php
function guid(){
    if (function_exists('com_create_guid')){
        return com_create_guid();
    }else{
        mt_srand((double)microtime()*10000);//optional for php 4.2.0 and up.
        $charid = strtoupper(md5(uniqid(rand(), true)));
        $hyphen = chr(45);// "-"
        $uuid = chr(123)// "{"
                .substr($charid, 0, 8).$hyphen
                .substr($charid, 8, 4).$hyphen
                .substr($charid,12, 4).$hyphen
                .substr($charid,16, 4).$hyphen
                .substr($charid,20,12)
                .chr(125);// "}"
        return $uuid;
    }
}


/***************
*     DATES
***************/
function make_display_date($datestr) {
	if(!empty($datestr)) {
		return date_format(date_create($datestr), "m/d/Y");
	}
	return NULL;
}

function make_seasonal_display_date($dateStr) {
	if(!isset($dateStr) || $dateStr == "0000") {
		return NULL;
	} else {
		return date_format(date_create_from_format("md", $dateStr), "m/d");
	}
}

/*
* From the days of SQL Server 2005, some time strings are stored as a 
* user-defined datatype.
* 
* "NULL" values of this type are returned as four spaces in the 
* response, so we must check for NULL or a string of four spaces.
*/
function make_display_time($timestr) {
	if (!isset($timestr) || $timestr == "    ") {
		return NULL;
	} else {
		return date_format(date_create($timestr), "g:i A");
	}
}
/**************************************
*       Web Service/SOAP
***************************************/
function getSoapClient(){
	// the url of the BBEC web service definition language
	//ex:  $wsdl = 'http://localhost/bbappfx/appfxwebservice.asmx?wsdl';
	$wsdl = 'http://localhost/bbappfx/appfxwebservice.asmx?wsdl';

	// the credentials to access the web service using basic authentication
	// for $userName use an application user that has rights to access the feature within Blackbaud CRM
	
	$userName = 'Domain\User Name';
	$password = 'Password';

	$options = array(
	  'login'        => $userName,
	  'password'     => $password,
	  'soap_version' => SOAP_1_2,
	  'exceptions'   => true,
	  'trace'        => 1,
	  'cache_wsdl'   => WSDL_CACHE_NONE
	);

	// access the BBEC web service through a SoapClient object
	$sc = new SoapClient($wsdl, $options); 

	return $sc;
}

function getClientAppInfo() {
	// the app info object must be supplied with each request made to the BBEC web service
	
	// the database to use
	$database = 'BBInfinity';
	
	$clientAppInfo = array(
		'ClientAppName'   => 'PHP Sample Application',
		'REDatabaseToUse' => $database,
		'TimeOutSeconds'  => 120
	);

	return $clientAppInfo;
}

function getSoapString($stringVar) {
	return new SoapVar($stringVar, XSD_STRING, "string", "http://www.w3.org/2001/XMLSchema");
}

function getSoapBoolean($booleanVar) {
	return new SoapVar($booleanVar, XSD_BOOLEAN, "boolean", "http://www.w3.org/2001/XMLSchema");
}

function getSoapDate($dateVar) {
    return new SoapVar($dateVar, XSD_DATETIME, "dateTime", "http://www.w3.org/2001/XMLSchema");
}

function getStringFilter($fieldID, $fieldValue, $fieldValueTrans='') {
	$fv = new stdClass;
	/* debug::dump(getSoapString($fieldValue)); */
	$fv->Value=getSoapString($fieldValue);
	$fv->ID=$fieldID;

	if (strlen($fieldValueTrans) > 0) {
		$fv->ValueTranslation=$fieldValueTrans;
		}
	return $fv;
}

function getBooleanFilter($fieldID, $fieldValue) {
	$fv = new stdClass;
	if ($fieldValue == 'on') {
		$fv->Value=getSoapBoolean(true);
		}
	else {
		$fv->Value=getSoapBoolean(false);
		}

	$fv->ID=$fieldID;

	return $fv;
}

function getDateFilter($fieldID, $fieldValue) {
    $fv = new stdClass;
    $fv->Value=getSoapDate($fieldValue);
    $fv->ID=$fieldID;
    return $fv;
}

function getNullFilter($fieldID) {
    $fv = new stdClass;
    $fv->ID=$fieldID;
    $fv->Value=NULL;
    return $fv;
}

 /*
 * When retrieving a GUID from a DataFormLoad, it will be stored as a
 * SoapVar because it will likely be associated with a value 
 * translation.
 * 
 * We just need to extract the GUID itself.
 */
function getGuidValue($fieldValue) {
	if(!empty($fieldValue->enc_value)) {
		return $fieldValue->enc_value;
	} else {
		return $fieldValue;
	}
}

/**************************************
*   Data Driven Lists for Drop Downs
***************************************/

function getCodeTableList($codeTableName) {
	 try {
		$sc            = getSoapClient();
		$clientAppInfo = getClientAppInfo();
	 
		  // build the parameters for the CodeTableEntryGetList method 
		  //(these correspond to properties of the CodeTableEntryGetListRequest type)
		  $codeTableReq = array(
			  'ClientAppInfo'   => $clientAppInfo,
			  'CodeTableName'   => $codeTableName, // the name of the code table in the db
			  'IncludeInactive' => 'false',     // Include inactive (soft deletes) code table records
			  'ReturnListSortMethod' => 'true',     // return how the list is sorted (User Defined v. Alpha)
			  'UseCodeTableEntrySecurity' => 'false'     // 
		  );
		  
		  $codeTableReply = $sc->CodeTableEntryGetList($codeTableReq);
		  //debug::dump(getSoapString($codeTableReply));
		  
		   If (isset($codeTableReply->Rows->r)){
			$codeTableRows=$codeTableReply->Rows->r;	
			}
			else {
				$codeTableRows = array();
			}
		  
			if (!empty($codeTableRows) && !is_array($codeTableRows)) {
				// This is a single row -- make it an array
				$codeTableRows = array($codeTableRows);
			}
	
		Return $codeTableRows;
		  
		  
		}
		catch (SOAPFault $f) {
			debug::dump(getSoapString($f));
		}
		 
}

function getSimpleDataList($dataListID, $parameterList = NULL) {
	try {
			$sc            = getSoapClient();
			$clientAppInfo = getClientAppInfo();
		 
			if (is_null($parameterList)) {
		 
				// build the parameters for the CodeTableEntryGetList method 
				//(these correspond to properties of the CodeTableEntryGetListRequest type)
				$simpleDataListLoadReq = array(
					'ClientAppInfo'   => $clientAppInfo,
					'DataListID'   => $dataListID // the name of the code table in the db
					
				);
			}
			else {
				$filterCount = 0;
				$filterValues=array();
				
				#debug::dump(getSoapString($parameterList));
				
				for ($i=0;$i < sizeof($parameterList); $i++) {
					
					//debug::dump(getSoapString($parameterList[$i]['ID']));
					//debug::dump(getSoapString($parameterList[$i]));
					
					$filterValues[$filterCount]= getStringFilter($parameterList[$i]['ID'], $parameterList[$i]['Value']);
					$filterCount++;
				}

				$parameters = new stdClass;
				$parameters->Values=new stdClass;
				$parameters->Values->fv=$filterValues;
				
				$simpleDataListLoadReq = array(
				  'ClientAppInfo'   => $clientAppInfo,
				  'DataListID'   	=> $dataListID, // the name of the code table in the db
				  'Parameters' 		=> $parameters
				);
			}

			$simpleDataListLoadReply = $sc->SimpleDataListLoad($simpleDataListLoadReq);  
					  
			If (isset($simpleDataListLoadReply->Rows->r)){
				$simpleDataListRows=$simpleDataListLoadReply->Rows->r;	
			}
			else {
				$simpleDataListRows = array();
			}
			 
			if (!empty($simpleDataListRows) && !is_array($simpleDataListRows)) {
				// This is a single row -- make it an array
				$simpleDataListRows = array($simpleDataListRows);
			}
			
			Return $simpleDataListRows;
			
		}
		  
		catch (SOAPFault $f) {
			debug::dump(getSoapString($f));
		}
		  
		
}

?>