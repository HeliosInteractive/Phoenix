CakePHP Plugin Installer
========================

A composer installer for installing CakePHP 3.0+ plugins.

This installer ensures your application is aware of CakePHP plugins installed
by composer in `vendor/`.

Usage
-----

Your CakePHP application should already depend on `cakephp/plugin-installer`, if
not in your CakePHP application run:

```
composer require cakephp/plugin-installer:*
```

Your plugins themselves do **not** need to require `cakephp/plugin-installer`. They
only need to specify the `type` in their composer config:

```json
"type": "cakephp-plugin"
```

For the installer to work properly ensure that your plugin's composer config
file has a proper autoload section. Assuming your plugin's namespace is "MyPlugin"
the autoload section would be like:

```json
"autoload": {
    "psr-4": {
        "MyPlugin\\": "src"
    }
}
```

Not strictly necessary for the working of the installer but ideally you would
also have an "autoload-dev" section for loading test files:

```json
"autoload": {
    "psr-4": {
        "MyPlugin\\": "src"
    }
},
"autoload-dev": {
    "psr-4": {
        "MyPlugin\\Test\\": "tests",
        "Cake\\Test\\" : "vendor/cakephp/cakephp/test"
    }
}
```

If your top level namespace is a vendor name then your namespace to path mapping
would be like:

```json
"autoload": {
    "psr-4": {
        "MyVendor\\MyPlugin\\": "src"
    }
},
"autoload-dev": {
    "psr-4": {
        "MyVendor\\MyPlugin\\Test\\": "tests",
        "Cake\\Test\\" : "vendor/cakephp/cakephp/test"
    }
}
```
