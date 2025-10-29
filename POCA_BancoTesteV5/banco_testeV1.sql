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
-- Table structure for table `tb_alunos`
--

DROP TABLE IF EXISTS `tb_alunos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_alunos` (
  `id_aluno` int NOT NULL AUTO_INCREMENT,
  `nome_aluno` varchar(45) NOT NULL,
  `idade_aluno` int NOT NULL,
  `progresso_aluno` int NOT NULL,
  `contato_aluno` varchar(45) NOT NULL,
  PRIMARY KEY (`id_aluno`),
  UNIQUE KEY `nome_aluno_UNIQUE` (`nome_aluno`),
  UNIQUE KEY `id_aluno_UNIQUE` (`id_aluno`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_alunos`
--

LOCK TABLES `tb_alunos` WRITE;
/*!40000 ALTER TABLE `tb_alunos` DISABLE KEYS */;
INSERT INTO `tb_alunos` (`id_aluno`, `nome_aluno`, `nascimento_aluno`, `progresso_aluno`, `contato_aluno`, `email_aluno`) VALUES(1,'Alice Silva','2010-01-01',40,NULL,'alice@email.com'),(2,'Carla Souza','2000-01-01',60,NULL,'carla@email.com'),(3,'Ellen Moura','1989-01-01',75,NULL,'ellen@email.com'),(4,'Felipe Costa','1941-01-01',30,NULL,'felipe@email.com'),(5,'Gabriela Martins','2013-01-01',50,NULL,'gabriela@email.com'),(6,'Heitor Oliveira','1969-01-01',80,NULL,'heitor@email.com');
/*!40000 ALTER TABLE `tb_alunos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_alunos_has_tb_materias`
--

DROP TABLE IF EXISTS `tb_alunos_has_tb_materias`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_alunos_has_tb_materias` (
  `tb_alunos_id_aluno` int NOT NULL,
  `tb_materias_id_materia` int NOT NULL,
  PRIMARY KEY (`tb_alunos_id_aluno`,`tb_materias_id_materia`),
  KEY `fk_tb_alunos_has_tb_materias_tb_materias1_idx` (`tb_materias_id_materia`),
  KEY `fk_tb_alunos_has_tb_materias_tb_alunos1_idx` (`tb_alunos_id_aluno`),
  CONSTRAINT `fk_tb_alunos_has_tb_materias_tb_alunos1` FOREIGN KEY (`tb_alunos_id_aluno`) REFERENCES `tb_alunos` (`id_aluno`),
  CONSTRAINT `fk_tb_alunos_has_tb_materias_tb_materias1` FOREIGN KEY (`tb_materias_id_materia`) REFERENCES `tb_materias` (`id_materia`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_alunos_has_tb_materias`
--

LOCK TABLES `tb_alunos_has_tb_materias` WRITE;
/*!40000 ALTER TABLE `tb_alunos_has_tb_materias` DISABLE KEYS */;
INSERT INTO `tb_alunos_has_tb_materias` VALUES (1,1),(5,1),(2,2),(3,3),(6,3),(4,4);
/*!40000 ALTER TABLE `tb_alunos_has_tb_materias` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_atividades`
--

DROP TABLE IF EXISTS `tb_atividades`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_atividades` (
  `id_atividade` int NOT NULL AUTO_INCREMENT,
  `nome_atividade` varchar(45) NOT NULL,
  PRIMARY KEY (`id_atividade`),
  UNIQUE KEY `nome_atividade_UNIQUE` (`nome_atividade`),
  UNIQUE KEY `id_atividade_UNIQUE` (`id_atividade`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_atividades`
--

LOCK TABLES `tb_atividades` WRITE;
/*!40000 ALTER TABLE `tb_atividades` DISABLE KEYS */;
INSERT INTO `tb_atividades` VALUES (1,'Atividade 1'),(2,'Atividade 2'),(3,'Atividade 3'),(4,'Atividade 4'),(5,'Atividade 5'),(6,'Atividade 6');
/*!40000 ALTER TABLE `tb_atividades` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_atividades_has_tb_materias`
--

DROP TABLE IF EXISTS `tb_atividades_has_tb_materias`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_atividades_has_tb_materias` (
  `tb_atividades_id_atividade` int NOT NULL,
  `tb_materias_id_materia` int NOT NULL,
  PRIMARY KEY (`tb_atividades_id_atividade`,`tb_materias_id_materia`),
  KEY `fk_tb_atividades_has_tb_materias_tb_materias1_idx` (`tb_materias_id_materia`),
  KEY `fk_tb_atividades_has_tb_materias_tb_atividades1_idx` (`tb_atividades_id_atividade`),
  CONSTRAINT `fk_tb_atividades_has_tb_materias_tb_atividades1` FOREIGN KEY (`tb_atividades_id_atividade`) REFERENCES `tb_atividades` (`id_atividade`),
  CONSTRAINT `fk_tb_atividades_has_tb_materias_tb_materias1` FOREIGN KEY (`tb_materias_id_materia`) REFERENCES `tb_materias` (`id_materia`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_atividades_has_tb_materias`
--

LOCK TABLES `tb_atividades_has_tb_materias` WRITE;
/*!40000 ALTER TABLE `tb_atividades_has_tb_materias` DISABLE KEYS */;
INSERT INTO `tb_atividades_has_tb_materias` VALUES (1,1),(2,2),(5,2),(3,3),(6,3),(4,4);
/*!40000 ALTER TABLE `tb_atividades_has_tb_materias` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_materias`
--

DROP TABLE IF EXISTS `tb_materias`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_materias` (
  `id_materia` int NOT NULL AUTO_INCREMENT,
  `nome_materia` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id_materia`),
  UNIQUE KEY `id_materia_UNIQUE` (`id_materia`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_materias`
--

LOCK TABLES `tb_materias` WRITE;
/*!40000 ALTER TABLE `tb_materias` DISABLE KEYS */;
INSERT INTO `tb_materias` VALUES (1,'Lógica'),(2,'Python'),(3,'Banco de Dados'),(4,'História da Computação');
/*!40000 ALTER TABLE `tb_materias` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_pessoas`
--

DROP TABLE IF EXISTS `tb_pessoas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_pessoas` (
  `id_pessoa` int NOT NULL AUTO_INCREMENT,
  `login_pessoa` varchar(45) NOT NULL,
  `senha_pessoa` varchar(45) NOT NULL,
  `bool_professor_pessoa` tinyint NOT NULL,
  PRIMARY KEY (`id_pessoa`),
  UNIQUE KEY `login_pessoa_UNIQUE` (`login_pessoa`),
  UNIQUE KEY `id_pessoa_UNIQUE` (`id_pessoa`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_pessoas`
--

LOCK TABLES `tb_pessoas` WRITE;
/*!40000 ALTER TABLE `tb_pessoas` DISABLE KEYS */;
INSERT INTO `tb_pessoas` VALUES (1,'alice','senha123',0),(2,'bob','senha456',1),(3,'carla','senha789',0),(4,'daniel','senha000',1),(5,'ellen','senha321',0),(6,'felipe','senha111',0),(7,'gabriela','senha222',0),(8,'heitor','senha333',0),(9,'isabela','senha444',1),(10,'joao','senha555',1);
/*!40000 ALTER TABLE `tb_pessoas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_pessoas_has_tb_alunos`
--

DROP TABLE IF EXISTS `tb_pessoas_has_tb_alunos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_pessoas_has_tb_alunos` (
  `tb_pessoas_id_pessoa` int NOT NULL,
  `tb_alunos_id_aluno` int NOT NULL,
  PRIMARY KEY (`tb_pessoas_id_pessoa`,`tb_alunos_id_aluno`),
  KEY `fk_tb_pessoas_has_tb_alunos_tb_alunos1_idx` (`tb_alunos_id_aluno`),
  KEY `fk_tb_pessoas_has_tb_alunos_tb_pessoas1_idx` (`tb_pessoas_id_pessoa`),
  CONSTRAINT `fk_tb_pessoas_has_tb_alunos_tb_alunos1` FOREIGN KEY (`tb_alunos_id_aluno`) REFERENCES `tb_alunos` (`id_aluno`),
  CONSTRAINT `fk_tb_pessoas_has_tb_alunos_tb_pessoas1` FOREIGN KEY (`tb_pessoas_id_pessoa`) REFERENCES `tb_pessoas` (`id_pessoa`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_pessoas_has_tb_alunos`
--

LOCK TABLES `tb_pessoas_has_tb_alunos` WRITE;
/*!40000 ALTER TABLE `tb_pessoas_has_tb_alunos` DISABLE KEYS */;
INSERT INTO `tb_pessoas_has_tb_alunos` VALUES (1,1),(3,2),(5,3),(6,4),(7,5),(8,6);
/*!40000 ALTER TABLE `tb_pessoas_has_tb_alunos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_professores`
--

DROP TABLE IF EXISTS `tb_professores`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_professores` (
  `id_professor` int NOT NULL AUTO_INCREMENT,
  `nome_professor` varchar(45) NOT NULL,
  `materia_professor` varchar(45) NOT NULL,
  `contato_professor` varchar(45) NOT NULL,
  PRIMARY KEY (`id_professor`),
  UNIQUE KEY `nome_professor_UNIQUE` (`nome_professor`),
  UNIQUE KEY `contato_professor_UNIQUE` (`contato_professor`),
  UNIQUE KEY `id_professor_UNIQUE` (`id_professor`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_professores`
--

LOCK TABLES `tb_professores` WRITE;
/*!40000 ALTER TABLE `tb_professores` DISABLE KEYS */;
INSERT INTO `tb_professores` VALUES (1,'Bob Lima','Matemática','bob@email.com'),(2,'Daniel Rocha','Programação','daniel@email.com'),(3,'Isabela Santos','História da Computação','isabela@email.com'),(4,'João Pereira','Inglês Técnico','joao@email.com');
/*!40000 ALTER TABLE `tb_professores` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_professores_has_tb_materias`
--

DROP TABLE IF EXISTS `tb_professores_has_tb_materias`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_professores_has_tb_materias` (
  `tb_professores_id_professor` int NOT NULL,
  `tb_materias_id_materia` int NOT NULL,
  PRIMARY KEY (`tb_professores_id_professor`,`tb_materias_id_materia`),
  KEY `fk_tb_professores_has_tb_materias_tb_materias1_idx` (`tb_materias_id_materia`),
  KEY `fk_tb_professores_has_tb_materias_tb_professores_idx` (`tb_professores_id_professor`),
  CONSTRAINT `fk_tb_professores_has_tb_materias_tb_materias1` FOREIGN KEY (`tb_materias_id_materia`) REFERENCES `tb_materias` (`id_materia`),
  CONSTRAINT `fk_tb_professores_has_tb_materias_tb_professores` FOREIGN KEY (`tb_professores_id_professor`) REFERENCES `tb_professores` (`id_professor`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_professores_has_tb_materias`
--

LOCK TABLES `tb_professores_has_tb_materias` WRITE;
/*!40000 ALTER TABLE `tb_professores_has_tb_materias` DISABLE KEYS */;
INSERT INTO `tb_professores_has_tb_materias` VALUES (1,1),(2,2),(2,3),(3,4),(4,4);
/*!40000 ALTER TABLE `tb_professores_has_tb_materias` ENABLE KEYS */;
UNLOCK TABLES;

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
INSERT INTO `tb_professores_has_tb_pessoas` VALUES (1,2),(2,4),(3,9),(4,10);
/*!40000 ALTER TABLE `tb_professores_has_tb_pessoas` ENABLE KEYS */;
UNLOCK TABLES;

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
  `tema_questao` varchar(30) NOT NULL,
  PRIMARY KEY (`id_questao`),
  UNIQUE KEY `id_questao_UNIQUE` (`id_questao`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_questoes`
--

LOCK TABLES `tb_questoes` WRITE;
/*!40000 ALTER TABLE `tb_questoes` DISABLE KEYS */;
INSERT INTO `tb_questoes` VALUES (1,'O que é uma variável?','Espaço na memória para armazenar valores','Uma função','Um operador','Um tipo de dado','Fácil','Teoria'),(2,'Qual comando imprime na tela em Python?','print()','input()','echo','scanf','Fácil','Python'),(3,'Qual estrutura representa uma repetição?','Laço','Função','Classe','Vetor','Médio','Programação'),(4,'Qual comando SQL é usado para inserir dados?','INSERT','CREATE','DELETE','UPDATE','Fácil','SQL'),(5,'Em POO, o que é encapsulamento?','Proteção de dados internos','Herança de características','Polimorfismo de métodos','Abstração de classes','Médio','Programação'),(6,'Qual função converte string para int em Python?','int()','str()','float()','input()','Fácil','Python'),(7,'O que é um JOIN em SQL?','Combinar tabelas','Ordenar resultados','Filtrar registros','Agrupar dados','Médio','SQL'),(8,'Qual é o operador lógico \"OU\" em Python?','or','and','not','xor','Fácil','Python'),(9,'Em qual ano foi criado o Python?','1991','1989','1995','2000','Difícil','História'),(10,'O que significa HTTP?','HyperText Transfer Protocol','High Transfer Text Protocol','Hyper Transfer Text Protocol','High Text Transfer Protocol','Médio','Redes'),(11,'Qual não é um tipo de dado em SQL?','loop','varchar','int','date','Fácil','SQL'),(12,'O que é um DataFrame?','Estrutura de dados tabular','Tipo de gráfico','Função matemática','Protocolo de rede','Médio','Dados'),(13,'Qual linguagem é usada para estilizar páginas web?','CSS','HTML','JavaScript','Python','Fácil','Web'),(14,'O que o comando git commit faz?','Salva alterações localmente','Envia para o repositório','Clona um repositório','Cria um novo branch','Médio','DevOps'),(15,'Qual é a complexidade do algoritmo Bubble Sort?','O(n²)','O(n log n)','O(n)','O(log n)','Difícil','Algoritmos'),(16,'O que é um NFT?','Token não-fungível','Protocolo de rede','Criptomoeda','Framework web','Médio','Blockchain'),(17,'Qual função retorna o tamanho de uma lista?','len()','size()','count()','length()','Fácil','Python'),(18,'O que é uma chave estrangeira?','Referência a outra tabela','Chave primária duplicada','Tipo de criptografia','Restrição de acesso','Médio','SQL'),(19,'Qual comando inicia um servidor Node.js?','node app.js','npm start','node start','npm run','Médio','JavaScript'),(20,'O que é um atributo em UML?','Característica de uma classe','Tipo de relacionamento','Comportamento de objeto','Diagrama de sequência','Difícil','Engenharia de Software'),(21,'Qual método adiciona item no final de uma lista?','append()','insert()','add()','push()','Fácil','Python'),(22,'O que é Big O Notation?','Medida de complexidade','Tipo de algoritmo','Sistema numérico','Padrão de projeto','Difícil','Algoritmos'),(23,'Qual protocolo é usado para e-mails?','SMTP','HTTP','FTP','TCP','Médio','Redes');
/*!40000 ALTER TABLE `tb_questoes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tb_questoes_has_tb_atividades`
--

DROP TABLE IF EXISTS `tb_questoes_has_tb_atividades`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tb_questoes_has_tb_atividades` (
  `tb_questoes_id_questao` int NOT NULL,
  `tb_atividades_id_atividade` int NOT NULL,
  PRIMARY KEY (`tb_questoes_id_questao`,`tb_atividades_id_atividade`),
  KEY `fk_tb_questoes_has_tb_atividades_tb_atividades1_idx` (`tb_atividades_id_atividade`),
  KEY `fk_tb_questoes_has_tb_atividades_tb_questoes1_idx` (`tb_questoes_id_questao`),
  CONSTRAINT `fk_tb_questoes_has_tb_atividades_tb_atividades1` FOREIGN KEY (`tb_atividades_id_atividade`) REFERENCES `tb_atividades` (`id_atividade`),
  CONSTRAINT `fk_tb_questoes_has_tb_atividades_tb_questoes1` FOREIGN KEY (`tb_questoes_id_questao`) REFERENCES `tb_questoes` (`id_questao`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tb_questoes_has_tb_atividades`
--

LOCK TABLES `tb_questoes_has_tb_atividades` WRITE;
/*!40000 ALTER TABLE `tb_questoes_has_tb_atividades` DISABLE KEYS */;
INSERT INTO `tb_questoes_has_tb_atividades` VALUES (1,1),(4,1),(5,1),(6,1),(7,1),(2,2),(8,2),(9,2),(10,2),(11,2),(3,3),(12,3),(13,3),(14,3),(15,3),(16,4),(17,4),(18,4),(19,5),(20,5),(21,5),(22,6),(23,6);
/*!40000 ALTER TABLE `tb_questoes_has_tb_atividades` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'db_poca'
--

--
-- Dumping routines for database 'db_poca'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-05-31 14:51:41

