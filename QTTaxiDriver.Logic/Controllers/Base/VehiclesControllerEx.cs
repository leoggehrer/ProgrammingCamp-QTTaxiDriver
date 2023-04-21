namespace QTTaxiDriver.Logic.Controllers.Base
{
    partial class VehiclesController
    {
        public Task<Models.Base.Vehicle[]> QueryByAsync(string? type, string? companyOrBrand)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Base.VehicleStatistics> CalculateStatistic(int id)
        {
            throw new NotImplementedException();
        }
    }
}
