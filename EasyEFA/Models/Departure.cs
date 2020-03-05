using System;
using System.Runtime.Serialization;

namespace EasyEFA.Models
{
	public class Departure
	{
		[DataMember(Name = "line")]
		public Line Line { get; set; }

		[DataMember(Name = "PlannedDateTime")]
		public DateTime PlannedDateTime { get; set; }

		[DataMember(Name = "RealDateTime")]
		public DateTime? RealDateTime { get; set; }
	}
}
