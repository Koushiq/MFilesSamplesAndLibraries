using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MFiles.Extensibility.ExternalObjectTypes;
using MFiles.Extensibility.Framework.ExternalObjectTypes;

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
			this.Config.ColumnMapping = this.InitializeColumnMapping();
			this.Config.ColumnMappingEx = this.Config.ColumnMapping;

        }
		private List<ColumnMappingData> InitializeColumnMapping()
		{
			//initialize property if needed here 


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
                list.Add(new ColumnMappingData()
                {
					SourceColumnName= fields[i].PropertyName,
					MappingType = ColumnMappingType.Property,
					Ordinal = ordinal++,
					Insert=false,
					Update=false,
					TargetProperty= fields[i].PropertyName,
                });
            }

			return list;
			
        }
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
