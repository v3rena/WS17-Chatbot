# WS17-Chatbot

## Definitions

* ``(2017-10-24)`` There is no user-management
* ``(2017-10-24)`` Application Language is German

## Contributing

Please refer to [CONTRIBUTING.md](/CONTRIBUTING.md) for contribution guidelines.

## Environment

You can find the production development under [http://itp35_dev-inf.technikum-wien.at/](http://itp35_dev-inf.technikum-wien.at/). It is updates as soon as any changes are made to the master branch. The staging environment is behind path [/staging](http://itp35_dev-inf.technikum-wien.at/staging). As soon as merge requests to staging branch are accepted, a deployment is done.

## How to add a plugin

* ``/Plugins`` Folder -> AddProject -> .NET Framework Class Library
* Naming Convention: ``Chatbot.Plugins.[PluginName]``
* Go to Project Properties -> Build Tab
* For configuration ``Debug`` set OutputPath to ``..\Chatbot\bin\debug\``
* For configuration ``Release`` set OutputPath to ``..\Chatbot\bin\release\``
* Implement ``IPlugin`` interface from ``Chatbot.Interfaces``

The ``CanHandle`` method returns a ``float`` value between 0 (not at all) and 1 (perfect match) determining how well the plugin can handle the request.

The ``Handle`` method actually handles the request.