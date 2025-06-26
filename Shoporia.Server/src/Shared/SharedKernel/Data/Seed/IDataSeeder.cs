namespace SharedKernel.Data.Seed;

public interface IDataSeeder
{
    Task SeedAllAsync();
}