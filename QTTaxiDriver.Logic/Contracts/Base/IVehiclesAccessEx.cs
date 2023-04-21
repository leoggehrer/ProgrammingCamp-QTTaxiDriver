namespace QTTaxiDriver.Logic.Contracts.Base
{
    partial interface IVehiclesAccess
    {
        Task<Models.Base.Vehicle[]> QueryByAsync(string? type, string? companyOrBrand);

        Task<Models.Base.VehicleStatistics> CalculateStatisticsAsync(int id);
    }
}
