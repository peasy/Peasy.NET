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
            return new[]
            {
                new Person() { ID = 1, Name = "Jimi Hendrix" },
                new Person() { ID = 2, Name = "James Page" },
                new Person() { ID = 3, Name = "David Gilmour" }
            };
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

Create a service class, which exposes CRUD commands, responsible for subjecting IDataProxy invocations to business rules before execution:

    public class PersonService : Peasy.Core.ServiceBase<Person, int>
    {
        public PersonService(IDataProxy<Person, int> dataProxy) : base(dataProxy)
        {
        }
    }

Now let's consume our PersonService synchronously:

    var service = new PersonService(new PersonDataProxy());
    var getResult = service.GetAllCommand().Execute();
    if (getResult.Success)
    {
        foreach (var person in getResult.Value)
            Debug.WriteLine(person.Name);
    }

    var newPerson = new Person() { Name = "Freed Jones", City = "Madison" };
    var insertResult = service.InsertCommand(newPerson).Execute();
    if (insertResult.Success)
    {
        Debug.WriteLine(insertResult.Value.ID.ToString());
    }

Now let's add a business rule whose execution must be successful before the call to IDataProxy.Insert is invoked

    public class PersonNameRule : Peasy.Core.RuleBase
    {
        private string _name;

        public PersonNameRule(string name)
        {
            _name = name;
        }

        protected override void OnValidate()
        {
            if (_name == "Fred Jones")
            {
                Invalidate("Name cannot be fred jones");
            }
        }
    }
    
And now let's hook it up:

    public class PersonService : Peasy.Core.ServiceBase<Person, int>
    {
        public PersonService(IDataProxy<Person, int> dataProxy) : base(dataProxy)
        {
        }

        protected override IEnumerable<IRule> GetBusinessRulesForInsert(Person entity, ExecutionContext<Person> context)
        {
            yield return new PersonNameRule(entity.Name);
        }
    }

And test it out (being sure to add a System.ComponentModel.DataAnnotations)...

    var newPerson = new Person() { Name = "Fred Jones", City = "Madison" };
    var insertResult = service.InsertCommand(newPerson).Execute();
    if (insertResult.Success)
    {
        Debug.WriteLine(insertResult.Value.ID.ToString());
    }
    else
    {
        // This line will execute and print 'Name cannot be fred jones' - also note that insertResult.Value will be NULL
        Debug.WriteLine(insertResult.Errors.First()); 
    }

