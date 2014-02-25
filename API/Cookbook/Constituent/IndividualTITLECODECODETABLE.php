<?php
require_once("debug.php");  // ex: debug::dump(getSoapString($fieldValue));
require_once("helperfunctions.php"); 

define("APPLICATION_ENV", "development"); //necessary for debug.php

	// Code Table:  Title
	// Database Table Name: TITLECODE
	// System record ID:	456ffd4c-0fbf-49db-a503-0726f86e2a39
	// Description: This code table stores translations of title codes.  
	 
	// access the BBEC web service through a SoapClient object
    $sc = getSoapClient(); 
	$clientAppInfo = getClientAppInfo();
	  
	 
	try {
      // build the parameters for the CodeTableEntryGetList method 
	  //(these correspond to properties of the CodeTableEntryGetListRequest  type)
      $req = array(
          'ClientAppInfo'   => $clientAppInfo,
          'CodeTableName'   => 'TITLECODE', // the name of the code table in the db
          'IncludeInactive' => 'false',     // Include inactive (soft deletes) code table records
		  'ReturnListSortMethod' => 'true',     // return how the list is sorted (User Defined v. Alpha)
		  'UseCodeTableEntrySecurity' => 'false'     // 
      );
  
        // invoke the CodeTableEntryGetList web service method, 
		// which returns a CodeTableEntryGetListReply object
      $reply = $sc->CodeTableEntryGetList($req);
	    }
	catch (SOAPFault $f) {
		print $f;
	}
	
	  
	  $codetableRows=$reply->Rows->r;	
      
	  if (!empty($codetableRows) && !is_array($codetableRows)) {
		// This is a single row -- make it an array
		$codetableRows = array($codetableRows);
		}
		#debug::dump($codetableRows);
	 
  ?>
  <html>
	<head>
		<title>TITLECODE</title>
		<meta name="viewport" content="width=device-width, initial-scale=1.0">
		<link href="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/css/bootstrap-combined.min.css" rel="stylesheet">
		<script src="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/js/bootstrap.min.js"></script>
	</head>
	<body>
	<div class="navbar">
        <div class="navbar-inner">
            <a class="brand" href="#">Code Table: TITLECODE</a>
        </div>
    </div>
	<div id="main" class="container">
		<table class="table table-condensed">
			<tr style="background-color:#dbdbdb">
				<td >
					ID
				</td>
				<td>
					CODE
				</td>
			</tr>
			<?php
				
				if (!empty($codetableRows) && is_array($codetableRows)) {
					
					foreach ($codetableRows as $value) {
						echo "<tr>\n<td>\n$$value->ID\n</td>\n";
						echo "<td>\n$value->Code\n</td>\n";
						echo "\n</tr>\n";
						}
					}
					
			?>
		</table>
		<table class="table table-bordered">
			<tr >
				<td >
					Title:
				</td>
				<td >
					<select  name="title" class="input-medium" >
						<option value="" />
						<?php
							if (!empty($codetableRows) && is_array($codetableRows)) {
								foreach ($codetableRows as $row) {
									
									echo "<option value=\"$row->ID\">$row->Code</option>\n";
									}
								}
						?>
					</select>
				</td>
			</tr>
		</table>
	</body>
</html>					