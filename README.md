# Phoenix

## Windows Application Monitoring

![Phoenix UI](https://raw.githubusercontent.com/HeliosInteractive/Phoenix/master/docs/image/local.png)

Pheonix is a Windows Application Monitoring tool designed by Helios Interactive to make deployment and maintaining of kiosk applications easier.

###Features

 1. Local
   * Restart the executable if it has crashed (with configurable delays)
   * Execute scripts upon executable crashes and restarts
   * Keeping executable's main window always on top
   * Supplying command line arguments without writing painful bat files
   * Supplying environment variables without changing system settings
   * Sending an email on executable's crash
   * Checking if executable's window is responsive or not and if not, restarting it
   * Hotkeys for toggling UI, stopping/starting the monitor process and etc.
   * Taking a screenshot upon executable's crash
   * Graphing computer-wide CPU, GPU, and RAM load
 2. Remote
   * Receiving commands via MQTT in order for a technician to remotely invoke actions without being physically present on-site
   * Receiving updates securely via SSH
   * Sending useful metrics (cpu/ram usage, etc.) to the cloud via MQTT
 3. Report
   * Customize-able email reports can be sent out at any time via MQTT or upon executable crashes.
   * Emails can have optional log files attached to them (useful for Unity3D applications)
 4. Logging
   * Verbose logging of all the activity with a default log rotation behavior so you can have access to the entire history of an executable's activity
   * Ability to capture executable's `stdout` and `stderr`and stream them to a remote location ([Papertrail](https://papertrailapp.com/) for example).

### Structure

Phoenix consists of a client written in `WinForms C#` and an *optional* CMS written in `CakePHP`. The client itself can be used independently from the CMS by using an MQTT client (for remote controlling a Phoenix instance). If all you ever want is just a standalone monitoring tool, you do not need to worry about MQTT at all.

### License

Phoenix' source code is licensed under the MIT license (MIT). Phoenix uses a number of third-party dependencies:

 1. [OHM](http://openhardwaremonitor.org/) is licensed under MPL v2.0
 2. [m2mqtt](https://github.com/ppatierno/m2mqtt) is licensed under EPL v1.0
 3. [Costura.Fody](https://github.com/Fody/Costura) is licensed under MIT
 4. [Log4Net](https://logging.apache.org/log4net/) is licensed under Apache License v2

### Documentation
Take a look at `/docs` for more information.
