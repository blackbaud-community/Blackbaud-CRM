<?php
	// Record Operation: 					Constituent Delete
	// Record Operation System record Id: 	569202fa-082a-4203-a2ae-91843e7c08ca
	
	// Data Form:							Individual Biographical View Form
	// Data Form System record ID:			48cefdc3-a719-4fef-bd61-3108a5971d2b
	
	// This view dataform template displays individual biographical information.
	// This example shows how to use PHP to call the BBEC web service to fetch information about a constituent. 
  
	require_once("debug.php");  // ex: debug::dump(getSoapString($fieldValue));
	require_once("helperfunctions.php"); 
	
	define("APPLICATION_ENV", "development"); //necessary for debug.php 
	
	$recordId = "";
	
	$submitposted = 0;
	$deleteposted = 0;
	
	$recordIDmissingerror = 0;
	$formvalidationerror = 0;
	
	$deleteerror = 0;

	if(isset($_POST['submit'])) {
		$submitposted = 1;
		$deleteposted = 0;
	}
	
	if(isset($_POST['delete'])) {
		$deleteposted = 1;
		$submitposted = 0;
	}
	
	############################
	#  DISPLAY THE CONSTITUENT
	############################
	if($submitposted == 1) 
	{ 
		if(trim($_POST['formConstituentID']) == ''){
			$recordIDmissingerror = 1;
			$formvalidationerror = 1;
		}
	
		if ($formvalidationerror == 0){
			// Make web service call to view data form.	
			// the ID of a constituent in the system
			$recordId = $_POST['formConstituentID'];
			
			$sc = getSoapClient(); 
			$clientAppInfo = getClientAppInfo();
	  
			// build the parameters for the DataFormLoad method (these correspond to properties of the DataFormLoadRequest type)
			$req = array(
				'ClientAppInfo'   => $clientAppInfo,
				'FormID'          => '48cefdc3-a719-4fef-bd61-3108a5971d2b',// the data form instance ID of the 'Individual Biographical View Form'
				'RecordID'        => $recordId                              // the ID of a constituent
				);
	  
			try {
				// invoke the DataFormLoad web service method, which returns a DataFormLoadReply object
				$reply = $sc->DataFormLoad($req);
				$saveerror = 0;
			}
			
			catch (SOAPFault $f) {
				$saveerror = 1;
				debug::dump(getSoapString($f));
			}
			
			$dataFormItem = $reply->DataFormItem;
			$fieldValues = $dataFormItem->Values->fv;
			#debug::dump($fieldValues);
    
			// pick off the desired info from the fields values returned by the web service
			$titlecode = $fieldValues[0]->Value;
			$suffixcode = $fieldValues[2]->Value;
			$nickname = $fieldValues[4]->Value;
			$gender = $fieldValues[6]->Value;
			$birthdate = $fieldValues[7]->Value;
			
			$age = $fieldValues[8]->Value;
			$webaddress = $fieldValues[12]->Value;
			$married = $fieldValues[13]->Value;
			
			$keyname = $fieldValues[15]->Value;
			$firstname = $fieldValues[16]->Value;
			$middlename = $fieldValues[17]->Value;
		}
	}
	
	###########################
	#  DELETE THE CONSTITUENT
	###########################
	if($deleteposted == 1) 
	{ 
		if(trim($_POST['formConstituentID']) == ''){
			$recordIDmissingerror = 1;
			$formvalidationerror = 1;
		}
		
		if ($formvalidationerror == 0){
			// Make web service call to RecordOperationPerform to delete constituent.	
			// the ID of a constituent in the system
			
			$recordId = $_POST['formConstituentID'];
			$formkeyname = $_POST['formKeyname'];
			$sc = getSoapClient(); 
			$clientAppInfo = getClientAppInfo();
	  
			// build the parameters for the DataFormLoad method (these correspond to properties of the DataFormLoadRequest type)
			$req = array(
				'ClientAppInfo'   	=> $clientAppInfo,
				'RecordOperationID' => '569202fa-082a-4203-a2ae-91843e7c08ca',  // the ID of the 'Constituent Delete' Record Operation
				'ID'        		=> $recordId,       // the ID of a constituent
				'DeferredResultKey' => '00000000-0000-0000-0000-000000000000'
				);
	  
			try {
				// invoke the DataFormLoad web service method, which returns a DataFormLoadReply object
				$reply = $sc->RecordOperationPerform($req);
				$saveerror = 0;
			}
			
			catch (SOAPFault $f) {
				$saveerror = 1;
				debug::dump(getSoapString($f));
			}
		} 
	}	
  ?>
  <html>
	<head>
		<title>Constituent Delete</title>
		<meta name="viewport" content="width=device-width, initial-scale=1.0">
		<link href="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/css/bootstrap-combined.min.css" rel="stylesheet">
		<script src="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/js/bootstrap.min.js"></script>
	</head>
	<body>
		<div class="navbar">
			<div class="navbar-inner">
				<a class="brand" href="#">Record Operation: Constituent Delete</a>
			</div>
		</div>
		<div id="main" class="container">
			<form action="<?php echo htmlentities($_SERVER['PHP_SELF']); ?>" class="form-inline" method="post" role="form">	
				<div class="form-group">
					<label class="sr-only" for="inputConstituentID" >Constituent ID:</label>
					<input type="text" class="form-control" id="inputConstituentID" 
					name="formConstituentID" maxlength="36"  placeholder="Enter Constituent ID" value="<?php echo $recordId;?>">
					<button type="submit" name="submit" class="btn btn-default">Submit</button>
					<input type="hidden" class="form-control" id="inputKeyname" 
					name="formKeyname" maxlength="36"  value="<?php echo $keyname;?>">
				
					<?php
					if($submitposted==1 and $formvalidationerror == 0 and $saveerror == 0) { 
						echo "<button type=\"submit\" name=\"delete\" class=\"btn btn-danger\">Delete</button>";
					}
					?>	
				</div>
			</form>	
				<?php
				if($deleteposted==1) { 
					if ($recordIDmissingerror == 1){
						echo "<div class=\"alert alert-warning\"><strong>Warning!</strong> The constituent id field is required.</div>";
					}
					elseif ($saveerror == 1) {
						echo "<div class=\"alert alert-danger\"><strong>Error!</strong> There was a problem deleting the data.</div>";
					}	
					elseif($saveerror == 0) {
						echo "<div class=\"alert alert-success\"><strong>Success!</strong> You deleted <strong>$formkeyname</strong>.</div>";
					}
				}
				
				if($submitposted==1) { 
					if ($recordIDmissingerror == 1){
						echo "<div class=\"alert alert-warning\"><strong>Warning!</strong> The constituent id field is required.</div>";
					}
					elseif ($saveerror == 1) {
						echo "<div class=\"alert alert-danger\"><strong>Error!</strong> There was a problem retrieving the data.</div>";
					}	
					elseif($saveerror == 0) {
						echo "<div class=\"alert alert-success\"><strong>Success!</strong> You retreived <strong>$keyname</strong>.</div>";
					}
				
					if($submitposted==1 and $saveerror == 0) { 
						echo "<table class=\"table table-hover\">\n";
						echo	"<tr>\n";
						echo		"<td>\n";
						echo			"Title code:";
						echo		"</td>\n";
						echo		"<td>$titlecode</td>\n";
						echo	"</tr>\n";
						echo	"<tr>\n";
						echo		"<td>\n";
						echo		"First name:";
						echo		"</td>\n";
						echo		"<td>$firstname</td>\n";
						echo	"</tr>\n";
						echo	"<tr>\n";
						echo		"<td>\n";
						echo			"Middle name:";
						echo		"</td>\n";
						echo		"<td>$middlename</td>\n";
						echo	"</tr>\n";
						
						echo	"<tr>\n";
						echo		"<td>\n";
						echo			"Last name/Key name:";
						echo		"</td>\n";
						echo		"<td>$keyname </td>\n";
						echo	"</tr>\n";
						
						echo	"<tr>\n";
						echo		"<td>\n";
						echo			"Suffix code:";
						echo		"</td>\n";
						echo		"<td>$suffixcode</td>\n";
						echo	"</tr>\n";
						
						echo	"<tr>\n";
						echo		"<td>\n";
						echo			"Nickname:";
						echo		"</td>\n";
						echo		"<td>$nickname</td>\n";
						echo	"</tr>\n";
						
						echo	"<tr>\n";
						echo		"<td>\n";
						echo			"Gender:";
						echo		"</td>\n";
						echo		"<td>$gender</td>\n";
						echo	"</tr>\n";
						
						echo	"<tr>\n";
						echo		"<td>\n";
						echo			"Birthdate:";
						echo		"</td>\n";
						echo		"<td>$birthdate</td>\n";
						echo	"</tr>\n";
						
						echo	"<tr>\n";
						echo		"<td>\n";
						echo			"Age:";
						echo		"</td>\n";
						echo		"<td>$age</td>\n";
						echo	"</tr>\n";
						
						echo	"<tr>\n";
						echo		"<td>\n";
						echo			"Web address:";
						echo		"</td>\n";
						echo		"<td>$webaddress</td>\n";
						echo	"</tr>\n";
						
						echo	"<tr>\n";
						echo		"<td>\n";
						echo			"Married:";
						echo		"</td>\n";
						echo		"<td>$married</td>\n";
						echo	"</tr>\n";
						
						echo "</table>";	 
					}
				}
				?> 
		</div>
	</body>
</html>		