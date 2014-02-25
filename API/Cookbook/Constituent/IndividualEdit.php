<?php
// Data Form: 							Individual Biographical Edit Form 3
// Data Form System record Id: 			788ab947-26ed-40c4-865e-8fe29577e593
// This dataform template is used to edit biographical information for an individual.

require_once("debug.php"); // ex: debug::dump($someValue);
require_once("helperfunctions.php");

define("APPLICATION_ENV", "development"); //necessary for debug.php 

$lastnamemissingerror = 0;
$recordIDmissingerror = 0;
$recordIDBadGuid      = 0;

$formSavevalidationerror = 0;
$formLoadvalidationerror = 0;

$loaderror = 0;
$saveerror = 0;

$load     = 0;
$save     = 0;
$recordId = "";

if (isset($_GET["ConstituentID"])) {
    $load     = 1;
    $recordId = trim($_GET["ConstituentID"]);
}

if (isset($_POST['load'])) {
    $load     = 1;
    $recordId = trim($_POST['formconstituentID']);
}


if (isset($_POST['save'])) {
    $save     = 1;
    $recordId = trim($_POST['formconstituentID']);
}



if ($load == 1 or $save == 1) {
    $sc            = getSoapClient();
    $clientAppInfo = getClientAppInfo();
    
    if ($recordId == '') {
        $recordIDmissingerror    = 1;
        $formLoadvalidationerror = 1;
    }
    
    if (preg_match('/^\{?[A-Z0-9]{8}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{12}\}?$/', $recordId)) {
        $recordIDBadGuid = 0;
    } else {
        $formLoadvalidationerror = 1;
        $recordIDBadGuid         = 1;
    }
}

if ($save == 1) {
    if (trim($_POST['formLastName']) == '') {
        $lastnamemissingerror    = 1;
        $formSavevalidationerror = 1;
    }
}

#########################################
#   LOAD AND DISPLAY THE CONSTITUENT
#########################################
if ($load == 1 and $formLoadvalidationerror == 0) {  
    // build the parameters for the DataFormLoad method (these correspond to properties of the DataFormLoadRequest type)
    $req = array(
        'ClientAppInfo' => $clientAppInfo,
        'FormID' => '788ab947-26ed-40c4-865e-8fe29577e593', // the data form instance ID of the 'Individual Biographical View Form'
        'RecordID' => $recordId // the ID of a constituent
    );
    
    try {
        // invoke the DataFormLoad web service method, which returns a DataFormLoadReply object
        $reply     = $sc->DataFormLoad($req);
        $loaderror = 0;
    }
    
    catch (SOAPFault $f) {
        $loaderror = 1;
        debug::dump(getSoapString($f));
    }
    
    $dataFormItem = $reply->DataFormItem;
    $fieldValues  = $dataFormItem->Values->fv;
    
    $mappedFieldValues = array();
    foreach ($fieldValues as $fieldValue) {
		
		if(isset($fieldValue->Value)){
			$mappedFieldValues[$fieldValue->ID] = $fieldValue->Value;
		}
		else{
			$mappedFieldValues[$fieldValue->ID] = '';
		}
    }
    
    // pick off the desired info from the fields values returned by the web service
    $lastname   = $mappedFieldValues["LASTNAME"];
    $firstname  = $mappedFieldValues["FIRSTNAME"];
    $middlename = $mappedFieldValues["MIDDLENAME"];
    $maidenname = $mappedFieldValues["MAIDENNAME"];
    $nickname   = $mappedFieldValues["NICKNAME"];
    $titlecode  = $mappedFieldValues["TITLECODEID"]->enc_value;
    $suffixcode = $mappedFieldValues["SUFFIXCODEID"]->enc_value;
    $gender = (string) $mappedFieldValues["GENDERCODE"];
    
    //reformat date of yyyymmdd ex: 19670109 TO mm/dd/yyyy ex: 1/9/1967
    $birthdate = $mappedFieldValues["BIRTHDATE"];
    
    If ($birthdate == "00000000") {
        $reformatbirthdate = '';
    } Else {
        $reformatbirthdate = (string) substr($birthdate, 4, 2) . "/" . (string) substr($birthdate, 6, 2) . "/" . (string) substr($birthdate, 0, 4);
    }
    
    $age              = $mappedFieldValues["AGE"];
    $givesanonymously = $mappedFieldValues["GIVESANONYMOUSLY"];
    
    $titlecodetableRows  = getCodeTableList('TITLECODE');
    $suffixcodetableRows = getCodeTableList('SUFFIXCODE');
}

