# Projet_Site_WEB_EMG_BackEnd_C# Par Med1000  

## 📌 Description

L'objectif était ici de créer un backend C# pour le projet de site Emg ([FrontEnd du projet EMG](https://github.com/Mouhamed1000/Projet_Site_WEB_EMG_FrontEnd_ReactJS)). 

Donc Ici je gère les bases de données des voitures, marques, modèles, ainsi que les jetons d'authentification (JWT) pour permettre à l'administrateur de s'inscrire 

dans le système et de se connecter afin de gérer l'ajout, la modification, et la suppression des objets du système.

  
### 🔥 Installation 

```bash
git clone https://github.com/Mouhamed1000/Projet_Site_WEB_EMG_BackEnd_C_Sharp.git
cd Projet_Site_WEB_EMG_BackEnd_C_Sharp
```

### 🔥 Restauration des dépendances

```bash
dotnet restore
```

### 🚀 Configuration de la base de données

- Avant d'appliquer les migrations, vérifiez le fichier `appsettings.json`.

-  Puis dans les sections **VoitureDb** et **IdentityDb**, remplacez les valeurs de connexion par : votre nom d'utilisateur et mot de passe.

### 👤 Utilisateurs par défaut (optionnel)

- Si vous souhaitez utiliser les mêmes utilisateurs que ceux du projet d'origine :

- **Nom d'utilisateur** : `UserEMG` 

- **password** : `passer`
  
### ⚙️ Bases de données utilisées 

- Les bases de données que j'ai configuré sont

  `projetEMGCarsDb` et `projetEMGIdentitydb`

### 🛩️ Application des migrations  

- Une fois la configuration terminée, faites : 

```bash
dotnet ef database update --context VoitureContext
dotnet ef database update --context ApplicationDbContext
```

### 🔥 Lancement de l'application

```bash
dotnet run
```

 #### 🌐 Port utilisé
 
- L'application est configurée pour s'exécuter sur le port : 32000.

- Assurez-vous que ce port soit libre
  
- Dans le cas contraire:
  
-  Redémarrer la machine

- ou terminez le processus utilisant le port




