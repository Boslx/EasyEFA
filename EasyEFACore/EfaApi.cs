using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EasyEFACore
{
	public class EfaApi
	{
		private readonly HttpClient _client = new HttpClient();

		public async Task<EfaModel> GetEfaModel(string stationId, System.DateTime dateTime, int limit, string language)
		{
			// We also use the Departure Monitor query (XML_DM_REQUEST) to search for stops,
			// as it makes no difference whether or not you use the designated query (XSLT_STOPFINDER_REQUEST). 
			// For more information about the Api, see https://www.muensterhack.de/themes/mshack/assets/docs/2015_EFA-API.pdf
			string efaQuery = ($"http://www.efamobil.de/mobile3/XML_DM_REQUEST?" +
			                   $"outputFormat=JSON&" +
			                   $"stateless=1&" +
			                   $"locationServerActive=1&" +
			                   $"coordOutputFormat=WGS84[DD.DDDDD]&" +
			                   $"coordOutputFormatTail=5&" +
			                   $"anySigWhenPerfectNoOtherMatches=1&" +
			                   $"limit={limit}&" +
			                   $"type_dm=any&" +
			                   $"anyObjFilter_dm=2&" +
			                   $"deleteAssignedStops=1&" +
			                   $"name_dm={stationId}&" +
			                   $"mode=direct&" +
			                   $"language={language}&" +
			                   $"itdDateYear={dateTime.Year}&" +
			                   $"itdDateMonth={dateTime.Month}&" +
			                   $"itdDateDay={dateTime.Day}&" +
			                   $"itdTimeHour={dateTime.Hour}&" +
			                   $"itdTimeMinute={dateTime.Minute}");

			using (Stream s = await _client.GetStreamAsync(efaQuery))
			using (StreamReader sr = new StreamReader(s))
			using (JsonReader reader = new JsonTextReader(sr))
			{
				JsonSerializer serializer = new JsonSerializer();
				return serializer.Deserialize<EfaModel>(reader);
			}
		}
	}
}
