<?php
	// Data Form: Address View Form 2
	// System record ID:	78f27fdf-6696-48cc-b6dc-85da47616c1b
	
	// TThis dataform template is used for viewing Address records.
	// This example shows how to use PHP to call the BBEC web service to fetch information about a phone record.
  
  require_once("debug.php");  // ex: debug::dump($fieldValue);
  require_once("helperfunctions.php"); 

	define("APPLICATION_ENV", "development"); //necessary for debug.php 
	$posted = 0;
	$recordIDmissingerror = 0;
	$formvalidationerror = 0;
	$recordIDBadGuid      = 0;
	$recordId = "";
	$loaderror = 0;
	
	$address = "";
	$country = "";
	$startDate = "";
	$endDate = "";
	$primary = 0;
	$doNotMail = 0;
	$doNotMailReasonCode = "";
	$isFormer = "";
	$isConfidential = 0;
	
	
	if(isset($_GET["AddressID"])) {
		$posted = 1;
		$recordId = trim($_GET["AddressID"]);
	}
	
	if(isset($_POST['submit'])) {
		$posted = 1;
		$recordId      = trim($_POST['addressID']);
	}

	if($posted==1)
	{ 
		if ($recordId == '') {
			$recordIDmissingerror = 1;
			$formvalidationerror  = 1;
		}
    
		if (preg_match('/^\{?[A-Z0-9]{8}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{12}\}?$/', $recordId)) {
			$recordIDBadGuid = 0;
		} else {
			$formvalidationerror = 1;
			$recordIDBadGuid     = 1;
		}
	
		
		if ($formvalidationerror == 0){
			// Make web service call to view data form.	
			
			// access the BBEC web service through a SoapClient object
			$sc = getSoapClient(); 
			$clientAppInfo = getClientAppInfo();
			
			// build the parameters for the DataFormLoad method (these correspond to properties of the DataFormLoadRequest type)
			$req = array(
				'ClientAppInfo'   => $clientAppInfo,
				'FormID'          => '78f27fdf-6696-48cc-b6dc-85da47616c1b',// the data form instance ID used to identify the 'Address View Form 2'
				'RecordID'        => $recordId                              // the ID of an address
				);
	  
			try {
			
				// invoke the DataFormLoad web service method, which returns a DataFormLoadReply object
				$reply = $sc->DataFormLoad($req);
				$loaderror = 0;
				
				$dataFormItem = $reply->DataFormItem;
				$fieldValues = $dataFormItem->Values->fv;
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
				
				$address = $mappedFieldValues["ADDRESS"];
				$country = $mappedFieldValues["COUNTRY"];
				$primary = $mappedFieldValues["PRIMARY"];
				$doNotMail = $mappedFieldValues["DONOTMAIL"];
				$doNotMailReasonCode = $mappedFieldValues["DONOTMAILREASONCODE"];
				$isFormer = $mappedFieldValues["ISFORMER"];
				$isConfidential = $mappedFieldValues["ISCONFIDENTIAL"];
				$startDate = $mappedFieldValues["STARTDATE"];
				$endDate = $mappedFieldValues["ENDDATE"];
				
				if (isset($startDate)) {
					$startDate = substr($startDate,0,2)."/".substr($startDate, 2);
				}
				if (isset($endDate)) {
					$endDate = substr($endDate,0,2)."/".substr($endDate, 2);
				}
				
				$historicalstartdate = make_display_date($mappedFieldValues["HISTORICALSTARTDATE"]);
				$historicalenddate = make_display_date($mappedFieldValues["HISTORICALENDDATE"]);
			
			}
			
			catch (SOAPFault $f) {
				$loaderror = 1;
				debug::dump(getSoapString($f->faultstring));
			}
		}
	}
  ?>
  <html>
	<head>
		<title>Address View</title>
		<meta name="viewport" content="width=device-width, initial-scale=1.0">
		
		<link href="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/css/bootstrap-combined.min.css" rel="stylesheet">
		
	</head>
	<body>
		<div class="navbar">
			<div class="navbar-inner">
				<a class="brand" href="#">View Data Form: Address View Form 2</a>
			</div>
		</div>
		<div id="main" class="container">
			<form action="<?php echo htmlentities($_SERVER['PHP_SELF']); ?>" class="form-inline" method="post" role="form" >	
				
				<div class="control-group">
					<label class="control-label" for="inputConstituentID" >Address ID:</label>
					<div class="controls">
						<input type="text" class="form-control" id="addressID" 
						name="addressID" maxlength="36"  placeholder="Enter Address ID" 
						value="<?php echo $recordId; ?>">
					</div>
				
					<button type="submit" name="submit" class="btn btn-primary">Submit</button>
				</div>
				

			</form>	
			
				<?php if($posted==1 and $formvalidationerror == 1)  : ?>
					<?php if ($recordIDmissingerror == 1) : ?>
						<div class="alert alert-warning"><strong>Warning!</strong> The address id field is required.</div>
					<?php elseif ($recordIDBadGuid == 1)  : ?>
						<div class="alert alert-warning"><strong>Warning!</strong> The address id field contains a bad guid.  Use a value that matches the following pattern: hhhhhhhh-hhhh-hhhh-hhhh-hhhhhhhhhhhh.</div>
					<?php endif; ?>
			
				<?php elseif ($posted == 1 and $loaderror == 1)  : ?>
					<div class="alert alert-danger"><strong>Error!</strong> There was a problem retrieving the data.</div>
					
				<?php elseif($posted==1 and $loaderror == 0) : ?>
					<div class="alert alert-success"><strong>Success!</strong> You retreived <strong><?php echo $address; ?></strong>.</div>
					
					<table class="table table-hover">
						<tr>
							<td>Address:</td>
							<td><?php echo $address; ?></td>
						</tr>
						<tr>
							<td>Country:</td>
							<td><?php echo $country; ?></td>
						</tr>
						<tr>
							<td>Seasonal Start Date:</td>
							<td><?php echo $startDate; ?></td>
						</tr>
						
						<tr>
							<td>Seasonal End Date:</td>
							<td><?php echo $endDate; ?></td>
						</tr>
						<tr>
                        <td>
                           Address Start and End Dates:
                        </td>
                        <td>
                            <?php
                            
                            if(isset($historicalstartdate) && !isset($historicalenddate)) {
                                $tojoin = array($historicalstartdate, "present");
                            } elseif (!isset($historicalstartdate) && isset($historicalenddate)) {
                                $tojoin = array("present", $historicalenddate);
                            } else {
                                $tojoin = array($historicalstartdate, $historicalenddate);
                            }
                            
                            $joined = join(" - ", $tojoin);
                            if($joined != " - ") {
                                echo $joined;
                            }
                            
                            ?>
                        </td>
                    </tr>
						
						<tr>
							<td>Primary?</td>
							<td>
								<?php if ($primary): ?>
									<span class='glyphicon icon-ok'></span>
								<?php else: ?>
									<span class='glyphicon icon-remove'></span>
								<?php endif; ?>
							</td>
						</tr>
						
						<tr>
							<td>Do not mail?</td>
							<td>
								<?php if ($doNotMail): ?>
									<span class='glyphicon icon-ok'></span>
									<?php if (isset($doNotMailReasonCode)): ?>
										<span><?php echo $doNotMailReasonCode; ?></span>
									<?php endif;?>
								<?php else: ?>
									&nbsp;
								<?php endif; ?>
							</td>
						</tr>
						<tr>
							<td>Former?</td>
							<td>
								<?php if ($isFormer): ?>
									<span class='glyphicon icon-ok'></span>
								<?php else: ?>
									<span class='glyphicon icon-remove'></span>
								<?php endif; ?>
							</td>
						</tr>
						
						<tr>
							<td>Confidential?</td>
							<td>
								<?php if ($isConfidential): ?>
									<span class='glyphicon icon-ok'></span>
								<?php else: ?>
									<span class='glyphicon icon-remove'></span>
								<?php endif; ?>
							</td>
						</tr>
						
					</table>
				<?php endif; ?>
		</div>
		<script type="text/javascript"  src="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/js/bootstrap.min.js">
		</script>
		
	</body>
</html>		