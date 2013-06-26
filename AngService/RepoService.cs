using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anglib;
using AngModel;

namespace AngService
{
    public class RepoService
    {
        private AngRepository Repo { get; set; }

        public RepoService()
        {
            Repo = new AngRepository();
        }

        public IEnumerable<User> GetUsers(int? id)
        {
            string sql = string.Empty;
            Hashtable parms= new Hashtable();
            if (id == null)
            {
                sql = "select * from ps_persons_vw where emplid > :emplid";
                parms.Add("emplid", 9090001);
            }
            else
            {
                sql = "select * from ps_persons_vw where emplid = :emplid";
                parms.Add("emplid", id.Value);
            }
            return Repo.GetUsers(sql, parms);
        }

    }
}
