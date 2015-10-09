![peasy](https://www.dropbox.com/s/2yajr2x9yevvzbm/peasy3.png?dl=0&raw=1)

An easy to use middle tier framework for .net

Peasy.net is a simple middle tier framework that offers/addresses the following:

- Easy to use [validation](https://github.com/ahanusa/Peasy.NET/wiki/Validation-Rules)/[business rules](https://github.com/ahanusa/Peasy.NET/wiki/Business-Rules) engine
- Thread safety
- Scalability
- Concurrency
- Swappable data proxies
- Async support
- Multiple client support
- Fault tolerance

#Where can I get it?

First, install NuGet. Then, install Peasy from the package manager console:

``` PM> Install-Package Peasy ```

You can also download and add the Peasy and/or Peasy.Core projects to your solution

# The simplest possible example

First create a [domain object (DTO)](https://github.com/ahanusa/Peasy.NET/wiki/Data-Transfer-Object-(DTO)----The-currency-of-exchange) that implements [```IDomainObject<T>```](https://github.com/ahanusa/Peasy.NET/blob/master/Peasy.Core/IDomainObject.cs):
```c#
public class Person : Peasy.Core.IDomainObject<int>
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
}
```
Then create a [data proxy](https://github.com/ahanusa/Peasy.NET/wiki/Data-Proxy) (aka repository) that implements [```IDataProxy<T, TKey>```](https://github.com/ahanusa/Peasy.NET/blob/master/Peasy.Core/IDataProxy.cs) (most method implementations left out for brevity):
```c#
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
```
Finally, create a [service class](https://github.com/ahanusa/Peasy.NET/wiki/ServiceBase), which exposes CRUD commands responsible for subjecting IDataProxy invocations to business rules before execution:
```c#
public class PersonService : Peasy.Core.ServiceBase<Person, int>
{
    public PersonService(Peasy.Core.IDataProxy<Person, int> dataProxy) : base(dataProxy)
    {
    }
}
```
Now let's consume our PersonService synchronously:
```c#
var service = new PersonService(new PersonMockDataProxy());
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
```
Let's add a [business rule](https://github.com/ahanusa/Peasy.NET/wiki/Business-Rules) whose execution must be successful before the call to IDataProxy.Insert is invoked
```c#
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
``` 
And hook it up in our PersonService and ensure it gets fired before inserts:
```c#
using Peasy.Core;

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
```
Testing it out (being sure to add a reference to [System.ComponentModel.DataAnnotations](https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations(v=vs.110).aspx))...
```c#
var service = new PersonService(new PersonMockDataProxy());
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
```
Let's add one more [rule](https://github.com/ahanusa/Peasy.NET/wiki/Business-Rules), just for fun:
```c#
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
```
We'll associate this one with inserts too:
```c#
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
```
And test it out (being sure to add a reference to [System.ComponentModel.DataAnnotations](https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations(v=vs.110).aspx))...
```c#
var service = new PersonService(new PersonMockDataProxy());
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
```
Finally, let's pass in valid data and watch it be a success
```c#
var service = new PersonService(new PersonMockDataProxy());
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
```
# Where's the async support??

Note that we *cheated* in PersonMockDataProxy.GetAllAsync by simply marking the method async and marshalling the call to GetAll.  Normally, you would invoke an EntityFramework async call or make an out-of-band async call to an http service, etc.
```c#
public async Task GetMyDataAsync()
{
    var service = new PersonService(new PersonMockDataProxy());
    var getResult = await service.GetAllCommand().ExecuteAsync();
    if (getResult.Success)
    {
        foreach (var person in getResult.Value)
            Debug.WriteLine(person.Name);
    }
}
```
Almost done -  Peasy supports "async all the way", which means that we need inject our business rules for our insert command into the async pipeline:
```c#
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
```
Notice that we simply marked the [```GetBusinessRulesForInsertAsync```](https://github.com/ahanusa/Peasy.NET/blob/master/Peasy.Core/ServiceBase.cs#L70) override with the async keyword and simply marshalled the call to GetBusinessRulesForInsert.  Sometimes you might want to asynchronously [acquire data that can be shared among rules](https://github.com/ahanusa/Peasy.NET/wiki/Business-Rules#wiring-up-business-rules-that-consume-data-proxy-data) and it is within the InsertAsync override where this can be done.

One final step - let's add async support to the PersonNameRule (skipping async support for the city rule for the sake of brevity):
```c#
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
```
Again, we simply marked [```OnValidateAsync```](https://github.com/ahanusa/Peasy.NET/blob/master/Peasy.Core/RuleBase.cs#L132) with the async keyword and marshalled the call to the synchronous OnValidate().  At times you will need to [pass a data proxy to a rule](https://github.com/ahanusa/Peasy.NET/wiki/Business-Rules#passing-lookup-data-to-a-business-rule) and execute it asynchronously for data validation.

And a final test...

```c#
public async Task SaveDataAsync()
{
    var service = new PersonService(new PersonMockDataProxy());
    var newPerson = new Person() { Name = "Freed Jones", City = "Madison" };
    var insertResult = await service.InsertCommand(newPerson).ExecuteAsync();
    if (insertResult.Success)
    {
        Debug.WriteLine(insertResult.Value.ID.ToString()); // prints the id value assigned via PersonMockDataProxy.Insert
    }
}
```            
