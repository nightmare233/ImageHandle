-- --------------------------------------------------------
-- 主机:                           127.0.0.1
-- Server version:               8.0.13 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL 版本:                  10.0.0.5460
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for imagehandle
CREATE DATABASE IF NOT EXISTS `imagehandle` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */;
USE `imagehandle`;

-- Dumping structure for table imagehandle.imagetext
CREATE TABLE IF NOT EXISTS `imagetext` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `SampleId` int(11) DEFAULT NULL,
  `Type` int(11) DEFAULT NULL,
  `Text` varchar(50) DEFAULT NULL,
  `Font` varchar(50) DEFAULT NULL,
  `PositionX` int(11) DEFAULT NULL,
  `PositionY` int(11) DEFAULT NULL,
  `FontSize` int(11) DEFAULT NULL,
  `Order` bit(1) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table imagehandle.imagetext: ~0 rows (approximately)
/*!40000 ALTER TABLE `imagetext` DISABLE KEYS */;
INSERT INTO `imagetext` (`Id`, `SampleId`, `Type`, `Text`, `Font`, `PositionX`, `PositionY`, `FontSize`, `Order`) VALUES
	(1, 1, 1, '冯', 'Arial', 148, 0, 60, NULL),
	(2, 1, 1, '梓', 'Arial', 148, 148, 60, NULL),
	(3, 1, 1, '骁', 'Arial', 0, 0, 60, NULL),
	(4, 1, 1, '印', 'Arial', 0, 148, 60, NULL),
	(5, 2, 1, '毛', 'D:\\pictures\\AdobeHeitiStd-Regular.otf', 148, 0, 60, NULL),
	(6, 2, 1, '主', 'D:\\pictures\\AdobeHeitiStd-Regular.otf', 148, 148, 60, NULL),
	(7, 2, 1, '席', 'D:\\pictures\\AdobeHeitiStd-Regular.otf', 0, 148, 60, NULL);
/*!40000 ALTER TABLE `imagetext` ENABLE KEYS */;

