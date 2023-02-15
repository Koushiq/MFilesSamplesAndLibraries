using MFiles.VAF.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Welldev.MyExternalObject.MyExternalObjectTypeDataSource
{
	/// <summary>
	/// Configuration.
	/// </summary>
	[DataContract]
	[JsonConfEditor]
	public class ConfigurationRoot
	{
        #region Fields
        public string Host { get; set; }
        public string UserId { get; set; }
		public string Password { get; set; }
		public string Database { get; set; }
		public string ColumnName { get; set; }
        #endregion

    }
}
