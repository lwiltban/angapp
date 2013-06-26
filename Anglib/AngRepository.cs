using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngModel;
using Lds.CES.OracleNETBase.OracleRepository;

namespace Anglib
{
    public class AngRepository : BaseRepository
    {
        protected override string ConnectionString()
        {
            return ConfigurationManager.AppSettings["Main.ConnectionString"];
        }

        public string ObjAsString(object obj)
        {
            return base.SafeString(obj);
        }

        public int ObjAsInt(object obj)
        {
            return base.SafeInt(obj);
        }

        public IEnumerable<User> GetUsers(string sql, Hashtable parms)
        {
            var table = GetDataTableForQuery(sql, parms);
            if (table != null)
            {
                List<User> users = new List<User>();
                foreach (DataRow row in table.Rows)
                {
                    Hashtable t = new Hashtable();
                    foreach (DataColumn column in table.Columns)
                    {
                        t.Add(column.ColumnName, row[column]);
                    }
                    var first = ObjAsString(row["FIRST_NAME"]);
                    var last = ObjAsString(row["LAST_NAME"]);
                    var emplid = ObjAsInt(row["EMPLID"]);
                    var user = new User()
                    {
                        id = emplid,
                        firstName = first,
                        lastName = last
                    };
                    users.Add(user);
                }
                return users;
            }
            return null;
        }

    }
}
