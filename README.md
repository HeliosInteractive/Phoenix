# Phoenix

## Windows Application Monitoring

![Phoenix UI](https://raw.githubusercontent.com/HeliosInteractive/Phoenix/master/docs/image/local.png)

Pheonix is a Windows Application Monitoring tool designed by Helios Interactive to make deployment and maintaining of kiosk applications easier.

### Structure

Phoenix consists of a client written in `WinForms C#` and an *optional* CMS written in `CakePHP`. The client itself can be used independently from the CMS by using an MQTT client (for remote controlling a Phoenix instance). If all you ever want is just a standalone monitoring tool, you do not need to worry about MQTT at all.

### License

Phoenix' source code is licensed under the MIT license (MIT). Phoenix a number of third-party dependencies:

 1. [m2mqtt](https://github.com/ppatierno/m2mqtt) which is licensed under EPL v1.0
 2. [Costura.Fody](https://github.com/Fody/Costura) which is licensed under MIT
 3. [Log4Net](https://logging.apache.org/log4net/) which is licensed under Apache License v2

### Documentation
Take a look at `/docs` for more information.
