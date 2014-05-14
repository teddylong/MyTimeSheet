using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using System.Configuration;

namespace TimeSheetWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class TimeSheetService : ITimeSheetService
    {

        string ConnStr = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
        
        public Collection<string> GetData(int value)
        {
            
            Collection<string> result = new Collection<string>();
            SqlConnection myConnection = new SqlConnection(ConnStr);
            string sCommand = "SELECT * FROM timesheet where week = " + value;
            SqlDataAdapter da = new SqlDataAdapter(sCommand, myConnection);
            DataTable dataTable = new DataTable();
            try
            {
                myConnection.Open();
                int record = da.Fill(dataTable);

                if (record > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            result.Add(dr[i].ToString());
                        }
                    }
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        public Collection<string> GetDetailData(int week, string username)
        {
            Collection<string> result = new Collection<string>();
            SqlConnection myConnection = new SqlConnection(ConnStr);
            string sCommand = "SELECT * FROM Detail where week = " + week + " and name = '" + username + "'";
            SqlDataAdapter da = new SqlDataAdapter(sCommand, myConnection);
            DataTable dataTable = new DataTable();
            try
            {
                myConnection.Open();
                int record = da.Fill(dataTable);

                if (record > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            result.Add(dr[i].ToString());
                        }
                    }
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        public int UpdateData(int id, string name, string summary)
        {
            SqlConnection myConnection = new SqlConnection(ConnStr);
            string sCommand = "Update timesheet SET summary='" + summary + "',name='" + name + "' where id = " + id;
            SqlDataAdapter da = new SqlDataAdapter(sCommand, myConnection);
            DataTable dataTable = new DataTable();
            try
            {
                myConnection.Open();
                int record = da.Fill(dataTable);
                return record;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                myConnection.Close();
            }
                
        }

        public int SubmitData(int week, string name, string pic, string ps, 
            string time, int pid, string text, double hours)
        {
            bool checkResult = false;
            Collection<string> result = new Collection<string>();
            SqlConnection myConnection = new SqlConnection(ConnStr);
            string sCommand = "SELECT * FROM timesheet where week = " + week + " and name='" + name + "'";
            string createTimeSheetCommand = "INSERT INTO timesheet (week,name,pic,ps,time) VALUES(" + week + ",'" + name + "','" + pic + "','" + ps + "','" + time + "')";
            string createDetailCommand = "INSERT INTO detail (week,pid,text,hours,name) VALUES(" + week + "," + pid + ",'" + text + "'," + hours + ",'" + name + "')";
            SqlDataAdapter da = new SqlDataAdapter(sCommand, myConnection);
            SqlDataAdapter createTimeSheetDA = new SqlDataAdapter(createTimeSheetCommand, myConnection);
            SqlDataAdapter createDetailDA = new SqlDataAdapter(createDetailCommand, myConnection);
            DataTable createTimeSheetDT = new DataTable();
            DataTable dataTable = new DataTable();
            DataTable createDetailDT = new DataTable();
            try
            {
                myConnection.Open();
                int record = da.Fill(dataTable);

                if (record > 0)
                {
                    checkResult = true;
                }
                else
                {
                    checkResult = false;
                }

                if (!checkResult)
                { 
                    try
                    {
                        int createTimeSheetRecord = createTimeSheetDA.Fill(createTimeSheetDT);
                    }
                    catch (Exception ex) { }
                }
                int createDetailRecord = createDetailDA.Fill(createDetailDT);
                return createDetailRecord;
            }
            catch (Exception e)
            {
                return 0;
            }
            finally
            {
                myConnection.Close();
            }


        }

        public int UpdateDetailData(int week, string name, int pid, string text, double hours, int uid)
        {
            SqlConnection myConnection = new SqlConnection(ConnStr);
            string uCommand = "UPDATE detail set pid=" + pid + ", text='" + text + "', hours=" + hours + " where id=" +uid;
            string sCommand = "SELECT * from detail where id=" + uid;
            string iCommand = "INSERT INTO detail (week,pid,text,hours,name) VALUES(" + week + "," + pid + ",'" + text + "'," + hours + ",'" + name + "')";
            SqlDataAdapter uda = new SqlDataAdapter(uCommand, myConnection);
            SqlDataAdapter sda = new SqlDataAdapter(sCommand, myConnection);
            SqlDataAdapter ida = new SqlDataAdapter(iCommand, myConnection);
            DataTable sdataTable = new DataTable();
            DataTable udataTable = new DataTable();
            DataTable idataTable = new DataTable();
            try
            {
                myConnection.Open();
                int srecord = sda.Fill(sdataTable);
                if (srecord > 0)
                {
                    int urecord = uda.Fill(udataTable);
                    return urecord;
                }
                else
                {
                    int irecord = ida.Fill(idataTable);
                    return irecord;
                }
            }
            catch (Exception e)
            { return 523623; }
            finally
            {
                myConnection.Close();
            }
        }

        public void DeleteDetailDate(int week, string name, int uid)
        {
            SqlConnection myConnection = new SqlConnection(ConnStr);
            string Command = "DELETE from detail where week=" + week + "and name='" + name + "' and id=" + uid;
            SqlDataAdapter da = new SqlDataAdapter(Command, myConnection);
            DataTable dataTable = new DataTable();
            try
            {
                myConnection.Open();
                int record = da.Fill(dataTable);
            }
            catch (Exception ex)
            { }
            finally
            {
                myConnection.Close();
            }
            
        }
    }
}
