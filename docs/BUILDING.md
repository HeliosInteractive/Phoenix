#Building

##Client

Phoenix client can be simply built by opening up `phoenix/phoenix.sln` in Visual Studio (which has Visual C# installed). `.NET Framework` version 4.5+ is required to build the client successfully.

##CMS

###CakePHP
Have [Composer](https://getcomposer.org/) installed. Navigate to `cms/` and execute:

 - Windows: `composer install`
 - Non-Windows: `php composer.phar install`

If everything goes smoothly, you should get a `cms/vender/` folder. At this point rename `cms/config/app.dist.php` to `cms/config/app.php` and edit it to match your environment. Everything Phoenix related is at the bottom of this file under `Phoenix` key. You might want to change your database config as well.

**Make sure your environment is also properly setup for CakePHP.** For example `mod_rewrite` of Apache should be on (which is off by default). For more specific instructions please visit CakePHP's documentation.

###MySQL Database
The schema is checked in under `/cms/config/schema/phoenix.sql`. Execute it in a MySQL shell or in PHPmyAdmin. You should get a database named `phoenix` afterwards.
