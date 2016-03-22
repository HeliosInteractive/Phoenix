#Phoenix

Restart On Crash on steroids! Phoenix is a Windows Application Monitoring tool designed by Helios Interactive to make deployment and maintaining of kiosk applications easier.

![Phoenix UI](docs/image/local.png?raw=true)

##Features

 1. **Local**
   * Restart the executable if it has crashed (with configurable delays)
   * Restart the executable if its main window is unresponsive (with configurable delays)
   * Execute scripts upon executable crashes and restarts
   * Keeping executable's main window always on top (works with full-screen applications as well)
   * Supplying command line arguments without writing painful bat files
   * Supplying environment variables without changing system settings
   * Sending an email on executable's crash with screenshots attached
   * Hotkeys for toggling UI, stopping/starting the monitor process and other functionalities
   * Taking a screenshot upon executable's crash or user request
   * Graphing computer-wide CPU, GPU, and RAM load
 2. **Remote**
   * Stop/Start/Monitor the executable via MQTT
   * Receiving updates securely over SSH and RSync
   * Sending useful metrics (cpu/ram usage, etc.) to a cloud via MQTT
 3. **Report**
   * Customize-able email reports can be sent out at any time or upon executable crashes
   * Emails can have optional log files attached to them (useful for Unity3D applications)
 4. **Logging**
   * Verbose logging of all the activity with a default log rotation behavior
   * Ability to capture and log executable's `stdout` and `stderr`
   * Ability to stream logs to a remote location ([Papertrail](https://papertrailapp.com/) for example)

##Structure

Phoenix consists of a client written in `WinForms C#` and an *optional* CMS written in `CakePHP`. The client itself can be used independently from the CMS by using an MQTT client (for remote controlling a Phoenix instance). If all you ever need is just a standalone monitoring tool, you do not need to worry about MQTT or the CMS at all.

##Documentation

Take a look at `/docs` for more information. `/docs` hold *end-user* documentation. Latest stable version of Doxygen generated *developer* documentation is available [here](https://phoenix.heliosinteractive.com/docs/index.html).

##License

Phoenix' source code is licensed under the MIT license (MIT). Phoenix uses a number of third-party dependencies:

 1. [OHM](http://openhardwaremonitor.org/) is licensed under MPL v2.0
 2. [m2mqtt](https://github.com/ppatierno/m2mqtt) is licensed under EPL v1.0
 3. [Costura.Fody](https://github.com/Fody/Costura) is licensed under MIT
 4. [Log4Net](https://logging.apache.org/log4net/) is licensed under Apache License v2
