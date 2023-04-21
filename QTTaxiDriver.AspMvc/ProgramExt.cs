//@CustomCopy
//MdStart
namespace QTTaxiDriver.AspMvc
{
    /// <summary>
    /// Extension Program
    /// </summary>
    public partial class Program
    {
        /// <summary>
        /// Services can be added using this method.
        /// </summary>
        /// <param name="builder">The builder</param>
        public static void BeforeBuild(WebApplicationBuilder builder)
        {
#if ACCOUNT_ON
            builder.Services.AddTransient<Logic.Contracts.Account.IRolesAccess<Logic.Models.Account.Role>, Logic.Facades.Account.RolesFacade>();
            builder.Services.AddTransient<Logic.Contracts.Account.IUsersAccess, Logic.Facades.Account.UsersFacade>();
            builder.Services.AddTransient<Logic.Contracts.Account.IIdentitiesAccess, Logic.Facades.Account.IdentitiesFacade>();
#endif

            builder.Services.AddTransient<Logic.Contracts.Base.ICompaniesAccess, Logic.Controllers.Base.CompaniesController>();
            builder.Services.AddTransient<Logic.Contracts.Base.IDriversAccess, Logic.Controllers.Base.DriversController>();
            builder.Services.AddTransient<Logic.Contracts.Base.IVehiclesAccess, Logic.Controllers.Base.VehiclesController>();

            builder.Services.AddTransient<Logic.Contracts.App.IDrivesAccess, Logic.Controllers.App.DrivesController>();
            AddServices(builder);
        }
        /// <summary>
        /// Configures can be added using this method.
        /// </summary>
        /// <param name="app"></param>
        public static void AfterBuild(WebApplication app)
        {
            AddConfigures(app);
        }
        static partial void AddServices(WebApplicationBuilder builder);
        static partial void AddConfigures(WebApplication app);
    }
}
//MdEnd
