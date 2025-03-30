-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema db_poca
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `db_poca` ;

-- -----------------------------------------------------
-- Schema db_poca
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `db_poca` DEFAULT CHARACTER SET utf8mb3 ;
USE `db_poca` ;

-- -----------------------------------------------------
-- Table `db_poca`.`tb_questoes`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `db_poca`.`tb_questoes` ;

CREATE TABLE IF NOT EXISTS `db_poca`.`tb_questoes` (
  `id_questao` INT NOT NULL AUTO_INCREMENT,
  `enunciado_questao` VARCHAR(200) NOT NULL,
  `respostacerta_questao` VARCHAR(100) NOT NULL,
  `respostaerrada1_questao` VARCHAR(100) NOT NULL,
  `respostaerrada2_questao` VARCHAR(100) NOT NULL,
  `respostaerrada3_questao` VARCHAR(100) NOT NULL,
  `dificuldade_questao` ENUM('Fácil', 'Médio', 'Difícil') NOT NULL,
  `tema_questao` ENUM('Teoria', 'Programação') NOT NULL,
  PRIMARY KEY (`id_questao`))
ENGINE = InnoDB
AUTO_INCREMENT = 2
DEFAULT CHARACTER SET = utf8mb3;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
