using MFiles.VAF.Configuration;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WellDev.AbacusMatrixReport.ExternalObjectTypeDataSource.Models;
using WellDev.AbacusMatrixReport.ExternalObjectTypeDataSource.Services;

namespace WellDev.AbacusMatrixReport.ExternalObjectTypeDataSource
{
	/// <summary>
	/// Configuration.
	/// </summary>
	[DataContract]
	[JsonConfEditor]
	public class ConfigurationRoot
	{
        [Security(ChangeBy = SecurityAttribute.UserLevel.VaultAdmin)]
		[DataMember]
		public IList<InitializationModel> Fields { get; set; }

        [Security(ChangeBy = SecurityAttribute.UserLevel.VaultAdmin)]
        [DataMember]
        public string VaultGuid { get; set; }	
    }
}
