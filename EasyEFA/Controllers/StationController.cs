using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EasyEFA.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EasyEFA.Controllers
{
    public class StationController : ControllerBase
    {
	    private readonly Services.IStationService _iStationService;

	    public StationController(Services.IStationService iStationService)
	    {
		    _iStationService = iStationService;
	    }

		/// <summary>
		/// Returns a list of all stations including the keyword
		/// </summary>
		/// <param name="keyword">Search phrase for the stations you're looking for</param>
		/// <param name="limit">Max records to return</param>
		/// <param name="language">Language of the returned strings</param>
		/// <response code="200">OK</response>
		[HttpGet]
	    [Route("/stations/")]
	    [Produces("application/json")]
		public virtual ActionResult<IEnumerable<Station>> GetStations([FromQuery][Required()]string keyword, [FromQuery]int limit=50, [FromQuery]string language = "de")
	    {
		    return StatusCode(200, _iStationService.GetStations(keyword, limit, language).Result);
	    }

		/// <summary>
		/// Returns all departures at the specified datetime
		/// </summary>
		/// <param name="stationID">ID of the station whose departures are returned</param>
		/// <param name="dateTime">Requested time. If empty, the current time is used</param>
		/// <param name="limit">Max records to return</param>
		/// <param name="language">Language of the returned strings</param>
		/// <response code="200">OK</response>
		[HttpGet]
	    [Route("/stations/{stationID}/departures")]
	    [Produces("application/json")]
		public virtual ActionResult<IEnumerable<Departure>> GetDeparturesFromStationID([FromRoute][Required]string stationID, [FromQuery]DateTime? dateTime, [FromQuery]int limit = 50, [FromQuery]string language = "de")
	    {
		    return StatusCode(200, _iStationService.GetDeparturesFromStationID(stationID, dateTime ?? DateTime.Now, limit, language).Result);
	    }
    }
}