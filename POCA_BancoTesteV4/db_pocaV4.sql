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


-- -----------------------------------------------------
-- Table `db_poca`.`tb_alunos`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `db_poca`.`tb_alunos` ;

CREATE TABLE IF NOT EXISTS `db_poca`.`tb_alunos` (
  `id_aluno` INT NOT NULL AUTO_INCREMENT,
  `nome_aluno` VARCHAR(45) NOT NULL,
  `idade_aluno` INT NOT NULL,
  `progresso_aluno` INT NOT NULL,
  `contato_aluno` VARCHAR(45) NOT NULL,
  `login_aluno` VARCHAR(45) NOT NULL,
  `senha_aluno` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id_aluno`),
  UNIQUE INDEX `nome_aluno_UNIQUE` (`nome_aluno` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `db_poca`.`tb_professores`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `db_poca`.`tb_professores` ;

CREATE TABLE IF NOT EXISTS `db_poca`.`tb_professores` (
  `id_professor` INT NOT NULL AUTO_INCREMENT,
  `nome_professor` VARCHAR(45) NOT NULL,
  `materia_professor` VARCHAR(45) NOT NULL,
  `contato_professor` VARCHAR(45) NOT NULL,
  `login_professor` VARCHAR(45) NOT NULL,
  `senha_professor` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id_professor`),
  UNIQUE INDEX `nome_professor_UNIQUE` (`nome_professor` ASC) VISIBLE,
  UNIQUE INDEX `contato_professor_UNIQUE` (`contato_professor` ASC) VISIBLE,
  UNIQUE INDEX `login_professor_UNIQUE` (`login_professor` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `db_poca`.`tb_materias`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `db_poca`.`tb_materias` ;

CREATE TABLE IF NOT EXISTS `db_poca`.`tb_materias` (
  `id_materia` INT NOT NULL AUTO_INCREMENT,
  `id_professor_materia` INT NULL,
  `id_aluno_materia` INT NULL,
  PRIMARY KEY (`id_materia`),
  INDEX `id_professor_materia_idx` (`id_professor_materia` ASC) VISIBLE,
  INDEX `id_aluno_materia_idx` (`id_aluno_materia` ASC) VISIBLE,
  CONSTRAINT `id_professor_materia`
    FOREIGN KEY (`id_professor_materia`)
    REFERENCES `db_poca`.`tb_professores` (`id_professor`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `id_aluno_materia`
    FOREIGN KEY (`id_aluno_materia`)
    REFERENCES `db_poca`.`tb_alunos` (`id_aluno`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
