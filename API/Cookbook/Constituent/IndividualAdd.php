<?php
/*
*  Individual, Spouse, Business Add Form
*  System record ID:	1f9671b3-6740-447c-ad15-ef2718c0e43a	
*
*  This dataform template is used to add an individual constituent, spouse(optional), and business(optional).
*  This example shows how to use PHP to call the BBEC web service to add information about a constituent.
*/

require_once("debug.php");  // ex: debug::dump(getSoapString($fieldValue));
require_once("helperfunctions.php"); 

define("APPLICATION_ENV", "development"); //necessary for debug.php 

// access the BBEC web service through a SoapClient object
$sc = getSoapClient(); 
$clientAppInfo = getClientAppInfo();


$titlecodetableRows  = getCodeTableList('TITLECODE');
$suffixcodetableRows = getCodeTableList('SUFFIXCODE');

$posted = 0;
$lastnamemissingerror = 0;
$formvalidationerror = 0;

$lastName ='';
$firstName  ='';
$middleName ='';
$formBirthdate ='';
$formtitle = '';
$formsuffix = '';
$formgender =0;

if(isset($_POST['submit'])) { 
	$posted = 1;
		
	if(trim($_POST['formLastName']) == ''){
		$lastnamemissingerror = 1;
		$formvalidationerror = 1;
	}
	
	/*
	*  Save attempted with errors.  Redisplay form and allow user to fix
	*/
	if ($formvalidationerror == 1){
		$lastName = $_POST['formLastName'];
		$firstName = $_POST['formFirstName'];
		$middleName = $_POST['formMiddleName'];
		$formBirthdate = $_POST['formBirthdate'];
		$formtitle = $_POST['formtitle'];
		$formsuffix = $_POST['formsuffix'];
		$formgender = $_POST['formgender'];
	}
	
	/*
	*  Save the constituent
	*/
	elseif ($formvalidationerror == 0){
		// Make web service call to add data form.
		// Build the filter values 
		$filterValues=array();
		  
		$lastName = $_POST['formLastName'];
		$filterValues[]= getStringFilter('LASTNAME', $lastName);
			
		if(trim($_POST['formFirstName']) != ''){
			$firstName = $_POST['formFirstName'];
			$filterValues[]= getStringFilter('FIRSTNAME', $firstName);
		} 
		
		if(trim($_POST['formMiddleName']) != ''){
			$middleName = $_POST['formMiddleName'];
			$filterValues[]= getStringFilter('MIDDLENAME', $middleName);
		} 
			
		if(trim($_POST['formBirthdate']) != ''){
			$formBirthdate = $_POST['formBirthdate'];
			// reformat a date of mm/dd/yyyy  ex: 1/9/1967 to yyyymmdd  ex: 19670109. 
			// and don't forget the padded zeros for month and day.
			$datearray = date_parse($formBirthdate);
			$reformatdate = (string) $datearray['year'] . str_pad($datearray['month'],2,"0",STR_PAD_LEFT) . str_pad($datearray['day'],2,"0",STR_PAD_LEFT);
			$filterValues[]= getStringFilter('BIRTHDATE', $reformatdate);
		} 	
				
		if(trim($_POST['formtitle']) != ''){
			$formtitle = $_POST['formtitle'];
			$filterValues[]= getStringFilter('TITLECODEID', $formtitle);
		} 
		if(trim($_POST['formsuffix']) != ''){
			$formsuffix = $_POST['formsuffix'];
			$filterValues[]= getStringFilter('SUFFIXCODEID', $formsuffix);
		} 
		if(trim($_POST['formgender']) != '')
			$formgender = $_POST['formgender'];
			$filterValues[]= getStringFilter('GENDERCODE', $formgender);
		 

		$dataformitem = new stdClass;
		$dataformitem->Values=new stdClass;
		$dataformitem->Values->fv=$filterValues;
		
		$fileuploadkey = guid();
		// build the parameters for the DataFormSave method 
		// (these correspond to properties of the DataFormSaveRequest type)
		$req = array(
			'ClientAppInfo' => $clientAppInfo,
			'FormID'        => '1f9671b3-6740-447c-ad15-ef2718c0e43a',  	// the data form instance ID of the 'Individual, Spouse, Business Add Form'
			'FileUploadKey' => $fileuploadkey, 
			'DataFormItem'	=> $dataformitem                             		// the DataFormItem containing the payload of form field values
			);
			
		try {
			// invoke the DataFormSave web service method, which returns a DataFormSaveReply object
			$reply = $sc->DataFormSave($req);
			$saveerror = 0;	
			// the ID of a newly saved constituent in the system
			$recordId = $reply->ID; 
			
			$lastName ='';
			$firstName  ='';
			$middleName ='';
			$formBirthdate ='';
			$formtitle = '';
			$formsuffix = '';
			$formgender =0;
		}
			
		catch (SOAPFault $f) {
			$saveerror = 1;
			debug::dump(getSoapString($f));
		}
	}
}   
  ?>
  <!DOCTYPE html>
  <html lang="en">
	<head>
		<title>Individual Add</title>
		<meta name="viewport" content="width=device-width, initial-scale=1.0">
		<link href="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/css/bootstrap-combined.min.css" rel="stylesheet">
		<link rel="stylesheet" type="text/css" media="screen"
     href="http://tarruda.github.com/bootstrap-datetimepicker/assets/css/bootstrap-datetimepicker.min.css">
		
		
	</head>
	<body>
		<div class="navbar">
			<div class="navbar-inner">
				<a class="brand" href="#">Add Data Form: Individual, Spouse, Business Add Form</a>
			</div>
		</div>
		<div id="main" class="container">
			<form action="<?php echo htmlentities($_SERVER['PHP_SELF']); ?>" method="post" role="form" class="form-horizontal">	
				
					
					<div class="control-group">
						<label for="inputFirstName" class="control-label">First:</label>
						
						<div class="controls">
							<input type="text" class="form-control" id="inputFirstName" 
							name="formFirstName" maxlength="50" placeholder="Enter first name" value="<?php echo $firstName; ?>">
						</div>
					</div>
				
					<div class="control-group" >
						<label for="inputMiddleName" class="control-label">Middle:</label>
						<div class="controls">
							<input type="text" class="form-control" id="inputMiddleName" 
							name="formMiddleName" maxlength="50" placeholder="Enter middle name" value="<?php echo $middleName; ?>">
						</div>
					</div>
	
						
						<?php  if($posted==1 and $lastnamemissingerror == 1) : ?>
							<div class="control-group has-error">
								<label class="control-label" for="inputLastName"><span class="label label-warning">Last Name Required</span></label>
								<div class="controls">
									<input type="text" class="form-control" id="inputLastName" 
									name="formLastName" maxlength="100" placeholder="Enter last name" value="<?php echo $lastName; ?>">
								</div>
							</div>
						<?php else : ?>
							<div class="control-group">
								<label for="inputLastName" class="control-label">Last:</label>
								<div class="controls">
									<input type="text" class="form-control" id="inputLastName" 
									name="formLastName" maxlength="100" placeholder="Enter last name" value="<?php echo $lastName; ?>">
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
									<?php IF (trim($formtitle) == trim($row->ID)) : ?>
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
									
										<?php IF (trim($formsuffix) == trim($row->ID)) : ?>
												<option value="<?php echo $row->ID;?>" selected><?php echo $row->Code; ?> </option>
										<?php else : ?>
												<option value="<?php echo $row->ID;?>"><?php echo $row->Code; ?></option>
										<?php endif; ?>
									<?php endforeach; ?>
								<?php endif; ?>
							</select>
						</div>	
				</div>
					
					<div class="control-group">
						<div id="datetimepicker" class="input-append date">
							<label for="inputBirthdate" class="control-label">Birthdate:</label> 
							<div class="controls">
								<input type="text" name="formBirthdate" class="form-control" value="<?php echo $formBirthdate; ?>" readonly></input>
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
							<?php if ($formgender == 1) : ?>
								<option value="1" selected>Male</option>
							<?php else : ?>
								<option value="1">Male</option>
							<?php endif; ?>
						
							<?php if ($formgender == 2) : ?>
								<option value="2" selected>Female</option>
							<?php else : ?>
								<option value="2">Female</option>
							<?php endif; ?>
						
							<?php if ($formgender == 0) : ?>
								<option value="0" selected>Unknown</option>
							<?php else : ?>
								<option value="0">Unknown</option>
							<?php endif; ?>
						
							</select>
						</div>
					</div>
						
					<div class="control-group">
						<div class="controls">
							<button type="submit" name="submit" class="btn btn-primary">Save</button>
						</div>
					</div>
					
					 <?php
					 if($posted==1) { 
						if ($formvalidationerror == 1){
							echo "<div class=\"alert alert-warning\"><strong>Warning!</strong> The last name field is required.</div>";
						}
						elseif ($saveerror == 1) {
							echo "<div class=\"alert alert-danger\"><strong>Error!</strong> There was a problem saving the form.</div>";
						}
						
						elseif($saveerror == 0) {
							echo "<div class=\"alert alert-success\"><strong>Success!</strong> The newly added constituent ID: <strong>$recordId</strong>.  You can pass this value to any feature that shares a record type of 'Constituent'.</div>";
						}
					}
					?>
					
				
			</form>
		</div> 
		
		
		<script type="text/javascript"
			src="http://cdnjs.cloudflare.com/ajax/libs/jquery/1.8.3/jquery.min.js">
		</script>  
		
		
		<script type="text/javascript" 
			src="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/js/bootstrap.min.js">
		</script>
		
		<script type="text/javascript"
			src="http://tarruda.github.com/bootstrap-datetimepicker/assets/js/bootstrap-datetimepicker.min.js">
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