#################################################################################
#  Save attempted with errors.  Redisplay form and allow user to fix
#################################################################################
If ($save == 1 and $formSavevalidationerror == 1) {
    $lastname  = $_POST['formLastName'];
    $firstname = $_POST['formfirstname'];
    
    $middlename        = $_POST['formmiddlename'];
    $nickname          = $_POST['formNickName'];
    $maidenname        = $_POST['formMaidenName'];
    $reformatbirthdate = $_POST['formBirthdate'];
 
	$titlecodetableRows  = getCodeTableList('TITLECODE');
    $suffixcodetableRows = getCodeTableList('SUFFIXCODE');
	
    $titlecode  = $_POST['formtitle'];
    $suffixcode = $_POST['formsuffix'];
    
    $gender = $_POST['formgender'];
	$age    = $_POST['age'];
	
    
	If (isset($_POST['formgivesanonymously'])) {
		If ($_POST['formgivesanonymously'] == 'on') {
			$givesanonymously = 1;
		} else {
			$givesanonymously = 0;
		}
	}
	else {
		$givesanonymously = 0;
	}
	
    
	
}

###########################
#  Save THE CONSTITUENT
###########################
if ($save == 1 and $formSavevalidationerror == 0) {
    
    // Make web service call to the data form.
    // Build the filter values 

    $filterValues = array();
    
    $lastname                   = $_POST['formLastName'];
    $filterValues[] = getStringFilter('LASTNAME', $lastname);
    
    $firstname                  = $_POST['formfirstname'];
    $filterValues[] = getStringFilter('FIRSTNAME', $firstname);
    
    $middlename                 = $_POST['formmiddlename'];
    $filterValues[] = getStringFilter('MIDDLENAME', $middlename);
    
    $fulldisplayname = $firstname . " " . $lastname;
    
    $nickname                   = $_POST['formNickName'];
    $filterValues[] = getStringFilter('NICKNAME', $nickname);
    
    $maidenname                 = $_POST['formMaidenName'];
    $filterValues[] = getStringFilter('MAIDENNAME', $maidenname);
    
    if (trim($_POST['formBirthdate']) != '') {
        $birthdate = $_POST['formBirthdate'];
        
        //Sigh... have to reformat a date of mm/dd/yyyy  ex: 1/9/1967 to yyyymmdd  ex: 19670109. 
        //And don't forget the padded zeros for month and day.
        $datearray                  = date_parse($birthdate);
        $reformatdate               = (string) $datearray['year'] . str_pad($datearray['month'], 2, "0", STR_PAD_LEFT) . str_pad($datearray['day'], 2, "0", STR_PAD_LEFT);
        $filterValues[] = getStringFilter('BIRTHDATE', $reformatdate);
    } else {
        $filterValues[] = getStringFilter('BIRTHDATE', '');
    }
   
    if (trim($_POST['formtitle']) != '') {
        $title                      = $_POST['formtitle'];
        $filterValues[] = getStringFilter('TITLECODEID', $title);
    }
	
    if (trim($_POST['formsuffix']) != '') {
        $suffix                     = $_POST['formsuffix'];
        $filterValues[] = getStringFilter('SUFFIXCODEID', $suffix);
    }
    
    if (trim($_POST['formgender']) != '') {
        $gender                     = $_POST['formgender'];
    } else {
        $gender                     = "0";
    }
    $filterValues[] = getStringFilter('GENDERCODE', $gender);
	 
	If (isset($_POST['formgivesanonymously'])) {
		$givesanonymously = $_POST['formgivesanonymously'];
	}
	Else {
		$givesanonymously = '';
	}			
    $filterValues[] = getBooleanFilter('GIVESANONYMOUSLY', $givesanonymously);
    
	/*
     * Construct the Data Form Item that will be associated with our request
     * and assign it value array we created.
	 */
    $dataformitem             = new stdClass;
    $dataformitem->Values     = new stdClass;
    $dataformitem->Values->fv = $filterValues;
    
	/*
	 * FileUploadKey is a required field for DataFormSave, but is not 
	 * necessary for this form.
	 */
    $fileuploadkey = guid();
	
    /*
	 * Build the parameters for the DataFormLoad method.
	 * These correspond to properties of the DataFormLoadRequest type.
	 */
    $req = array(
        'ClientAppInfo' => $clientAppInfo,
        'FormID' => '788ab947-26ed-40c4-865e-8fe29577e593',
        'ID' => $recordId, // the ID of a constituent			
        'FileUploadKey' => $fileuploadkey,
        'DataFormItem' => $dataformitem // the DataFormItem containing the payload of form field values
    );
    
    try {
       /*
        * Invoke the DataFormSave web service method, which returns a DataFormSaveReply object
        */
        $reply     = $sc->DataFormSave($req);
        $saveerror = 0;
		// the ID of a newly saved constituent in the system
		$recordId = $reply->ID;
    }
    
    catch (SOAPFault $f) {
        $saveerror = 1;
        debug::dump($f);
    }
}

