using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Sql;
using System.Data.OleDb;
using System.Data;


namespace SellManagement
{
    public class CDatabase
    {
        string SRC_PATH;
        OleDbConnection CONN;
        public CDatabase(string _SRC_PATH)
        {
            SRC_PATH = _SRC_PATH;
            OPEN_CONNECTTION();
        }

        public CDatabase()
        {
            SRC_PATH = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + CPublic.WG_DBPATH;
            //SRC_PATH = @"Provider = Microsoft.ACE.OLEDB.12.0;Data Source=" + CPublic.WG_DBPATH;
            OPEN_CONNECTTION();
        }       

        public  bool OPEN_CONNECTTION()
        {
            CONN = new OleDbConnection(SRC_PATH);
            try
            {
                CONN.Open();
                if (CONN.State == ConnectionState.Open)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void CLOSE_CONNECTTION()
        {
            if (CONN.State == ConnectionState.Open)
                CONN.Close();            
        }

        public DataTable SELECT_DATA(string CMD_TEXT)
        {
            OleDbCommand OLB_CMD = new OleDbCommand(CMD_TEXT, CONN);
            DataSet DS_DATA = new DataSet();
            OleDbDataAdapter OLB_ADAPTER = new OleDbDataAdapter(OLB_CMD);

            OLB_ADAPTER.SelectCommand.ExecuteNonQuery();
            DS_DATA.Clear();
            OLB_ADAPTER.Fill(DS_DATA);

            return DS_DATA.Tables[0];    
        }

        public bool INSERT_DATA(string CMD_TEXT)
        {
            try
            {
                OleDbCommand OLB_CMD = new OleDbCommand(CMD_TEXT, CONN);
                DataSet DS_DATA = new DataSet();
                OleDbDataAdapter OLB_ADAPTER = new OleDbDataAdapter();

                OLB_ADAPTER.InsertCommand = OLB_CMD;
                OLB_ADAPTER.InsertCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool UPDATE_DATA(string CMD_TEXT)
        {
            try
            {
                OleDbCommand OLB_CMD = new OleDbCommand(CMD_TEXT, CONN);
                DataSet DS_DATA = new DataSet();
                OleDbDataAdapter OLB_ADAPTER = new OleDbDataAdapter();

                OLB_ADAPTER.UpdateCommand = OLB_CMD;
                OLB_ADAPTER.UpdateCommand .ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool DELETE_DATA(string CMD_TEXT)
        {
            try
            {
                OleDbCommand OLB_CMD = new OleDbCommand(CMD_TEXT, CONN);
                DataSet DS_DATA = new DataSet();
                OleDbDataAdapter OLB_ADAPTER = new OleDbDataAdapter();

                OLB_ADAPTER.DeleteCommand = OLB_CMD;
                OLB_ADAPTER.DeleteCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception EX)
            {
                return false;
            }
        } 
        
        public bool CHECK_DATA_EXISTED(string CMD_TEXT)
        {
            if (SELECT_DATA(CMD_TEXT).Rows.Count <= 0)
                return false;
            else
                return true;
        }

        public string SELECT_NAME_BY_CODE(string CMD_TEXT)
        {
            OleDbCommand OLB_CMD = new OleDbCommand(CMD_TEXT, CONN);
            DataSet DS_DATA = new DataSet();
            OleDbDataAdapter OLB_ADAPTER = new OleDbDataAdapter(OLB_CMD);
            
            return OLB_ADAPTER.SelectCommand.ExecuteScalar().ToString() ;
            
        }
    }
}
