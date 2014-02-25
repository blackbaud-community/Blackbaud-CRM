<?php
// Data Form: 							Address Edit Form 4
// Data Form System record Id: 			9bef22ab-496f-48fd-98fe-5be72270ef30
// This dataform template is used to edit an address.

require_once("debug.php"); // ex: debug::dump($someValue);
require_once("helperfunctions.php");
 
//function getErrorMessage($exceptionMsg)
//{
    
    // loop thru array looking for error wihtin $exceptionMsg
    // If found
    //	Return a nice error message
    // ELSE
    //	Return a basic non specific error message
    
    /* To simplify data retrieval, we first map these errors into a 
     * dictionary where the key is the error and the value is the
     * pretty error message. 
     */
 //   $potentialConstraintErrors                                       = array();
  //  $potentialConstraintErrors["ERR_ADDRESS_MUSTHAVEPRIMARY"]        = "This will be the only address record for this constituent and must therefore be primary.  Be sure to place a check within the 'Primary' check box to indicate this is the constituent's primary address. ";
  //  $potentialConstraintErrors["BBERR_OLDADDRESSIDREQUIRED"]         = "Old address' is required";
  //  $potentialConstraintErrors["BBERR_ADDRESS_DUPLICATENOTALLOWED"]  = "The address you entered already exists for this constituent.";
  //  $potentialConstraintErrors["FK_ADDRESS_CONSTITUENTID"]           = "There is not a constituent in the database as identified by the constituent id you provided.  Provide a valid constituent id for a constituent that exists in the system.";
  //  $potentialConstraintErrors["CK_ADDRESS_PRIMARYCOUNT"]            = "There is already a primary address for this constituent.";
  //  $potentialConstraintErrors["CK_ADDRESS_VALIDSTARTDATEIFENDDATE"] = "Both 'Start Date' and 'End Date' are required for seasonal addresses.";
  //  $potentialConstraintErrors["CK_ADDRESS_VALIDENDDATEIFSTARTDATE"] = "Both 'Start Date' and 'End Date' are required for seasonal addresses.";
    //$pos1 = false;
  //  $returnErrorMsg                                                  = "There was an error saving the address." . " " . $exceptionMsg;
  //  while ($errorMsg = current($potentialConstraintErrors)) {
        
  //      If (strpos($exceptionMsg, key($potentialConstraintErrors)) > 0) {
  //          $returnErrorMsg = $errorMsg;
  //          break;
  //      }
       
  //     next($potentialConstraintErrors);
  //  }
  //  return $returnErrorMsg;
//}

define("APPLICATION_ENV", "development"); //necessary for debug.php 


//$recordIDmissingerror    = 0;
//$recordIDBadGuid         = 0;
$formSearchValidationError = 0;
$formSelectValidationError = 0;

$searchError    = "";
$selectError    = "";

$search         = 0;
$select         = 0;
$constituentID  = "";
$keyname        = "";
$lookupid       = "";
$address        = "";
$city           = "";
$select         = 0;
$constituentID  = "";
$filterCount    = 0;

if (isset($_GET["keyname"])) {
    $search     = 1;
    $keyname    = $_POST["keyname"];
}

/*  
* The Search button is clicked.  Grab the search critera values from the form and place into variables.
*/
if (isset($_POST['search'])) {
    $search     = 1;
   
    $keyname    = $_POST["keyname"];
    $lookupid               = $_POST["lookupid"];
    $address                = $_POST["address"];
    $city                   = $_POST["city"];
}
/*  After the search results have been displayed, a constituent has been selected from the search results.
*  constituentID - the selected constituent.   
*  <Client side> When row is selected?, the 'Select' button will appear on the row or the row background will change color
*  the consistuent id value on a hidden input within the table row.  Pressing 'Select' will nav use to a 
*  different constituent view page (Constituent Summary Profile View Form) using the constituent id value witin the url query string.
*/
if (isset($_POST['select'])) {
    $select     = 1;
    $constituentID = trim($_POST['constituentID']);
}

