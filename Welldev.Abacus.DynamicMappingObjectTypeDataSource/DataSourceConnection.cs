using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Threading;
using MFiles.Extensibility.ExternalObjectTypes;
using MFiles.Extensibility.Framework.ExternalObjectTypes;
using MFiles.Extensibility.Framework.Terminologies;
using MFiles.VAF.Configuration;
using Newtonsoft.Json;
using Welldev.Abacus.DynamicMappingObjectTypeDataSource.Models;

namespace Welldev.Abacus.DynamicMappingObjectTypeDataSource
{
	/// <summary>
	/// An implementation of IExternalObjectTypeConnection,
	/// using a JsonDataProvider for the actual data storage/retrieval.
	/// </summary>
	/// 
	// Note that implementations for reading data, inserting data, updating data, and deleting data
	// have been split into separate partial classes:
	// DataSourceConnection.cs (helper methods)
	// DataSourceConnection.Read.cs (methods for reading data)
	// DataSourceConnection.Insert.cs (methods for inserting data)
	// DataSourceConnection.Update.cs (methods for updating data)
	// DataSourceConnection.Delete.cs (methods for deleting data)
	public partial class DataSourceConnection
		: ExternalObjectTypeConnectionBase
	{
		/// <summary>
		/// Connection configuration.
		/// </summary>
		protected ExternalObjectTypeConfiguration<ConfigurationRoot> Config { get; set; }

		/// <summary>
		/// Map of managed types to ColumnType.
		/// </summary>
		private static readonly Dictionary<Type, ColumnType> typeMappingByType = new Dictionary<Type, ColumnType>() {
				{ typeof( string ), ColumnType.DBTYPE_WSTR },
				{ typeof( int ), ColumnType.DBTYPE_I4 },
				{ typeof( bool ), ColumnType.DBTYPE_BOOL },
				{ typeof( double ), ColumnType.DBTYPE_DECIMAL },
				{ typeof( DateTime ), ColumnType.DBTYPE_DBDATE } };

		/// <summary>
		/// Instantiates a DataSourceConnection for a specific data source.
		/// </summary>
		/// <param name="config">The configuration.</param>
		/// <param name="stopToken">Stop token.</param>
		public DataSourceConnection(
			ExternalObjectTypeConfiguration<ConfigurationRoot> config,
			CancellationToken stopToken
		) : base( config, stopToken )
		{
			// Set.
			this.Config = config;
			this.MapColumn();
		}

        #region Utility 
		private void MapColumn()
		{
			var list = new List<ColumnMappingData>();
			var firstNode = GetFirstNode();
			var ordinal = 1;
			foreach (var property in firstNode.GetType().GetProperties())
			{
				var ty = typeMappingByType[property.PropertyType];
				list.Add(new CustomColumMappingData()
				{
					Insert = false,
					Update= false,
					MappingType = property.Name == nameof(Datum.id) ? ColumnMappingType.ObjectId : ColumnMappingType.Property,
					SourceColumnName= property.Name,
					CustomSourceColumnName= property.Name,
					Ordinal = ordinal++,
				});
			}

			this.Config.ColumnMapping = list;
			this.Config.ColumnMappingEx = list;
		}
		private Datum GetFirstNode()
		{
			var httpClient = new HttpClient();
			var response = httpClient.GetStringAsync(Defaults.UsersEndPoint).Result;
			var rootModel = JsonConvert.DeserializeObject<Root>(response);
			var datumModel = rootModel.data.FirstOrDefault();
			return datumModel;
		}
        #endregion


        /// <summary>
        /// Close connection.
        /// </summary>
        public override void CloseConnectionImpl()
		{
			// TODO: Close any connection, if needed.
		}
	}
    [Serializable]
    [DataContract]
    [PreviewableTextEditor(UseNonPublicMembers = true, NameMember = "CustomSourceColumnName", PreviewTemplate = "{0}{1}{2}", PreviewSources = new string[] { "._children{ .key == 'objectTypeMappingType' }", "._children{ .key == 'valueListMappingType' }", "._children{ .key == 'targetProperty' }" }, PreviewUnsetTexts = new string[] { "", "", "" }, PreviewValueFormats = new string[] { null, null, ": {0}" })]
    public class CustomColumMappingData  : ColumnMappingData, IEquatable<CustomColumMappingData>
	{
        //[DataMember(Name = "CustomSourceColumnName", IsRequired = false, EmitDefaultValue = false, Order = 1)]
        //[JsonConfEditor(Label = "Custom Source Column", IsRequired = true, HelpText = "Specify the name of the column to be mapped to M-Files.")]
        //[Security(ChangeBy = SecurityAttribute.UserLevel.VaultAdmin)]
        //public virtual MFIdentifier CustomSourceColumnName { get; set; }

        //[DataMember(Name = "objectTypeMappingTypeCustom", IsRequired = true, EmitDefaultValue = false, Order = 7)]
        //[JsonConfEditor(Label = "Mapping Type Custom", DefaultValue = ObjectTypeColumnMappingType.Ignore, HelpText = "Specifies how the source column is mapped to M-Files.\n\n - Ignore: The source column is not mapped.\n - Object ID: The source column is mapped as the external object ID.\n - Property: The source column is mapped as an M-Files property. To use this mapping type, specify the \"Target Property\" setting.")]
        //[Security(ChangeBy = SecurityAttribute.UserLevel.VaultAdmin)]
        //internal ObjectTypeColumnMappingType? ObjectTypeMappingTypeCustom { get; set; }

        //[DataMember(Name = "targetPropertyCusmtom", IsRequired = false, EmitDefaultValue = false, Order = 4)]
        //[JsonConfEditor(Label = "Target Property Custom", Hidden = true, ShowWhen = ".parent._children{ .key == 'objectTypeMappingType' && .value == 'Property' }", Options = "{ 'liveDatatype': { 'id': 'target', args: [ 'sourceColumnName' ] } }")]
        //[MFPropertyDef(AllowEmpty = true, Required = false)]
        //[Security(ChangeBy = SecurityAttribute.UserLevel.VaultAdmin)]
        //public virtual MFIdentifier TargetPropertyCustom { get; set; }

        //public bool Equals(CustomColumMappingData other)
        //{
        //    if (other == null)
        //    {
        //        return false;
        //    }

        //    if (MappingType != other.MappingType || SourceColumnName != other.SourceColumnName || SourceType != other.SourceType || Ordinal != other.Ordinal)
        //    {
        //        return false;
        //    }

        //    if (MappingType != 0 && (Insert != other.Insert || Update != other.Update))
        //    {
        //        return false;
        //    }

        //    if (MappingType == ColumnMappingType.Property && TargetProperty?.ID != other.TargetProperty?.ID)
        //    {
        //        return false;
        //    }

        //    return true;
        //}
    }
}
