-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: j10c211.p.ssafy.io    Database: kokopang
-- ------------------------------------------------------
-- Server version	8.0.36-0ubuntu0.20.04.1

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
-- Table structure for table `UserProfile`
--

DROP TABLE IF EXISTS `UserProfile`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `UserProfile` (
  `id` int NOT NULL AUTO_INCREMENT,
  `userId` int DEFAULT NULL,
  `saveFolder` varchar(255) DEFAULT NULL,
  `originalName` varchar(255) DEFAULT NULL,
  `saveName` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=38 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `UserProfile`
--

LOCK TABLES `UserProfile` WRITE;
/*!40000 ALTER TABLE `UserProfile` DISABLE KEYS */;
INSERT INTO `UserProfile` VALUES (5,0,'240402_profile','KIM_01.png','965acaf7-3e85-411e-8ae2-3b6e5af91a90.png'),(25,91,'240403_profile','8B08038E-9247-40EC-9284-58C82EBE667F.jpg','c1206765-3425-4b37-846f-96b77e0477a4.jpg'),(26,93,'240403_profile','해킹당한날 24.3.29.PNG','88805024-e333-4a98-bcf2-acd470a4ca2f.PNG'),(27,105,'240403_profile','케로피.png','769a9692-dc9b-4b41-af40-566ad40e6835.png'),(28,44,'240403_profile','뚱이.jfif','28a944c0-e1ba-442d-bc23-43eeee49fef4.jfif'),(33,43,'240403_profile','image (5).png','333b9b51-e5ef-49c2-adbb-e39cb1269edc.png'),(34,0,'240403_profile','KIM_01.png','88fdf313-7586-434f-8944-bbe8cd39d468.png'),(35,51,'240403_profile','KIM_01.png','1c08a7d0-8a9b-4479-8e58-a70bcc754fbf.png'),(36,50,'240403_profile','불.png','5bac301f-f51c-4483-b241-e639eeb806b6.png'),(37,68,'240404_profile','koko_logo.png','bdca32d5-d009-4bb9-9730-1af9ec71ef23.png');
/*!40000 ALTER TABLE `UserProfile` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-04-04  9:32:15
