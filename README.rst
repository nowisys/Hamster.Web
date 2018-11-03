Hamster.Web
===========

This project provides a plugin for openHamster to host web services. It uses
ASP.NET Core and the included Kestrel web server to host the web services.

Usage
-----

The plugin allows to host multiple independent web services. Each user of the
plugin can use a URL prefix and configure it with all available mechnisms of
ASP.NET Core. For convenience of the main use case, the ``WebService`` class
can be used to provide a web API to access a specific service object.

The service object can be of any type, for example another plugin. The web
API is then provided by additional `Controller`_ classes. Instances of these
classes are associated with web service endpoints and created on demand by
the web server. The instances can access the service object by requesting a
reference in their constructors.

The controllers map URLs to functions. The functions get the parameters from
the request, call the appropriate functions from the service object, and return
a result matching the outcome of the operations (e.g. a response or an error code).
See ``Hamster.Web.Controllers.PluginController`` for a simple example.

.. Controller: https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/adding-controller?view=aspnetcore-2.1
