CREATE DATABASE IF NOT EXISTS phoenix;
USE phoenix;

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";

CREATE TABLE IF NOT EXISTS `machines` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `created` datetime DEFAULT NULL,
  `modified` datetime DEFAULT NULL,
  `name` varchar(256) COLLATE utf8_persian_ci NOT NULL COMMENT 'Machine name',
  `public_key` varchar(2049) COLLATE utf8_persian_ci NOT NULL COMMENT 'SSH public key',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_persian_ci AUTO_INCREMENT=0 ;

CREATE TABLE IF NOT EXISTS `users` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `created` datetime DEFAULT NULL,
  `modified` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_persian_ci AUTO_INCREMENT=0 ;

/* This is the default user account. Password is admin, needs to be changed after first login. */
INSERT INTO `phoenix`.`users` (`id`, `email`, `password`, `created`, `modified`) VALUES (NULL, 'phoenix@heliosinteractive.com', '$2y$10$f4DjFiHr0LiLZODOBpi10enljbo3fT66VxiXr45exosz7.3ZEaI1O', NULL, NULL);
