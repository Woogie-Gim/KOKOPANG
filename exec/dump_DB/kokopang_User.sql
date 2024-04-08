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
-- Table structure for table `User`
--

DROP TABLE IF EXISTS `User`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `User` (
  `id` int NOT NULL AUTO_INCREMENT,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `name` varchar(16) NOT NULL,
  `role` varchar(32) DEFAULT NULL,
  `rating` int DEFAULT NULL,
  `gender` varchar(8) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `email` (`email`),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=126 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `User`
--

LOCK TABLES `User` WRITE;
/*!40000 ALTER TABLE `User` DISABLE KEYS */;
INSERT INTO `User` VALUES (43,'sugo','$2a$10$wd3TXA27KuFGQP2KO6MS9ep6tRxh8IgW/BuITmK0I9Di8WevGeejG','영일이',NULL,1001,NULL),(44,'patrick','$2a$10$i2nxsy4y5uAqlfoJUkgXMelznZOsHNFxhuNnTK6GhymT8ms/LgeNu','뚱이',NULL,1000,NULL),(45,'qq','$2a$10$xZC1vr90ljKPr18PvPsM0Om5AIiAq112sJmO0FR7KGZS91eJTRZ3W','큐큐',NULL,1011,NULL),(46,'yy','$2a$10$nRxgvToOnfyq9RLYVMVVCuT3pM03UnZNG9gF1q9LFpfLvSwUGeIKK','와와',NULL,1000,NULL),(47,'bb','$2a$10$r3lNfHGHjRY6ahAPIyJkD./LUMyTk7mW9xpYlkbjOyPm1WYf.4Vbi','bb',NULL,1000,NULL),(48,'ww','$2a$10$SvuzY/znZo6hHWZrAaD71uAwu23yNJY4yody5NWQPShki1jrGTz/O','더블유',NULL,1000,NULL),(49,'2710yap','$2a$10$D1.Z6Qq45V2Uo/5UU6SPWOFsD4skNabv08gSUv30uX1HYf1uKtyiO','풍기위원쟝',NULL,1000,NULL),(50,'qwer@naver.com','$2a$10$5qXqqYJYs8.rmlr2.OgMEeO3ZFzzQa/W3PedTy.pnqLH3gBWenzie','불코치',NULL,1000,NULL),(51,'kim01@jjang.com','$2a$10$wFRFgRT5Q2lN3a/NaUBm5uGzDfYetOEvXf.TonP8mEYsWa.voLhh.','김영일짱',NULL,1000,NULL),(52,' ssafy@ssafy','$2a$10$lfd5PLEXnpmktMr..a6rbOBlApjtHgz05Zrl1fmx8jC7LsA8SqC0S','테스트용',NULL,1000,NULL),(59,'ee','$2a$10$anbN6I4v8oSx6QwAdvpG9.IXgXBx9CP3I/S908A4oAPFh4FWqcGeK','이이',NULL,1000,NULL),(60,'ssafy@ssafy','$2a$10$nWbgrDxRWMGs71IE0a9xvujk7.jnJe235WWkbZ/AtayIU389odWQC','테스트',NULL,1000,NULL),(61,'ss','$2a$10$Ogtxw4QZOehbj0O14u7iIe34sKp0eP.zQJDEcRzJ/2dQLQkOfJYM.','살사소스',NULL,1013,NULL),(62,'woo@gmail.com','$2a$10$5diRuLd8rPN6Z5hZDqqQVO1Dy77437CW.KBKgfbIxQqlvD.6x/8lq','우와',NULL,1000,NULL),(63,'rr','$2a$10$J0eTYGbyKKPtmxQsEf7WcewTssPMWPPvD9wZ2by1oSUtWSOBpF9zi','알알',NULL,1000,NULL),(64,'tt','$2a$10$CbTY1On4VgXkFiMNVD5EU.k3.6JPzdHOK2ESRJxtSUwMBWuyFrPjC','티티',NULL,1000,NULL),(65,'uu','$2a$10$WFNQ4QovzuZBJlXBMQblfeH90BSW5VzIFrlhUE7B3zb6QcAvmCVkm','유유',NULL,1000,NULL),(66,'jdm','$2a$10$.Hxnx/4x8ftzbaCVeiG7Ke5YOs9ND2aFkhqJqmVAt2fMJvaVe.JFe','징더다맥',NULL,1000,NULL),(67,'haha@hoho.com','$2a$10$lXNrRAMA1MUIxXkE9OXOOORyRdHt7thkskw1/Cz41Bu.4VZRXkbCW','하하호호',NULL,1000,NULL),(68,'gkddn','$2a$10$fgzweodgGufIVgp8QGZ3J./3VDvHnbPQh5rPPeQtNqQyVlyMnkTya','나는이항우',NULL,1014,NULL),(69,'asd','$2a$10$3Ax9hapmwn/mN9qcnW/T5en5GWBFy9wcdooGksitMM/73/4bYlAda','asd',NULL,1000,NULL),(70,'blzbtn0923@gmail.com','$2a$10$tGALTtWeHamxhHoMsNs/P.KmprYypd4nwVCdpAoyR3y/Q0//XE1iu','김중광',NULL,1000,NULL),(71,'sss@naver.com','$2a$10$dR1plRXr/bM6popkDNvAKe5vt5kT8huiiVIdG7L5J/29XKTG7J/wO','싸피',NULL,1000,NULL),(72,'power649@naver.com','$2a$10$GbRNTxEdINckHWmifA8tQO2onO5YS4WK3awxIQIMc/cHJhPSafBvW','항우바보',NULL,1320,NULL),(73,'ww71er@naver.com','$2a$10$7vbf2G0objpyIxqFV6oY1OGENz6SlF./A8iHKC5X4REwYP9Eo3/b6','아리무',NULL,1000,NULL),(74,'2@2','$2a$10$El2e2X.Wo5kANuas9fREOO/HX5ZJRKDun6fIdxKxeu4c1LBxpbb8K','주현짱',NULL,1000,NULL),(75,'gogwang@ssafy.com','$2a$10$h8YGm8DZlCdQiLeWG3xGpOEkfCnqvROEFY3nkxwydArZe5r2nS4Ka','WEED',NULL,1000,NULL),(76,'test000@test.com','$2a$10$jnWozcfA06.jMWRGdLvfxeJ.Kb8NnZOYGkSeHs6uRpIrcm/k.Uo5C','빨간장갑',NULL,1000,NULL),(77,'duddlfzld@naver.com','$2a$10$pc/ERnROwfEHQr3o6l.u.eK64DL8.LCrL1VpWs1YOHjpWTsIKJWG2','영일킹',NULL,1000,NULL),(78,'1@1','$2a$10$wNO6M0nWtxp2r.Dy.ieh4e8w0Uz2jZG5VViDIcWoq7b08dghlt7n.','킹',NULL,1000,NULL),(79,'duddlf@duddlf','$2a$10$FIlJm9EbypJWBuQhAXBhkOqE/Kky6laPjKHuwrcp3G/2o.IpHDPrS','항우짜응',NULL,1000,NULL),(80,'sirika2547@naver.com','$2a$10$kxC0f7vlgQk394uT9ZQDuOgBPmvjF53M5LYCEftM5lPw1fmJttyuW','뀨쀼뀨쀼',NULL,1000,NULL),(81,'ssafy@gmail.com','$2a$10$GWvtV1x8czGRkx0j81EIw.79uCrT6GX0gHorgPMyETeaqa6qKpKS2','김싸피',NULL,1000,NULL),(82,'koko@pang.com','$2a$10$yvIdpRF7hq1ry15Jc2yYC.dD6mj/0GHjfXmIj41v394mz2WoifJci','코코팡팡',NULL,1000,NULL),(83,'ssafy@abc.com','$2a$10$e.SPLLtXsWfUmeEO/UUy..4zANU/YZkwDyAYFTo6UXhofBoHn5rBm','짜라빠빠',NULL,1000,NULL),(84,'gabgitalja2166@gmail.com','$2a$10$zbWyVIiJg5259A0Vrz.AwO7/YbE/36xg1P2eMP578oP6JCBVm3YfG','gabk',NULL,1000,NULL),(85,'ssafy123@ssafy.com','$2a$10$MXWIjiH9gXklnDn6657nCOyC7duXGFSfaiwjgnn8Ht0v1yB4DH9cC','사피킹',NULL,1000,NULL),(86,'kdh@naver.com','$2a$10$kDTKqxwJTdLfqwnny9NG2./25TubtzwwNUeAjaltlCAss2GjL4EbC','오우오우',NULL,1000,NULL),(87,'kimttokang@naver.com','$2a$10$JLyzFrFBXYhxo4C3Hbhsi.cFb0uZ8yas3qJ9F2waOjsuWYGsy6mbG','김또깡',NULL,1000,NULL),(88,'chosh9128@gmail.com','$2a$10$lAQDBU7HYbZ.Nbsiq4Us7.AZWhkqfKk/QRKSBTQgF8LAj0uHuMSHu','조수훈이',NULL,1000,NULL),(89,'ssaf@123123.com','$2a$10$Q5v2j/5AYxYaJ9akeQfMfebCoVOMrScGkTG1fj1Tg/kIBl9nvFlSC','하이',NULL,1000,NULL),(90,'ssa@123.com','$2a$10$9DxoIHbUgXgVP9RMNaXzCea2DK3cLm3IsSM0k64Z0.XqNkH1kkidC','sd',NULL,1000,NULL),(91,'ttt@gmail.com','$2a$10$T7nk2JAMHX3EbfLOFs.LGuv3lHN0JuRx0y8zmVoDrZa3pnizzfA.G','손가락',NULL,1000,NULL),(92,'kanagyi95@naver.com','$2a$10$5rRzNOVW3nfclrEspqtfgOs9mYEi7HTvgyhcMcNmaww8YKPPmtICS','도파민지',NULL,1000,NULL),(93,'2312312@naevr.com','$2a$10$5bxycYh/ZKyfh9eFFCpIE.ruyG4ExhUzgj/DoN/mRStDzTpPA1Rra','마이나키',NULL,1000,NULL),(94,'asdf@asdf.com','$2a$10$kwGPGkk2Hg2d3AxPwPyh7e68wQoPXDzj3TZXEqhYCnr.BivZZHusy','싸피사랑',NULL,1000,NULL),(95,'asdf@email.com','$2a$10$.E9pj5XlJAKAPRMYCkyT4.NHj9eS7UwUNJCIXvC4EbazDFLKKqJhi','qwer',NULL,1000,NULL),(96,'tjsalszla@naver.com','$2a$10$j3stlPBVciN9JSImWUuAeeTqFMhJl0Q6nsQ63Y.eosp7c3dBUTTTy','seon',NULL,1000,NULL),(97,'buslaut@naver.com','$2a$10$c9ivFwkVZuQJ4ArnpXppl.FymE2cxhWlqAMt1X1R0Lu.jYkPKWlYm','부슬로트',NULL,1000,NULL),(98,'tbja@naver.com','$2a$10$DTqchUxWtoIngljqrcw99e5flS64kjNy1ub9uJDkj5veYGUw0m6va','ssaf',NULL,1000,NULL),(99,'aaa@aaa.com','$2a$10$KdRu6korXX3JPoSkcJvXOezYTz0WJtbyX3Ph5K548dgpo4hOfZSiq','야호',NULL,1000,NULL),(100,'guneleven@gmail.com','$2a$10$jCiZniELSwfxISd3/wa4COh3OJTf6qEnC0Fzu3KXeQGJoG68Uy1kK','건재신',NULL,1000,NULL),(101,'yt1356@naver.com','$2a$10$5iUZexfGza4YXFuyNQsp7eX52mDkOhrXYa2HehkZpFgpChPsAgDpK','규규',NULL,1000,NULL),(102,'wjdwldud0601@naver.com','$2a$10$Iz.spERTM2QEfWu9w6.Gr.NenVSZqyTXY/M3RUxvDUskdTHuNcFIC','zii',NULL,1000,NULL),(103,'aa@naver.com','$2a$10$pU3s8fgjNEGTWbGVq4x8ueuwB5htuyXpddja2HE1C7IunTJssIlTe','aaaa',NULL,1000,NULL),(104,'zsa332@gmail.com','$2a$10$fieBEusojKDNAr64yLLSsuTifvb7L7D5CG.UtnqwzSN8P6vtEYI1m','아시아나',NULL,1000,NULL),(105,'eundeok9@gmail.com','$2a$10$Q/.SomvCU1dhTS5GMBLksenuFi.9zm2NZgVIo.qNqLY6iwgCySb4C','은덕',NULL,1000,NULL),(106,'qwe123@gmail.com','$2a$10$ambU2hSWPfZdgfeIhiaLXewoede11OS11U3m5TKfN4LqpOLhvuehS','홈키노래',NULL,1000,NULL),(107,'test@test.com','$2a$10$4oJOV/9cDUxmzCO.WV4UYuBFtVzqs6e1NYUm3GHwL396DNDWELs9a','코코팽',NULL,1000,NULL),(108,'peku9693@gmail.com','$2a$10$7dZi.SFZkC/R35Bqu2bmKe1s521aOAdd1VyW3q9UrUFSL2NLqekbe','코코팜',NULL,1000,NULL),(109,'chae0738@naver.com','$2a$10$swx1H7e9P.RvjpymWZjT7.GX4kiOQduoqyFCnALYSRQsLZ/2/lC/a','나',NULL,1000,NULL),(110,'son9aram@gmail.com','$2a$10$e8V2R3BXt6hHyvLawo7gNeGwaWammjh6KAZVFb8cioxBh0fGDcC9C','민코',NULL,1000,NULL),(111,'leeminkyu1212@gmail.com','$2a$10$eu/IyJ28egITD578y2i0YuVUtYbBZE0kFyajmXKIL/6paiyqAHKxa','아코',NULL,1000,NULL),(112,'markrla@naver.com','$2a$10$2ZefSZCN3.GJQzdYlAPbJesLXx2CdB3pKPtmo0f8BjHlEvcNoyFJq','kim',NULL,1000,NULL),(113,'dud@naver.com','$2a$10$sQe9D7g/prmUpY1SghPSo.xtqJOmvt/Bdoa/V3KO96boJqGtMcr3u','코코코코',NULL,1000,NULL),(114,'42.4.woonchoi@gmail.com','$2a$10$vbyCJ0tTq67HRgpcG643ceben.pgQQ24XoIa4wgSbmd9DiVFyiGJ2','woon',NULL,1000,NULL),(115,'306yyy@naver.com','$2a$10$9wvfWQ9X5KDoBFFn7/JCK.ZmkKBHz/igAKduXSL5ro9x2zmBido02','YEAH',NULL,1000,NULL),(116,'hangwoo@naver.com','$2a$10$HM0eVGSlncwecSiBhPuAjuCBmbOvJDH7ZUT.fhM1D08D3MPX37t8a','hang',NULL,1000,NULL),(117,'1234@gmail.com','$2a$10$b6HLUXd7XI1KFPuoGRQxtO4nipblYmemFbFhWJPp40WASdRdkAJZu','dodo',NULL,1000,NULL),(122,'dododo@gmail.com','$2a$10$Cduka.uEEEtDO9gyeJ/Ebu0EUIqyW4wgQ5ef/QYfON3wOl4.flQya','1234',NULL,1000,NULL),(123,'eh6848zzz@gmail.com','$2a$10$Qc1siyvpiWfVCM2gAOqFGueLLfaeIds3buX63mTo1n4p6tkNMANP6','zzzz',NULL,1000,NULL),(124,'aop8021@gmail.com','$2a$10$ebAC3xty76uNuZDFQKBUDum5EF9zuyBklXLDAxLbjKPMT8YX0x3b.','나다지용',NULL,1000,NULL),(125,'chltjdgh3@naver.com','$2a$10$y.7c3JaPqbRwV1ECBrAMKO2kg8JUwKr6yzBOIczY7sqXEQej7wgOy','성호호호',NULL,1000,NULL);
/*!40000 ALTER TABLE `User` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-04-04  9:32:16
