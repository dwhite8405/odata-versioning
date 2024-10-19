API Versioning in OData
===================

This is a short demo of how to do API versioning in OData. OData is implemented as a REST service such that versioning techniques for REST will work as well with OData.

As an application changes, API changes need to be implemented. If functionality is added, usually it is sufficient just to make the changes and deploy. However when functionality is altered or removed, the API needs to have a new version while maintaining the old version for backwards compatibility with old clients.

The story here is that we have Customers and Orders. When our system is first implemented, each Customer has an Address, implemented as a string for simplicity. Later a requirement is created to separate the Address into a Billing Address and a Mailing Address.

This demo was made using the starting point provided at https://learn.microsoft.com/en-us/odata/webapi-8/getting-started.

Entity-based versioning.
------------------------

In the first attempt at versioning, we simply add a new entity to our model, Customer2. Both exist at the same time. Hypothetically both would use the same database table, with the original Customer entity being modified to save it's address to both fields.

Thus we have the original Customer.cs, and the new Customer2.cs. We add both to our model. Backwards compatibility is preserved because older clients can still interact with Customer, whereas new clients can interact with Customer2.

However, this leads to pollution in our model. If few changes are expected then this type of versioning will work and be minimally invasive. If pollution becomes severe, then we should use Path-based versioning.

Path-based versioning
----------------------

Path-based versioning uses a short segment of the URL for the version. In this case, we have http://localhost/odata/v1/, and http://localhost/odata/v2.

In Program.cs, we added a new ODataConventionModelBuilder which contains a completely new EDM model for our API. We then add this as a "v2" route.

Now Customer has been fully deprecated and removed, and Customer2 is now accessible. We have a clean model with the ability to maintain backwards compatibility for as long as necessary.

Running
-------

In a console, 

::
    $ dotnet run

Then using a web browser or OData client, navigation to http://localhost:5205/odata/v1 and http://localhost:5205/odata/v2.

TODO
----

Add RevisionKind Deprecated annotations.

Maybe try adding $schemaversion query support?

Split this project into different versions?