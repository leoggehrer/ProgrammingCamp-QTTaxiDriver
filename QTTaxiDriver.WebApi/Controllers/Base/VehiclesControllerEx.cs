using Microsoft.AspNetCore.Mvc;

namespace QTTaxiDriver.WebApi.Controllers.Base
{
    partial class VehiclesController
    {
        /// <summary>
        /// This query determines the vehicles depending on the parameters.
        /// </summary>
        /// <param name="vehicleType">The vehicle type as string (optional)</param>
        /// <param name="companyOrBrand">Contains company or name (optional)</param>
        /// <returns>The result of the query.</returns>
        [HttpGet("queryBy", Name = nameof(QueryByAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Models.Base.Vehicle>>> QueryByAsync(
         [FromQuery(Name = "vehicleType")] string? vehicleType,
         [FromQuery(Name = "companyOrBrand")] string? companyOrBrand)
        {
            IEnumerable<Logic.Models.Base.Vehicle> result = Array.Empty<Logic.Models.Base.Vehicle>();
            var instanceAccess = DataAccess as Logic.Contracts.Base.IVehiclesAccess;

            if (instanceAccess != default)
            {
                result = await instanceAccess.QueryByAsync(vehicleType, companyOrBrand);
            }
            return Ok(ToOutModel(result));
        }

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
