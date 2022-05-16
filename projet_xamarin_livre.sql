-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le : lun. 16 mai 2022 à 15:55
-- Version du serveur : 8.0.21
-- Version de PHP : 8.0.13

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `projet_xamarin_livre`
--

-- --------------------------------------------------------

--
-- Structure de la table `enfant`
--

DROP TABLE IF EXISTS `enfant`;
CREATE TABLE IF NOT EXISTS `enfant` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nom` varchar(50) DEFAULT NULL,
  `classe` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Structure de la table `evaluer`
--

DROP TABLE IF EXISTS `evaluer`;
CREATE TABLE IF NOT EXISTS `evaluer` (
  `id_utilisateur` int DEFAULT NULL,
  `id_livre` int DEFAULT NULL,
  `note` int DEFAULT NULL,
  `commentaire` varchar(50) DEFAULT NULL,
  `date` date DEFAULT NULL,
  KEY `FK_evaluer_utilisateur` (`id_utilisateur`),
  KEY `FK_evaluer_livre` (`id_livre`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déclencheurs `evaluer`
--
DROP TRIGGER IF EXISTS `ajout_nb_eval_livre`;
DELIMITER $$
CREATE TRIGGER `ajout_nb_eval_livre` AFTER INSERT ON `evaluer` FOR EACH ROW BEGIN
UPDATE livre SET nb_eval_livre = nb_eval_livre + 1 WHERE livre.id = NEW.id_livre ;
END
$$
DELIMITER ;
DROP TRIGGER IF EXISTS `verif_note_ajout_eval`;
DELIMITER $$
CREATE TRIGGER `verif_note_ajout_eval` AFTER INSERT ON `evaluer` FOR EACH ROW BEGIN

IF(NEW.note > 5 OR NEW.note < 0)
	THEN		
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='On ne peut pas attribuer une note superieur à 5 ou inferieur 1';
	END IF;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Structure de la table `livre`
--

DROP TABLE IF EXISTS `livre`;
CREATE TABLE IF NOT EXISTS `livre` (
  `id` int NOT NULL AUTO_INCREMENT,
  `titre` varchar(50) DEFAULT NULL,
  `id_utilisateur` int DEFAULT NULL,
  `date_livre` date DEFAULT NULL,
  `nb_eval_livre` int UNSIGNED NOT NULL,
  `img` text,
  PRIMARY KEY (`id`),
  KEY `FK_livre_utilisateur` (`id_utilisateur`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `livre`
--

INSERT INTO `livre` (`id`, `titre`, `id_utilisateur`, `date_livre`, `nb_eval_livre`, `img`) VALUES
(1, 'Une sourie verte', NULL, '2022-03-31', 0, 'icone_une_sourie_verte.jpg'),
(2, 'Les trois petits cochons', NULL, '2022-04-09', 1, 'icone_trois_petit_cochon.jpg'),
(3, 'Leon le bourdon', NULL, '2016-10-13', 1, 'icone_leon_le_bourdon.jpg');

-- --------------------------------------------------------

--
-- Structure de la table `utilisateur`
--

DROP TABLE IF EXISTS `utilisateur`;
CREATE TABLE IF NOT EXISTS `utilisateur` (
  `id` int NOT NULL AUTO_INCREMENT,
  `login` varchar(50) DEFAULT NULL,
  `mdp` varchar(50) DEFAULT NULL,
  `id_enfant` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_utilisateur_enfant` (`id_enfant`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `evaluer`
--
ALTER TABLE `evaluer`
  ADD CONSTRAINT `FK_evaluer_livre` FOREIGN KEY (`id_livre`) REFERENCES `livre` (`id`),
  ADD CONSTRAINT `FK_evaluer_utilisateur` FOREIGN KEY (`id_utilisateur`) REFERENCES `utilisateur` (`id`);

--
-- Contraintes pour la table `livre`
--
ALTER TABLE `livre`
  ADD CONSTRAINT `FK_livre_utilisateur` FOREIGN KEY (`id_utilisateur`) REFERENCES `utilisateur` (`id`);

--
-- Contraintes pour la table `utilisateur`
--
ALTER TABLE `utilisateur`
  ADD CONSTRAINT `FK_utilisateur_enfant` FOREIGN KEY (`id_enfant`) REFERENCES `enfant` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
