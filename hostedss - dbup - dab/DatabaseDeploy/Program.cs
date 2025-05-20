using DbUp;

namespace DatabaseDeploy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__db") ?? string.Empty;
            EnsureDatabase.For.SqlDatabase(connectionString);
            DeployChanges.To.SqlDatabase(connectionString)                
                .WithScriptsFromFileSystem("../../../scripts")
                .LogToConsole()
                .Build()
                .PerformUpgrade();
        }
    }
}
