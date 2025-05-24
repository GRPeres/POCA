CREATE DATABASE  IF NOT EXISTS `db_poca` /*!40100 DEFAULT CHARACTER SET utf8mb3 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `db_poca`;
-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: localhost    Database: db_poca
-- ------------------------------------------------------
-- Server version	8.0.32

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `tb_questoes`
--

DROP TABLE IF EXISTS `tb_questoes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_questoes` (
  `id_questao` int NOT NULL AUTO_INCREMENT,
  `enunciado_questao` varchar(200) NOT NULL,
  `respostacerta_questao` varchar(100) NOT NULL,
  `respostaerrada1_questao` varchar(100) NOT NULL,
  `respostaerrada2_questao` varchar(100) NOT NULL,
  `respostaerrada3_questao` varchar(100) NOT NULL,
  `dificuldade_questao` enum('Fácil','Médio','Difícil') NOT NULL,
  `tema_questao` enum('Teoria','Programação') NOT NULL,
  PRIMARY KEY (`id_questao`),
  UNIQUE KEY `id_questao_UNIQUE` (`id_questao`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_questoes`
--

LOCK TABLES `tb_questoes` WRITE;
/*!40000 ALTER TABLE `tb_questoes` DISABLE KEYS */;
INSERT INTO `tb_questoes` VALUES (1,'Este é um teste?','Sim.','Não!','Chute errado.','Também não!','Fácil','Teoria'),(2,'Este é um teste?','Sim.','Não!','Chute errado.','Também não!','Médio','Programação');
/*!40000 ALTER TABLE `tb_questoes` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-05-23 15:10:01
