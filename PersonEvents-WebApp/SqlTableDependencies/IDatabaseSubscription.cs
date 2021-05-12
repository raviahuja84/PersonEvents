namespace PersonEvents_WebApp.SqlTableDependencies
{
    public interface IDatabaseSubscription
    {
        void Configure(string connectionString);
    }
}
