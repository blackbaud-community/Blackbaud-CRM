##Overview##

The API cookbook samples demonstrate how to call individual features (ex: Contact Information Address List) via the Blackbaud CRM SOAP Web Service (AppFxWebService.asmx).  The samples utilize twitter bootstrap 2.3.2 which provide a sleek, intuitive, and responsive front-end framework. 

Each sample typically takes an ID (Primary Key) value of a certain Record Type (Constituent, Address, etc.) in either an html input ...

![AddressSample](http://blackbaud-community.github.io/Blackbaud-CRM/images/CookbookAddressList.png)

... and/or query string in the URL

    Ex: http://localhost:800/AddressList.php?ConstituentID=0030D1BA-4A49-44AB-A44C-F9E294CDE8C0

##Technologies##

The following technologies, langages, and libraries are used:

1. [PHP](http://www.php.net/)
2. [jQuery 1.8.3](http://jquery.com/)
3. [Bootstrap 2.3.2](http://getbootstrap.com/2.3.2/)
4. [Bootstrap date time picker](http://tarruda.github.io/bootstrap-datetimepicker/)

###About Bootstrap###

Bootstrap is built to work best in the latest desktop and mobile browsers, meaning older browsers might display differently styled, though fully functional, renderings of certain components.  The cookbook is geared toward [modern browsers](http://getbootstrap.com/getting-started/#support). 

##Prerequisites##

1. You will need to [Install and Configure PHP](http://www.php.net/manual/en/install.php.)
2. You will need an instance of Blackbaud CRM.  Here are some [installation instructions](https://www.blackbaud.com/files/support/infinityinstaller/infinity-installation.htm) if you want to do a local install.
2. You will need domain/user name and password credentials.  These credentials must be associated with an application user within Blackbaud CRM.  The application user must have permission to use the feature(s) that the cookbook recipe is interacting with. See [Authentication and Authorization](https://www.blackbaud.com/files/support/guides/infinitydevguide/Subsystems/inwebapi-developer-help/Content/InfinityWebAPI/coAuthenticationAndAuthorization.htm).
4. Within **helperfunctions.php** you will need to provide the correct values for the following variables:

**$wsdl** - *url to the BBEC web service end point wsdl document. See [Locating the Endpoint](https://www.blackbaud.com/files/support/guides/infinitydevguide/Subsystems/inwebapi-developer-help/Content/LocatingAppFxWebServiceEndpoint.htm). See getSoapClient() function within helperfunctions.php.*

	Ex: $wsdl = 'http://localhost/bbappfx/appfxwebservice.asmx?wsdl';

**$userName** - *Domain\User credentials to access the web service using basic authentication.  See [Authentication and Authorization](https://www.blackbaud.com/files/support/guides/infinitydevguide/Subsystems/inwebapi-developer-help/Content/InfinityWebAPI/coAuthenticationAndAuthorization.htm) See getSoapClient() function within helperfunctions.php.*

	Ex: $userName = 'Domain\User Name';

**$password** - *domain user password credentials to access the web service using basic authentication (See getSoapClient() function within helperfunctions.php*
	
	Ex:  $password = 'Password';

**$database** - *the database to use.  See function getClientAppInfo() within helperfunctions.php*
	
	Ex:  $database = 'BBInfinity';

##Related Resources##
* [API Overview](https://www.blackbaud.com/files/support/guides/infinitydevguide/Subsystems/inwebapi-developer-help/Content/InfinityWebAPI/coAPIOverview.htm)
* [AppFxWebService.asmx](https://www.blackbaud.com/files/support/guides/infinitydevguide/Subsystems/inwebapi-developer-help/Content/InfinityWebAPI/coAppFxWebService.asmx.htm)
* [Locating the Endpoint](https://www.blackbaud.com/files/support/guides/infinitydevguide/Subsystems/inwebapi-developer-help/Content/LocatingAppFxWebServiceEndpoint.htm)
* [Authentication and Authorization](https://www.blackbaud.com/files/support/guides/infinitydevguide/Subsystems/inwebapi-developer-help/Content/InfinityWebAPI/coAuthenticationAndAuthorization.htm)
