using System;
using System.Collections.Generic;
using MFiles.Extensibility.ExternalObjectTypes;
using MFiles.Extensibility.Framework.ExternalObjectTypes;

namespace Welldev.MyExternalObject.MyExternalObjectTypeDataSource
{
	/// <summary>
	/// An implementation of IExternalObjectTypeConnection,
	/// using a JsonDataProvider for the actual data storage/retrieval.
	/// Parts for reading data.
	/// </summary>
	public partial class DataSourceConnection
	{
		/// <summary>
		/// Gets the available columns from the currently open selection.
		/// </summary>
		/// <returns>Collection of available columns.</returns>
		public override List<ColumnDefinition> GetAvailableColumns()
		{
			// TODO: Return the column definitions that are supported for the current configuration.
			// NOTE: You may need to use "this.Config.CustomConfiguration" to see what's configured.
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the items as specified by the select statement.
		/// </summary>
		/// <returns>Items from the data source</returns>
		public override IEnumerable<DataItem> GetItems()
		{
			// TODO: Return actual items.
			// NOTE: You may need to use "this.Config.CustomConfiguration" to see what's configured.
			// NOTE: You will probably want to return instances of DataItemSimple.
			throw new NotImplementedException();
		}
	}
}
