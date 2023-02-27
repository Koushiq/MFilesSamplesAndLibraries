using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MFiles.Extensibility.Applications;
using MFiles.Extensibility.ExternalObjectTypes;
using MFiles.Extensibility.Framework.ExternalObjectTypes;
using MFiles.VAF.Configuration;
using MFilesAPI;
using WellDev.AbacusMatrixReport.ExternalObjectTypeDataSource.Services;

namespace WellDev.AbacusMatrixReport.ExternalObjectTypeDataSource
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
			//this.InitializeProperties();
			this.InitializeColumnMapping();

        }

        #region UtilityMethods
        private void InitializeProperties()
		{
			var firstNode = JsonToModelFormatter.GetFirstNode(); 
			var propertyListDef = new List<PropertyDefAdmin>();

            var clientApplication = new MFilesClientApplication();
            var vault =
                clientApplication
                        .GetVaultConnectionsWithGUID(this.Config.CustomConfiguration.VaultGuid)
                        .Cast<VaultConnection>()
                        .FirstOrDefault()?
                        .BindToVault(IntPtr.Zero, true, true);

            for (int i = 0; i < firstNode.Count; i++)
			{
                var propertyDefAdmin = new PropertyDefAdmin();
				propertyDefAdmin.SemanticAliases = new SemanticAliases() { Value = DMSDefaults.PropertyNamePerfix + firstNode[i].PropertyName } ;
				var propertyDef = propertyDefAdmin.PropertyDef;

                if (firstNode[i].PropertyType == "int")
				{
                    propertyDef.DataType = MFDataType.MFDatatypeInteger;
                }
				else
				{
                    propertyDef.DataType = MFDataType.MFDatatypeText;
				}

                propertyDef.Name = firstNode[i].PropertyName;
                propertyDef.ValueList = -1;
                propertyDef.ContentType = MFContentType.MFContentTypeGeneric;
                propertyDef.UpdateType = MFUpdateType.MFUpdateTypeNormal;
				propertyListDef.Add( propertyDefAdmin );
				vault.PropertyDefOperations.AddPropertyDefAdmin(propertyDefAdmin);
            }

            var propertyValues = new MFilesAPI.PropertyValues();
			var classPropertyValue = new MFilesAPI.PropertyValue()
			{
				PropertyDef = (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefClass,
			};

			classPropertyValue.Value.SetValue(
				MFDataType.MFDatatypeLookup,  // This must be correct for the property definition.
				101 // This must be the ID of a class within the object type specified below.
			);
			propertyValues.Add(-1, classPropertyValue);

			var sourceFiles = new MFilesAPI.SourceObjectFiles();
			var objectTypeID = 101; // Project

			var isSingleFileDocument =
					objectTypeID == (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument && sourceFiles.Count == 1;

			var objectVersion = vault.ObjectOperations.CreateNewObjectEx(
								objectTypeID,
								propertyValues,
								CheckIn: true);
		}

        private void InitializeColumnMapping()
		{
            //initialize property if needed here 
            this.Config.CustomConfiguration.Fields = JsonToModelFormatter.GetFirstNode();
            var fields = this.Config.CustomConfiguration.Fields;
			var list = new List<ColumnMappingData>();
			var firstNode = fields[0];
			var ordinal = 1;
			list.Add(new ColumnMappingData()
			{
				SourceColumnName = firstNode.PropertyName,
				MappingType = ColumnMappingType.ObjectId,
				Ordinal = ordinal++,
			});

			for (int i = 1; i < fields.Count; i++)
			{

				MFIdentifier targetProperty = DMSDefaults.PropertyNamePerfix + fields[i].PropertyName;
                list.Add(new ColumnMappingData()
                {
					SourceColumnName= fields[i].PropertyName,
					MappingType = ColumnMappingType.Property,
					Ordinal = ordinal++,
                    Insert = false,
                    Update = false,
                    TargetProperty = targetProperty
                });
            }
			this.Config.ColumnMapping= list;
			this.Config.ColumnMappingEx = list;
			
        }

        #endregion
        /// <summary>
        /// Close connection.
        /// </summary>
        public override void CloseConnectionImpl()
		{
			// TODO: Close any connection, if needed.
			//throw new NotImplementedException();
		}
	}
}
