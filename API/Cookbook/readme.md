*Overview*

The API cookbook samples demonstrate how to call individual features (ex: Contact Information Address List) via the Blackbaud CRM SOAP Web Service (AppFxWebService.asmx).  The samples utilize twitter bootstrap 2.3.2 which provide a sleek, intuitive, and responsive front-end framework. 

Each sample typically takes an ID (Primary Key) value of a certain Record Type (Constituent, Address, etc.) in either an html input or query string.

	Ex: http://localhost:800/AddressList.php?ConstituentID=0030D1BA-4A49-44AB-A44C-F9E294CDE8C0

![AddressSample](http://blackbaud-community.github.io/Blackbaud-CRM/images/CookbookAddressList.png)

*Technologies*

The following technologies, langages, and libraries are used:

1. [PHP](http://www.php.net/)
2. [jQuery 1.8.3](http://jquery.com/)
3. [Bootstrap 2.3.2](http://getbootstrap.com/2.3.2/)
4. [Bootstrap date time picker](http://tarruda.github.io/bootstrap-datetimepicker/)

*About Bootstrap*

Bootstrap is built to work best in the latest desktop and mobile browsers, meaning older browsers might display differently styled, though fully functional, renderings of certain components.

*IMPORTANT STUFF TO DO*

Within helperfunctions.php you will need to provide the correct values for the following variables:

1. $wsdl - the url of the BBEC web service wsdl document (See getSoapClient() function within helperfunctions.php)

	Ex:  $wsdl = 'http://localhost/bbappfx/appfxwebservice.asmx?wsdl';

2. $userName - Domain\User credentials to access the web service using basic authentication (See getSoapClient() function within helperfunctions.php)

	Ex: $userName = 'Domain\User Name';

3. $password - domain user password credentials to access the web service using basic authentication (See getSoapClient() function within helperfunctions.php)
	
	Ex:  $password = 'Password';

4. $database - the database to use  (see function getClientAppInfo() within helperfunctions.php)
	
	Ex:  $database = 'BBInfinity';
