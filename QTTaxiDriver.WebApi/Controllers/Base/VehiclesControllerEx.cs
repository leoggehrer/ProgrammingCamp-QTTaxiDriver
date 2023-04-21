using Microsoft.AspNetCore.Mvc;

namespace QTTaxiDriver.WebApi.Controllers.Base
{
    partial class VehiclesController
    {
        /// <summary>
        /// Calculates the statistics of an vehicle.
        /// </summary>
        /// <param name="id">The vehicle id</param>
        /// <returns>The result of the query.</returns>
        [HttpGet("calculateStatisticsBy", Name = nameof(CalculateStatisticsByAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Logic.Models.Base.VehicleStatistics>> CalculateStatisticsByAsync(
         [FromQuery(Name = "vehicleId")] int id)
        {
            var result = default(Logic.Models.Base.VehicleStatistics);
            var instanceAccess = DataAccess as Logic.Contracts.Base.IVehiclesAccess;

            if (instanceAccess != default)
            {
                result = await instanceAccess.CalculateStatisticsAsync(id);
            }
            return result == default || result.CompanyName == default ? NotFound() : Ok(result);
        }
    }
}
