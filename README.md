# Sinalia
2D MMORPG using Monogame Framework

## Fonctionnement actuel du projet :
Le client se connecte à un serveur en l'occurrence ZoneServer afin de se connecter et de jouer.

## Comment le projet va évouluer ?
Par la suite le projet de MMORPG sera diviser en plusieurs serveurs afin d'accueillir le maximum de joueurs possible sans lags.
Il y aura un serveur d'authenfication qui lui redirigera vers les différents serveurs de jeu.
Il y aura un serveur de tchat pour la gestion du tchat en jeu.
Il y aura un serveur par monde de jeu, c'est à dire qu'a chaque changement de region dans le jeu, c'est un serveur différent. (Actuellement ZoneServer)
Et enfin le serveur principal, le serveur monde qui gere la transition entre les différents serveur de Zone.

## Explications des différents projets
Lidgren.Network -> Bibliotheque réseau
Nez.Portable -> Framework qui ajoute une surcouche à Monogame
SN.BackendProtocols -> Projet qui gere la partie réseau coté serveur
SN.Client -> Projet du client (contient toute la partie graphique)
SN.ClientProtocol -> Projet qui gere la partie réseau coté client
SN.Core -> Projet qui est le core du jeu (commun au client et serveur)
SN.CoreAbstractions -> Projet abstrait du core
SN.Messages -> Projet contenant les messages réseaux
SN.ProtocolAbstractions -> Projet abstrait pour le réseau
SN.ZoneServer -> Projet du serveur

## Technologies utilisées
- Monogame / Nez
- Systeme réseau avec Handlers
- Asynchrone
- Injection de dépendance
- Systeme de token pour l'authentification
- Multithread
- PostgreSQL

## Technologies envisagées
- Entity Core Framework
