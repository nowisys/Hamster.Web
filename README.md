# Hamster.Web
> Hamster.Web is a plugin designed for openHAMSTER to provide Services of other Plugins as Web API. Due to Discovery Mechanisms of ASP.NET Core, Services and Controller are published without any further configuration, iff requirements are met.

## Discovery Routine
Controllers are responsible of defining URIs, ensuring Validation, and provide additional Data to Service Calls, while Services implement additional functional requirements.

Hamster.Web needs to have a *Binding* from Plugins that aspire to publish specific services. The Property *IPluginServiceProvider* of *IPlugin* is used to add Business Logic Services to the ASP.NET Core IoC Container, which will be used by Controllers later on via Dependency Injection.

The service-providing Plugin holds a reference to the Microsoft.AspNetCore.Mvc library. A discoverable Controller ensures to implement the `IController`-Interface and has to be located in a \*Hamster\*-Namespace.

Further information on Dependency Injection in ASP.NET Core can be found here: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection
## Sample Functional Plugin
> An example for a service-providing Plugin is located in the *example* folder.

## Sample Configuration
To enable Hamster.Web within an openHAMSTER-Project, following steps have to be taken:
1. Add libraries of Hamster.Web and the functional Plugin to the Kernel (Directory where the Kernel is looking for libs is configured in hamster.xml)
2. Add configuration to hamster.xml
3. Define a Binding from the functional plugin to Hamster.Web
E.g.:
```xml 
<!-- Hamster.Messaging Plugin -->
<plugin xmlns="http://www.nowisys.de/hamster/schemas/plugin.xsd">
	<name>Hamster.Messaging</name>
	<type>Hamster.Messaging.Plugin, Hamster.Messaging</type>
</plugin>
<!-- Hamster.Web Plugin -->
<plugin xmlns="http://www.nowisys.de/hamster/schemas/plugin.xsd">
	<name>Hamster.Web</name>
	<type>Hamster.Web.WebPlugin, Hamster.Web</type>
    <!-- Binding, as required by 3. -->
	<bind slot="message" plugin="Hamster.Messaging"/>
    <!-- Additional PluginSettings -->
	<settings xmlns="http://www.nowisys.de/hamster/plugins/services/web.xsd">
		<url>http://localhost:8080/</url>
	</settings>
</plugin>
```

## Feasible, additional Steps and Concepts
A Web API needs to be secured against unauthorized Clients. We follow the idea that an additional Reverse Proxy needs to guarantuee Authentication and Authorization. ASP.NET Core provides additional features in this section, which should be evaluated regarding potential integration opportunities.