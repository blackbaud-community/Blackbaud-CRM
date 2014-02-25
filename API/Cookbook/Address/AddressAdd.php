<?php
	// Data Form: Address Add Form 2
	// System record ID:	c31a23ff-1913-4501-bd62-63474923d1bc	
	// Adds an address for a constituent

require_once("debug.php");  // ex: debug::dump(getSoapString($fieldValue));
require_once("helperfunctions.php"); 

function getErrorMessage($exceptionMsg) {
	
	# loop thru array looking for error wihtin $exceptionMsg
	# If found
	#	Return a nice error message
	# ELSE
	#	Return a basic non specific error message
	
	/* To simplify data retrieval, we first map these errors into a 
         * dictionary where the key is the error and the value is the
         * pretty error message. 
    */
	$potentialConstraintErrors = array();
	$potentialConstraintErrors["ERR_ADDRESS_MUSTHAVEPRIMARY"] = "This will be the only address record for this constituent and must therefore be primary.  Be sure to place a check within the 'Primary' check box to indicate this is the constituent's primary address. ";
	$potentialConstraintErrors["BBERR_OLDADDRESSIDREQUIRED"] = "Old address' is required";
	$potentialConstraintErrors["BBERR_ADDRESS_DUPLICATENOTALLOWED"] = "The address you entered already exists for this constituent.";
	$potentialConstraintErrors["FK_ADDRESS_CONSTITUENTID"] = "There is not a constituent in the database as identified by the constituent id you provided.  Provide a valid constituent id for a constituent that exists in the system.";
	$potentialConstraintErrors["CK_ADDRESS_PRIMARYCOUNT"] = "There is already a primary address for this constituent.";
	$potentialConstraintErrors["CK_ADDRESS_VALIDSTARTDATEIFENDDATE"] = "Both 'Start Date' and 'End Date' are required for seasonal addresses.";
	$potentialConstraintErrors["CK_ADDRESS_VALIDENDDATEIFSTARTDATE"] = "Both 'Start Date' and 'End Date' are required for seasonal addresses.";
	//$pos1 = false;
	$returnErrorMsg = "There was an error saving the address." . " " . $exceptionMsg;
	while ($errorMsg = current($potentialConstraintErrors)) {
		//if ($pos1 !== false) {
		//debug::dump(key($potentialConstraintErrors));
			If (strpos($exceptionMsg, key($potentialConstraintErrors)) > 0) {
				$returnErrorMsg = $errorMsg;
				break;
			}
		//}
		next($potentialConstraintErrors);
	}	
	return $returnErrorMsg;
}

define("APPLICATION_ENV", "development"); //necessary for debug.php 

// access the BBEC web service through a SoapClient object
$sc = getSoapClient(); 
$clientAppInfo = getClientAppInfo();

$addressTypeCodeRows  = getCodeTableList('ADDRESSTYPECODE');
$infoSourceCodeRows = getCodeTableList('INFOSOURCECODE');

#USA	
$simpleDataListParams = array( array( 'ID' => "COUNTRYID", 'Value' => "d81cef85-7569-4b2e-8f2e-f7cf998a3342"));
													
/*	CANADA
 *   You can list the Canadian provinces by using the following param.
	$simpleDataListParams = array( array( 'ID' => "COUNTRYID", 'Value' => "d9ee54cd-2183-490c-a3ad-11152b271335"));							
 */					

$stateAbbrListRows = getSimpleDataList('7fa91401-596c-4f7c-936d-6e41683121d7',$simpleDataListParams);
$countryAbbrListRows = getSimpleDataList('c9649672-353d-42e8-8c25-4d34bbabfbba');

$formstate = "begin";
$formerror = "";
$constituentID = '';
$addressType = '';
$address  = '';;
$city = '';
$state = '';
$zip = '';
$doNotSend  = 0;
$primary  = 0;
$startDate = '';
$endDate = '';

if(isset($_GET["ConstituentID"])) {
	$constituentID = trim($_GET["ConstituentID"]);
}

