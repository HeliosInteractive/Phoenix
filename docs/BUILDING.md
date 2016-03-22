#Building

##Client

Phoenix client can be simply built by opening up `phoenix/phoenix.sln` in Visual Studio (which has *Visual C#* installed). `.NET Framework` version 4.5+ is required to build the client successfully. You may need to do a "Rebuild" the first time you open the solution file to obtain all Nuget packages.

##CMS

Content Management System of Phoenix is an optional piece of the puzzle and is only needed if you need remote management. All its functionalities can be done with alternative tools. CMS is just provided for user's convenience.

Content Management System of Phoenix glues a couple of tools together: SSH server key management, MQTT communication and Phoenix instance monitoring.

###CakePHP

Have [Composer](https://getcomposer.org/) installed. Navigate to `cms/` and execute:

 - Windows: `composer install`
 - Non-Windows: `php composer.phar install`

If everything goes smoothly, you should get a `cms/vender/` folder. At this point rename `cms/config/app.dist.php` to `cms/config/app.php` and edit it to match your environment. Everything Phoenix related is at the bottom of this file under `Phoenix` key. You might want to change your database config as well.

In your `cms/config/app.php`, you need to point CMS to your `authorized_keys` file of the SSH server. Make sure this file is write-able by CMS. You also need to point it to your MQTT server. Following keys are examples of such configuration:

```PHP
'Phoenix' => [
	'KeyFile' => '~/.ssh/authorized_keys',
	'MqttUrl' => 'ws://test.mosquitto.org:8080',
],
```

**Make sure you properly jail your webserver's user**. CMS of Phoenix is not designed with security in mind at all. If you do not feel comfortable about reading/writing to your key file via CMS, do not use the CMS!

**Make sure your environment is also properly setup for CakePHP.** For example `mod_rewrite` of Apache should be on (which is off by default). For more specific instructions please visit [CakePHP's documentation](http://book.cakephp.org/3.0/en/installation.html).

Move `cms` to your web server's web root folder after you are done configuring.

###SSH server

Make sure your SSH server is authenticate-able with public keys and a jailed user. A step-by-step guide on how to do this with OpenSSH can be found [here](http://www.g-loaded.eu/2005/11/10/ssh-with-keys/).

###MySQL Database

The schema is checked in under `/cms/config/schema/phoenix.sql`. Execute it in a MySQL shell or in PHPmyAdmin. You should get a database named `phoenix` afterwards.

###MQTT server with Websockets support

One available open-source option is [Mosquitto](http://mosquitto.org/). Mosquitto at the time of writing this note does not come with Websocket support out of the box and you need to recompile it with Websocket support enabled.

A great guide on how to compile Mosquitto with Websockets support is [available here](http://www.xappsoftware.com/wordpress/2015/05/18/six-steps-to-install-mosquitto-1-4-2-with-websockets-on-debian-wheezy/). After compilation make sure you enable both a websocket port and a regular port in your server by appending the following lines to your configuration file (usually found in `/etc/mosquitto/mosquitto.conf`):

```CONF
# regular MQTT requests (non-Websocket)
port 1883
# Websocket MQTT requests
listener 8080
protocol websockets
```