?>
 
  <!DOCTYPE html>
  <html lang="en">

	<head>
		<title>Individual Edit</title>
		<meta name="viewport" content="width=device-width, initial-scale=1.0">
				
		<link href="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/css/bootstrap-combined.min.css" rel="stylesheet">
		<link rel="stylesheet" type="text/css" media="screen"
     href="http://tarruda.github.com/bootstrap-datetimepicker/assets/css/bootstrap-datetimepicker.min.css">
		
	</head>
	<body>
		<div class="navbar">
			<div class="navbar-inner">
				<a class="brand" href="#">Edit Data Form: Individual Biographical Edit Form 3</a>
			</div>
		</div>
		<div id="main" class="container">
			<form action="<?php echo htmlentities($_SERVER['PHP_SELF']); ?>"  method="post" role="form" class="form-horizontal">	
				
				<div class="control-group">
					<label class="control-label" for="inputConstituentID" >Constituent ID:</label>
					<div class="controls">
						<input type="text" class="form-control" id="inputConstituentID" 
						name="formconstituentID" maxlength="36"  placeholder="Enter Constituent ID" value="<?php echo $recordId;?>">
					</div>
				</div>	
				<div class="control-group">
					<div class="controls">
						<button type="submit" name="load" class="btn btn-default">Submit</button>
					</div>	
				</div>	
				
				
				<div class="control-group">
					 <?php if ($load==1): ?>
						<?php if ($recordIDmissingerror == 1): ?>
								<div class="alert alert-warning"><strong>Warning!</strong> The constituent id field is required.</div>
						<?php elseif ($recordIDBadGuid == 1): ?>
								<div class="alert alert-warning"><strong>Warning!</strong> The constituent id field contains a bad guid.  Use a value that matches the following pattern: hhhhhhhh-hhhh-hhhh-hhhh-hhhhhhhhhhhh.</div>
						<?php elseif ($loaderror == 1): ?>
								<div class="alert alert-danger"><strong>Error!</strong> There was a problem loading the data.</div>";
						<?php endif; ?>
					<?php endif; ?>
				</div>
			
				<?php if (($load==1 and $loaderror == 0 and $formLoadvalidationerror == 0) or ($save==1 and $formSavevalidationerror == 1)) : ?>
					<div class="control-group" >
						<label for="inputfirstname" class="control-label">First:</label>  
						<div class="controls">
							<input type="text" class="form-control" id="inputfirstname" name="formfirstname" maxlength="50" placeholder="Enter first name" value="<?php echo $firstname;?>">
						</div>
					</div>
				
					<div class="control-group" >
						<label for="inputmiddlename" class="control-label">Middle:</label>
						<div class="controls">
							<input type="text" class="form-control" id="inputmiddlename" name="formmiddlename" maxlength="50" placeholder="Enter middle name" value="<?php echo $middlename;?>">
						</div>
					</div>
				
					<?php if ($save==1 and $lastnamemissingerror == 1) : ?>
						<div class="control-group has-error">
							<label class="control-label" for="inputLastName"><span class="label label-warning">Last Name Required</span></label>
							<div class="controls">
								<input type="text" class="form-control" id="inputLastName" 
								name="formLastName" maxlength="100" placeholder="Enter last name" value="<?php echo $lastname;?>">
							</div>
						</div>
					<?php else : ?>
						<div class="control-group">
							<label for="inputLastName" class="control-label">Last:</label>
							<div class="controls">
								<input type="text" class="form-control" id="inputLastName" 
								name="formLastName" maxlength="100" placeholder="Enter last name" value="<?php echo $lastname;?>">
							</div>
						</div>
					<?php endif; ?>
				
					<div class="control-group">
						<label for="inputTitle" class="control-label">Title:</label>
						<div class="controls">
							<select name="formtitle" class="form-control" id="inputTitle">
							<?php if (!empty($titlecodetableRows) && is_array($titlecodetableRows)) : ?>
								<option value="">Select Title</option>
								<?php foreach ($titlecodetableRows as $row) : ?>
									<?php IF ($titlecode === $row->ID) : ?>
											<option value="<?php echo $row->ID;?>" selected><?php echo $row->Code;?></option>
									<?php else : ?>
											<option value="<?php echo $row->ID;?>"><?php echo $row->Code;?></option>
									<?php endif; ?>
								<?php endforeach; ?>
							<?php endif; ?>
							</select>
						</div>
					</div>
				
					<div class="control-group">
						<label for="inputSuffix" class="control-label">Suffix:</label>
						<div class="controls">
							<select name="formsuffix" class="form-control" id="inputSuffix">
						
							<?php if (!empty($suffixcodetableRows) && is_array($suffixcodetableRows)) : ?>
									<option value="">Select Suffix</option>
								<?php foreach ($suffixcodetableRows as $row): ?>
								
									<?php IF (trim($suffixcode) == trim($row->ID)) : ?>
											<option value="<?php echo $row->ID;?>" selected><?php echo $row->Code; ?> </option>
									<?php else : ?>
											<option value="<?php echo $row->ID;?>"><?php echo $row->Code; ?></option>
									<?php endif; ?>
								<?php endforeach; ?>
							<?php endif; ?>
							</select>
						</div>
					</div>
			
					<div class="control-group" >
						<label for="inputNickName" class="control-label">Nick Name:</label>
						<div class="controls">
							<input type="text" class="form-control" id="inputNickName" name="formNickName" maxlength="50" placeholder="Enter nick name" value="<?php echo $nickname; ?>">
						</div>
					</div>
					
					<div class="control-group" >
						<label for="inputMaidenName" class="control-label">Maiden Name:</label>
						<div class="controls">
							<input type="text" class="form-control" id="inputMaidenName" name="formMaidenName" maxlength="100" placeholder="Enter maiden name" value="<?php echo $maidenname; ?>">
						</div>
					</div>
				
					<div class="control-group">
						<div id="datetimepicker" class="input-append date">
							<label for="inputBirthdate" class="control-label">Birthdate:</label>
							<div class="controls">
								<input type="text" name="formBirthdate" class="form-control" value="<?php echo $reformatbirthdate; ?>" readonly></input>
								<span class="add-on">
									<i data-date-icon="icon-calendar"></i>
								</span>
							</div>
						</div>
					</div>
					
					<div class="control-group">
						<label for="inputGender" class="control-label">Gender:</label>
						<div class="controls">
							<select  name="formgender" class="form-control" id="inputGender">
								<option value="">Select Gender</option>
							<?php if ($gender == 1) : ?>
								<option value="1" selected>Male</option>
							<?php else : ?>
								<option value="1">Male</option>
							<?php endif; ?>
						
							<?php if ($gender == 2) : ?>
								<option value="2" selected>Female</option>
							<?php else : ?>
								<option value="2">Female</option>
							<?php endif; ?>
						
							<?php if ($gender == 0) : ?>
								<option value="0" selected>Unknown</option>
							<?php else : ?>
								<option value="0">Unknown</option>
							<?php endif; ?>
						
							</select>
						</div>
					</div>
				
					<div class="control-group">
							<label for="age" class="control-label">Age:</label>
							<div class="controls">
								<input class="form-control" name="age" id="age" type="text" value="<?php echo $age;?>" readonly >
							</div>
					</div>
				
					<div class="control-group">
						<div class="controls">
							<label class="checkbox">
								
							<?php if (trim($givesanonymously) == 1) : ?>
									<input name="formgivesanonymously" id="formgivesanonymously" type="checkbox" checked> Gives anonymously
							<?php else : ?>
									<input name="formgivesanonymously" id="formgivesanonymously" type="checkbox"> Gives anonymously
							<?php endif; ?>
								
							</label>
						</div>
					</div>
		
				<?php endif; ?>

			
			<?php if (($load==1 and $loaderror == 0 and $formLoadvalidationerror == 0) or ($save==1 and $formSavevalidationerror == 1)) : ?>
				<div class="control-group">
					<div class="controls">
						<button type="submit" name="save" class="btn btn-primary">Save</button>
					</div>
				</div>	
			<?php endif; ?>

			</form>
		
			<?php if ($save==1) : ?>
				<?php if ($recordIDmissingerror == 1) : ?>
					<div class="alert alert-warning"><strong>Warning!</strong> The constituent id field is required.</div>
				<?php elseif  ($lastnamemissingerror == 1) : ?>
					<div class="alert alert-warning"><strong>Warning!</strong> The last name is required.</div>
				<?php elseif  ($saveerror == 1) : ?>
					<div class="alert alert-danger"><strong>Error!</strong> There was a problem saving the record.</div>
				<?php elseif  ($saveerror == 0) : ?>
					<div class="alert alert-success"><strong>Success!</strong> You saved the record for <strong><?php echo $fulldisplayname; ?></strong>.</div>
				<?php endif; ?>
			<?php endif; ?>
	
		</div>
		<script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/jquery/1.8.3/jquery.min.js">
		</script>  
		
		<script type="text/javascript"  src="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/js/bootstrap.min.js">
		</script>
		
		<script type="text/javascript" src="http://tarruda.github.com/bootstrap-datetimepicker/assets/js/bootstrap-datetimepicker.min.js">
		</script>
		
		<script type="text/javascript">
			$('#datetimepicker').datetimepicker({
				format: 'MM/dd/yyyy',
				language: 'en',
				pickTime: false
		
			});
		</script>
		
		<script src="http://www.google-analytics.com/urchin.js" type="text/javascript">
		</script>
		
	</body>
</html>		