using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounting1.DAL
{
    class Sqlconnection
    {
        private string myconnstrng;

        public Sqlconnection(string myconnstrng)
        {
            // TODO: Complete member initialization
            this.myconnstrng = myconnstrng;
        }

        public Sqlconnection()
        {
            // TODO: Complete member initialization
        }

        internal void Close()
        {
            throw new NotImplementedException();
        }

        internal void Open()
        {
            throw new NotImplementedException();
        }
    }
}
