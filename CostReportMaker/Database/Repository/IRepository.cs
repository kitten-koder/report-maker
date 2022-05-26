namespace CostReportMaker.Database.Repository
{
    public interface IRepository<in Typed> where Typed : class
    {
        void Add(Typed entity);

        void Delete(Typed entity);

        void Update(Typed entity);
    }
}