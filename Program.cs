using System;
using System.Collections.Generic;
using System.Linq;

namespace HNI_TPmoyennes
{
    // Classe représentant un élève
    class Eleve
    {
        public string nom;
        public string prenom;
        public List<Note> notes;  // Liste des notes de l'élève
        public const int MAX_NOTES = 200;  // Maximum de 200 notes par élève

        // Constructeur
        public Eleve(string prenom, string nom)
        {
            this.nom = nom;
            this.prenom = prenom;
            notes = new List<Note>();
        }

        // Méthode pour ajouter une note à l'élève
        public void ajouterNote(Note note)
        {
            if (notes.Count < MAX_NOTES)
            {
                notes.Add(note);
            }
            else
            {
                Console.WriteLine($"Impossible d'ajouter plus de notes pour {prenom} {nom}. Maximum atteint.");
            }
        }

        // Méthode pour calculer la moyenne d'un élève dans une matière
        public float moyenneMatiere(int idMatiere)
        {
            // Filtrer les notes pour la matière spécifiée
            var notesMatiere = notes.Where(n => n.matiere == idMatiere).ToList();

            if (notesMatiere.Count == 0)
                return 0;

            // Calculer la somme
            float somme = 0;
            foreach (var note in notesMatiere)
            {
                somme += note.note;
            }

            // Calculer la moyenne et tronquer à deux chiffres après la virgule
            float moyenne = somme / notesMatiere.Count;
            return TronquerNombre(moyenne);
        }

        // Méthode pour calculer la moyenne générale d'un élève
        public float moyenneGeneral()
        {
            // Obtenir les identifiants des matières de l'élève
            var idMatieres = notes.Select(n => n.matiere).Distinct().ToList();

            if (idMatieres.Count == 0)
                return 0;

            // Calculer la moyenne pour chaque matière
            float somme = 0;
            foreach (var idMatiere in idMatieres)
            {
                somme += moyenneMatiere(idMatiere);
            }

            // Calculer la moyenne générale et tronquer à deux chiffres après la virgule
            float moyenneGenerale = somme / idMatieres.Count;
            return TronquerNombre(moyenneGenerale);
        }

        // Méthode pour tronquer un nombre à deux chiffres après la virgule
        private float TronquerNombre(float nombre)
        {
            // Multiplier par 100, tronquer, puis diviser par 100
            int nombreEntier = (int)(nombre * 100);
            return nombreEntier / 100f;
        }
    }

    // Classe représentant une classe d'école
    class Classe
    {
        public string nomClasse;
        public List<Eleve> eleves;
        public List<string> matieres;
        public const int MAX_ELEVES = 30;   // Maximum de 30 élèves par classe
        public const int MAX_MATIERES = 10; // Maximum de 10 matières

        // Constructeur
        public Classe(string nomClasse)
        {
            this.nomClasse = nomClasse;
            eleves = new List<Eleve>();
            matieres = new List<string>();
        }

        // Méthode pour ajouter un élève à la classe
        public void ajouterEleve(string prenom, string nom)
        {
            if (eleves.Count < MAX_ELEVES)
            {
                eleves.Add(new Eleve(prenom, nom));
            }
            else
            {
                Console.WriteLine($"Impossible d'ajouter plus d'élèves à la classe {nomClasse}. Maximum atteint.");
            }
        }

        // Méthode pour ajouter une matière à la classe
        public void ajouterMatiere(string matiere)
        {
            if (matieres.Count < MAX_MATIERES)
            {
                matieres.Add(matiere);
            }
            else
            {
                Console.WriteLine($"Impossible d'ajouter plus de matières à la classe {nomClasse}. Maximum atteint.");
            }
        }

        // Méthode pour calculer la moyenne de la classe dans une matière
        public float moyenneMatiere(int idMatiere)
        {
            if (eleves.Count == 0)
                return 0;

            // Calculer la somme des moyennes des élèves dans cette matière
            float somme = 0;
            foreach (var eleve in eleves)
            {
                somme += eleve.moyenneMatiere(idMatiere);
            }

            // Calculer la moyenne de la classe dans cette matière et tronquer à deux chiffres après la virgule
            float moyenneClasse = somme / eleves.Count;
            return TronquerNombre(moyenneClasse);
        }

        // Méthode pour calculer la moyenne générale de la classe
        public float moyenneGeneral()
        {
            if (matieres.Count == 0)
                return 0;

            // Calculer la somme des moyennes de la classe dans chaque matière
            float somme = 0;
            for (int i = 0; i < matieres.Count; i++)
            {
                somme += moyenneMatiere(i);
            }

            // Calculer la moyenne générale de la classe et tronquer à deux chiffres après la virgule
            float moyenneGenerale = somme / matieres.Count;
            return TronquerNombre(moyenneGenerale);
        }

        // Méthode pour tronquer un nombre à deux chiffres après la virgule
        private float TronquerNombre(float nombre)
        {
            // Multiplier par 100, tronquer, puis diviser par 100
            int nombreEntier = (int)(nombre * 100);
            return nombreEntier / 100f;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Création d'une classe
            Classe sixiemeA = new Classe("6eme A");
            // Ajout des élèves à la classe
            sixiemeA.ajouterEleve("Jean", "RAGE");
            sixiemeA.ajouterEleve("Paul", "HAAR");
            sixiemeA.ajouterEleve("Sibylle", "BOQUET");
            sixiemeA.ajouterEleve("Annie", "CROCHE");
            sixiemeA.ajouterEleve("Alain", "PROVISTE");
            sixiemeA.ajouterEleve("Justin", "TYDERNIER");
            sixiemeA.ajouterEleve("Sacha", "TOUILLE");
            sixiemeA.ajouterEleve("Cesar", "TICHO");
            sixiemeA.ajouterEleve("Guy", "DON");
            // Ajout de matières étudiées par la classe
            sixiemeA.ajouterMatiere("Francais");
            sixiemeA.ajouterMatiere("Anglais");
            sixiemeA.ajouterMatiere("Physique/Chimie");
            sixiemeA.ajouterMatiere("Histoire");
            Random random = new Random(); // Graine fixe pour notes reproductibles
                                            // Ajout de 5 notes à chaque élève et dans chaque matière
            for (int ieleve = 0; ieleve < sixiemeA.eleves.Count; ieleve++)
            {
                for (int matiere = 0; matiere < sixiemeA.matieres.Count; matiere++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        sixiemeA.eleves[ieleve].ajouterNote(new Note(matiere, (float)((6.5 +
                        random.NextDouble() * 34)) / 2.0f));
                    }
                }
            }

            Eleve eleve = sixiemeA.eleves[6];
            // Afficher la moyenne d'un élève dans une matière (choisir matière)
            Console.Write(eleve.prenom + " " + eleve.nom + ", Moyenne en " + sixiemeA.matieres[1] + " : " +
            eleve.moyenneMatiere(1) + "\n");
            // Afficher la moyenne générale du même élève
            Console.Write(eleve.prenom + " " + eleve.nom + ", Moyenne Generale : " + eleve.moyenneGeneral() + "\n");
            // Afficher la moyenne de la classe dans une matière (choisir matière)
            Console.Write("Classe de " + sixiemeA.nomClasse + ", Moyenne en " + sixiemeA.matieres[1] + " : " +
            sixiemeA.moyenneMatiere(1) + "\n");
            // Afficher la moyenne générale de la classe
            Console.Write("Classe de " + sixiemeA.nomClasse + ", Moyenne Generale : " + sixiemeA.moyenneGeneral() + "\n");
            Console.ReadKey();
        }
    }
}