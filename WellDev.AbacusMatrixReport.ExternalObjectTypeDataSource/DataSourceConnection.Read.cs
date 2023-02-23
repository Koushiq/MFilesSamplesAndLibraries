using System;
using System.Collections.Generic;
using System.Linq;
using MFiles.Extensibility.ExternalObjectTypes;
using MFiles.Extensibility.Framework.ExternalObjectTypes;
using WellDev.AbacusMatrixReport.ExternalObjectTypeDataSource.Services;

namespace WellDev.AbacusMatrixReport.ExternalObjectTypeDataSource
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
			var fields = this.Config.CustomConfiguration.Fields;
			var list = new List<ColumnDefinition>();
			var ordinalCounter = 1;
			
			foreach (var field in fields) {
				var type = field.PropertyType == "int" ? typeof(int) : typeof(string);
				list.Add(new ColumnDefinition()
				{
					Name = field.PropertyName,
					Ordinal= ordinalCounter++,
					Type = typeMappingByType[type]
                });
			}

			return list;
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
			var list = new List<DataItemSimple>();
			var models = JsonToModelFormatter.GetAllNodesAsModel();
			foreach (var model in models)
			{
                var dictionaryItem = new Dictionary<int, object>();
				var ordinalCount = 1;

				if(model.PropertyType== "int")
				{
					dictionaryItem.Add(ordinalCount++, Convert.ToInt32(model.PropertyValue));
                }
				else
				{
					dictionaryItem.Add(ordinalCount++, Convert.ToString(model.PropertyValue));
				}
                var dataItemSimple = new DataItemSimple(dictionaryItem);
				list.Add(dataItemSimple);
            }
			return list;
		}
	}
}
