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
-- Table structure for table `tb_professores_has_tb_pessoas`
--

DROP TABLE IF EXISTS `tb_professores_has_tb_pessoas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_professores_has_tb_pessoas` (
  `tb_professores_id_professor` int NOT NULL,
  `tb_pessoas_id_pessoa` int NOT NULL,
  PRIMARY KEY (`tb_professores_id_professor`,`tb_pessoas_id_pessoa`),
  KEY `fk_tb_professores_has_tb_pessoas_tb_pessoas1_idx` (`tb_pessoas_id_pessoa`),
  KEY `fk_tb_professores_has_tb_pessoas_tb_professores1_idx` (`tb_professores_id_professor`),
  CONSTRAINT `fk_tb_professores_has_tb_pessoas_tb_pessoas1` FOREIGN KEY (`tb_pessoas_id_pessoa`) REFERENCES `tb_pessoas` (`id_pessoa`),
  CONSTRAINT `fk_tb_professores_has_tb_pessoas_tb_professores1` FOREIGN KEY (`tb_professores_id_professor`) REFERENCES `tb_professores` (`id_professor`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_professores_has_tb_pessoas`
--

LOCK TABLES `tb_professores_has_tb_pessoas` WRITE;
/*!40000 ALTER TABLE `tb_professores_has_tb_pessoas` DISABLE KEYS */;
INSERT INTO `tb_professores_has_tb_pessoas` VALUES (1,2),(2,4);
/*!40000 ALTER TABLE `tb_professores_has_tb_pessoas` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-05-31 12:59:12