/*
if ($load == 1 or $save == 1) {
    $sc            = getSoapClient();
    $clientAppInfo = getClientAppInfo();
    $formerror     = "";
    
    if ($recordId == '') {
        $formLoadvalidationerror = 1;
        $formerror               = "RecordIDMissingError";
    }
    
    if (preg_match('/^\{?[A-Z0-9]{8}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{12}\}?$/', $recordId)) {
        $recordIDBadGuid = 0;
    } else {
        $formLoadvalidationerror = 1;
        $formerror               = "RecordIDBadGuid";
    }
}


if ($search == 1) {
    if (trim($_POST['keyname']) == '') {
        $searchError               = "No Constituent Name.";
        $formSearchValidationError = 1;
    }

}
*/
//########################################
//   Search for the constituent
//########################################
if ($search == 1 and $formSearchValidationError == 0) {
    $sc            = getSoapClient();
    $clientAppInfo = getClientAppInfo();
    // Build the filter values
    $filterCount = 0;
    $filterValues=array();

    If (isset($_POST['keyname'])) {
        $filterValues[] = getStringFilter('KEYNAME', $_POST['keyname']);
    }

    If (isset($_POST['lookupid'])) {
        $filterValues[] = getStringFilter('LOOKUPID', $_POST['lookupid']);
    }

    If (isset($_POST['address'])) {
        $filterValues[] = getStringFilter('ADDRESSBLOCK', $_POST['address']);
    }

    If (isset($_POST['city'])) {
        $filterValues[] = getStringFilter('CITY', $_POST['city']);
    }

    $filter = new stdClass;
    $filter->Values=new stdClass;
    $filter->Values->fv=$filterValues;

    // build the parameters for the SearchListLoad method (these correspond to properties of the SearchListLoadRequest type)
    $req = array(
        'ClientAppInfo' => $clientAppInfo,
        'SearchListID'  => 'fdf9d631-5277-4300-80b3-fdf5fb8850ec',  // Constituent Search by Name or Lookup ID //'9bef22ab-496f-48fd-98fe-5be72270ef30', // the search list ID
        'Filter'        => $filter,
        'MaxRecords'    => 100,
        'TaskID'        => '00000000-0000-0000-0000-000000000000',
        'ReturnSearchFilters' => false
        );
    
    try {
        // invoke the SearchListLoad web service method, which returns a SearchListLoadReply object
        $reply     = $sc->SearchListLoad($req);
        $searchError = "";
        }
    
    catch (SOAPFault $f) {
        $searchError = getSoapString($f);
        debug::dump($searchError);
        }

       $moreResults = $reply->Output->HadMoreResults;   //TO DO:  Initialization?
       $rowCount    = $reply->Output->RowCount;

       if(isset($reply->Output->Rows->r)){
           $resultRows = $reply->Output->Rows->r;   
            if (!empty($resultRows) && !is_array($resultRows)) {
                // This is a single row -- make it an array
                $resultRows = array($resultRows);
            }   
        }
        else {
            $resultRows = array();
        }


        
        /*
         * Grab the data used to populate the drop down lists on the form
         */
        /*  Code Table: ADDRESS TYPE */
        //$addressTypeCodeRows = getCodeTableList('ADDRESSTYPECODE');
        
        /* COUNTRY:  We are not allowing user to pick country.  We assume USA as the country in this code sample. */
        /* Simple Data List: STATE */
        // Filter States by the USA.  
        /*
        $simpleDataListParams = array(
            array(
                'ID' => "COUNTRYID",
                'Value' => "d81cef85-7569-4b2e-8f2e-f7cf998a3342"
            )
        );
        */
        //$stateAbbrListRows    = getSimpleDataList('7fa91401-596c-4f7c-936d-6e41683121d7', $simpleDataListParams);
        
        /*  Code Table: INFOSOURCECODE */
        //$infoSourceCodeRows = getCodeTableList('INFOSOURCECODE');
        
        /* Code Table: Do Not Mail Reason Type */
        //$doNotMailReasonTypeCodeRows = getCodeTableList('DONOTMAILREASONCODE');    
}

