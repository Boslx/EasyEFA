using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace EasyEFA.Models
{
	public class Station
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "stationId")]
		public string StationId { get; set; }

		[DataMember(Name = "location")]
		public double[] Location { get; set; }
    }
}
