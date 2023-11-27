using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;

class Plateau
{
    private static Random randInt = new Random();

    public static string[,] generationPlateau(int lignes, int colonnes)
    {
        // indiquer le chemin du fichier
        string filePath = "utils/Lettre.csv";

        // Liste pour stocker les lignes 
        List<string[]> lignesCSV = new List<string[]>();

        // Lecture du fichier
        using (TextFieldParser lecteurCSV = new TextFieldParser(filePath))
        {
            lecteurCSV.TextFieldType = FieldType.Delimited;
            lecteurCSV.SetDelimiters(",");

            // Lecture des lignes
            while (!lecteurCSV.EndOfData)
            {
                string[] champs = lecteurCSV.ReadFields();
                lignesCSV.Add(champs);
            }
        }

        // Création du plateau
        
        string[,] plateau = new string[lignes, colonnes];
        for (int i = 0; i < plateau.GetLength(0); i++)
        {
            for (int j = 0; j < plateau.GetLength(1); j++)
            {
                string addLetter = getLetter(lignesCSV);
                plateau[i, j] = addLetter;
            }
        }

        // Affichage du plateau
        for (int i = 0; i < lignes; i++)
        {
            for (int j = 0; j < colonnes; j++)
            {
                Console.Write(plateau[i, j] + "\t");
            }
            Console.WriteLine(); // Passer à la ligne suivante après chaque ligne de la matrice
        }

        return plateau;
    }

    private static string getLetter(List<string[]> lignesCSV)
    {
        int nb = randInt.Next(0, 26);
        int intRemainLetter = int.Parse(lignesCSV[nb][1]);
        intRemainLetter--;

        if (intRemainLetter != 0)
        {
            string stringRemainLetter = intRemainLetter.ToString();
            lignesCSV[nb][1] = stringRemainLetter;
            return lignesCSV[nb][0];
        }
        else
        {
            return getLetter(lignesCSV); // Ajout de la récursivité manquante
        }
    }

    public static void toFile (string[,] plateau){
        int nbSave = Directory.GetFiles("save/").Length;
        string pathFile = "save/"+nbSave+1.ToString();
        using (StreamWriter redacteur = new StreamWriter(pathFile)){
            for(int i=0;i<plateau.GetLength(0);i++){
                for(int j=0;j<plateau.GetLength(1);j++){
                    if(j!=plateau.GetLength(1)-1){
                        redacteur.Write(plateau[i,j]+",");                        
                    } else {
                        redacteur.Write(plateau[i,j]);                        
                    }
                }
                redacteur.Write("\n");
            }
        }
        
    }
// return string[,]
    public static string[,] toRead(string fileName){
        string pathFile = "save/11";
        List<string[]> lignesCSV = new List<string[]>();
        using(TextFieldParser lecteurCSV = new TextFieldParser(pathFile)){
            lecteurCSV.TextFieldType = FieldType.Delimited;
            lecteurCSV.SetDelimiters(",");

            while(!lecteurCSV.EndOfData){
                string[] champs = lecteurCSV.ReadFields();
                lignesCSV.Add(champs);
            }
        }

        int ligne = lignesCSV[0][0].Length;
        int colonne = lignesCSV[0].Length;
        string[,] plateau = new string[ligne,colonne];
        for(int i=0;i<ligne;i++){
            for(int j=0;j<colonne;j++){
                string addLetter = lignesCSV[i][j];
                plateau[i,j] = addLetter;
            }
        }        

        return plateau;

    }
    static void Main()
    {
        // Appel de la fonction et stockage du résultat dans une variable
        string[,] plateau = generationPlateau(8,8);
        //toFile(plateau);
        //toRead("11");

    }
}