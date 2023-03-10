using Leopotam.Ecs;

public static class EcsWorldExtensions
{
    public static void SendRequest<T>(this EcsWorld world, in T request)
        where T : struct
    {
        var newEntity = world.NewEntity();
        newEntity.Get<T>() = request;
    }
}