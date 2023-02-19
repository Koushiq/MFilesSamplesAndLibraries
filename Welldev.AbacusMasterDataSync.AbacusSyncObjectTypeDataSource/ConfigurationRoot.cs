using MFiles.VAF.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Welldev.AbacusMasterDataSync.AbacusSyncObjectTypeDataSource
{
	/// <summary>
	/// Configuration.
	/// </summary>
	[DataContract]
	[JsonConfEditor]
	public class ConfigurationRoot
	{
        //      [DataMember]
        //      [Security(ChangeBy = SecurityAttribute.UserLevel.VaultAdmin)]
        //      public string ConnectionStringRaw { get; set; }
        //[DataMember]
        //      [Security(ChangeBy = SecurityAttribute.UserLevel.VaultAdmin)]
        //      public string TableName { get; set; }

        //"Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
        //[Security(ChangeBy = SecurityAttribute.UserLevel.VaultAdmin)]
        //[DataMember]
        //[MFPropertyDef(Required = true)]
        //public MFIdentifier NameOrTitle = "{3E2BB7EB-C49E-4C8C-825C-CAE0AEBA9A06}";
    }
}
