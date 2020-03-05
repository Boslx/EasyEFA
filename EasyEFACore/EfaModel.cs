namespace EasyEFACore
{
	using System;
	using Newtonsoft.Json;

	/// <summary>
	/// The Points property returns either an object or an array in certain cases. 
	/// </summary>
	public class PointsConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotImplementedException();

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			switch (reader.TokenType)
			{
				case JsonToken.StartObject:
					return new[] { serializer.Deserialize<Points>(reader).Point };
				case JsonToken.StartArray:
					return serializer.Deserialize<Point[]>(reader);
				default:
					return null;
			}
		}

		public override bool CanConvert(Type objectType) => throw new NotImplementedException();
	}

	/// <summary>
	/// !!!WIP!!!
	/// The data types are currently not chosen ideally. There are probably still some serialization errors hidden here. 
	/// </summary>
	public class EfaModel
	{
		[JsonProperty("parameters")]
		public Parameter[] Parameters { get; internal set; }

		[JsonProperty("dm")]
		public Station Dm { get; internal set; }

		[JsonProperty("arr")]
		public Station Arr { get; internal set; }

		[JsonProperty("dateTime")]
		public EfaCurrentDateTime DateTime { get; internal set; }

		[JsonProperty("dateRange")]
		public EfaDateRange[] DateRange { get; internal set; }

		[JsonProperty("option")]
		public Option Option { get; internal set; }

		[JsonProperty("servingLines")]
		public ServingLines ServingLines { get; internal set; }

		[JsonProperty("departureList")]
		public DepartureList[] DepartureList { get; internal set; }
	}

	public class Station
	{
		[JsonProperty("input")]
		public Input Input { get; internal set; }

		[JsonProperty("points")]
		[JsonConverter(typeof(PointsConverter))]
		public Point[] Points { get; internal set; }
	}

	public class Points
	{
		[JsonProperty("point")]
		public Point Point { get; internal set; }
	}

	public class Input
	{
		[JsonProperty("input")]
		public string InputInput { get; internal set; }
	}

	public class Point
	{
		[JsonProperty("usage")]
		public string Usage { get; internal set; }

		[JsonProperty("type")]
		public string Type { get; internal set; }

		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("stateless")]
		public string Stateless { get; internal set; }

		[JsonProperty("ref")]
		public Ref Ref { get; internal set; }

		[JsonProperty("infos")]
		public object Infos { get; internal set; }
	}

	public class Ref
	{
		[JsonProperty("id")]
		public int Id { get; internal set; }

		[JsonProperty("gid")]
		public string Gid { get; internal set; }

		[JsonProperty("omc")]
		public int Omc { get; internal set; }

		[JsonProperty("placeID")]
		public uint PlaceId { get; internal set; }

		[JsonProperty("place")]
		public string Place { get; internal set; }

		[JsonProperty("coords")]
		public string Coords { get; internal set; }
	}

	public class EfaDateRange
	{
		[JsonProperty("day")]
		public string Day { get; internal set; }

		[JsonProperty("month")]
		public string Month { get; internal set; }

		[JsonProperty("year")]
		public int Year { get; internal set; }

		[JsonProperty("weekday")]
		public int Weekday { get; internal set; }
	}

	public class EfaCurrentDateTime:EfaDateTime
	{
		[JsonProperty("deparr")]
		public string Deparr { get; internal set; }

		[JsonProperty("ttpFrom")]
		public int TtpFrom { get; internal set; }

		[JsonProperty("ttpTo")]
		public int TtpTo { get; internal set; }
	}

	public class DepartureList
	{
		[JsonProperty("stopID")]
		public int StopId { get; internal set; }

		[JsonProperty("x")]
		public decimal X { get; internal set; }

		[JsonProperty("y")]
		public decimal Y { get; internal set; }

		[JsonProperty("mapName")]
		public string MapName { get; internal set; }

		[JsonProperty("area")]
		public int Area { get; internal set; }

		[JsonProperty("platform")]
		public string Platform { get; internal set; }

		[JsonProperty("platformName")]
		public string PlatformName { get; internal set; }

		[JsonProperty("stopName")]
		public string StopName { get; internal set; }

		[JsonProperty("nameWO")]
		public string NameWo { get; internal set; }

		[JsonProperty("pointType", NullValueHandling = NullValueHandling.Ignore)]
		public string PointType { get; internal set; }

		[JsonProperty("countdown")]
		public int Countdown { get; internal set; }

		[JsonProperty("dateTime")]
		public EfaDateTime DateTime { get; internal set; }

		[JsonProperty("realDateTime", NullValueHandling = NullValueHandling.Ignore)]
		public EfaDateTime RealDateTime { get; internal set; }

		[JsonProperty("servingLine")]
		public ServingLine ServingLine { get; internal set; }

		[JsonProperty("operator")]
		public Operator Operator { get; internal set; }

		[JsonProperty("attrs", NullValueHandling = NullValueHandling.Ignore)]
		public Parameter[] Attrs { get; internal set; }
	}

	public class Parameter
	{
		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("value")]
		public string Value { get; internal set; }
	}

	public class EfaDateTime
	{
		public DateTime DateTime => new DateTime(Year, Month, Day, Hour, Minute, 0);

		[JsonProperty("year")]
		public int Year { get; internal set; }

		[JsonProperty("month")]
		public int Month { get; internal set; }

		[JsonProperty("day")]
		public int Day { get; internal set; }

		[JsonProperty("weekday")]
		public int Weekday { get; internal set; }

		[JsonProperty("hour")]
		public int Hour { get; internal set; }

		[JsonProperty("minute")]
		public int Minute { get; internal set; }
	}

	public class Operator
	{
		[JsonProperty("code")]
		public string Code { get; internal set; }

		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("publicCode")]
		public string PublicCode { get; internal set; }
	}

	public partial class ServingLine
	{
		[JsonProperty("key")]
		public int Key { get; internal set; }

		[JsonProperty("code")]
		public int Code { get; internal set; }

		[JsonProperty("number")]
		public string Number { get; internal set; }

		[JsonProperty("symbol")]
		public int Symbol { get; internal set; }

		[JsonProperty("motType")]
		public int MotType { get; internal set; }

		[JsonProperty("mtSubcode")]
		public int MtSubcode { get; internal set; }

		[JsonProperty("realtime")]
		public int Realtime { get; internal set; }

		[JsonProperty("direction")]
		public string Direction { get; internal set; }

		[JsonProperty("directionFrom")]
		public string DirectionFrom { get; internal set; }

		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("delay", NullValueHandling = NullValueHandling.Ignore)]
		public int? Delay { get; internal set; }

		[JsonProperty("hints", NullValueHandling = NullValueHandling.Ignore)]
		public Hint[] Hints { get; internal set; }

		[JsonProperty("liErgRiProj")]
		public LiErgRiProj LiErgRiProj { get; internal set; }

		[JsonProperty("destID")]
		public int DestId { get; internal set; }

		/// <summary>
		/// One might think that this is an integer, but unfortunately this is not always the case.
		/// </summary>
		[JsonProperty("stateless")]
		public string Stateless { get; }
	}

	public class Hint
	{
		[JsonProperty("content")]
		public string Content { get; internal set; }
	}

	public class LiErgRiProj
	{
		[JsonProperty("line")]
		public string Line { get; internal set; }

		[JsonProperty("project")]
		public string Project { get; internal set; }

		[JsonProperty("direction")]
		public string Direction { get; internal set; }

		[JsonProperty("supplement")]
		public string Supplement { get; internal set; }

		[JsonProperty("network")]
		public string Network { get; internal set; }
	}

	public class Option
	{
		[JsonProperty("ptOption")]
		public PtOption PtOption { get; internal set; }
	}

	public class PtOption
	{
		[JsonProperty("active")]
		public int Active { get; internal set; }

		[JsonProperty("maxChanges")]
		public int MaxChanges { get; internal set; }

		[JsonProperty("maxTime")]
		public int MaxTime { get; internal set; }

		[JsonProperty("maxWait")]
		public int MaxWait { get; internal set; }

		[JsonProperty("routeType")]
		public string RouteType { get; internal set; }

		[JsonProperty("changeSpeed")]
		public string ChangeSpeed { get; internal set; }

		[JsonProperty("lineRestriction")]
		public int LineRestriction { get; internal set; }

		[JsonProperty("useProxFootSearch")]
		public int UseProxFootSearch { get; internal set; }

		[JsonProperty("useProxFootSearchOrigin")]
		public int UseProxFootSearchOrigin { get; internal set; }

		[JsonProperty("useProxFootSearchDestination")]
		public int UseProxFootSearchDestination { get; internal set; }

		[JsonProperty("bike")]
		public int Bike { get; internal set; }

		[JsonProperty("plane")]
		public int Plane { get; internal set; }

		[JsonProperty("noCrowded")]
		public int NoCrowded { get; internal set; }

		[JsonProperty("noSolidStairs")]
		public int NoSolidStairs { get; internal set; }

		[JsonProperty("noEscalators")]
		public int NoEscalators { get; internal set; }

		[JsonProperty("noElevators")]
		public int NoElevators { get; internal set; }

		[JsonProperty("lowPlatformVhcl")]
		public int LowPlatformVhcl { get; internal set; }

		[JsonProperty("wheelchair")]
		public int Wheelchair { get; internal set; }

		[JsonProperty("needElevatedPlt")]
		public int NeedElevatedPlt { get; internal set; }

		[JsonProperty("assistance")]
		public int Assistance { get; internal set; }

		[JsonProperty("SOSAvail")]
		public int SosAvail { get; internal set; }

		[JsonProperty("noLonelyTransfer")]
		public int NoLonelyTransfer { get; internal set; }

		[JsonProperty("illumTransfer")]
		public int IllumTransfer { get; internal set; }

		[JsonProperty("overgroundTransfer")]
		public int OvergroundTransfer { get; internal set; }

		[JsonProperty("noInsecurePlaces")]
		public int NoInsecurePlaces { get; internal set; }

		[JsonProperty("privateTransport")]
		public int PrivateTransport { get; internal set; }

		[JsonProperty("excludedMeans")]
		public ExcludedMean[] ExcludedMeans { get; internal set; }

		[JsonProperty("activeImp")]
		public int ActiveImp { get; internal set; }

		[JsonProperty("activeCom")]
		public int ActiveCom { get; internal set; }

		[JsonProperty("activeSec")]
		public int ActiveSec { get; internal set; }
	}

	public class ExcludedMean
	{
		[JsonProperty("means")]
		public string Means { get; internal set; }

		[JsonProperty("value")]
		public int Value { get; internal set; }

		[JsonProperty("selected")]
		public int Selected { get; internal set; }
	}

	public class ServingLines
	{
		[JsonProperty("lines")]
		public Line[] Lines { get; internal set; }
	}

	public class Line
	{
		[JsonProperty("mode")]
		public Mode Mode { get; internal set; }

		[JsonProperty("index")]
		public string Index { get; internal set; }
	}

	public class Mode
	{
		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("number")]
		public string Number { get; internal set; }

		[JsonProperty("product")]
		public string Product { get; internal set; }

		[JsonProperty("productId")]
		public int ProductId { get; internal set; }

		[JsonProperty("type")]
		public int Type { get; internal set; }

		[JsonProperty("mtSubcode")]
		public int MtSubcode { get; internal set; }

		[JsonProperty("code")]
		public int Code { get; internal set; }

		[JsonProperty("destination")]
		public string Destination { get; internal set; }

		[JsonProperty("destID")]
		public int DestId { get; internal set; }

		[JsonProperty("desc")]
		public string Desc { get; internal set; }

		[JsonProperty("timetablePeriod")]
		public string TimetablePeriod { get; internal set; }

		[JsonProperty("diva")]
		public Diva Diva { get; internal set; }
	}

	public class Diva
	{
		[JsonProperty("branch")]
		public string Branch { get; internal set; }

		[JsonProperty("line")]
		public string Line { get; internal set; }

		[JsonProperty("supplement")]
		public string Supplement { get; internal set; }

		[JsonProperty("dir")]
		public string Dir { get; internal set; }

		[JsonProperty("project")]
		public string Project { get; internal set; }

		[JsonProperty("network")]
		public string Network { get; internal set; }

		[JsonProperty("stateless")]
		public string Stateless { get; internal set; }

		[JsonProperty("tripCode")]
		public int TripCode { get; internal set; }

		[JsonProperty("operator")]
		public string Operator { get; internal set; }

		[JsonProperty("opCode")]
		public string OpCode { get; internal set; }

		[JsonProperty("vF")]
		public int VF { get; internal set; }

		[JsonProperty("vTo")]
		public int VTo { get; internal set; }

		[JsonProperty("isSTT", NullValueHandling = NullValueHandling.Ignore)]
		public int? IsStt { get; internal set; }

		[JsonProperty("attrs")]
		public object[] Attrs { get; internal set; }
	}
}
