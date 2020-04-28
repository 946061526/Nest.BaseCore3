/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50726
Source Host           : localhost:3306
Source Database       : db1

Target Server Type    : MYSQL
Target Server Version : 50726
File Encoding         : 65001

Date: 2020-04-24 15:47:17
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for appticket
-- ----------------------------
DROP TABLE IF EXISTS `appticket`;
CREATE TABLE `appticket` (
  `Id` varchar(36) NOT NULL,
  `AppId` varchar(50) NOT NULL,
  `ClientType` varchar(20) NOT NULL,
  `DeviceNo` varchar(30) NOT NULL,
  `Noncestr` varchar(50) DEFAULT NULL,
  `AppSecret` varchar(200) NOT NULL,
  `Ticket` varchar(50) NOT NULL,
  `LastUpdateTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of appticket
-- ----------------------------
INSERT INTO `appticket` VALUES ('7db59180081b4a0b8f8dc7e0237b7ee5', '1234', 'ios', '123', 'e0ec453e28e061cc58ac43f91dc2f3f0', 'qOV/F+i6hNVGkn7JHv74oIqQc7579vQECkbc4tGS1Pwwa93yQ/ZHpUlaDHdnOrwBUxZzMi53yHilYhWW0O9oxg==', '4E8ADA639D95B6C6E24E0070A078F69A', '2019-12-31 17:57:46');

-- ----------------------------
-- Table structure for cap.published
-- ----------------------------
DROP TABLE IF EXISTS `cap.published`;
CREATE TABLE `cap.published` (
  `Id` int(127) NOT NULL AUTO_INCREMENT,
  `Name` varchar(200) NOT NULL,
  `Content` longtext,
  `Retries` int(11) DEFAULT NULL,
  `Added` datetime NOT NULL,
  `ExpiresAt` datetime DEFAULT NULL,
  `StatusName` varchar(40) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of cap.published
-- ----------------------------

-- ----------------------------
-- Table structure for cap.received
-- ----------------------------
DROP TABLE IF EXISTS `cap.received`;
CREATE TABLE `cap.received` (
  `Id` int(127) NOT NULL AUTO_INCREMENT,
  `Name` varchar(400) NOT NULL,
  `Group` varchar(200) DEFAULT NULL,
  `Content` longtext,
  `Retries` int(11) DEFAULT NULL,
  `Added` datetime NOT NULL,
  `ExpiresAt` datetime DEFAULT NULL,
  `StatusName` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of cap.received
-- ----------------------------

-- ----------------------------
-- Table structure for menu
-- ----------------------------
DROP TABLE IF EXISTS `menu`;
CREATE TABLE `menu` (
  `id` varchar(50) NOT NULL,
  `name` varchar(50) DEFAULT NULL,
  `parentId` int(11) DEFAULT NULL,
  `path` varchar(255) DEFAULT NULL,
  `icon` varchar(255) DEFAULT NULL,
  `type` int(4) NOT NULL DEFAULT '1' COMMENT '类型：1模块 2功能 3操作',
  `sort` int(4) NOT NULL DEFAULT '0' COMMENT '排序',
  `hidden` bit(1) NOT NULL,
  PRIMARY KEY (`id`,`type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of menu
-- ----------------------------

-- ----------------------------
-- Table structure for role
-- ----------------------------
DROP TABLE IF EXISTS `role`;
CREATE TABLE `role` (
  `id` varchar(50) NOT NULL,
  `name` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of role
-- ----------------------------

-- ----------------------------
-- Table structure for rolemenu
-- ----------------------------
DROP TABLE IF EXISTS `rolemenu`;
CREATE TABLE `rolemenu` (
  `id` varchar(50) NOT NULL,
  `roleId` varchar(50) DEFAULT NULL,
  `menuId` varchar(50) DEFAULT NULL,
  `readed` bit(1) NOT NULL,
  `writed` bit(1) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of rolemenu
-- ----------------------------

-- ----------------------------
-- Table structure for tbstock
-- ----------------------------
DROP TABLE IF EXISTS `tbstock`;
CREATE TABLE `tbstock` (
  `storeHouse` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `code` varchar(255) DEFAULT NULL,
  `goodsCode` varchar(255) DEFAULT NULL,
  `color` varchar(255) DEFAULT NULL,
  `price` varchar(100) DEFAULT NULL,
  `stockNum` int(10) DEFAULT NULL,
  `stockMoney` varchar(255) DEFAULT NULL,
  `styleNo` varchar(50) DEFAULT NULL,
  `asi` varchar(255) DEFAULT NULL,
  `id` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbstock
-- ----------------------------

-- ----------------------------
-- Table structure for tbstockcheck
-- ----------------------------
DROP TABLE IF EXISTS `tbstockcheck`;
CREATE TABLE `tbstockcheck` (
  `code` varchar(100) DEFAULT NULL,
  `type` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `price` varchar(10) DEFAULT NULL,
  `styleNo` varchar(255) DEFAULT NULL,
  `name` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `color` varchar(255) DEFAULT NULL,
  `size` varchar(100) CHARACTER SET utf8 DEFAULT NULL,
  `num` int(10) DEFAULT NULL,
  `stockNum` int(10) DEFAULT NULL,
  `goodsNo` varchar(100) DEFAULT NULL,
  `asi` varchar(255) DEFAULT NULL,
  `id` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbstockcheck
-- ----------------------------

-- ----------------------------
-- Table structure for userinfo
-- ----------------------------
DROP TABLE IF EXISTS `userinfo`;
CREATE TABLE `userinfo` (
  `Id` varchar(36) NOT NULL,
  `UserName` varchar(20) NOT NULL,
  `Pwd` varchar(50) NOT NULL,
  `RealName` varchar(20) NOT NULL,
  `Status` int(11) NOT NULL DEFAULT '1',
  `CreateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of userinfo
-- ----------------------------
