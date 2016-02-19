#Network

##RSync

Phoenix can use `rsync` to pull down updates from a remote location. In order to setup your environment for this feature to work, an `SSH` is required which is able to authenticate requests over **public keys**.

In case of regular `sshd` (openssh server package found in most Linux distributions), this means the public key shown in "Remote" tab of the Phoenix UI should be added to the `.authorized_keys` in server side.

##MQTT

An instance of Phoenix can be remote controlled over network via MQTT. For this to work you need to have a working MQTT server setup. (*NOTE: for now this should be a server available on port 1883 without a password. Authentication will be added in near future*).

If you are using the cms, your MQTT server also should support Websockets. [Mosquitto](http://mosquitto.org/) **compiled with Websockets support** is an option. You might want to lookup a guide online on how to compile it with such feature.
