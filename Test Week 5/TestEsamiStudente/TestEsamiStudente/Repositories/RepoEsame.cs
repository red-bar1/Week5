using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEsamiStudente.Entities;

namespace TestEsamiStudente.Repositories
{
    public class RepoEsame : IRepoEsame
    {
        const string connectionString = @"Server = .\SQLEXPRESS; Persist Security Info = False; 
                Integrated Security = true; Initial Catalog = Esami;";

        public static IList<Esame> GetByStudent()
        {
            
            Console.WriteLine("Inserisci l'ID dello studente per avere l'elenco degli esami sostenuti");
            bool success1 = Int32.TryParse(Console.ReadLine(), out int ID1);
            while (!success1)
            {
                Console.WriteLine("Inserisci un ID valido");
                success1 = Int32.TryParse(Console.ReadLine(), out ID1);
            }
            
            IList <Esame> elencoEsami = new List<Esame>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    //uso la modalità connessa
                    //apro la connessione
                    connection.Open();
                    //creo il comando
                    SqlCommand selectCommand = new SqlCommand()
                    {
                        Connection = connection,
                        CommandType = System.Data.CommandType.Text,
                        CommandText = "SELECT * FROM Esame WHERE StudenteID=@StudenteID"
                    };

                    selectCommand.Parameters.AddWithValue("@StudenteID", ID1);

                    //eseguo il comando
                    SqlDataReader reader = selectCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[0]} - {reader[1]}, {reader[2]}, " +
                            $"{reader[3]}, {reader[4]}, {reader[5]}" );
                        //Studente esameAcquisito = new Esame()
                        //{

                        //    ID = Int32.Parse(reader["ID"].ToString()),
                        //    Nome = reader["Nome"].ToString(),
                        //    CFU = Int32.Parse(reader["ID"].ToString()),
                        //    DataEsame = DateTime.Parse(reader["DataEsame"].ToString()),
                        //    Votazione = reader["Votazione"].ToString(),
                        //    Esito = reader["Esito"].ToString(),
                        //    Studente = repoStudente.GetByID(Int32.Parse(reader["StudenteID"].ToString()))
                        //    //Studente = Int32.Parse(reader["StudenteID"].ToString())

                        //};
                        //elencoEsami.Add(esameAcquisito);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    
                }

            }
            return elencoEsami;
        }

        public static bool InsertEsame(Studente studente)
        {
            bool esitoConnessione = true;
            Console.WriteLine("Inserisci il nome dell'esame:");
            string nome = Console.ReadLine();
            Console.WriteLine("Inserisci i CFU:");
            int CFU = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Inserisci la data:");
            DateTime dataEsame = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Inserisci votazione:");
            string votazione = Console.ReadLine();
            Console.WriteLine("Inserisci esito:");
            string esito = Console.ReadLine();

            //uso connected mode
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    //Creo il comando
                    SqlCommand insertCommand = new SqlCommand();
                    insertCommand.Connection = connection;
                    insertCommand.CommandType = System.Data.CommandType.Text;
                    insertCommand.CommandText = "INSERT INTO Esame VALUES " +
                        "(@Nome, @CFU, @DataEsame, @Votazione, @Esito, @StudenteID)";

                    insertCommand.Parameters.AddWithValue("@Nome", nome);
                    insertCommand.Parameters.AddWithValue("@CFU", CFU);
                    insertCommand.Parameters.AddWithValue("@DataEsame", dataEsame);
                    insertCommand.Parameters.AddWithValue("@Votazione", votazione);
                    insertCommand.Parameters.AddWithValue("@Esito", esito);
                    insertCommand.Parameters.AddWithValue("@StudenteID", studente.ID);

                    //Esecuzione del comando
                    int row = insertCommand.ExecuteNonQuery();
                    Console.WriteLine("Numero di righe aggiornate: {0}", row);

                }
                catch(SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return esitoConnessione = false;
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            return esitoConnessione;
        }

        public bool Add(Esame item)
        {
            //non implemento perché non serve
            throw new NotImplementedException();
        }

        public IList<Esame> GetAll()
        {
            //non implemento perché non serve
            throw new NotImplementedException();
        }

        public Esame GetByID(int ID)
        {
            //non lo implemento perché non mi serve
            throw new NotImplementedException();
        }

        public IList<Esame> OrderByVotoData()
        {
            IEnumerable<Esame> esameVotoData = GetByStudent().OrderBy(e => e.Votazione).ThenBy(e => e.DataEsame);
            return esameVotoData.ToList();
        }
    }
}
