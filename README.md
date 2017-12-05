# WS17-Chatbot

## Definitions

* ``(2017-10-24)`` There is no user-management
* ``(2017-10-24)`` Application Language is German

## Contributing

Please refer to [CONTRIBUTING.md](/CONTRIBUTING.md) for contribution guidelines.

## Environment

You can find the production development under [http://itp35_dev-inf.technikum-wien.at/](http://itp35_dev-inf.technikum-wien.at/). It is updates as soon as any changes are made to the master branch. The staging environment is behind path [/staging](http://itp35_dev-inf.technikum-wien.at/staging). As soon as merge requests to staging branch are accepted, a deployment is done.

## How to setup the database

All necessary files are located in the ``/db-setup`` folder of the repository. Execute the ``db-setup`` script.
> Note that the windows ``PATH`` environment variable has to be set to match the postgres ``psql`` command line tool. Usually it is located in ``C:\Program Files\PostgreSQL\[Version]\bin\``.

You have to enter your postgres-superuser-password 3 times (if you have no password set, leave it blank).

If it is not possible to execute that script for any reason, do create a new database by hand by copy-pasting the SQL-Statements in the script to pg-admin. As a last step, you can copy-paste the content of ``setup-user.sql`` to pg-admin and execute it. This should create a new user and grant it access to the database. 

## How to add a plugin

* ``/Plugins`` Folder -> AddProject -> .NET Framework Class Library
* Naming Convention: ``Chatbot.Plugins.[PluginName]``
* Go to Project Properties -> Build Tab
* For configuration ``Debug`` set OutputPath to ``..\Chatbot.ServiceLayer\bin\debug\``
* For configuration ``Release`` set OutputPath to ``..\Chatbot.ServiceLayer\bin\release\``
* Implement ``IPlugin`` interface from ``Chatbot.Common.Interfaces``

The ``CanHandle`` method returns a ``float`` value between 0 (not at all) and 1 (perfect match) determining how well the plugin can handle the request.

The ``Handle`` method actually handles the request.

## How to add database usage to your plugin

* Create a model class file named after your plugin e.g. ``Message.cs`` (will be the name of the table in the db)
* Fill it with all needed attributes of the right type (will be the columns in your table)
* Create a file in your plugin project called ``[PluginName]Context.cs``. See for example ``Chatbot.Plugins.EchoBot.EchoBotContext.cs``
* Copy methods and change type and schema name (lowercase) to the name of your plugin
* In Package Manager Console, choose your project as standard project
* Enable migrations for your project: run ``enable-migrations`` command in Package Manager Console
* It creates an internal sealed Configuration class in the Migration folder in your project e.g. ``Chatbot.Plugins.EchoBot\Migrations\Configuration.cs``
* Add migration: run ``add-migration initial`` command in Package Manager Console, it will create a migration file 
* Update Db: run ``update-database`` command in Package Manager Console, it should create your table in the database
* In the ``Configuration.cs`` change ``AutomaticMigrationsEnabled`` to ``true`` 
* You also need a small Dal for your plugin for inserting and manipulating the data in the table see example ``Chatbot.DataAccessLayer.DAL``
* In the Dal's constructor write the methodcall for automated migrations e.g. ``Database.SetInitializer<MessageContext>(new MigrateDatabaseToLatestVersion<MessageContext, Migrations.ConfigurationMessage>());``
* For more details: http://www.entityframeworktutorial.net/code-first/entity-framework-code-first.aspx
