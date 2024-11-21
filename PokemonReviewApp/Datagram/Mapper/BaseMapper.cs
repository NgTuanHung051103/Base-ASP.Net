using Mapster;

namespace PokemonReviewApp
{
    public abstract class BaseMapper<TMapper, TEntity> : IRegister
        where TMapper : class, new()
        where TEntity : class, new()
    {
        private TypeAdapterConfig Config { get; set; } = default!;
        public void Register(TypeAdapterConfig config)
        {
            Config = config;
            AddCustomMappings();
        }
        public TEntity ToEntity() => this.Adapt<TEntity>();
        public TEntity ToEntity(TEntity entity) => (this as TMapper).Adapt(entity);
        public static TMapper FromEntity(TEntity entity)
        {
            var mappedEntity = entity.Adapt<TMapper>();
            //custome by Annotate
            
            return mappedEntity;
        }
        protected TypeAdapterSetter<TMapper, TEntity> SetCustomMappings() => Config.ForType<TMapper, TEntity>();
        protected TypeAdapterSetter<TEntity, TMapper> SetCustomMappingsInverse() => Config.ForType<TEntity, TMapper>();
        public virtual void AddCustomMappings() { }
    }
}
