using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEsamiStudente.Entities;

namespace TestEsamiStudente.Repositories
{
    public class RepoStudente : IRepoStudente
    {
        const string connectionString = @"Server = .\SQLEXPRESS; Persist Security Info = False; 
                Integrated Security = true; Initial Catalog = Esami;";


        public IList<Studente> GetAll()
        {
            IList<Studente> elencoStudenti = new List<Studente>();

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
                        CommandText = "SELECT * FROM Studente"
                    };

                    //eseguo il comando
                    SqlDataReader reader = selectCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        Studente studenteAcquisito = new Studente()
                        {
                            ID = Int32.Parse(reader["ID"].ToString()),
                            Nome = reader["Nome"].ToString(),
                            Cognome = reader["Cognome"].ToString(),
                            AnnoNascita = Int32.Parse(reader["AnnoNascita"].ToString()),


                        };
                        elencoStudenti.Add(studenteAcquisito);
                    }
                }
                catch(SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            return elencoStudenti;
        }

        public Studente GetByID(int ID)
        {
            Studente studenteTrovato = GetAll().FirstOrDefault(stud => stud.ID.Equals(ID));
            if (studenteTrovato == null)
            {
                Console.WriteLine("Non esistono studenti con questo ID");
            }
            return studenteTrovato;
        }

        public bool Add(Studente item)
        {
            bool esito = true;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //creo adapter
                SqlDataAdapter adapter = new SqlDataAdapter();
                //creo comando di selezione
                SqlCommand selectCommand = new SqlCommand()
                {
                    Connection = connection,
                    CommandType = System.Data.CommandType.Text,
                    CommandText = "SELECT * FROM Studente"
                };

                //creo comando di aggiunta
                SqlCommand insertCommand = new SqlCommand()
                {
                    Connection = connection,
                    CommandType = System.Data.CommandType.Text,
                    CommandText = "INSERT INTO Studente VALUES (@Nome, @Cognome, @AnnoNascita)"
                };

                insertCommand.Parameters.Add("@Nome", SqlDbType.VarChar, 20, "Nome");
                insertCommand.Parameters.Add("@Cognome", SqlDbType.VarChar, 50, "Cognome");
                insertCommand.Parameters.Add("@AnnoNascita", SqlDbType.Int, 3000, "AnnoNascita");
                

                //associo i comandi all'adapter
                adapter.SelectCommand = selectCommand;
                adapter.InsertCommand = insertCommand;

                //creo il dataset
                DataSet dataset = new DataSet();
                try
                {
                    //apro connessione
                    connection.Open();
                    adapter.Fill(dataset, "Studente");

                    //creo riga in dataset

                    DataRow studente = dataset.Tables["Studente"].NewRow();
                    studente["Nome"] = item.Nome;
                    studente["Cognome"] = item.Cognome;
                    studente["AnnoNascita"] = item.AnnoNascita;
                    

                    dataset.Tables["Studente"].Rows.Add(studente);
                    adapter.Update(dataset, "Studente");

                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                    return esito = false;
                }

            }

            return esito;
        }
    }
}
