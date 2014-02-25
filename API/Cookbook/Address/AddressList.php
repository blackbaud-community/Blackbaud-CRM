<?php
	// Data List: Contact Information Address List
	// System record ID:	e04c7070-e928-4646-859b-2e2c22c84b9d
	// This datalist returns all address contact information for a constituent.
  
	require_once("debug.php");  // ex: debug::dump($somevalue);
	require_once("helperfunctions.php"); 
	
	define("APPLICATION_ENV", "development"); //necessary for debug.php 
	
	$posted = 0;
	$recordIDmissingerror = 0;
	$formvalidationerror = 0;
	$recordIDBadGuid      = 0;
	$recordId = "";
	$formIncludeFormer = "";
	
    $filterValues = array();
	
	if(isset($_GET["ConstituentID"])) {
		$posted = 1;
		$recordId = trim($_GET["ConstituentID"]);
	}
	
	if(isset($_POST['submit'])) {
		$posted = 1;
		$recordId      = trim($_POST['formConstituentID']);
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
			
		If (isset($_POST['formIncludeFormer'])) {
			$formIncludeFormer = $_POST['formIncludeFormer'];	
			$filterValues[] = getBooleanFilter('INCLUDEFORMER', $formIncludeFormer);
		}
		else {
			$formIncludeFormer = "";
		}
    
		$parameters             = new stdClass;
		$parameters->Values     = new stdClass;
		$parameters->Values->fv = $filterValues;
		
		If ($formvalidationerror == 0){
				
			$sc = getSoapClient(); 
			$clientAppInfo = getClientAppInfo();
		  
			// build the parameters for the DataListLoad method (these correspond to properties of the DataListLoadRequest type)
			$req = array(
				'ClientAppInfo'   	=> $clientAppInfo,
				'DataListID'        => 'e04c7070-e928-4646-859b-2e2c22c84b9d' ,// the id for the Contact Information Address List
				'ContextRecordID'   => $recordId,  				// the constituent (parent/context) ID for the child addresses
				'Parameters'		=> $parameters,	            // the parameters represent the optional filter values for the data list.
				'MaxRows'			=> 30,
				'IncludeMetaData'   => 'true',
				'ViewFormID'		=> '00000000-0000-0000-0000-000000000000'
				);
	  
			try {
				// invoke the DataListLoad web service method, which returns a DataListLoadReply object
				$reply = $sc->DataListLoad($req);
	
				$loaderror = 0;			
				$includedRecordIndex = $reply->IncludedRecordIndex;
				$startRowIndex = $reply->StartRowIndex;
				$totalAvailableRows = $reply->TotalAvailableRows;
				$totalRowsRequested = $reply->TotalRowsRequested;
				$totalRowsInReply = $reply->TotalRowsInReply;
				$hadMoreRows = $reply->HadMoreRows;
					  			
				if(isset($reply->Rows->r)){
					$addressList=$reply->Rows->r;	
					if (!empty($addressList) && !is_array($addressList)) {
						// This is a single row -- make it an array
						$addressList = array($addressList);
					}	
				}
				else {
					$addressList = array();
				}
			}
			
			catch (SOAPFault $f) {
				$loaderror = 1;
				debug::dump(getSoapString($f));
			}
			
		}
	}
  ?>
  <html>
	<head>
		<title>Addresses</title>
		<meta name="viewport" content="width=device-width, initial-scale=1.0">
		
		<!-- Latest compiled and minified CSS -->
		<link rel="stylesheet" href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap.min.css">

		<!-- Optional theme -->
		<link rel="stylesheet" href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap-theme.min.css">

		
	</head>
	<body>
		<div class="navbar">
			<div class="navbar-inner">
				<a class="brand" href="#">Data List: Contact Information Address List</a>
			</div>
		</div>
		
		<div id="main" class="container">
			<form action="<?php echo htmlentities($_SERVER['PHP_SELF']); ?>" class="form-inline" method="post" role="form">	
				
				<div class="form-group">
					<label class="sr-only" for="inputConstituentID" >Constituent ID:</label>
				
						<input type="text" class="form-control" id="inputConstituentID" 
						name="formConstituentID" maxlength="36"  placeholder="Enter Constituent ID" value="<?php echo $recordId; ?>">
					
						<button type="submit" name="submit" class="btn btn-default">Submit</button>
					
						<div class="checkbox">
					<label>
					<?php
					
					
					IF (trim($formIncludeFormer) == "on") {
						echo "<input name=\"formIncludeFormer\" type=\"checkbox\" checked> Include former</input>";
					} else {
						echo "<input name=\"formIncludeFormer\" type=\"checkbox\"> Include former</input>";
					}
					?>
						</label>
					</div>
					
				</div>
				
			</form>	
				<?php
	
				if($posted==1 and $formvalidationerror == 1) { 		
					if ($recordIDmissingerror == 1) {
						echo "<div class=\"alert alert-warning\"><strong>Warning!</strong> The constituent id field is required.</div>";
					} 
					elseif ($recordIDBadGuid == 1) {
						echo "<div class=\"alert alert-warning\"><strong>Warning!</strong> The constituent id field contains a bad guid.  Use a value that matches the following pattern: hhhhhhhh-hhhh-hhhh-hhhh-hhhhhhhhhhhh.</div>";
					}
				}	
					
				elseif ($posted == 1 and $loaderror == 1) {
					echo "<div class=\"alert alert-danger\"><strong>Error!</strong> There was a problem retrieving the data.</div>";
					}	
				elseif($posted == 1 and $loaderror == 0) {
					echo "<div class=\"panel-group\" id=\"accordion\">\n";
					echo 	"<div class=\"panel panel-default\">\n";  
					echo 		"<div class=\"panel-heading\">\n";
					echo			"<h4 class=\"panel-title\">\n";
					echo 				"<div class=\"pull-right\">\n";
					echo					"<a class=\"accordion-toggle\" data-toggle=\"collapse\" data-parent=\"#accordion\" href=\"#collapseOne\">\n";
				
					echo								"<span class=\"indicator glyphicon glyphicon-chevron-up\"></span>\n";
				
					echo					"</a>\n";
				
					echo 				"</div>\n";
					echo						"Total Addresses: <strong>$totalAvailableRows</strong>\n";
					echo 			"</h4>\n";
					echo 		"</div>\n";
					echo		"<div id=\"collapseOne\" class=\"panel-collapse collapse in\">\n";  // Panel Begin
					
					echo 			"<table class=\"table table-hover\">\n";
					echo				"<tr>\n";
					echo					"<th>\n";
					echo						"ID";
					echo					"</th>\n";
					echo					"<th>\n";
					echo						"Contact Info";
					echo					"</th>\n";
					echo					"<th>\n";
					echo						"Type";
					echo					"</th>\n";					
					
					echo					"<th>\n";
					echo						"Is Primary";
					echo					"</th>\n";					
					
					echo					"<th>\n";
					echo						"Do Not Contact";
					echo					"</th>\n";					
					
					echo					"<th>\n";
					echo						"Start Date";
					echo					"</th>\n";					
					
					echo					"<th>\n";
					echo						"End Date";
					echo					"</th>\n";			
					
					echo				"</tr>\n"; 	
					
					foreach ($addressList as $row) {						
						$id = $row->Values->v[0];
						$contactInfo = $row->Values->v[1];
						$type = $row->Values->v[2];
						$isPrimary = $row->Values->v[3];
						$doNotContact = $row->Values->v[4];
						
						$startDate = $row->Values->v[7];
    
						If ($startDate != "") {
							$startDate = (string) substr($startDate, 5, 2) . "/" . (string) substr($startDate, 8, 2) . "/" . (string) substr($startDate, 0, 4);
						} 
						
						$endDate = $row->Values->v[8];
						If ($endDate != "") {
							$endDate = (string) substr($endDate, 5, 2) . "/" . (string) substr($endDate, 8, 2) . "/" . (string) substr($endDate, 0, 4);
						} 
						
						if ($isPrimary == "Yes"){
							echo 		"<tr class=\"success\">\n";
						}
						elseif ($doNotContact == "Do not mail"){
							echo 		"<tr class=\"warning\">\n";
						}
						else {
							echo 		"<tr>\n";
						}
						
						echo				"<td>$id</td>\n";
						echo				"<td>$contactInfo</td>\n";
						echo				"<td>$type</td>\n";					
						
					 	if ($isPrimary == "Yes"){				
							echo 			"<td>&nbsp;&nbsp;\n<i class=\"glyphicon glyphicon-ok\"></i></td>\n";
						}
						else {
							echo			"<td></td>\n";
						}
						
						if ($doNotContact == "Do not mail"){
							echo 			"<td><span class=\"label label-warning\">&nbsp;$doNotContact&nbsp;</span></td>\n";
						}
						else {
							echo			"<td></td>\n";
						}
														
						echo				"<td>$startDate</td>\n";
						echo				"<td>$endDate</td>\n";
						echo 			"</tr>\n"; 	
					}	
					echo 			"</table>";
					echo 		"</div>\n";		 // Panel End
					echo 	"</div>\n";
					echo "</div>\n";
					echo "</div>";
					}
				?> 
			
		</div>
		<script type="text/javascript"
			src="http://cdnjs.cloudflare.com/ajax/libs/jquery/1.8.3/jquery.min.js">
		</script>  
		<!-- Latest compiled and minified JavaScript -->
		<script src="//netdna.bootstrapcdn.com/bootstrap/3.0.0/js/bootstrap.min.js"></script>
		
		<script>
		
			$('#accordion .accordion-toggle').click(function (e){
				 $(this).find('.indicator').toggleClass('glyphicon-chevron-down glyphicon-chevron-up');
			}); 
			
			
		</script>  
		
	</body>
</html>		