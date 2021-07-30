using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounting1.DAL
{
    class adapter
    {
        private string sql;
        private Sqlconnection conn;

        public adapter(string sql, Sqlconnection conn)
        {
            // TODO: Complete member initialization
            this.sql = sql;
            this.conn = conn;
        }

        internal static void Fill(System.Data.DataTable dt)
        {
            throw new NotImplementedException();
        }
    }
}
