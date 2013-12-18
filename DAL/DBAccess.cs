using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class DBAccess
    {
        private DBAccess()
        {
        }
        private volatile static MSSQL instance;
        private static object lockingObject = new object();
        public static MSSQL InstanceCreation()
        {

            if (instance == null)
            {
                lock (lockingObject)
                {
                    if (instance == null)
                    {
                        instance = new MSSQL();
                    }
                }
            }
            return instance;
        }
    }
}
