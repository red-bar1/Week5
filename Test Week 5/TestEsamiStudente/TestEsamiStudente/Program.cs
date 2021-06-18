using System;
using TestEsamiStudente.Repositories;
using TestEsamiStudente.Entities;

namespace TestEsamiStudente
{
    class Program
    {
        static void Main(string[] args)
        {
            //recupero studenti da database
            IRepoStudente repoStudente = new RepoStudente();
            IRepoEsame repoEsame = new RepoEsame();
            foreach (var studente in repoStudente.GetAll())
            {
                Console.WriteLine(studente);
            }

            //inserimento esame per studente specifico
            Console.WriteLine("Inserisci l'ID dello studente che deve registrare l'esame");
            bool success = Int32.TryParse(Console.ReadLine(), out int ID);
            while (!success)
            {
                Console.WriteLine("Inserisci un ID valido");
                success = Int32.TryParse(Console.ReadLine(), out ID);
            }

            try
            {
                Studente studente = repoStudente.GetByID(ID);
                if(studente != null)
                {
                    bool esitoInserimentoEsame = RepoEsame.InsertEsame(studente);
                    if (esitoInserimentoEsame)
                    {
                        Console.WriteLine("Inserimento andato a buon fine");
                    }
                    else
                    {
                        Console.WriteLine("Qualcosa è andato storto");
                    }
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //inserimento studente in mod disconnessa
            Console.WriteLine("Inserisci Nome studente");
            string nome = Console.ReadLine();
            Console.WriteLine("Inserisci Cognome studente");
            string cognome = Console.ReadLine();
            Console.WriteLine("Inserisci Anno di nascita");
            int annoNascita = Int32.Parse(Console.ReadLine());
            
            Studente newStudente = new Studente()
            {
                Nome = nome,
                Cognome = cognome,
                AnnoNascita = annoNascita               
            };
            bool esito = repoStudente.Add(newStudente);
            if (newStudente.ID != null || esito)
            {
                Console.WriteLine("Inserimento riuscito");
            }
            else
            {
                Console.WriteLine("Qualcosa è andato storto");
            }




        }
    }
}
