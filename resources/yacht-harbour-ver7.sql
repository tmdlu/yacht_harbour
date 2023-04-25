
-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET latin2;


-- -----------------------------------------------------
-- Table `mydb`.`account`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`account` (
  `id_account` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(64) NOT NULL,
  `surname` VARCHAR(64) NOT NULL,
  `photo` BLOB NULL,
  `email` VARCHAR(128) NOT NULL,
  `password` VARCHAR(64) NOT NULL,
  `phone_number` VARCHAR(11) NULL,
  `club_status` VARCHAR(128) NULL,
  `role` VARCHAR(64) NULL,
  PRIMARY KEY (`id_account`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`function`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`function` (
  `id_function` INT NOT NULL AUTO_INCREMENT,
  `function_name` VARCHAR(128) NULL,
  PRIMARY KEY (`id_function`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`power`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`power` (
  `id_power` INT NOT NULL AUTO_INCREMENT,
  `power_name` VARCHAR(128) NULL,
  PRIMARY KEY (`id_power`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`role`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`role` (
  `function_id_function` INT NOT NULL,
  `account_id_account` INT NOT NULL,
  PRIMARY KEY (`function_id_function`, `account_id_account`),
  INDEX `fk_function_has_account_account1_idx` (`account_id_account` ASC),
  INDEX `fk_function_has_account_function_idx` (`function_id_function` ASC),
  CONSTRAINT `fk_function_has_account_function`
    FOREIGN KEY (`function_id_function`)
    REFERENCES `mydb`.`function` (`id_function`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_function_has_account_account1`
    FOREIGN KEY (`account_id_account`)
    REFERENCES `mydb`.`account` (`id_account`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`power_assignment`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`power_assignment` (
  `account_id_account` INT NOT NULL,
  `power_id_power` INT NOT NULL,
  PRIMARY KEY (`account_id_account`, `power_id_power`),
  INDEX `fk_account_has_power_power1_idx` (`power_id_power` ASC),
  INDEX `fk_account_has_power_account1_idx` (`account_id_account` ASC),
  CONSTRAINT `fk_account_has_power_account1`
    FOREIGN KEY (`account_id_account`)
    REFERENCES `mydb`.`account` (`id_account`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_account_has_power_power1`
    FOREIGN KEY (`power_id_power`)
    REFERENCES `mydb`.`power` (`id_power`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`yacht`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`yacht` (
  `id_yacht` INT NOT NULL AUTO_INCREMENT,
  `yacht_name` VARCHAR(128) NOT NULL,
  `type` VARCHAR(128) NOT NULL,
  `length` FLOAT NOT NULL,
  `width` FLOAT NOT NULL,
  `draft` FLOAT NOT NULL,
  `sailed_surface` FLOAT NOT NULL,
  `crew_number` INT NOT NULL,
  `remarks` VARCHAR(256) NULL,
  `isAvailable` TINYINT(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id_yacht`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`order`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`order` (
  `id_order` INT NOT NULL AUTO_INCREMENT,
  `request_date` DATETIME NOT NULL,
  `isAccepted` TINYINT(1) NOT NULL DEFAULT 0,
  `isReleased` TINYINT(1) NOT NULL,
  `start_date` DATETIME NOT NULL,
  `end_date` DATETIME NOT NULL,
  `account_id_account` INT NOT NULL,
  `yacht_id_yacht` INT NOT NULL,
  `account_id_account1` INT NULL,
  `account_id_account2` INT NULL,
  `isReturned` TINYINT(1) NOT NULL DEFAULT 0,
  `orderType` VARCHAR(64) NULL,
  PRIMARY KEY (`id_order`),
  INDEX `fk_order_account1_idx` (`account_id_account` ASC),
  INDEX `fk_order_yacht1_idx` (`yacht_id_yacht` ASC),
  INDEX `fk_order_account2_idx` (`account_id_account1` ASC),
  INDEX `fk_order_account3_idx` (`account_id_account2` ASC),
  CONSTRAINT `fk_order_account1`
    FOREIGN KEY (`account_id_account`)
    REFERENCES `mydb`.`account` (`id_account`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_order_yacht1`
    FOREIGN KEY (`yacht_id_yacht`)
    REFERENCES `mydb`.`yacht` (`id_yacht`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_order_account2`
    FOREIGN KEY (`account_id_account1`)
    REFERENCES `mydb`.`account` (`id_account`)
    ON DELETE SET NULL
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_order_account3`
    FOREIGN KEY (`account_id_account2`)
    REFERENCES `mydb`.`account` (`id_account`)
    ON DELETE SET NULL
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`status`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`status` (
  `id_status` INT NOT NULL AUTO_INCREMENT,
  `status_date` DATETIME NOT NULL,
  `conditon` VARCHAR(64) NOT NULL,
  `yacht_id_yacht` INT NOT NULL,
  `sailing_possibility` TINYINT(1) NOT NULL,
  `account_id_account` INT NOT NULL,
  PRIMARY KEY (`id_status`),
  INDEX `fk_status_yacht1_idx` (`yacht_id_yacht` ASC),
  INDEX `fk_status_account1_idx` (`account_id_account` ASC),
  CONSTRAINT `fk_status_yacht1`
    FOREIGN KEY (`yacht_id_yacht`)
    REFERENCES `mydb`.`yacht` (`id_yacht`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_status_account1`
    FOREIGN KEY (`account_id_account`)
    REFERENCES `mydb`.`account` (`id_account`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


