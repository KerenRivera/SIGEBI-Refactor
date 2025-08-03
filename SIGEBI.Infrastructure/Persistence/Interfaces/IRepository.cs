namespace SIGEBI.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        //patron repositorio generico, permite acceder a los datos de cualquier entidad
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
