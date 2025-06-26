namespace SharedKernel.DDD;

public interface IEntity<T> : IEntity
{
    T Id { get; }
}

public interface IEntity
{
    DateTime? CreatedAt { get; set; }
    string? CreatedBy { get; set; }
    DateTime? LastUpdatedAt { get; set; }
    string? LastUpdatedBy { get; set; }
}