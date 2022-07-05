# DotNetEasyCache

### Introduction
DotNetEasyCache is a simple way to handle caching items in your .Net project.  It utilizes `iDistributedCache` to help handle caching.

### Documentation
#### Installation/Setup
You can install DotNetEasyCache through Nuget package manager by running the following:

```
Install-Package DotNetEasyCache -Version 1.0.0
```
If you use the dotnet CLI you can use the following:

```
dotnet add package DotNetEasyCache
```
**Add to appsettings.json**
In your appsettings.json you'll need to include the following entry to utilize DotNetEasyCache:

```
"EasyCache": {
    "CacheHandler": "Memory",
    "Redis": {
      "Host": "127.0.0.1:6379",
      "Instance": "0"
    },
    "Sql": {
      "CacheConnectionString": "",
      "SchemaName": "",
      "TableName": ""
    }
  }
```

#### If you are using SQL Server Cache
To create a SQL Server cached item table in a SQL Server instance, you can use the sql-cache tool. The tool creates a table with the name and schema that you specify.

Create a table in SQL Server by running the sql-cache create command. Provide the SQL Server instance (Data Source), database (Initial Catalog), schema (for example, dbo), and table name (for example, TestCache):

You'll also want to use that same connection string, schema, and table name in your `appsettings.json`

```
dotnet sql-cache create "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DistCache;Integrated Security=True;" dbo TestCache
```


**Program.cs Changes**

Add the following line to your `program.cs` file:

```
builder.Services.SetEasyCacheServices();
```
This will implement all the DotNetEasyCache required services.

#### Usage
##### Dependency Injection
You can utilize dependency injection to bring the DotNetEasyCache into your project files. An example when using DotNetEasyCache in your a controller can be seen here:

```
private readonly IEasyCache _easyCache;

public IndexModel(IEasyCache easyCache)
{
    _easyCache = easyCache;
}
```
##### Storing Items in the Cache
You may use the Put method to store items in the cache. The Put method requires you to set an expiration in seconds.

The "key" value is the name you want your cache item to be stored as, and the "value" is what you are storing.

When using DotNetEasyCache in the DI container you can use the following:

```
var seconds = 10;
_easyCache.Put("key", "value", seconds);
```
To store an item that does not expire you can use the Forever method. This will store an item in cache until it is removed manually

```
_easyCache.Forever("key", "value");
```
##### Retrieve Items from Cache
The Get method is used to retrieve items from the cache. If the item does not exist in the cache, null will be returned.

```
_easyCache.Get("key");
```

##### Removing Item from Cache
You may remove items from the cache using the forget method:

```
_easyCache.Forget("key")
```

##### Checking For Item Existence
The Exists method can be used to determine if an item exists in the cache. If the value is null this method will return false

```
_easyCache.Exists("key");
```
### License
DotNetEasyCache is open-sourced software licensed under the [MIT License](https://opensource.org/licenses/MIT)