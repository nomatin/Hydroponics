using System;

using System.IO;


namespace hydroponics.sqlClasses
{
    static class pointBd
    {
        public static DataBase database;
        public static DataBase Database
        {
            get
            {
                if (database == null)
                {
                    database = new DataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "listPots.db3"));
                }
                return database;
            }

        }
    }
}