if(isset($_POST['submit'])) {
	$formstate = "posted";
	$formerror = "";
	$constituentID = trim($_POST['formConstituentID']);

	if ($constituentID == '') {
		$formerror = "ConstituentIDmissing";
	}
    
	if (!preg_match('/^\{?[A-Z0-9]{8}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{12}\}?$/', $constituentID)) {
		$formerror = "ConstituentIDBadGuid";
	} 
	
	if(trim($_POST['formAddressType']) == ''){
		$formerror = "AddressTypeMissing";
	}
	
	if(trim($_POST['formAddress']) == ''){
		$formerror = "AddressMissingError";
	}
	
	#################################################################################
	#  Save attempted with errors Validation Errors.  Redisplay form and allow user to fix
	#################################################################################
	if (!$formerror ==""){
		$addressType = $_POST['formAddressType'];
		$address  = $_POST['formAddress'];
		$city = $_POST['formCity'];
		$state = $_POST['formState'];
		$zip = $_POST['formZip'];
		
		If (isset($_POST['formDoNotSend'])) {
			If ($_POST['formDoNotSend'] == 'on') {
				$doNotSend  = 1;
			}
			Else {
				$doNotSend  = 0;
			}		
		}
		Else {
			$doNotSend  = 0;
		}		
		
		If (isset($_POST['formPrimary'])) {
			If ($_POST['formPrimary'] == 'on') {
				$primary  = 1;
			}
			Else {
				$primary  = 0;
			}		
		}
		Else {
			$primary  = 0;
		}		
		
		$startDate = $_POST['formStartdate'];
		$endDate = $_POST['formEnddate'];
	}
	
	###########################
	#  Save the Address
	###########################
	else {
		// Make web service call to add data form.
		// Build the filter values 
		$filterValues=array();
		  
		$addressType = $_POST['formAddressType'];
		$filterValues[]= getStringFilter('ADDRESSTYPECODEID', $addressType);
		
		$address  = $_POST['formAddress'];
		$filterValues[]= getStringFilter('ADDRESSBLOCK', $address);
		
		if(trim($_POST['formCity']) != ''){
			$city = $_POST['formCity'];
			$filterValues[]= getStringFilter('CITY', $city);
		} 
		
		if(trim($_POST['formState']) != ''){
			$state = $_POST['formState'];
			$filterValues[]= getStringFilter('STATEID', $state);
		} 
			
		if(trim($_POST['formZip']) != ''){
			$zip = $_POST['formZip'];
			$filterValues[]= getStringFilter('POSTCODE', $zip);
		} 	
		
		If (isset($_POST['formDoNotSend'])) {
			If ($_POST['formDoNotSend'] == 'on') {
				$doNotSend  = 1;
			}
			Else {
				$doNotSend  = 0;
			}		
			$doNotSendValue = $_POST['formDoNotSend'];
		}
		Else {
			$doNotSend  = 0;
			$doNotSendValue = '';
		}		
		$filterValues[]= getBooleanFilter('DONOTMAIL', $doNotSendValue);
			
		If (isset($_POST['formPrimary'])) {
			If ($_POST['formPrimary'] == 'on') {
				$primary  = 1;
			}
			Else {
				$primary  = 0;
			}		
		
			$primaryValue = $_POST['formPrimary'];
		}
		Else {
			$primary  = 0;
			$primaryValue = '';
		}
		$filterValues[]= getBooleanFilter('PRIMARY', $primaryValue);
	
		if(trim($_POST['formStartdate']) != ''){
			$startDate = $_POST['formStartdate'];
			$unmaskedstartDate = str_replace("/","",$startDate);
			$filterValues[]= getStringFilter('STARTDATE', $unmaskedstartDate);
		} 
		
		if(trim($_POST['formEnddate']) != ''){	
			$endDate = $_POST['formEnddate'];
			$unmaskedendDate = str_replace("/","",$endDate);
			$filterValues[]= getStringFilter('ENDDATE', $unmaskedendDate);
		}
		
		$filterValues[]= getStringFilter('COUNTRYID', 'd81cef85-7569-4b2e-8f2e-f7cf998a3342');

		$dataformitem = new stdClass;
		$dataformitem->Values=new stdClass;
		$dataformitem->Values->fv=$filterValues;
		
		$fileuploadkey = guid();
		// build the parameters for the DataFormSave method 
		// (these correspond to properties of the DataFormSaveRequest type)
		
		$req = array(
			'ClientAppInfo' => $clientAppInfo,
			'FormID'        => 'c31a23ff-1913-4501-bd62-63474923d1bc',  	// the data form instance ID of the 'Address Add Form 2'
			'ContextRecordID'        => $constituentID, 
			'FileUploadKey' => $fileuploadkey, 
			'DataFormItem'	=> $dataformitem                             		// the DataFormItem containing the payload of form field values
			);
			
		try {
			// invoke the DataFormSave web service method, which returns a DataFormSaveReply object
			$reply = $sc->DataFormSave($req);
			$formstate = "savesuccess";
			
			// the ID of a newly saved address record in the system
			$recordId = $reply->ID; 
			$formerror = "";
			$addressType = '';
			$address  = '';;
			$city = '';
			$state = '';
			$zip = '';
			$doNotSend  = 0;
			$primary  = 0;
			$startDate = '';
			$endDate = '';
		}
			
		catch (SOAPFault $f) {
			$formstate = "saveerror";
			$faultstring = $f->faultstring;
			$errormsg = getErrorMessage($faultstring);
		}
	}
 }   
  ?>
  <!DOCTYPE html>
  <html lang="en">
	<head>
		<title>Address Add (USA)</title>
		<meta name="viewport" content="width=device-width, initial-scale=1.0">
		<link href="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/css/bootstrap-combined.min.css" rel="stylesheet">
		
	</head>
	<body>
		<div class="navbar">
			<div class="navbar-inner">
				<a class="brand" href="#">Add Data Form: Address Add Form 2</a>
			</div>
		</div>
		<div id="main" class="container">
			<form action="<?php echo htmlentities($_SERVER['PHP_SELF']); ?>" method="post" role="form" class="form-horizontal">	
					
					<?php if($formstate == "posted" and $formerror == "ConstituentIDmissing") : ?>
						<div class="control-group has-error">
							<label class="control-label" for="inputConstituentID" ><span class="label label-warning">Missing constituent id</span></label>
							<div class="controls">
								<input type="text" class="form-control" id="inputConstituentID" 
								name="formConstituentID" maxlength="36"  placeholder="Enter Constituent ID" 
								value="<?php echo $constituentID; ?>">
							</div>
						</div>
					<?php elseif($formstate == "posted" and $formerror == "ConstituentIDBadGuid") : ?>
						<div class="control-group has-error">
							<label class="control-label" for="inputConstituentID" ><span class="label label-warning">Not a valid guid</span></label>
							<div class="controls">
								<input type="text" class="form-control" id="inputConstituentID" 
								name="formConstituentID" maxlength="36"  placeholder="Enter Constituent ID" 
								value="<?php echo $constituentID; ?>">
							</div>
						</div>
					
					<?php else : ?>
						<div class="control-group">
							<label class="control-label" for="inputConstituentID" >Constituent ID:</label>
							<div class="controls">
								<input type="text" class="form-control" id="inputConstituentID" 
								name="formConstituentID" maxlength="36"  placeholder="Enter Constituent ID" 
								value="<?php echo $constituentID; ?>">
							</div>
						</div>
					<?php endif; ?>
					
					<?php  if($formstate == "posted" and $formerror == "AddressTypeMissing") : ?>
						<div class="control-group has-error">
							<label for="inputAddressType" class="control-label"><span class="label label-warning">Address type missing</span></label>
							<div class="controls">
								<select name="formAddressType" class="form-control" id="inputAddressType" >							
									<option value="">Select Address Type</option>
									<?php if (isset($addressTypeCodeRows) && is_array($addressTypeCodeRows)) : ?>
										<?php foreach ($addressTypeCodeRows as $row) : ?>
											<?php IF (trim($addressType) == trim($row->ID)) : ?>
													<option value="<?php echo $row->ID;?>" selected><?php echo $row->Code;?></option>
											<?php else : ?>
													<option value="<?php echo $row->ID;?>"><?php echo $row->Code;?></option>
											<?php endif; ?>
										<?php endforeach; ?>
									<?php endif; ?>
								</select>	
							</div>
						</div>
					<?php else : ?>
						<div class="control-group">
							<label for="inputAddressType" class="control-label">Address Type:</label>
							<div class="controls">
								<select name="formAddressType" class="form-control" id="inputAddressType" >							
									<option value="">Select Address Type</option>
									<?php if (isset($addressTypeCodeRows) && is_array($addressTypeCodeRows)) : ?>
										<?php foreach ($addressTypeCodeRows as $row) : ?>
											<?php IF (trim($addressType) == trim($row->ID)) : ?>
													<option value="<?php echo $row->ID;?>" selected><?php echo $row->Code;?></option>
											<?php else : ?>
													<option value="<?php echo $row->ID;?>"><?php echo $row->Code;?></option>
											<?php endif; ?>
										<?php endforeach; ?>
									<?php endif; ?>
								</select>	
							</div>
						</div>
					<?php endif; ?>
					
					
					<?php if($formstate == "posted" and $formerror == "AddressMissingError") : ?>
						<div class="control-group" >
							<label for="inputAddress" class="control-label"><span class="label label-warning">Address missing</span></label>
							<div class="controls">
								<input type="text" class="form-control" id="inputAddress" 
								name="formAddress" maxlength="150" placeholder="Enter address" 
								value="<?php echo $address; ?>">
							</div>
						</div>
					<?php else : ?>
						<div class="control-group" >
							<label for="inputAddress" class="control-label">Address:</label>
							<div class="controls">
								<input type="text" class="form-control" id="inputAddress" 
								name="formAddress" maxlength="150" placeholder="Enter address" 
								value="<?php echo $address; ?>">
							</div>
						</div>
					<?php endif; ?>
				
					<div class="control-group" >
						<label for="inputCity" class="control-label">City:</label>
						<div class="controls">
							<input type="text" class="form-control" id="inputCity" name="formCity" maxlength="50" 
							placeholder="Enter city" value="<?php echo $city; ?>">
						</div>
					</div>
					
					<div class="control-group">
						<label for="inputState" class="control-label">State:</label>
							<div class="controls">
								<select name="formState" class="form-control" id="inputState" >		
									<option value="">Select State</option>
									<?php if (isset($stateAbbrListRows) && is_array($stateAbbrListRows)) : ?>
										<?php foreach ($stateAbbrListRows as $row) : ?>
											<?php IF (trim($state) == trim($row->Value)) : ?>
												<option value="<?php echo $row->Value; ?>" selected><?php echo $row->Description; ?></option>
											<?php else : ?>
												<option value="<?php echo $row->Value; ?>"><?php echo $row->Description; ?></option>
											<?php endif; ?>
										<?php endforeach; ?>
									<?php endif; ?>
								</select>	
							</div>	
					</div>
	
					<div class="control-group" >
						<label for="inputZip" class="control-label">Zip:</label>
						<div class="controls">
							<input type="text" class="form-control" id="inputZip" name="formZip"  
							pattern="(\d{5}([\-]\d{4})?)" 
							maxlength="10" placeholder="Enter zip" 
							value="<?php echo $zip; ?>">
						</div>
					</div>
					
					<div class="control-group">
						<div class="controls">
							<label class="checkbox">
							<?php if ($doNotSend == 1) : ?>
								<input id="doNotSend" name="formDoNotSend" type="checkbox" checked> Do not send mail
							<?php else : ?>
								<input id="doNotSend" name="formDoNotSend" type="checkbox"> Do not send mail
							<?php endif; ?>
							</label>
						</div>
					</div>
					
					<div class="control-group">
						<div class="controls">
							<label class="checkbox">
							<?php if ($primary == 1) : ?>
								<input id="primary" name="formPrimary" type="checkbox" checked> Primary
							<?php else : ?>
								<input id="primary" name="formPrimary" type="checkbox"> Primary
							<?php endif; ?>
							</label>
						
						</div>
					</div>
					<!-- 
					<div class="control-group" >						
						<label for="inputStartdate" class="control-label">Start Date:</label>
						<div class="controls">
							<input type="text" class="form-control" id="inputStartdate" 
							name="formStartdate" 
							pattern="([0][1-9]|[1][0-2])[/]([0][1-9]|[1|2][0-9]|[3][0|1])" 
							maxlength="5" placeholder="MM/DD" 
							value="">
						</div>
					</div>
					
					<div class="control-group" >
						<label for="inputEnddate" class="control-label">End Date:</label>
						<div class="controls">
							<input type="text" class="form-control" id="inputEnddate" 
							name="formEnddate" pattern="([0][1-9]|[1][0-2])[/]([0][1-9]|[1|2][0-9]|[3][0|1])" 
							maxlength="5" placeholder="MM/DD" 
							value="">
						
						</div>
					</div>
					
					-->
					 <fieldset>
                        <legend>Seasonal Information</legend>
                        <div class="control-group">
                            <div id="seasonalstartdatetimepicker" class="input-append date">
                                <label for="inputStartdate" class="control-label">Start date:</label>
                                <div class="controls">
                                    <input type="text" name="formStartdate" id="inputStartdate" 
                                           class="form-control" placeholder="MM/DD" value="<?php if(isset($startDate)) { echo $startDate; } ?>"></input>
                                    <span class="add-on">
                                        <i data-date-icon="icon-calendar"></i>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="control-group">
                            <div id="seasonalenddatetimepicker" class="input-append date">
                                <label for="inputEnddate" class="control-label">End date:</label>
                                <div class="controls">
                                    <input type="text" name="formEnddate" id="inputEnddate" 
                                           class="form-control" placeholder="MM/DD" value="<?php if(isset($endDate)) { echo $endDate; } ?>"></input>
                                    <span class="add-on">
                                        <i data-date-icon="icon-calendar"></i>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </fieldset>
					<!---->
					
					

						<?php
						/*
						 if($posted==1 and $lastnamemissingerror == 1) { 
							echo "<div class=\"form-group has-error\">\n";
							echo 	"<label class=\"control-label\" for=\"inputLastName\"><span class=\"label label-warning\">Last Name Required</span></label>\n";
							echo 	"<input type=\"text\" class=\"form-control\" id=\"inputLastName\" name=\"formLastName\" maxlength=\"100\" placeholder=\"Enter last name\" value=\"\">\n";
							echo "</div>\n";
						}
						 else {
							echo "<div class=\"form-group\">\n";
							echo "<label for=\"inputMiddleName\" class=\"control-label\">Last:</label>\n";
							echo "<input type=\"text\" class=\"form-control\" id=\"inputLastName\" name=\"formLastName\" maxlength=\"100\" placeholder=\"Enter last name\" value=\"\">\n";
							echo "</div>\n";
						 }
						 */
						?>
						
					<div class="control-group">
						<div class="controls">
							<button type="submit" name="submit" class="btn btn-primary">Save</button>
						</div>
					</div>
					
					
					
					  <?php if($formstate == "posted") : ?>		
						<?php if(!$formerror =="") : ?>		
							<?php if ($formerror == "ConstituentIDmissing") : ?>	
								<div class="alert alert-warning"><strong>Warning!</strong> The constituent id  is required.</div>
							<?php elseif ($formerror == "ConstituentIDBadGuid"): ?>	
								<div class="alert alert-warning"><strong>Warning!</strong> The constituent id contains a bad guid.  Use a value that matches the following pattern: hhhhhhhh-hhhh-hhhh-hhhh-hhhhhhhhhhhh.</div>
							<?php elseif ($formerror == "AddressTypeMissing"): ?>	
								<div class="alert alert-warning"><strong>Warning!</strong> The address type is required.  Select an address type.</div>
							<?php elseif ($formerror == "AddressMissingError"): ?>	
								<div class="alert alert-warning"><strong>Warning!</strong> The address is required.  Enter an address.</div>
							<?php endif; ?>
						 <?php endif; ?>	
							
						<?php elseif ($formstate == "saveerror") : ?>	
							<div class="alert alert-danger"><strong>Error! </strong><?php echo $errormsg; ?> </div>
						<?php elseif ($formstate == "savesuccess") : ?>
							<div class="alert alert-success"><strong>Success! </strong> The newly added Address ID: <strong><?php echo $recordId; ?></strong>.  You can pass this value to any feature that shares a record type of 'Address'.</div>
						<?php endif; ?>
					
			</form>
		</div> 
		<script type="text/javascript"
                src="http://cdnjs.cloudflare.com/ajax/libs/jquery/1.8.3/jquery.min.js">
        </script>  
		<script type="text/javascript"  src="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/js/bootstrap.min.js">
		</script>
		<script src="http://www.google-analytics.com/urchin.js" type="text/javascript"></script>
		<script type="text/javascript"
                src="http://tarruda.github.com/bootstrap-datetimepicker/assets/js/bootstrap-datetimepicker.min.js">
        </script>
		<script type="text/javascript">
			$('#startdatetimepicker, #enddatetimepicker').datetimepicker({
                format: 'MM/dd/yyyy',
                language: 'en',
                pickTime: false
            });
			
			$('#seasonalstartdatetimepicker, #seasonalenddatetimepicker').datetimepicker({
                format: 'MM/dd',
                language: 'en',
                pickTime: false
            });
			
		</script>
		
	</body>
</html>		