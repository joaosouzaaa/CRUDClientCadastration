using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace CRUDClientCadastration
{
    class Clients
    {
        public int ID;
        public string Name;
        public string Email;
        public string CPF;
        // Constructors, data where the correspondent data will be defined.
        public DataSet Att()
        {
            OleDbConnection con = new OleDbConnection(Connection.Local());
            con.Open();
            // Do the connection with the database and open it.
            OleDbDataAdapter da = new OleDbDataAdapter("Select ID as ID, Name as Name, Email as Email, CPF as CPF From Clients", con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            // Show your data and make a select command that updates the table.
            return ds;
        }
        public void AddNew()
        {
            OleDbConnection con = new OleDbConnection(Connection.Local());
            con.Open();
            OleDbCommand cmd = new OleDbCommand("Insert into Clients(Name, Email, CPF) Values('" + Name + "','" + Email + "','" + CPF + "')", con);
            cmd.ExecuteNonQuery();
            // Does the query command, which insert into the table the correspondig fields.
        }
        public void Update(int id)
        {
            OleDbConnection con = new OleDbConnection(Connection.Local());
            con.Open();
            string atualizeDatabase = "update Clients set Name = '" + Name + "', Email = '" + Email + "', CPF = '" + CPF + "' Where ID = " + id;
            OleDbCommand cmd = new OleDbCommand(atualizeDatabase, con);
            cmd.ExecuteNonQuery();
            // Does the query command, which updates the data that was inserted.
        }
        public void Delete(int id)
        {
            OleDbConnection con = new OleDbConnection(Connection.Local());
            con.Open();
            OleDbCommand cmd = new OleDbCommand("DELETE * FROM Clients Where ID= " + id, con);
            cmd.ExecuteNonQuery();
            // Does the query command, which deletes from the table wich line has the corresponding id.
        }
        public static DataSet Search(int id)
        {
            try
            {
                OleDbConnection con = new OleDbConnection(Connection.Local());
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter("Select ID as ID, Name as Name, Email as Email, CPF as CPF From Clients Where ID= " + id, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "DGV1");
                return ds;
                // Select the data and show them on the DataGridView.
            }
            catch
            {
                throw;
            }
        }
    }
}
