-- --------------------------------------------------------
-- Servidor:                     127.0.0.1
-- Versão do servidor:           8.0.32 - MySQL Community Server - GPL
-- OS do Servidor:               Win64
-- HeidiSQL Versão:              12.3.0.6589
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

-- Copiando dados para a tabela projectwyden.coursedicipline: ~4 rows (aproximadamente)
INSERT INTO `coursedicipline` (`CoursesCourseId`, `DiciplinesDiciplineId`) VALUES
	(1, 1),
	(2, 1),
	(8, 1),
	(1, 2),
	(8, 2),
	(8, 3);

-- Copiando dados para a tabela projectwyden.courses: ~0 rows (aproximadamente)
INSERT INTO `courses` (`CourseId`, `Name`) VALUES
	(1, 'Ciência da Computação'),
	(2, 'Engenharia da Computação'),
	(3, 'Biomedicina'),
	(4, 'Psicologia'),
	(5, 'Engenharia de Controle e Automação'),
	(6, 'Gastronomia'),
	(7, 'Engenharia Quimica'),
	(8, 'Engenharia de Software');

-- Copiando dados para a tabela projectwyden.diciplines: ~0 rows (aproximadamente)
INSERT INTO `diciplines` (`DiciplineId`, `Code`, `Name`) VALUES
	(1, 'ARA001', 'Fundamentos de Redes'),
	(2, 'ARA002', 'Introdução a C'),
	(3, 'ARA003', 'Segurança da Informação');

-- Copiando dados para a tabela projectwyden.professors: ~0 rows (aproximadamente)
INSERT INTO `professors` (`ProfessorId`, `Name`, `AR`, `DiciplineId`) VALUES
	(1, 'Douglas', '202302378989', 1),
	(2, 'Jose Alonso', '202302379090', 3);

-- Copiando dados para a tabela projectwyden.students: ~0 rows (aproximadamente)
INSERT INTO `students` (`StudentId`, `Name`, `AR`, `CourseId`) VALUES
	(1, 'Gustavo Santimaria Anacleto', '202302379567', 1),
	(2, 'Dhara Melissa', '202302398990', 3),
	(3, 'Thiago Lacerda', '202302398991', 1),
	(4, 'Vitor Augusto', '202302398992', 1);

-- Copiando dados para a tabela projectwyden.tests: ~1 rows (aproximadamente)
INSERT INTO `tests` (`TestId`, `Date`, `Type`, `Result`, `StudentId`, `DiciplineId`) VALUES
	(1, '2023-05-10 00:00:00.000000', 1, 10, 1, 1);

-- Copiando dados para a tabela projectwyden.__efmigrationshistory: ~1 rows (aproximadamente)
INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
	('20230510105928_FirstMigration', '6.0.16');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
