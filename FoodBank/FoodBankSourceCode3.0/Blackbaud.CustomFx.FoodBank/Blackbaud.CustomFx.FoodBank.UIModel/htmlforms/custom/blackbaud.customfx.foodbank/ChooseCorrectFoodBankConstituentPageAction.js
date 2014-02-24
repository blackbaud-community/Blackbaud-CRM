/// <reference path="D:\InfinityWebServer\BBInfinity\bbappfx\vroot\browser\jQuery\jquery-vsdoc.js" />
/// <reference path="D:\InfinityWebServer\BBInfinity\bbappfx\vroot\browser\ExtJS\ext-3.2.0\adapter\jquery\ext-jquery-adapter.js" />
/// <reference path="D:\InfinityWebServer\BBInfinity\bbappfx\vroot\browser\ExtJS\ext-3.2.0\ext-all-debug.js" />
/// <reference path="D:\InfinityWebServer\BBInfinity\bbappfx\vroot\browser\json\json.js" />
/// <reference path="D:\InfinityWebServer\BBInfinity\bbappfx\vroot\browser\BBUI\bbui\bbui.js" />
/// <reference path="D:\InfinityWebServer\BBInfinity\bbappfx\vroot\browser\BBUI\forms\utility.js" />
/// <reference path="D:\InfinityWebServer\BBInfinity\bbappfx\vroot\browser\BBUI\pages\dialogs.js" />
/// <reference path="D:\InfinityWebServer\BBInfinity\bbappfx\vroot\browser\BBUI\forms\formcontainer.js" />
/// <reference path="D:\InfinityWebServer\BBInfinity\bbappfx\vroot\browser\BBUI\forms\actionmapping.js" />
/// <reference path="D:\InfinityWebServer\BBInfinity\bbappfx\vroot\browser\BBUI\uimodeling\service.js" />

// Many thanks to Paul Crowder for his help with this sample.
// Creating a Custom Action with JavaScript let's you create your own action beyond the 
// Infinity Platform's predefined action types.  Custom Actions are the Web Shell replacement
// for CLR Actions.  CustomActions only work in the Web Shell and will not be visible 
// within the ClickOnce shell.
// this Custom Action is called from FoodBankOrgSearchCustomAction.Task.xml within the food
// bank catalog project.  

// See "Setting up Visual Studio for JavaScript Development" within the on-line development guides
// use the latest version of jsLint with stricter rule checks
/*lintversion 2*/

// add jslint validation rules to help validate the code for errors
// use Blackbaud.AppFx.UIModeling.HtmlLint.exe to validate both JavaScript and HTML
// 3rd party developers can obtain Blackbaud.AppFx.UIModeling.HtmlLint.exe can be obtain
// from the on-line developer guides
/*jslint bitwise: true, browser: true, eqeqeq: true, undef: true, white: true, indent: 4*/

// the /*global BBUI...line below indicates global variables that are 
// defined outside of this particular JavaScript file so the “undef” rule 
// doesn’t fail when it encounters these variables in use.  
// Typically all variables should be defined with a “var“ statement.
// this is what the  undef: true within the jslist options above checks 
/*global BBUI, Ext, $ */