?>
 
  <!DOCTYPE html>
  <html lang="en">

	<head>
		<title>Constituent Search</title>
		<meta name="viewport" content="width=device-width, initial-scale=1.0">
		<link rel="stylesheet" href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap.min.css">

        <!-- Optional theme -->
        <link rel="stylesheet" href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap-theme.min.css">

	
	</head>
	<body>
		<div class="navbar">
			<div class="navbar-inner">
				<a class="brand" href="#">Constituent Search: Constituent Search by Name or Lookup ID</a>  
			</div>
		</div>
		<div id="main" class="container">
			<form action="<?php echo htmlentities($_SERVER['PHP_SELF']);?>" 
			method="post" 
			role="form" class="form-horizontal"  name="constituentSearchForm" onSubmit="return validate(constituentSearchForm)">	
				
                <div class="control-group">
                    <label for="keyname" class="control-label">Last/Org name:</label>
                    <div class="controls">
                        <input type="text" class="form-control" id="keyname" 
                        name="keyname" maxlength="50" placeholder="Enter Last/Org name" value="<?php echo $keyname;?>">
                    </div>
                </div>

				<div class="control-group">
					<label class="control-label" for="lookupid" >Lookup ID:</label>
					<div class="controls">
						<input type="text" class="form-control" id="lookupid" 
						name="lookupid" maxlength="36"  placeholder="Enter Lookup ID" value="<?php echo $lookupid;?>">
					</div>
				</div>	

                <div class="control-group" >
                    <label for="address" class="control-label">Address:</label>
                    <div class="controls">
                        <input type="text" class="form-control" id="address" 
                        name="address" maxlength="150" placeholder="Enter address" 
                        value="<?php echo $address; ?>">
                    </div>
                </div>

                <div class="control-group" >
                    <label for="city" class="control-label">City:</label>
                    <div class="controls">
                        <input type="text" class="form-control" id="city" name="city" maxlength="100" 
                        placeholder="Enter city" value="<?php echo $city; ?>">
                    </div>
                </div>
               
                <div class="control-group">
                    <div class="controls">
                        <button type="submit" name="search" class="btn btn-default">Search</button>
                    </div>  
                </div>  
            </form>	
            <div id="errormsg" class="control-group">
                <?php if ( $searchError != ""):?>
                    <div class="alert alert-danger"><strong>Error! </strong><?php echo $searchError;?> </div>

                <?php endif;?>
            </div>
             

				<?php if ($search == 1 and $searchError == ""):?>
				
                    <div class="panel-group" id="accordion">
                        <div class="panel panel-default">  
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <div class="pull-right">
                                        <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
                                                    <span class="indicator glyphicon glyphicon-chevron-up"></span>            
                                        </a>
                                    </div>
                                            HadMoreResults: <strong><?php echo var_export($moreResults); ?></strong>
                                            <p>RowCount: <strong><?php echo $rowCount; ?></strong>
                                </h4>
                            </div>
                            <div id="collapseOne" class="panel-collapse collapse in">   <!-- Panel Begin-->
                                <div id="searchResults">
                                    <table class="table table-hover">
                                        <tr>
                                            <th>
                                                ID
                                            </th>
                                            <th>
                                                Lookup ID
                                            </th>
                                            <th>
                                                Name
                                            </th>                  
                        
                                            <th>
                                                Constituent Type
                                            </th>                  
                        
                                            <th>
                                                Address
                                            </th>                  
                        
                                            <th>
                                                City
                                            </th>                  
                        
                                            <th>
                                                State
                                            </th>    
                                            <th>
                                                
                                            </th>         
                        
                                        </tr>  
                         <?php 
                    foreach ($resultRows as $row) {                        
                        $idrowvalue = $row->Values->v[0];
                        $lookupidrowvalue = $row->Values->v[1];
                        $sortconstituentnamerowvalue = $row->Values->v[2];
                        $constituenttyperowvalue = $row->Values->v[3];
                        $addressrowvalue = $row->Values->v[4];
                        $cityrowvalue = $row->Values->v[5];
                        $staterowvalue = $row->Values->v[6];
                        echo            "<tr>\n"; 
                        echo                "<td>$idrowvalue</td>\n";
                        echo                "<td>$lookupidrowvalue</td>\n";
                        echo                "<td>$sortconstituentnamerowvalue</td>\n";                 
                        echo                "<td>$constituenttyperowvalue</td>\n";
                        echo                "<td>$addressrowvalue</td>\n";
                        echo                "<td>$cityrowvalue</td>\n";
                        echo                "<td>$staterowvalue</td>\n";
                        echo                "<td>\n";
                        echo                    "<div class=\"control-group\">\n";
                        echo                        "<div class=\"controls\">\n";
                        echo                            "<a href=\"IndividualView_Bio.php?ConstituentID=$idrowvalue\"><span class=\"badge\">more ...</span></a>\n";
                        //echo                            "<button type=\"submit\" id=\"$idrowvalue\" name=\"$idrowvalue\" class=\"btn btn-primary\">Select</button>\n";
                        echo                        "</div>\n";
                        echo                    "</div>\n";
                        echo                "</td>\n";
                        echo            "</tr>\n";  
                    }   
                    echo            "</table>";
                    echo        "</div>\n";        //searchResults End
                    echo        "</div>\n";      // Panel End
                    echo    "</div>\n";
                    echo "</div>\n";
                    echo "</div>\n";
                    ?>
				<?php endif;?>
		</div>
		<script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/jquery/1.8.3/jquery.min.js">
		</script>  
		
		<script src="//netdna.bootstrapcdn.com/bootstrap/3.0.0/js/bootstrap.min.js"></script>
		
        <!--
		<script type="text/javascript" src="http://tarruda.github.com/bootstrap-datetimepicker/assets/js/bootstrap-datetimepicker.min.js">
		</script>
		-->
		<script type="text/javascript">

            $(document).ready(function() {
                $("#searchResults table tr").live('click', function() {
                    //alert("You clicked my <tr>!");
                    alert($(this).children('td').html());
                //get <td> element values here!!??
                });
            });
	       
            $('#accordion .accordion-toggle').click(function (e){
                 $(this).find('.indicator').toggleClass('glyphicon-chevron-down glyphicon-chevron-up');
            }); 
          
			function validate(frm) {
				//
				// Check the seasonal end date field to see if any characters were entered
				//
				//if ((frm.seasonalstartdate.value.length > 0) && (frm.seasonalenddate.value.length == 0))

                //filter out all the inputs that are not empty, and check if you have any inputs left.  
                var inputformcontrolcount = $('.form-control').length;

                var empties = $('.form-control').filter(function () {
                    return $.trim($(this).val()) == '';
                    });

                if (empties.length == inputformcontrolcount) { 
                    /* Error! All are empty.  Must have at least one empty search field.*/ 
                    alert("Please enter at least 1 search criteria!");
                    frm.keyname.focus();
                    // if a previous warning is on the page, do nothing.
                    if($("#warningNoSearchCriteria").length == 0) {
                        $("#errormsg").append("<div id=\"warningNoSearchCriteria\" class=\"alert alert-warning\"><strong>Warning! </strong>You must enter at least one search criteria.</div>");
                    }

                    return false
                }
			}
			
		</script>
		
		<script src="http://www.google-analytics.com/urchin.js" type="text/javascript">
		</script>
		
	</body>
</html>		