-- Dumping structure for table imagehandle.orders
CREATE TABLE IF NOT EXISTS `orders` (
  `Id` int(255) NOT NULL AUTO_INCREMENT,
  `TaobaoId` int(11) DEFAULT NULL,
  `MainText` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '印章文字',
  `SmallText` varchar(20) NOT NULL,
  `SampleId` int(11) NOT NULL,
  `ImageUrl` varchar(200) DEFAULT NULL COMMENT '图片链接',
  `SubmitTime` datetime DEFAULT NULL,
  `Status` tinyint(4) DEFAULT NULL COMMENT '枚举 待审批、待生产、已完成、已删除',
  `AuditTime` datetime DEFAULT NULL,
  `Auditor` int(255) NOT NULL DEFAULT '0',
  `Productor` int(255) NOT NULL DEFAULT '0',
  `ProductTime` datetime DEFAULT NULL,
  `DeleteTime` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table imagehandle.orders: ~14 rows (approximately)
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
INSERT INTO `orders` (`Id`, `TaobaoId`, `MainText`, `SmallText`, `SampleId`, `ImageUrl`, `SubmitTime`, `Status`, `AuditTime`, `Auditor`, `Productor`, `ProductTime`, `DeleteTime`) VALUES
	(1, 123, '11', '', 1, '', '2018-12-30 10:24:36', 4, '0001-01-01 00:00:00', 9, 10, '2019-01-01 21:38:45', '2019-01-01 21:38:51'),
	(2, 123, 'text', '', 1, '', '2018-12-30 00:00:00', 3, '0001-01-01 00:00:00', 9, 10, '2018-12-31 15:16:45', '2018-12-31 15:16:51'),
	(3, 77, '77', '', 1, '', '2018-12-31 12:16:40', 3, '2018-12-31 15:16:33', 10, 10, '2018-12-31 15:16:49', '2018-12-31 15:16:54'),
	(5, 88, '88', '', 1, '', '2018-12-31 15:43:52', 4, '2018-12-31 15:16:33', 11, 11, '2018-12-31 15:16:33', '2018-12-31 15:16:33'),
	(6, 233, '233', '', 1, '', '2018-12-31 16:32:40', 3, '0001-01-01 00:00:00', 11, 12, '2018-12-31 16:37:30', '2018-12-31 16:37:33'),
	(7, 8888, '888', '', 1, '', '2018-12-31 16:37:55', 4, '0001-01-01 00:00:00', 11, 12, '0001-01-01 00:00:00', '0001-01-01 00:00:00'),
	(8, 4444, '4444', '', 1, '', '2019-01-01 11:35:11', 3, '0001-01-01 00:00:00', 12, 10, '2019-01-01 21:40:14', '0001-01-01 00:00:00'),
	(9, 112233, '34567', '', 1, '', '2019-01-01 14:53:52', 1, '2019-01-01 21:35:13', 10, 13, '0001-01-01 00:00:00', '0001-01-01 00:00:00'),
	(10, 123456, '123456', '', 1, '', '2019-01-01 21:39:21', 4, '0001-01-01 00:00:00', 0, 0, '0001-01-01 00:00:00', '2019-01-01 21:45:03'),
	(11, 567, '789', '', 1, '', '2019-01-01 21:45:20', 3, '0001-01-01 00:00:00', 10, 10, '2019-01-01 21:45:39', '0001-01-01 00:00:00'),
	(12, 0, '000', '', 1, '', '2019-01-01 21:49:51', 3, '0001-01-01 00:00:00', 10, 10, '2019-01-01 21:50:09', '0001-01-01 00:00:00'),
	(13, 3344, '1问问', '', 1, '', '2019-01-06 16:00:27', 1, '2019-01-06 19:50:00', 10, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00'),
	(14, 222, '222', '', 1, '', '2019-01-06 21:19:02', 0, '0001-01-01 00:00:00', 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00'),
	(15, 444, '444', '', 1, '', '2019-01-06 21:19:28', 0, '0001-01-01 00:00:00', 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00');
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;

-- Dumping structure for table imagehandle.sample
CREATE TABLE IF NOT EXISTS `sample` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ImageType` int(11) NOT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `ImageSizeX` int(11) NOT NULL,
  `ImageSizeY` int(11) NOT NULL,
  `Style` int(11) NOT NULL,
  `ImageURL` varchar(200) NOT NULL,
  `BgImage` varchar(200) NOT NULL,
  `MainTextNumber` int(11) NOT NULL,
  `IfHasSmallText` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table imagehandle.sample: ~0 rows (approximately)
/*!40000 ALTER TABLE `sample` DISABLE KEYS */;
INSERT INTO `sample` (`Id`, `ImageType`, `Name`, `ImageSizeX`, `ImageSizeY`, `Style`, `ImageURL`, `BgImage`, `MainTextNumber`, `IfHasSmallText`) VALUES
	(1, 2, 'Sampl1', 296, 0, 1, ' ', ' ', 4, b'0'),
	(2, 3, 'Sample2', 296, 0, 0, ' ', ' ', 3, b'0');
/*!40000 ALTER TABLE `sample` ENABLE KEYS */;

-- Dumping structure for table imagehandle.users
CREATE TABLE IF NOT EXISTS `users` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `LoginName` varchar(100) NOT NULL,
  `Role` varchar(50) NOT NULL,
  `Password` varchar(255) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table imagehandle.users: ~8 rows (approximately)
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` (`Id`, `Name`, `LoginName`, `Role`, `Password`) VALUES
	(9, '冯梓骁', 'fengzixiao', '管理员', '111'),
	(10, '冯楚福', 'fengchufu', '管理员', '111'),
	(11, '8888', '8888', '管理员', '88'),
	(12, '滚滚滚', '滚滚滚', '管理员', '4F-0B-4B-00-A4-B7-C9-09-47-66-FF-B7-F3-A4-0E-37'),
	(13, '11', '11', '管理员', '86-CE-15-A7-F3-9F-C1-43-23-B7-6C-9B-95-C6-61-65'),
	(14, '223', '22', '管理员', '9F-37-28-B3-8B-44-E6-C5-1B-62-A7-8D-DC-48-47-F8'),
	(15, '33', '33', '管理员', '9F-37-28-B3-8B-44-E6-C5-1B-62-A7-8D-DC-48-47-F8'),
	(16, 'qq', 'qq', '管理员', 'F0-EC-B0-C8-0C-7E-C5-84-41-C7-F2-DD-F4-A7-6F-90'),
	(17, '11', '1122', '管理员', '86-CE-15-A7-F3-9F-C1-43-23-B7-6C-9B-95-C6-61-65'),
	(18, 'COMM100', 'comm100', '管理员', '70-66-A4-0F-42-77-69-CC-43-34-7A-A9-6B-72-93-1A'),
	(19, '111', '111', '生产员', '6E-CE-4F-D5-1B-C1-13-94-26-92-63-7D-9D-4B-86-0E');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;

CREATE TABLE `imagefont` (
	`id` INT(11) NOT NULL AUTO_INCREMENT,
	`Name` VARCHAR(50) NOT NULL,
	`IfSystem` BIT(1) NOT NULL DEFAULT b'0',
	`URL` VARCHAR(100) NULL DEFAULT '',
	PRIMARY KEY (`id`)
)
COMMENT='字体列表'
COLLATE='utf8mb4_0900_ai_ci'
ENGINE=InnoDB
AUTO_INCREMENT=2
;
