using MFiles.VAF.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WellDev.AbacusMatrixReport.ExternalObjectTypeDataSource.Services;

namespace WellDev.AbacusMatrixReport.ExternalObjectTypeDataSource.Models
{
    [DataContract]
    public class InitializationModel
    {
        [DataMember]
        public string PropertyName { get; set; }
        [DataMember]
        [JsonConfEditor(TypeEditor = "options",
        Options = JsonToModelFormatter.AvaliableDataTypes)]
        public string PropertyType { get; set; }
        [DataMember]
        public string PropertyValue { get; set; }
    }
}
