using MFiles.Extensibility.ExternalObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Welldev.AbacusMasterDataSync.AbacusSyncObjectTypeDataSource.Data.Domains
{
    public class FileExtension : DataItem, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override object GetValue(int columnOrdinal)
        {
            return null;
            //throw new NotImplementedException();
        }
    }
}
