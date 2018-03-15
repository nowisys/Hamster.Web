# Hamster.Message
> Plugin, which provides dummy features for a fictional messaging service exposed via Hamster.Web.

## Architecture
The Plugin is separated in following components:
### Domain
Simply said, a Web API aims to provide specific requirements. These requirements are placed in a specific domain, respectively context. In the context of a messaging service, `Users` are allowed to send `Messages`. To simplify, those messages are send in some kind of "global chatroom" and not directed to a specific `User`. Those entities are relevant for our requirements and should be conceptualized in this specific context.

As you may not expose the whole domain to clients at once or you want to avoid additional Round Trips, you should consider using "DTOs" (Data Transfer Objects). Those bundle required Data for an api call in one object, or restrict transferred data in an easy way. E.g., the client should not set an object Id by himself, this job is intended for the server. `DTO.MessageDTO` encapsulates data the server is expecting to receive from a client to *POST* a new message.
### Controllers
A Controller defines a set of actions to handle a request. Controllers follow the Explicit Dependencies Principle - actions require services, which are provided by constructor injection.

Controllers should expose their services with following route structure:
`api/<pluginName>/[controller]`

Further information on how to work with Controllers can be found here: https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/actions#what-is-a-controller
### Services
A Service implements Business Logic required to handle a request.
While the Plugin is initialized (`Init()`), you have to instantiate required Services on your own, add them to a `PluginServiceProvider` and set this object to the `IPluginServiceProvider` Property. Binded "infrastructure plugins" (e.g., a Plugin to interact with a Database) can be used in a Service.