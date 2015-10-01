# Peasy.NET
An easy to use middle tier framework for .net

Peasy.net is a simple middle tier framework that offers/addresses the following:

- Easy to use validation/business rules engine
- Thread safety
- Scalability
- Concurrency
- Swappable data proxies
- Async support
- Multiple client support
- Fault tolerance

Looking for contributors for sample app consumers, framework improvements, and Nuget distributions!

# The simplest possible example

Create your domain object (DTO) that inherits from IDomainObject<T>:

    public class Person : Peasy.Core.IDomainObject<int>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }

Create your data proxy (aka repository) that implements IDataProxy<T, TKey> (most method implementations left out for brevity):

    public class PersonMockDataProxy : Peasy.Core.IDataProxy<Person, int>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetAll()
        {
            yield return new Person() { ID = 1, Name = "Aaron Hanusa" };
            yield return new Person() { ID = 2, Name = "Jimi Hendrix" };
            yield return new Person() { ID = 3, Name = "Sam Jackson" };
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return GetAll();
        }

        public Person GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Person Insert(Person entity)
        {
            return new Person() { ID = new Random(300).Next(), Name = entity.Name };
        }

        public async Task<Person> InsertAsync(Person entity)
        {
            return Insert(entity);
        }

        public Person Update(Person entity)
        {
            throw new NotImplementedException();
        }

        public Task<Person> UpdateAsync(Person entity)
        {
            throw new NotImplementedException();
        }
    }

Create a service class, which subjects operations to business rules before IDataProxy operations:

