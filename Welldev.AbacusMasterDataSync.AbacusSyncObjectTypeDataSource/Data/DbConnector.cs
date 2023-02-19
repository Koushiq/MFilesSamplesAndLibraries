using MFiles.Extensibility.ExternalObjectTypes;
using MFiles.Extensibility.Framework.ExternalObjectTypes;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
//using Welldev.AbacusMasterDataSync.AbacusSyncObjectTypeDataSource.Data.Builders;
using Welldev.AbacusMasterDataSync.AbacusSyncObjectTypeDataSource.Data.Domains;

namespace Welldev.AbacusMasterDataSync.AbacusSyncObjectTypeDataSource.Data
{
    public class SQLServerTypes
    {
        public static readonly string INTERGER = "int";
        public static readonly string NCHAR = "nchar";
    }
    public class DbConnector
    {
        private SqlConnection Connection { get; set; }
        private ExternalObjectTypeConfiguration<ConfigurationRoot> configuration;
        public DbConnector(ExternalObjectTypeConfiguration<ConfigurationRoot> configuration)
        {
            this.configuration = configuration;
            this.DbConnection(this.configuration);
        }
        private void DbConnection(ExternalObjectTypeConfiguration<ConfigurationRoot> configuration)
        {
            var connetionString = "Data Source=DESKTOP-KK6D3RE;Initial Catalog=Sample Vault Extension;User ID=sa;Password=admin;"; //configuration.CustomConfiguration.ConnectionStringRaw as string;
            Connection = new SqlConnection(connetionString);
            try
            {
                Connection.Open();
                Console.WriteLine("Connection Open ! ");
                //var list = new List<ColumnMappingData>();
                //list.Add(new ColumnMappingData()
                //{
                //    SourceColumnName = nameof(FileExtension.Name),
                //    MappingType = ColumnMappingType.Property,
                //    Insert = true,
                //    Update = true,
                //    Ordinal = 2,
                //    TargetPropertyDefId = 0
                //});
                
                //configuration.ColumnMapping = list;
                Connection.Close();
            }
            catch (Exception ex)
            {
               Console.WriteLine("Can not open connection ! ");
            }
        }
        
        private ColumnType GetColumnType(object obj)
        {

            var objRawName = obj as string;
            if (objRawName == null)
            {
                throw new ArgumentException("Unsupported Column Type");
            }
            ColumnType columnType = ColumnType.DBTYPE_EMPTY;
            
            if(objRawName == SQLServerTypes.NCHAR )
            {
                columnType =  ColumnType.DBTYPE_STR;
            }
            else if(objRawName==SQLServerTypes.INTERGER )
            {
                columnType = ColumnType.DBTYPE_NUMERIC;
            }
            return columnType;
        }
        public List<ColumnDefinition> GetAllColumnDefinations()
        {
            var query = "  SELECT\r\n        COLUMN_NAME, ORDINAL_POSITION, DATA_TYPE\r\n    FROM\r\n        INFORMATION_SCHEMA.COLUMNS\r\n    WHERE\r\n        TABLE_NAME = 'FileExtension'";
            var command = new SqlCommand(query, Connection);
            var dataList = new List<ColumnDefinition>();
            Connection.Open();
            var reader = command.ExecuteReader();
            while(reader.Read())
            {
                var columnDefinition = new ColumnDefinition();
                columnDefinition.Name = Convert.ToString(reader["COLUMN_NAME"]);
                columnDefinition.Ordinal = Convert.ToInt32(reader["ORDINAL_POSITION"]);
                columnDefinition.Type = GetColumnType(reader["DATA_TYPE"]);
                dataList.Add(columnDefinition);
            }
            Connection.Close();
            return dataList;
        }
        public IEnumerable<DataItem> GetAllData()
        {
            var tableName = "FileExtension"; //this.configuration.CustomConfiguration.TableName as string;
            var query = "select * from " + tableName;
            var command = new SqlCommand(query, Connection);
            var dataList = new List<DataItem>();
            Connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var fileExtension = new FileExtension();
                fileExtension.Id = Convert.ToInt32(reader[nameof(FileExtension.Id)]);
                fileExtension.Name = Convert.ToString(reader[nameof(FileExtension.Name)]);
                dataList.Add(fileExtension);
            }
            Connection.Close();
            return dataList;
        }
        public void UpdateData()
        {
            //var tableName = this.configuration.CustomConfiguration.TableName as string;

        }
        public void CreateData()
        {
            //var tableName = this.configuration.CustomConfiguration.TableName as string;

        }
        public void DeleteData()
        {
            //var tableName = this.configuration.CustomConfiguration.TableName as string;

        }
    }
}
