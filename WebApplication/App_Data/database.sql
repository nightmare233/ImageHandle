
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
  `FontOrder` bit(1) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping structure for table imagehandle.orders
CREATE TABLE IF NOT EXISTS `orders` (
  `Id` int(255) NOT NULL AUTO_INCREMENT,
  `TaobaoId` varchar(50) DEFAULT NULL,
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
	(1, 'COMM100', 'comm100', '管理员', '70-66-A4-0F-42-77-69-CC-43-34-7A-A9-6B-72-93-1A')
/*!40000 ALTER TABLE `users` ENABLE KEYS */;

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
