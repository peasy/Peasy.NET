![peasy](https://www.dropbox.com/s/2yajr2x9yevvzbm/peasy3.png?dl=0&raw=1)

```c#
A middle tier micro-framework for .NET and .NET Core
```
<p>
<a href="https://gitter.im/peasy/peasy.net?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge" target="_blank">
	<img src="https://badges.gitter.im/peasy/peasy.net.svg" alt="Gitter">
</a>
<a href="https://www.nuget.org/packages/Peasy/" target="_blank">
	<img src="http://img.shields.io/nuget/v/Peasy.svg" alt="nuget version">
</a>
<a href="https://ci.appveyor.com/project/ahanusa/peasy-net" target="_blank">
	<img src="https://ci.appveyor.com/api/projects/status/5uhfvwaju6bagdg2?svg=true" alt="Gitter">
</a>
</p>

# What's a middle tier framework?

A middle tier framework is code that facilitates creating business logic in a reusable, extensible, maintainable, and testable manner.   It promotes creating business logic that is completely decoupled from its consuming technologies and helps to ensure that separation of concerns ([SoC](https://en.wikipedia.org/wiki/Separation_of_concerns)) are adhered to.

##### Peasy.NET offers/addresses the following:

- [Business rules](https://github.com/peasy/Peasy.NET/wiki/RuleBase)/[validation](https://github.com/peasy/Peasy.NET/wiki/Validation-Rules) engine
- [Multiple client support](https://github.com/peasy/Peasy.NET/wiki/Multiple-client-support)
- [Multiple deployment scenario support](https://github.com/peasy/Peasy.NET/wiki/data-proxy#multiple-deployment-scenarios)
- [Scalability](https://github.com/peasy/Peasy.NET/wiki/data-proxy#scalability)
- [Testability](https://github.com/peasy/Peasy.NET/wiki/Testing)
- [Thread safety](https://github.com/peasy/Peasy.NET/wiki/Thread-Safety)
<!-- - [Transactional support and fault tolerance](https://github.com/peasy/Peasy.NET/wiki/ITransactionContext) -->
<!-- - [Concurrency](https://github.com/peasy/Peasy.NET/wiki/BusinessServiceBase#concurrency-handling) -->
<!-- - [Async support](https://github.com/peasy/Peasy.NET/wiki/The-Asynchronous-Pipeline) -->

# The main actors

### Data Proxy
The [data proxy](https://github.com/peasy/Peasy.NET/wiki/Data-Proxy) is responsible for data storage and retrieval, and serves as an abstraction layer for data stores (database, web services, cache, etc.).

### Rule
A rule can be created to represent a [business](https://github.com/peasy/Peasy.NET/wiki/RuleBase) rule (authorization, price validity, etc.) or a [validation](https://github.com/peasy/Peasy.NET/wiki/Validation-Rules) rule (field length, required, etc.). Rules are consumed by commands and can be chained, configured to execute based on a previous rule’s execution, etc. Rules can also be configured to invoke code based on the result of their execution.

### Command
The [command](https://github.com/peasy/Peasy.NET/wiki/CommandBase) is responsible for orchestrating the execution of initialization logic, business and validation rule execution, and other logic (data proxy invocations, workflow logic, etc.), respectively, via the command execution pipeline.

### Business Service
A [business service](https://github.com/peasy/Peasy.NET/wiki/ServiceBase) implementation represents an entity (e.g. users, or projects) and is responsible for exposing business functionality via commands. These commands encapsulate CRUD and other business related logic.

&nbsp;

##### Peasy actors at work
<p>
  <img src="peasy-uml.svg">
</p>
Note: the services, rules, and proxies are examples of classes you might develop

# Where can I get it?

### Visual Studio

Install Peasy from the package manager console:

``` PM> Install-Package Peasy ```

### VS Code

Install Peasy from the command line:

``` dotnet add package peasy ```


# Getting started

You can get started by reviewing the [getting started example](https://github.com/peasy/Peasy.NET/wiki#the-simplest-possible-example) on the Peasy wiki.  The wiki also covers in-depth how-to's, general framework design, and usage scenarios.

You can also check out the [samples repo](https://github.com/peasy/Samples) that contains a sample implementation of a middle tier built with peasy, as well as sample consumer clients (WPF, Web API, ASP.NET MVC).  You can clone the repo or download the entire project as a [zip](https://github.com/peasy/samples/archive/master.zip).  

Once downloaded, open Orders.com.sln with Visual Studio, set the WPF or ASP.NET MVC project as the startup project and run.  More information about the samples application can be found [here](https://github.com/peasy/Samples).

# Contributing

All contributions are welcome, from general framework improvements to sample client consumers, proxy implementations, and documentation updates.  Want to get involved?  Please hit us up with your ideas.  Alternatively, you can make a pull request and we'll get to it ASAP.

# Like what you see?

Please consider showing your support by starring the project.
