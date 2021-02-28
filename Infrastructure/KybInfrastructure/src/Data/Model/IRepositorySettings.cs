namespace KybInfrastructure.Data
{
    public interface IRepositorySettings<T> where T : IDatabaseSettings
    {
        T DatabaseSettings { get; set; }
    }
}