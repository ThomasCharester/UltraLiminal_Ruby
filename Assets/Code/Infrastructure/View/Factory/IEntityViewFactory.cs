namespace Code.Infrastructure.View.Factory
{
    public interface IEntityViewFactory
    {
        IEntityView CreateEntityViewFromPrefab(GameEntity entity);
        IEntityView CreateEntityViewFromPath(GameEntity entity);
    }
}