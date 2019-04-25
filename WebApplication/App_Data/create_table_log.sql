 
CREATE TABLE `Logs` (
	`id` INT NOT NULL,
	`time` DATETIME NOT NULL,
	`action` INT NOT NULL,
	`userId` INT NOT NULL,
	`detail` VARCHAR(500) NULL,
	PRIMARY KEY (`id`)
)
COMMENT='logs'
COLLATE='utf8mb4_0900_ai_ci'
;
SELECT `DEFAULT_COLLATION_NAME` FROM `information_schema`.`SCHEMATA` WHERE `SCHEMA_NAME`='imagehandle';
SHOW TABLE STATUS FROM `imagehandle`;
SHOW FUNCTION STATUS WHERE `Db`='imagehandle';
SHOW PROCEDURE STATUS WHERE `Db`='imagehandle';
SHOW TRIGGERS FROM `imagehandle`;
SELECT *, EVENT_SCHEMA AS `Db`, EVENT_NAME AS `Name` FROM information_schema.`EVENTS` WHERE `EVENT_SCHEMA`='imagehandle';
/* Table node "Logs" not found in tree. */
/* Entering session "Unnamed" */
SHOW CREATE TABLE `imagehandle`.`Logs`;
ALTER TABLE `Logs`
	ADD INDEX `time` (`time`);
	
 