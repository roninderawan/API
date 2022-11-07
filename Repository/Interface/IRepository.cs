namespace API.Repository.Interface
{
    public interface IRepository<Entity, Key> where Entity : class
    {

        public ICollection<Entity> Get();
        public Entity GetById(Key id);

        public int Create(Entity entity);

        public int Update(Entity entity);

        public int Delete(Key id);
    }
}
