using MFiles.Extensibility.ExternalObjectTypes;
using MFiles.Extensibility.Framework.ExternalObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welldev.AbacusMasterDataSync.AbacusSyncObjectTypeDataSource.Data;

namespace Welldev.AbacusMasterDataSync.AbacusSyncObjectTypeDataSource.Factories
{
    public  class DataPrepareFactory
    {
        private DbConnector dbConnector;
        public DataPrepareFactory(DbConnector dbConnector)
        {
            this.dbConnector = dbConnector;
        }

        public IEnumerable<DataItem> PrepareGetItems()
        {
            var datas = this.dbConnector.GetAllData();
            return datas;
        }

        public List<ColumnDefinition> PrepareAvaliableColums()
        {
            var datas = this.dbConnector.GetAllColumnDefinations();
            return datas;
        }

    }
}
