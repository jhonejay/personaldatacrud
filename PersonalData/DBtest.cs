using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace PersonalData
{
    class DBtest
    {
        private static string tablename = "test";

        private int id;
        private string firstname;
        private string middlename;
        private string lastname;

        public int Id {
            get { return id; }
            set { id = value; }
        }

        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }

        public string Middlename
        {
            get { return middlename; }
            set { middlename = value; }
        }

        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }

        public DBtest() { }

        public DBtest(string firstname, string middlename, string lastname) {

            this.firstname = firstname;
            this.middlename = middlename;
            this.lastname = lastname;
        }

        public void Add ()
        {
            string cmdText = "INSERT INTO " + tablename + " VALUES (0,@firstname,@middlename,@lastname)";
            MySqlConnection con = DBConnection.ConnectDatabase();

            try
            {
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                cmd.Parameters.AddWithValue("@firstname", this.Firstname);
                cmd.Parameters.AddWithValue("@middlename", this.Middlename);
                cmd.Parameters.AddWithValue("@lastname", this.Lastname);
                cmd.ExecuteNonQuery(); //execute the mysql command

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        public void Update()
        {
            string cmdText = "UPDATE " + tablename + " SET  firstname = @firstname, lastname=@lastname, middlename=@middlename WHERE id=@id";
            MySqlConnection con = DBConnection.ConnectDatabase();

            try
            {
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                cmd.Parameters.AddWithValue("@id", this.Id);
                cmd.Parameters.AddWithValue("@firstname", this.Firstname);
                cmd.Parameters.AddWithValue("@middlename", this.Middlename);
                cmd.Parameters.AddWithValue("@lastname", this.Lastname);

                cmd.ExecuteNonQuery(); //execute the mysql command
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void Delete()
        {
            MySqlConnection con = DBConnection.ConnectDatabase();
            string command = "DELETE FROM " + tablename + " WHERE id=@id";

            try
            {
                MySqlCommand cmd = new MySqlCommand(command, con);
                cmd.Parameters.AddWithValue("@id", this.Id);

                cmd.ExecuteNonQuery(); //execute the mysql command

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


        public static List<DBtest> GetData()
        {
            List<DBtest> data = new List<DBtest>();

            MySqlConnection con = DBConnection.ConnectDatabase();

            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM " + tablename, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DBtest rawData = new DBtest();
                        rawData.Id = reader.GetInt32(0);
                        rawData.Firstname = reader.GetString(1);
                        rawData.Middlename = reader.GetString(2);
                        rawData.Lastname = reader.GetString(3);
                      

                        data.Add(rawData);
                    }
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return data;
        }

        //getdatabyID
        public static DBtest GetDataID(int id)
        {
            DBtest data = null;

            MySqlConnection con = DBConnection.ConnectDatabase();

            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM " + tablename + " WHERE id=" + id, con);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {

                    reader.Read();

                    data = new DBtest();
                    data.Id = reader.GetInt32(0);
                    data.Firstname = reader.GetString(1);
                    data.Middlename = reader.GetString(2);
                    data.Lastname = reader.GetString(3);
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }

            return data;

        }

    }//end
}
