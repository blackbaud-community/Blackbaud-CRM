<?php
	// Data List: 			Alternate Lookup ID List
	// System record ID:	6cda64a6-b408-4d99-98d3-74af7a32d488
	// This datalist returns all alternate lookup IDs for a constituent (ContextRecordID).
  
	require_once("debug.php");  // ex: debug::dump($somevalue);
	require_once("helperfunctions.php"); 
	
	define("APPLICATION_ENV", "development"); //necessary for debug.php 
	
	$posted = 0;
	$recordIDmissingerror = 0;
	$formvalidationerror = 0;
	$recordIDBadGuid      = 0;
	$recordIDNoData      = 0;

	if(isset($_POST['submit'])) 
	{ 
		$posted = 1;
		$recordId      = trim($_POST['formConstituentID']);
		
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
			
		If ($formvalidationerror == 0){
				
			$sc = getSoapClient(); 
			$clientAppInfo = getClientAppInfo();
		  
			// build the parameters for the DataFormLoad method (these correspond to properties of the DataFormLoadRequest type)
			$req = array(
				'ClientAppInfo'   	=> $clientAppInfo,
				'DataListID'        => '6cda64a6-b408-4d99-98d3-74af7a32d488' ,// the id for the Alternate Lookup ID List
				'ContextRecordID'   => $recordId,                              // the constituent (parent/context) ID for the child alternate lookup id(s).
				'MaxRows'			=> 30,
				'IncludeMetaData'   => 'true',
				'ViewFormID'		=> '00000000-0000-0000-0000-000000000000'
				);
	  
			try {
				// invoke the DataListLoad web service method, which returns a DataListLoadReply object
				$reply = $sc->DataListLoad($req);
				$loaderror = 0;
			}
			
			catch (SOAPFault $f) {
				$loaderror = 1;
				debug::dump(getSoapString($f));
			}
			
			//$alternateLookupIDList=$reply->Rows->r;	
		  		  
			//if (!empty($alternateLookupIDList) && !is_array($alternateLookupIDList)) {
				// This is a single row -- make it an array
			//	$alternateLookupIDList = array($alternateLookupIDList);
			//}
			
			if(isset($reply->Rows->r)){
				$alternateLookupIDList=$reply->Rows->r;	
				if (!empty($alternateLookupIDList) && !is_array($alternateLookupIDList)) {
					// This is a single row -- make it an array
					$alternateLookupIDList = array($alternateLookupIDList);
				}	
			}
			else {
				$alternateLookupIDList = array();
				$recordIDNoData      = 1;
			}
			
			
			
			
		}
	}
  ?>
  <html>
	<head>
		<title>Alternate Lookup IDs</title>
		<meta name="viewport" content="width=device-width, initial-scale=1.0">
		<link href="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/css/bootstrap-combined.min.css" rel="stylesheet">
		<script src="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/js/bootstrap.min.js"></script>
	</head>
	<body>
		<div class="navbar">
			<div class="navbar-inner">
				<a class="brand" href="#">Data List: Alternate Lookup ID List</a>
			</div>
		</div>
		<div id="main" class="container">
			<form action="<?php echo htmlentities($_SERVER['PHP_SELF']); ?>" class="form-inline" method="post" role="form">	
				
				<div class="form-group">
					<label class="sr-only" for="inputConstituentID" >Constituent ID:</label>
					<input type="text" class="form-control" id="inputConstituentID" 
					name="formConstituentID" maxlength="36"  placeholder="Enter Constituent ID" value="0030D1BA-4A49-44AB-A44C-F9E294CDE8C0">
					<button type="submit" name="submit" class="btn btn-default">Submit</button>
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
					
			
				elseif ($posted == 1 and $recordIDNoData == 1) {
					echo "<div class=\"alert alert-warning\"><strong>Warning!</strong> No lookup IDs for the provided constituent ID.</div>";
					}		
				elseif ($posted == 1 and $loaderror == 1) {
					echo "<div class=\"alert alert-danger\"><strong>Error!</strong> There was a problem retrieving the data.</div>";
					}	
				elseif($posted == 1 and $loaderror == 0) {
					echo "<div class=\"alert alert-success\"><strong>Success!</strong> You retreived the data from the data list.</div>";
					echo "<table class=\"table table-hover\">\n";
					echo	"<tr>\n";
					echo		"<td><strong>\n";
					echo			"ID";
					echo		"</strong></td>\n";
					echo		"<td><strong>\n";
					echo		"Type";
					echo		"</strong></td>\n";
					echo		"<td><strong>\n";
					echo		"Lookup ID";
					echo		"</strong></td>\n";					
					echo	"</tr>\n"; 	
					
					foreach ($alternateLookupIDList as $row) {						
						$id = $row->Values->v[0];
						$type = $row->Values->v[1];
						$lookupID = $row->Values->v[2];
						
						echo "<tr>\n";
						echo	"<td>$id</td>\n";
						echo	"<td>$type</td>\n";
						echo	"<td>$lookupID</td>\n";					
						echo "</tr>\n"; 	
					}	
					echo "</table>";	 
					}
				?> 
			
		</div>
	</body>
</html>		