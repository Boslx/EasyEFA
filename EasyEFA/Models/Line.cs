using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace EasyEFA.Models
{
	public class Line
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "number")]
		public string Number { get; set; }

		[DataMember(Name = "direction")]
		public string Direction { get; set; }

		[DataMember(Name = "directionFrom")]
		public string DirectionFrom { get; set; }
    }
}
