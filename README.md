<h1>SuperMarket Checkout for Degree53</h1>

<p>Create a simple C# client and service for a supermarket checkout basket total.</p>

<h2>Prerequisites</h2>
<ul>
    <li>Visual Studio 2017</li>
    <li>ASP.NET Core 2.2</li>
    <li>Visual Studio Code 1.31.1</li>
</ul>

<h3>Steps to run</h3>
<ul>
    <li>Build Solution.</li>
    <li>Set SuperMarket.Console project as Startup project.</li>
    <li>Start with or without debugging with IIS Express on Visual Studio.</li>
</ul>

<h2>Solution</h2>

This solution consists of the following projects:

<ul>
<li><strong>SuperMarket.Console</strong></li>
Contains simple menu based console which implements service. Also implemented DI to resolve service class, factory class/business rules, and repository class.</br></br>

<li><strong>SuperMarket.Repository</strong></li>
Contains the Repository that holds temporary supermarket data. Can be extended to connect to real time database.</br></br>

<li><strong>SuperMarket.Repository.Tests</strong></li>
Contains the tests for Repository class.</br></br>

<li><strong>SuperMarket.Rules</strong></li>
Contains Business rules for super market to implement special offers. At the moment it implements single and multiple buy. Can be extended to change current business rules or to add new business rules.</br></br>

<li><strong>SuperMarket.Service</strong></li>
Contains Service that implements business rules using a factory classes.</br></br>

<li><strong>SuperMarket.Service.Tests</strong></li>
Contains Tests for service and factory class.</br></br>

<h2>Setup</h2>
<p>NodeJS must be installed on your system if you are planning to use VS code.</p>

<p>Clone or download this repository to your local machine. Then click the file SuperMarket.sln file, this should open the solution correctly whereby you will be presented with an N-layer solution.

Ensure you set the Startup project to SuperMarket.Console. Restore all Nuget and npm packages, then, right click the solution icon and select BUILD.</p>

<h2>Assumptions</h2>
<ul>
    <li>Default products are used as per specifications.</li>
    <li>Default business rules are used as per specification.</li>
</ul>
