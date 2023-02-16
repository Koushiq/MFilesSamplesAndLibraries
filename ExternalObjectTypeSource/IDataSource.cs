using MFiles.Server.Extensions;

namespace ExternalObjectTypeSource
{
    public class DataSource : IDataSource, IDataSourceConnection
    {
        public bool CanAlterData()
        {
            throw new NotImplementedException();
        }

        public bool CanGetOneItem()
        {
            throw new NotImplementedException();
        }

        public void CloseConnection()
        {
            throw new NotImplementedException();
        }

        public void CompletedDataRetrieval()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ColumnDefinition> GetAvailableColumns()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DataItem> GetItems()
        {
            throw new NotImplementedException();
        }

        public DataItem GetOneItem(int columnOrdinalExtID, string externalID)
        {
            throw new NotImplementedException();
        }

        public void Interrupt()
        {
            throw new NotImplementedException();
        }

        public IDataSourceConnection OpenConnection(string connectionString, Guid configurationId)
        {
            throw new NotImplementedException();
        }

        public void PrepareForDataRetrieval(string selectStatement)
        {
            throw new NotImplementedException();
        }

        public void SetColumnsToRetrieve(int[] columnOrdinals)
        {
            throw new NotImplementedException();
        }
    }
}