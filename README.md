![peasy](https://www.dropbox.com/s/2yajr2x9yevvzbm/peasy3.png?dl=0&raw=1)

###An easy to use middle tier framework for .net

Peasy.net is a simple middle tier framework that offers/addresses the following:

- Easy to use [validation](https://github.com/ahanusa/Peasy.NET/wiki/Validation-Rules)/[business rules](https://github.com/ahanusa/Peasy.NET/wiki/Business-Rules) engine
- [Thread safety](https://github.com/ahanusa/Peasy.NET/wiki/Thread-Safety)
- [Scalability](https://github.com/ahanusa/Peasy.NET/wiki/data-proxy#scalability)
- [Concurrency](https://github.com/ahanusa/Peasy.NET/wiki/BusinessServiceBase#concurrency-handling)
- [Swappable](https://github.com/ahanusa/Peasy.NET/wiki/data-proxy#swappable-data-proxies) [data proxies](https://github.com/ahanusa/Peasy.NET/wiki/Data-Proxy)
- [Async support](https://github.com/ahanusa/Peasy.NET/wiki/The-Asynchronous-Pipeline)
- [Multiple client support](https://github.com/ahanusa/Peasy.NET/wiki/Multiple-client-support)
- [Multiple deployment scenario support](https://github.com/ahanusa/Peasy.NET/wiki/data-proxy#multiple-deployment-scenarios)
- [Transactional support and fault tolerance](https://github.com/ahanusa/Peasy.NET/wiki/ITransactionContext)
- [Easy testability](https://github.com/ahanusa/Peasy.NET/wiki/Testing)

#Where can I get it?

First, install NuGet. Then, install Peasy from the package manager console:

``` PM> Install-Package Peasy ```

You can also download and add the Peasy and/or Peasy.Core projects to your solution

#Getting started

You can get started by reviewing the [getting started example](https://github.com/ahanusa/Peasy.NET/wiki#the-simplest-possible-example) on the Peasy wiki.  The wiki also covers in-depth how-to's, general framework design, and usage scenarios.

You can also download the entire repo that contains a full sample implementation of a middle tier built with peasy, as well as a sample WPF consumer client.  You can clone the repo or download the entire project as a [zip](https://github.com/ahanusa/Peasy.NET/archive/master.zip).  Once downloaded, open Peasy.sln with Visual Studio, set the WPF project as the startup project and run.

#Contributing

All contributions are welcome, from general framework improvements to sample client consumers, proxy implementations, and documentation updates.  Want to get involved?  Please hit me up with your ideas.  Alternatively, you can make a pull request and I'll get to it ASAP.
