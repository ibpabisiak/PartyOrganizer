using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;
using System.IO;


namespace PartyOrganizer
{
    public static class DataBase
    {
        private static SQLiteConnection dbConnection;

        public static void DBConnect(string dbName = "database")
        {
            if (File.Exists(dbName + ".db"))
            {
                dbConnection = new SQLiteConnection("Data Source=" + dbName + ".db;Version=3;");
                dbConnection.Open();
            } else
            {
                MessageBox.Show("#ERR (DataBase.DBConnect): failed to connect to the database");
                Application.Current.Shutdown();
            }
        }

        public static void DBClose(string dbName = "database")
        {
            //dbConnection.Close();
        }

        public static Guest AddGuest(int eventId, string name, string surname, string sex, string email)
        {
            string query = "INSERT INTO Guests (ID_EVENT, Name, Surname, Sex, Email) VALUES ('" + eventId + "', '" + name + "', '" + surname + "', '" + sex + "', '" + email + "');";
            SQLiteCommand cmd = new SQLiteCommand(query, dbConnection);
            
            try
            {
                cmd.ExecuteNonQuery();
            } catch
            {
                MessageBox.Show("#ERR (DataBase.AddGuest): mistake sql query, try again");
            }

            query = "SELECT * FROM Guests WHERE Name = '" + name + "' LIMIT 1";
            cmd = new SQLiteCommand(query, dbConnection);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("#ERR (DataBase.AddGuest): mistake sql query, try again");
                throw;
            }

            SQLiteDataReader data = cmd.ExecuteReader();
            data.Read();
            Guest tmp = new Guest
            {
                ID_GUEST = Convert.ToInt32(data["ID_GUEST"]),
                ID_EVENT = Convert.ToInt32(data["ID_EVENT"]),
                Name = Convert.ToString(data["Name"]),
                Surname = Convert.ToString(data["Surname"]),
                Email = Convert.ToString(data["Email"]),
                Sex = Convert.ToString(data["Sex"]),
                Confirmed = Convert.ToBoolean(Convert.ToInt32(data["Confirmed"]))
            };

            return tmp;
        }

        public static int AddEvent(string eventName, string desc)
        {
            string query = "INSERT INTO Events (Name, Desc) VALUES ('" + eventName + "', '" + desc + "');";
            SQLiteCommand cmd = new SQLiteCommand(query, dbConnection);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("#ERR (DataBase.AddEvent): mistake sql query, try again");
            }

            query = "SELECT ID_EVENT FROM Events WHERE Name = '" + eventName + "' AND Desc = '" + desc + "' LIMIT 1";
            cmd = new SQLiteCommand(query, dbConnection);
            int tmp = 0;

            try
            {
                cmd.ExecuteNonQuery();
                SQLiteDataReader data = cmd.ExecuteReader();
                data.Read();
                tmp = Convert.ToInt32(data["ID_EVENT"]);
            }
            catch
            {
                MessageBox.Show("#ERR (DataBase.AddEvent): mistake sql query, try again");
            }

            return tmp;
        }

        public static ObservableCollection<Guest> GetGuestsByEventID(int eventID)
        {
            string query = "SELECT * FROM Guests WHERE ID_EVENT = " + eventID + "";
            SQLiteCommand cmd = new SQLiteCommand(query, dbConnection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("#ERR (DataBase.GetGuestsByEventID): mistake sql query, try again");
            }

            SQLiteDataReader data = cmd.ExecuteReader();

            ObservableCollection<Guest> tmp = new ObservableCollection<Guest>();

            while (data.Read())
            {
                tmp.Add(new Guest
                {
                    ID_GUEST = Convert.ToInt32(data["ID_GUEST"]),
                    ID_EVENT = Convert.ToInt32(data["ID_EVENT"]),
                    Name = Convert.ToString(data["Name"]),
                    Surname = Convert.ToString(data["Surname"]),
                    Email = Convert.ToString(data["Email"]),
                    Sex = Convert.ToString(data["Sex"]),
                    Confirmed = Convert.ToBoolean(Convert.ToInt32(data["Confirmed"])),
                    UpdateChanges = false
                });
            }
            return tmp;
        }

        public static List<Event> GetEventsList()
        {
            string query = "SELECT * FROM Events";
            SQLiteCommand cmd = new SQLiteCommand(query, dbConnection);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("#ERR (DataBase.GetGuestsByEventID): mistake sql query, try again");
            }

            SQLiteDataReader data = cmd.ExecuteReader();

            List<Event> tmp = new List<Event>();

            while (data.Read())
            {
                tmp.Add(new Event
                {
                    ID_EVENT = Convert.ToInt32(data["ID_EVENT"]),
                    Name = Convert.ToString(data["Name"]),
                    Desc = Convert.ToString(data["Desc"])
                });
            }

            return tmp;
        }

        public static void UpdateChanges(ObservableCollection<Guest> guestsList)
        {
            foreach (var item in guestsList)
            {
                if(item.UpdateChanges)
                {
                    string query = "UPDATE Guests SET Name = '" + item.Name + "', Surname = '" + item.Surname + "', Confirmed = '" + Convert.ToString(Convert.ToInt32(item.Confirmed)) + "', Email = '" + item.Email + "' WHERE ID_GUEST = '" + item.ID_GUEST + "';";

                    SQLiteCommand cmd = new SQLiteCommand(query, dbConnection);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        MessageBox.Show("#ERR (DataBase.UpdateChanges): mistake sql query, try again");
                    }
                }
            }
        }
    }
}
