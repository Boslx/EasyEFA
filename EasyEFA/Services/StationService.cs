using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using EasyEFA.Models;
using EasyEFACore;
using Station = EasyEFA.Models.Station;

namespace EasyEFA.Services
{
	public interface IStationService
	{
		public Task<IEnumerable<Station>> GetStations(string keyword, int limit, string language);

		public Task<IEnumerable<Departure>> GetDeparturesFromStationID(string stationID, DateTime dateTime,
			int limit, string language);
	}

	public class StationService : IStationService
	{
		public class StationNotFoundException : Exception { }

		private readonly EasyEFACore.EfaApi _efaApiClient = new EfaApi();

		public async Task<IEnumerable<Station>> GetStations(string keyword, int limit, string language)
		{
			double[] coordStringToDecimal(Point point)
			{
				var splittedCoords = point.Ref.Coords.Split(',', StringSplitOptions.RemoveEmptyEntries);
				return new double[] { double.Parse(splittedCoords[0], CultureInfo.InvariantCulture), double.Parse(splittedCoords[1], CultureInfo.InvariantCulture) };
			}

			var result = new List<Station>();
			var model = await _efaApiClient.GetEfaModel(keyword, DateTime.Now, limit, language);
			if (model.Dm.Points == null)
				return null;
			foreach (var point in model.Dm.Points)
			{
				var test = coordStringToDecimal(point);
				result.Add(new Station() { Location = coordStringToDecimal(point), Name = point.Name, StationId = point.Stateless });
			}

			return result;
		}

		public async Task<IEnumerable<Departure>> GetDeparturesFromStationID(string stationID, DateTime dateTime, int limit, string language)
		{
			var result = new List<Departure>();
			var model = await _efaApiClient.GetEfaModel(stationID, DateTime.Now, limit, language);

			if (model.Dm.Points == null)
				throw new StationNotFoundException();

			foreach (var departure in model.DepartureList)
			{
				result.Add(new Departure() { PlannedDateTime = departure.DateTime.DateTime, 
					RealDateTime = departure.RealDateTime?.DateTime, 
					Line = new Models.Line() 
						{  
							Direction = departure.ServingLine.Direction, 
							DirectionFrom = departure.ServingLine.DirectionFrom, 
							Name = departure.ServingLine.Name, 
							Number = departure.ServingLine.Number}
				});
			}

			return result;
		}
	}
}
