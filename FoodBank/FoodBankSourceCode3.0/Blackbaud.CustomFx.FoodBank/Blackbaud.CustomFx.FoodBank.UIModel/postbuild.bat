echo copy all htmlforms to the appropriate location on the webserver...
xcopy "C:\BBEC\SDK Training\TrainingCourse\Blackbaud.CustomFx.FoodBank\Blackbaud.CustomFx.FoodBank.UIModel\htmlforms\custom\blackbaud.customfx.foodbank\*.html" "C:\Inetpub\wwwroot\bbappfx25\bbappfx\vroot\browser\htmlforms\custom\blackbaud.customfx.foodbank\" /Y /E /R

echo minify the html and js files to optimize their payload on the wire
rem %~dp0..\..\..\..\Utils\Blackbaud.AppFx.JSMinifier\bin\JSMinifier.exe %~dp0..\..\..\..\Blackbaud.AppFx.Server\Deploy\browser\htmlforms\<subfolder>\*.html /pre
rem %~dp0..\..\..\..\Utils\Blackbaud.AppFx.JSMinifier\bin\JSMinifier.exe %~dp0..\..\..\..\Blackbaud.AppFx.Server\Deploy\browser\htmlforms\<subfolder>\*.js