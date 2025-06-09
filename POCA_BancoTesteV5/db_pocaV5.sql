-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema db_poca
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema db_poca
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `db_poca` DEFAULT CHARACTER SET utf8mb3 ;
USE `db_poca` ;

-- -----------------------------------------------------
-- Table `db_poca`.`tb_alunos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `db_poca`.`tb_alunos` (
  `id_aluno` INT NOT NULL AUTO_INCREMENT,
  `nome_aluno` VARCHAR(45) NOT NULL,
  `idade_aluno` int NOT NULL,
  `progresso_aluno` INT NOT NULL,
  `contato_aluno` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id_aluno`),
  UNIQUE INDEX `nome_aluno_UNIQUE` (`nome_aluno` ASC) VISIBLE,
  UNIQUE INDEX `id_aluno_UNIQUE` (`id_aluno` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `db_poca`.`tb_materias`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `db_poca`.`tb_materias` (
  `id_materia` INT NOT NULL AUTO_INCREMENT,
  `nome_materia` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`id_materia`),
  UNIQUE INDEX `id_materia_UNIQUE` (`id_materia` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `db_poca`.`tb_alunos_has_tb_materias`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `db_poca`.`tb_alunos_has_tb_materias` (
  `tb_alunos_id_aluno` INT NOT NULL,
  `tb_materias_id_materia` INT NOT NULL,
  PRIMARY KEY (`tb_alunos_id_aluno`, `tb_materias_id_materia`),
  INDEX `fk_tb_alunos_has_tb_materias_tb_materias1_idx` (`tb_materias_id_materia` ASC) VISIBLE,
  INDEX `fk_tb_alunos_has_tb_materias_tb_alunos1_idx` (`tb_alunos_id_aluno` ASC) VISIBLE,
  CONSTRAINT `fk_tb_alunos_has_tb_materias_tb_alunos1`
    FOREIGN KEY (`tb_alunos_id_aluno`)
    REFERENCES `db_poca`.`tb_alunos` (`id_aluno`),
  CONSTRAINT `fk_tb_alunos_has_tb_materias_tb_materias1`
    FOREIGN KEY (`tb_materias_id_materia`)
    REFERENCES `db_poca`.`tb_materias` (`id_materia`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `db_poca`.`tb_atividades`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `db_poca`.`tb_atividades` (
  `id_atividade` INT NOT NULL AUTO_INCREMENT,
  `nome_atividade` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id_atividade`),
  UNIQUE INDEX `nome_atividade_UNIQUE` (`nome_atividade` ASC) VISIBLE,
  UNIQUE INDEX `id_atividade_UNIQUE` (`id_atividade` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `db_poca`.`tb_atividades_has_tb_materias`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `db_poca`.`tb_atividades_has_tb_materias` (
  `tb_atividades_id_atividade` INT NOT NULL,
  `tb_materias_id_materia` INT NOT NULL,
  PRIMARY KEY (`tb_atividades_id_atividade`, `tb_materias_id_materia`),
  INDEX `fk_tb_atividades_has_tb_materias_tb_materias1_idx` (`tb_materias_id_materia` ASC) VISIBLE,
  INDEX `fk_tb_atividades_has_tb_materias_tb_atividades1_idx` (`tb_atividades_id_atividade` ASC) VISIBLE,
  CONSTRAINT `fk_tb_atividades_has_tb_materias_tb_atividades1`
    FOREIGN KEY (`tb_atividades_id_atividade`)
    REFERENCES `db_poca`.`tb_atividades` (`id_atividade`),
  CONSTRAINT `fk_tb_atividades_has_tb_materias_tb_materias1`
    FOREIGN KEY (`tb_materias_id_materia`)
    REFERENCES `db_poca`.`tb_materias` (`id_materia`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `db_poca`.`tb_pessoas`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `db_poca`.`tb_pessoas` (
  `id_pessoa` INT NOT NULL AUTO_INCREMENT,
  `login_pessoa` VARCHAR(45) NOT NULL,
  `senha_pessoa` VARCHAR(45) NOT NULL,
  `bool_professor_pessoa` TINYINT NOT NULL,
  PRIMARY KEY (`id_pessoa`),
  UNIQUE INDEX `login_pessoa_UNIQUE` (`login_pessoa` ASC) VISIBLE,
  UNIQUE INDEX `id_pessoa_UNIQUE` (`id_pessoa` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `db_poca`.`tb_pessoas_has_tb_alunos`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `db_poca`.`tb_pessoas_has_tb_alunos` (
  `tb_pessoas_id_pessoa` INT NOT NULL,
  `tb_alunos_id_aluno` INT NOT NULL,
  PRIMARY KEY (`tb_pessoas_id_pessoa`, `tb_alunos_id_aluno`),
  INDEX `fk_tb_pessoas_has_tb_alunos_tb_alunos1_idx` (`tb_alunos_id_aluno` ASC) VISIBLE,
  INDEX `fk_tb_pessoas_has_tb_alunos_tb_pessoas1_idx` (`tb_pessoas_id_pessoa` ASC) VISIBLE,
  CONSTRAINT `fk_tb_pessoas_has_tb_alunos_tb_alunos1`
    FOREIGN KEY (`tb_alunos_id_aluno`)
    REFERENCES `db_poca`.`tb_alunos` (`id_aluno`),
  CONSTRAINT `fk_tb_pessoas_has_tb_alunos_tb_pessoas1`
    FOREIGN KEY (`tb_pessoas_id_pessoa`)
    REFERENCES `db_poca`.`tb_pessoas` (`id_pessoa`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `db_poca`.`tb_professores`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `db_poca`.`tb_professores` (
  `id_professor` INT NOT NULL AUTO_INCREMENT,
  `nome_professor` VARCHAR(45) NOT NULL,
  `materia_professor` VARCHAR(45) NOT NULL,
  `contato_professor` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id_professor`),
  UNIQUE INDEX `nome_professor_UNIQUE` (`nome_professor` ASC) VISIBLE,
  UNIQUE INDEX `contato_professor_UNIQUE` (`contato_professor` ASC) VISIBLE,
  UNIQUE INDEX `id_professor_UNIQUE` (`id_professor` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `db_poca`.`tb_professores_has_tb_materias`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `db_poca`.`tb_professores_has_tb_materias` (
  `tb_professores_id_professor` INT NOT NULL,
  `tb_materias_id_materia` INT NOT NULL,
  PRIMARY KEY (`tb_professores_id_professor`, `tb_materias_id_materia`),
  INDEX `fk_tb_professores_has_tb_materias_tb_materias1_idx` (`tb_materias_id_materia` ASC) VISIBLE,
  INDEX `fk_tb_professores_has_tb_materias_tb_professores_idx` (`tb_professores_id_professor` ASC) VISIBLE,
  CONSTRAINT `fk_tb_professores_has_tb_materias_tb_materias1`
    FOREIGN KEY (`tb_materias_id_materia`)
    REFERENCES `db_poca`.`tb_materias` (`id_materia`),
  CONSTRAINT `fk_tb_professores_has_tb_materias_tb_professores`
    FOREIGN KEY (`tb_professores_id_professor`)
    REFERENCES `db_poca`.`tb_professores` (`id_professor`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `db_poca`.`tb_professores_has_tb_pessoas`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `db_poca`.`tb_professores_has_tb_pessoas` (
  `tb_professores_id_professor` INT NOT NULL,
  `tb_pessoas_id_pessoa` INT NOT NULL,
  PRIMARY KEY (`tb_professores_id_professor`, `tb_pessoas_id_pessoa`),
  INDEX `fk_tb_professores_has_tb_pessoas_tb_pessoas1_idx` (`tb_pessoas_id_pessoa` ASC) VISIBLE,
  INDEX `fk_tb_professores_has_tb_pessoas_tb_professores1_idx` (`tb_professores_id_professor` ASC) VISIBLE,
  CONSTRAINT `fk_tb_professores_has_tb_pessoas_tb_pessoas1`
    FOREIGN KEY (`tb_pessoas_id_pessoa`)
    REFERENCES `db_poca`.`tb_pessoas` (`id_pessoa`),
  CONSTRAINT `fk_tb_professores_has_tb_pessoas_tb_professores1`
    FOREIGN KEY (`tb_professores_id_professor`)
    REFERENCES `db_poca`.`tb_professores` (`id_professor`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `db_poca`.`tb_questoes`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `db_poca`.`tb_questoes` (
  `id_questao` INT NOT NULL AUTO_INCREMENT,
  `enunciado_questao` VARCHAR(200) NOT NULL,
  `respostacerta_questao` VARCHAR(100) NOT NULL,
  `respostaerrada1_questao` VARCHAR(100) NOT NULL,
  `respostaerrada2_questao` VARCHAR(100) NOT NULL,
  `respostaerrada3_questao` VARCHAR(100) NOT NULL,
  `dificuldade_questao` ENUM('Fácil', 'Médio', 'Difícil') NOT NULL,
  `tema_questao` VARCHAR(30) NOT NULL,
  PRIMARY KEY (`id_questao`),
  UNIQUE INDEX `id_questao_UNIQUE` (`id_questao` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `db_poca`.`tb_questoes_has_tb_atividades`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `db_poca`.`tb_questoes_has_tb_atividades` (
  `tb_questoes_id_questao` INT NOT NULL,
  `tb_atividades_id_atividade` INT NOT NULL,
  PRIMARY KEY (`tb_questoes_id_questao`, `tb_atividades_id_atividade`),
  INDEX `fk_tb_questoes_has_tb_atividades_tb_atividades1_idx` (`tb_atividades_id_atividade` ASC) VISIBLE,
  INDEX `fk_tb_questoes_has_tb_atividades_tb_questoes1_idx` (`tb_questoes_id_questao` ASC) VISIBLE,
  CONSTRAINT `fk_tb_questoes_has_tb_atividades_tb_atividades1`
    FOREIGN KEY (`tb_atividades_id_atividade`)
    REFERENCES `db_poca`.`tb_atividades` (`id_atividade`),
  CONSTRAINT `fk_tb_questoes_has_tb_atividades_tb_questoes1`
    FOREIGN KEY (`tb_questoes_id_questao`)
    REFERENCES `db_poca`.`tb_questoes` (`id_questao`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
