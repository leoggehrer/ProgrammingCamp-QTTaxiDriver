
namespace CommonBase.Modules.Configuration
{
    partial class Configurator
    {
        static partial void ClassConstructed()
        {
            Environment.SetEnvironmentVariable("ConnectionStrings:SqliteDefaultConnection", "Data Source=C:\\Temp\\QTTaxiDriverDb.db");
        }
    }
}
