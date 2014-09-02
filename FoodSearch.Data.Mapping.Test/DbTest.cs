using FoodSearch.Data.Mapping.Interface;
using FoodSearch.Data.Mapping.Repository;
using System;
using System.IO;

namespace FoodSearch.Data.Mapping.Test
{
    public abstract class DbTest
    {
        private readonly string _dbName = "FoodSearch";
        private readonly string _dbPath;

        protected ISessionSource SessionSource
        {
            get { return new UnitTestSessionSource(_dbName, _dbPath); }
        }

        public IStoredProcedureRepository StoredProcedures
        {
            get { return new FoodSearchStoredProcedureRepository(SessionSource); }
        }

        public IRepository<T> RepositoryOf<T>() where T : class
        {
            return new FoodSearchRepository<T>(SessionSource);
        }

        protected DbTest()
        {
            var path = typeof (DbTest).Assembly.CodeBase;
            var pathUri = new Uri(path);
            _dbPath = Path.GetDirectoryName(pathUri.LocalPath + pathUri.Fragment);
            string dbFileName = _dbName + ".mdf";
            string orgFileName = Path.Combine(_dbPath, dbFileName);
            string orgFileNameLog = Path.Combine(_dbPath, string.Format("{0}_log.ldf", _dbName));
            Guid guid = Guid.NewGuid();
            string newFileName = Path.Combine(_dbPath, string.Format("{0}.mdf", guid));
            string newFileNameLog = Path.Combine(_dbPath, string.Format("{0}_log.ldf", guid));
            _dbPath = newFileName;
            File.Copy(orgFileName, newFileName);
            File.Copy(orgFileNameLog, newFileNameLog);
        }
    }
}
