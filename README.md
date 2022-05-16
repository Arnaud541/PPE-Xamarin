# PPE-Xamarin

Il n'y a pas de fonctionnalité d'inscription dans l'application pour pouvoir créer un utilisateur, il vous suffit de rentrer cette commande SQL dans la base de données :

INSERT INTO utilisateur (login,mdp) VALUES ('test',sha1('testxamarin'));

Lors de l'insertion d'un utilisateur, n'oubliez pas de mettre le mot 'xamarin' après votre mot de passe.

Le mot xamarin correspond à un grain de sel ajouté au mot de passe qui nous permettra de le vérifier lors de la connexion de l'utilisateur.


