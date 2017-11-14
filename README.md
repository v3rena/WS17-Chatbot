# WS17-Chatbot

## Definitionen

* ``(2017-10-24)`` Es gibt kein User Management.
* ``(2017-10-24)`` Sprache ist Deutsch.

## Contributing

Please refer to [CONTRIBUTING.md](/CONTRIBUTING.md) for contribution guidelines.

## Environment

You can find the production development under [http://itp35_dev-inf.technikum-wien.at/](http://itp35_dev-inf.technikum-wien.at/). It is updates as soon as any changes are made to the master branch. The staging environment is behind path [/staging](http://itp35_dev-inf.technikum-wien.at/staging). As soon as merge requests to staging branch are accepted, a deployment is done.


## How to add database usage to your plugin

* Create a model class file named after your plugin e.g. Message.cs (will be the name of the table in the db)
* Fill it with all attributes with the right type (will be the columns in your table)
* Create a file in your plugin project called PluginNameContext.cs see for example Chatbot.Plugins.EchoBot.EchoBotContext.cs
* Copy methods and change type and schema name (lowercase) to the name of your plugin
* In Package Manager Console choose your project as standard project
* Enable migrations for your project: run 'enable-migrations' command in Package Manager Console
* It creates an internal sealed Configuration class in the Migration folder in your project e.g. Chatbot.Plugins.EchoBot\Migrations\Configuration.cs
* Add migration: run 'add-migration initial' command in Package Manager Console, it will create a migration file 
* Update Db: run 'update-database' command in Package Manager Console, it should create your table in the database
* In the Configuration.cs change AutomaticMigrationsEnabled to true 
* You also need a small Dal for your plugin for inserting and manipulating the data in the table see example Chatbot.DataAccessLayer.DAL
* In the Dal's constructor write the methodcall for automated migrations e.g. Database.SetInitializer<MessageContext>(new MigrateDatabaseToLatestVersion<MessageContext, Migrations.ConfigurationMessage>());
* For more details: http://www.entityframeworktutorial.net/code-first/entity-framework-code-first.aspx