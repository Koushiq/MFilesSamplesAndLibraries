using MFiles.Extensibility.ExternalObjectTypes;
using System;

namespace Welldev.AbacusMasterDataSync.AbacusSyncObjectTypeDataSource.Data.Domains
{
    [Serializable]
    public class FileExtension : DataItem, IEntity
    {
        public int Id { get; set; }
        public string NameOrTitle { get; set; }

        public override object GetValue(int columnOrdinal)
        {
            return null;
            //throw new NotImplementedException();
        }
    }
}
