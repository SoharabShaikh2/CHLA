using DataRepository;
using System;
using System.Collections.Generic;
using System.Text;

using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace MySQLDataRepository
{
	public class LogDataRepository : IDataRepository<ILog>
	{

		private IConnectionParameters connectionParameters;

		public LogDataRepository(IConnectionParameters conParam)
		{
			connectionParameters = conParam;


		}
		public int Add(ILog data, string collectionName)
		{
			int rowseffected = 0;


            var cycleJson = JObject.Parse(data.LogData);

           // var v = cycleJson.ToString();


            JToken vx = null;
            var mid = 0;
            if (cycleJson.TryGetValue("Master", out vx))
            {
                if (vx != null && !String.IsNullOrEmpty( vx.ToString()))
               mid=     AddMaster(vx.ToString(),collectionName);
            }

          

            JToken vxt=null;
           if(! cycleJson.TryGetValue("Events",out vxt))
            {
                return 0;
            }

           if(String.IsNullOrEmpty( vxt.ToString()))
            {
                return 0;
            }

            var vxd = (JArray)vxt;
            if(vxd==null ||  vxd.Count==0)
            {
                return 0;
            }
            

            MySqlConnection con = new MySqlConnection(connectionParameters.ConnectionString);

            StringBuilder sb = new StringBuilder("Insert into logs(primarypartition,log,fourthpartition) Values (@c,@log,@mid)");

            if (vxd.Count>1)

            {
                sb.Append(",");
            }
            MySqlCommand cmd = new MySqlCommand();

            var chilren = vxd.Children();

            JToken valforGname = null;

            var modJObj = (JObject)vxd[0];

            if (modJObj.TryGetValue("GameName", out valforGname))
            {
                modJObj["GameName"] = GetInternalMappedName(valforGname.ToString());
            }

            cmd.Parameters.AddWithValue("@log", modJObj.ToString());
            cmd.Parameters.AddWithValue("@c", collectionName);
            cmd.Parameters.AddWithValue("@mid", mid);
            for (int i= 1;i< vxd.Count; i++)

            {

                sb.Append(" (@c,@log"+i+",@mid) ");
                if (i < vxd.Count - 1)

                {
                    sb.Append(",");
                }



                modJObj = (JObject)vxd[i];

                if (modJObj.TryGetValue("GameName", out valforGname))
                {
                    modJObj["GameName"] = GetInternalMappedName(valforGname.ToString());
                }

                // cmd.Parameters.AddWithValue("@log"+i, vxd[i].ToString());

                cmd.Parameters.AddWithValue("@log" + i, modJObj.ToString());

                Debug.WriteLine(vxd[i].ToString());


            }


            cmd.CommandText = sb.ToString();

            cmd.Connection = con;

            cmd.CommandTimeout = 320000;
				
				
				con.Open();
				rowseffected = cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                con.Dispose();

			return rowseffected;

		}

        public string GetInternalMappedName(string opsName)
        {
            switch (opsName)
            {
                case "WeBareBears":
                    return "We_Bare_Bears";
                case "RodentRage":
                    return "RodentRage";
                case "StarDefence":
                    return "Star_Defence";
                case "ClockTower":
                    return "ClockTower";
                case "AlienTakeDown":
                    return "Alien_Takedown";
                case "MayanTemple":
                    return "Mayan_Temple";
                default:
                    return opsName;
            }
        }
        public int AddMaster(string data, string collectionName)
        {
            int insertId = 0;


            

         
            String SQL = "Insert into MasterLogs(primarypartition,log) Values (@c,@log);SELECT LAST_INSERT_ID();";
            using (MySqlConnection con = new MySqlConnection(connectionParameters.ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(SQL, con);
                cmd.Parameters.AddWithValue("@log", data);
                cmd.Parameters.AddWithValue("@c", collectionName);
                con.Open();
                insertId = int.Parse( cmd.ExecuteScalar()?.ToString()??"0");
            
            }

            return insertId;

        }

        public int AddBulk(DataTable dt)
		{

			//foreach (var itm in yourList)
			//{
			//	DataRow row = dt.NewRow();
			//	row["Field1"] = itm.Field1;
			//	row["Field2"] = itm.Field2;
			//	dt.Rows.Add(row);
			//}
			MySqlConnection con = new MySqlConnection(connectionParameters.ConnectionString);
			string tempCsvFileSpec = @"C:\Users\Gord\Desktop\dump.csv";
			ToCSV(dt, "abc\\csvf.csv");
			var msbl = new MySqlBulkLoader(con);
			msbl.TableName = "logs";
			msbl.FileName = tempCsvFileSpec;
			msbl.FieldTerminator = ",";
			msbl.FieldQuotationCharacter = '"';
			int x = msbl.Load();
			System.IO.File.Delete(tempCsvFileSpec);
			return x;
		}

		public static string DataTableToCSV( DataTable datatable, char seperator)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < datatable.Columns.Count; i++)
			{
				sb.Append(datatable.Columns[i]);
				if (i < datatable.Columns.Count - 1)
					sb.Append(seperator);
			}
			sb.AppendLine();
			foreach (DataRow dr in datatable.Rows)
			{
				for (int i = 0; i < datatable.Columns.Count; i++)
				{
					sb.Append(dr[i].ToString());

					if (i < datatable.Columns.Count - 1)
						sb.Append(seperator);
				}
				sb.AppendLine();
			}
			return sb.ToString();
		}

		public static void ToCSV(DataTable dtDataTable, string strFilePath)
		{
			StreamWriter sw = new StreamWriter(strFilePath, false);
			//headers  
			for (int i = 0; i < dtDataTable.Columns.Count; i++)
			{
				sw.Write(dtDataTable.Columns[i]);
				if (i < dtDataTable.Columns.Count - 1)
				{
					sw.Write(",");
				}
			}
			sw.Write(sw.NewLine);
			foreach (DataRow dr in dtDataTable.Rows)
			{
				for (int i = 0; i < dtDataTable.Columns.Count; i++)
				{
					if (!Convert.IsDBNull(dr[i]))
					{
						string value = dr[i].ToString();
						if (value.Contains(','))
						{
							value = String.Format("\"{0}\"", value);
							sw.Write(value);
						}
						else
						{
							sw.Write(dr[i].ToString());
						}
					}
					if (i < dtDataTable.Columns.Count - 1)
					{
						sw.Write(",");
					}
				}
				sw.Write(sw.NewLine);
			}

		}
	}
}
