COLMANT Amaury, LOQUET Jonathan, TIXIER Lucas

Version beta : Axé Simulations de vie

2 rendus : Taille de map 512*512 et taille de map 1024*1024 (la 1024 peut lagguer sur certain PC)

Mode d'emploi:
Pour démarrer le jeu cliquez sur Join Game.
Lors de la vue d'ensemble dite "vue 2D" vous pouvez voir l'état a gauche de chaque carré de map (SquareMap) c'est a dire : Nombre d'entités, nourriture, eau
Vous pouvez cliquer sur un des squareMap pour acceder a la vue 3D. Pour revenir a la vue 2D cliquez sur le bouton "monde" en bas a droite.


Generation aléatoire de la map (par forme, pas par texture)
Creation des civilisations. La couleur de la civilisation est généré en fonction du génotype. (la couleur de la peau étant un gene commun a la civilisation)
Les entités mangent et boivent quand elles en ont besoin. Elles spawn avec 50 de nourriture et perde 1 toutes les 10 secondes. Lorsqu'elles boivent elles reprennent entre 10 et 50 unités d'eau ou nourriture.
Les entités meurent lorsqu'elles n'ont plus de nourriture ou d'eau.
Les points d'eau ou de nourriture sont stocké dans un entityContext en fonction de la mémoire de celle ci. Si dans l'adn de l'entité, la mémoire est a la valeur 2, l'entité pourra alors stocké que 2 spots de nourriture et 2 spots d'eau. (lorsqu'un 3eme sera rencontré il prendra la place du spot le plus vieux). La gestion des groupes a été enlevé de l'alpha pour cause de bug avant le rendu.
Beaucoup de changement ont été effectué sur la structure qui la rend beaucoup plus maléable mais nous a fait perdre beaucoup de temps.
Les features vont etre implémenté plus rapidement mais les changements éffectué entre l'alpha et la beta ne sont presque pas visibles.



Si vous voulez lancer le jeu depuis unity : 

ouvrir mainScene -> cliquer sur le composant EntityManager dans GameManager -> faire glisser le prefab Entity dans le serializeField -> choisir le nombre d'entité voulu sur la scene (400 minimum) -> Add.

lancer lobbyAlpha ou directement MainScene