// Encapsulate all logic inside a self-invoking function so objects are not added to the 
// global (window) object.
// See "Don’t add objects to the global namespace"  and 
//  "Wrapping your code in a self-invoking function" within the on-line 
//   development guides
(function () {
    'use strict';  //enforces latest ECMAScript5
    
    // Declare any static objects or constants here.  Variables you declare here are shared 
    // across multiple instances of this action and should never be assigned during the 
    // execution of an action.
    //
    // Useful utility methods can be found in BBUI.forms.Utility (usually aliased as "Util") 
    // and BBUI.pages.ActionUtility (usually aliased as "ActionUtil").  See the BBUI 
    // documentation for more information about methods available on those utility classes.
    // must define variables before we use them.
    
    // Immediately using BBUI is an exception to the 
    //   "define variables before we use them" rule
    //   since it is a global variable... see /*global BBUI...line above.
    var ns, 
        Util = BBUI.forms.Utility;
        
    // Ensure the namespace for your action exists.  The naming convention for custom action 
    // namespaces is "BBUI.customactions.yournamespace" where "yournamespace" is the lower-case 
    // type of the UI model project where this file is stored.  For instance, if you are creating
    // a custom action in the Blackbaud.AppFx.FoodBank.UIModel project, your action's namespace 
    // would be "BBUI.customactions.foodbank".
    ns = BBUI.ns("BBUI.customactions.foodbank");

    // This is the constructor for the java script object that represents our custom action.  
    // The constructor will always be passed a host object (BBUI.pages.ActionHost) by the Infinity platform when the custom
    // action is called upon at run time, by a TaskSpec, for example.
    // See the BBUI documentation for BBUI.pages.ActionHost for information about 
    // properties and methods available on the host object.
    // https://www.blackbaud.com/files/support/guides/infinitytechref/Content/apidocs-BB_2-91/index.html?class=BBUI.pages.ActionHost

    // Helpful javascript articles
    // http://helephant.com/2009/01/18/javascript-object-prototype/

    ns.ChooseCorrectFoodBankConstituentPageAction = function (host) {
        // Cache the host object so it can be referenced later.
        // "this" keyword is a special javascript operator that provides a reference to the object
        //you are creating
        this.host = host;
    };

    // Instance methods are declared as properties of the action's prototype object.
    // So below we have a property named executeAction 
    // the Infinity platform will invoke executeAction when the user clicks the button or 
    // link to which the action is mapped or when Task is clicked.
    ns.ChooseCorrectFoodBankConstituentPageAction.prototype = {
    
        executeAction: function (callback) {
        
			// Note that callback is deliberately not called since it does not 
			// make sense in the context of this script, analogous to ShowPage 
			// actions not having a PostAction.
        
            // we are going to use a view data form to tell us if a 
            // constituent (organization) is a food bank. 
			var dataFormInstanceId = "23119D4E-3F04-4931-8219-A5AE9E3587B3",
				options,
				constituentId,
				hostRef;
				
            // Prior to this JavaScripts execution, TaskSpec will call upon the Organization Search
            // to determine the constituent id/org id/context value.
            // Grab the action context (constituent ID/org ID) from the 
            // search list's return value
			constituentId = this.host.getContextRecordId();
			hostRef = this.host;
			
            // Soon we will call upon the view data form to determine if the 
            // searched constituent is a food bank.  If all goes well with the
            // call to the view data form, the successCallback function will be called upon
            // to direct the user to the appropriate page.
            // So let's define the successCallback function before we load the data form
			function successCallback(result) {
				
				var isFoodBankField,
					isFoodBank,
					constituentPageId,
					foodBankPageId;
				
				isFoodBankField = BBUI.findByProp(result.values, "name", "ISFOODBANK");
				
				isFoodBank = false;
				if (BBUI.is(isFoodBankField)) {
					isFoodBank = isFoodBankField.value;
				}
				
                // grab the static parameter value from the task.  
                // check out the parameters defined within the ParameterList element 
                // within FoodBankOrgSearchCustomAction.Task.xml				
				constituentPageId = hostRef.getParameterValue("ConstituentPageID");
				foodBankPageId = hostRef.getParameterValue("FoodBankPageID");
				
                // use nav to redirect the user to the final destination page.
                // Note that callback is deliberately not called since it does not 
			    // make sense in the context of this script, analogous to ShowPage 
			    // actions not having a PostAction.
				if (isFoodBank) {
					hostRef.nav.goToPage(foodBankPageId, null, constituentId);
				}
				else {
					hostRef.nav.goToPage(constituentPageId, null, constituentId);
				}
			}

            // Soon we will call upon the view data form to determine if the 
            // searched constituent is a food bank.  If all does NOT goes well with the
            // call to the view data form (like no data returned by the view data form), 
            // the failureCallback function will be upon.
            // So let's define the failureCallback function here before we load the data form
			function failureCallback(request, error) {
				Util.alert(error);
			}
				
            // lets define the options for the view data form. The data form will need to know
            // the record to retrive from the database.  pass the value of the constituentId 
            // as the recordId for the view data form. 
			options = {
				recordId: constituentId
			};
        
            // Finally, once the success and fail callback methods are defined and we have prepared the 
            // options object with a value for the recordId property. call the dataFormLoad to 
            // determine if the constituentId is a food bank.
            // Use callbacks to asynchronously branch the code depending on whether the load was
            // successful or unsuccessful.  Pass the options into the call which contains the value of our
            // searched constituent id.
			this.host.webShellSvc.dataFormLoad(dataFormInstanceId, successCallback, failureCallback, options);
        
        }
    
    };
    
}());