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

#Where can I get it?

First, install NuGet. Then, install Peasy from the package manager console:

PM> Install-Package Peasy

# The simplest possible example

Let's create a domain object (DTO) that implements IDomainObject<<T>>:

    public class Person : Peasy.Core.IDomainObject<int>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }

Now we'll create a data proxy (aka repository) that implements IDataProxy<T, TKey> (most method implementations left out for brevity):

    public class PersonMockDataProxy : Peasy.Core.IDataProxy<Person, int>
    {
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
        
        public Person Insert(Person entity)
        {
            return new Person() { ID = new Random(300).Next(), Name = entity.Name };
        }

        public async Task<Person> InsertAsync(Person entity)
        {
            return Insert(entity);
        }
        
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        
        public Person GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetByIDAsync(int id)
        {
            throw new NotImplementedException();
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

Finally, we'll create a service class, which exposes CRUD commands responsible for subjecting IDataProxy invocations to business rules before execution:

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
            Debug.WriteLine(person.Name);  // prints each person's name retrieved from PersonMockDataProxy.GetAll
    }

    var newPerson = new Person() { Name = "Freed Jones", City = "Madison" };
    var insertResult = service.InsertCommand(newPerson).Execute();
    if (insertResult.Success)
    {
        Debug.WriteLine(insertResult.Value.ID.ToString()); // prints the id value assigned via PersonMockDataProxy.Insert
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
    
And now let's hook it up in our PersonService and ensure it gets fired before inserts:

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

And test it out (being sure to add a reference to System.ComponentModel.DataAnnotations)...

    var newPerson = new Person() { Name = "Fred Jones", City = "Madison" };
    var insertResult = service.InsertCommand(newPerson).Execute();
    if (insertResult.Success)
    {
        Debug.WriteLine(insertResult.Value.ID.ToString());
    }
    else
    {
        // This line will execute and print 'Name cannot be fred jones' 
        // Note that insertResult.Value will be NULL as PersonMockDataProxy.Insert did not execute due to failed rule
        Debug.WriteLine(insertResult.Errors.First()); 
    }

Now let's add one more rule, just for fun:

    public class ValidCityRule : Peasy.Core.RuleBase
    {
        private string _city;

        public ValidCityRule(string city)
        {
            _city = city;
        }

        protected override void OnValidate()
        {
            if (_city == "Nowhere")
            {
                Invalidate("Nowhere is not a city");
            }
        }
    }

We'll associate this one with inserts too:

    public class PersonService : Peasy.Core.ServiceBase<Person, int>
    {
        public PersonService(IDataProxy<Person, int> dataProxy) : base(dataProxy)
        {
        }

        protected override IEnumerable<IRule> GetBusinessRulesForInsert(Person entity, ExecutionContext<Person> context)
        {
            yield return new PersonNameRule(entity.Name);
            yield return new ValidCityRule(entity.City);
        }
    }

And test it out (being sure to add a reference to System.ComponentModel.DataAnnotations)...

    var newPerson = new Person() { Name = "Fred Jones", City = "Nowhere" };
    var insertResult = service.InsertCommand(newPerson).Execute();
    if (insertResult.Success)
    {
        Debug.WriteLine(insertResult.Value.ID.ToString());
    }
    else
    {
        // This line will execute and print 'Name cannot be fred jones' and 'Nowhere is not a city'
        // Note that insertResult.Value will be NULL as PersonMockDataProxy.Insert did not execute due to failed rule
        foreach (var error in insertResult.Errors)
            Debug.WriteLine(error);
    }

And finally, let's pass in valid data and watch it be a success

    var newPerson = new Person() { Name = "Freida Jones", City = "Madison" };
    var insertResult = service.InsertCommand(newPerson).Execute();
    if (insertResult.Success)
    {
        Debug.WriteLine(insertResult.Value.ID.ToString()); // prints the id value assigned via PersonMockDataProxy.Insert
    }
    else
    {
        foreach (var error in insertResult.Errors)
            Debug.WriteLine(error);
    }

# Where's the async support??

Note that we *cheated* in PersonMockDataProxy.GetAllAsync by simply marking the method async and marshalling the call to GetAll.  Normally, you would invoke an EntityFramework async call or make an out-of-band async call to an http service, etc.

    private async Task GetMyDataAsync()
    {
        var service = new PersonService(new PersonDataProxy());
        var getResult = await service.GetAllCommand().ExecuteAsync();
        if (getResult.Success)
        {
            foreach (var person in getResult.Value)
                Debug.WriteLine(person.Name);
        }
    }

Almost done -  Peasy supports "async all the way", which means that we need to tell the PersonService that we'll need the insert command to participate in an async workflow:

    public class PersonService : Peasy.Core.ServiceBase<Person, int>
    {
        public PersonService(IDataProxy<Person, int> dataProxy) : base(dataProxy)
        {
        }

        protected override IEnumerable<IRule> GetBusinessRulesForInsert(Person entity, ExecutionContext<Person> context)
        {
            yield return new PersonNameRule(entity.Name);
            yield return new ValidCityRule(entity.City);
        }

        protected override async Task<IEnumerable<IRule>> GetBusinessRulesForInsertAsync(Person entity, ExecutionContext<Person> context)
        {
            return GetBusinessRulesForInsert(entity, context);
        }
    }

Notice that we simply marked the InsertAsync override with async and simply marshalled the call to GetBusinessRulesForInsert. Sometimes you might want to asynchronously aquire data that can be shared among rules and it is within the InsertAsync override where this can be done.

One final step - let's add async support to the PersonNameRule (skipping async support for the city rule for the sake of brevity):

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

        protected async override Task OnValidateAsync()
        {
            OnValidate();
        }
    }

Again, we simply marked OnValidateAsync() with the async keyword and marshalled the call to the synchronous OnValidate().  At times you will need to pass a DataProxy into a rule and execute it asynchronously for data validation, which is when this async method will shine.
