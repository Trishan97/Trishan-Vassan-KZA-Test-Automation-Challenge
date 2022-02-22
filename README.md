# Trishan-Vassan-Kza-Test-Automation-Challenge

## Heading 2

Trishan Vassan KZA Test Automation Challenge Repository
<br />

# Deliverables

* Create a series of **manual test cases** that covers the CRUD operations on Trello board. Cover both **positive** & **negative** cases.
  * This Deliverable can be found in this folder : [Deliverables](/Deliverables/) , in the ***Manual Test Cases.pdf*** file

<br />

* Create **test plan** for the above created test cases. Approach to decide what needs to be tested at Back-End & Front-End
  * This Deliverable can be found in this folder : [Deliverables](/Deliverables/) , in the ***Test Plan.pdf*** file
  
<br />

* Perform **CRUD operations** on Trello board. Automate (at least three) of the previously created test cases for both UI and Back-End.

<br />

This Deliverable can be found here :
  
* Frontend Automated Test Features
  * [Post/Create Frontend Scenarios](/FrontendAutomatedTests/Features/CreateScenarios.feature)
  * [Get/Read Frontend Scenarios](/FrontendAutomatedTests/Features/GetScenarios.feature)
  * [Put/Update Frontend Scenarios](/FrontendAutomatedTests/Features/UpdateScenarios.feature)
  * [Delete Frontend Scenarios](/FrontendAutomatedTests/Features/DeleteScenarios.feature)

<br />

* Frontend Automated Test Features
  * [Post/Create Backend Scenarios](/BackendAutomatedTests/Features/CreateScenarios.feature)
  * [Get/Read Backend Scenarios](/BackendAutomatedTests/Features/GetScenarios.feature)
  * [Put/Update Backend Scenarios](/BackendAutomatedTests/Features/UpdateScenarios.feature)
  * [Delete Backend Scenarios](/BackendAutomatedTests/Features/DeleteScenarios.feature)
  
<br />

* **Provide a test report.**

  * This Deliverable can be found in this folder : [Deliverables](/Deliverables/) , in the ***Test Report.pdf*** file

  * Frontend Test Results Report can be found here : [Frontend Automated Results](/Test_Results/LivingDocFrontend.html) - Clone repo first then You can download the HTML file and then open it in a browser to view test results
  * Backend Test Results Report can be found here : [Backend Automated Results](/Test_Results/LivingDocBackend.html) - Clone repo first then You can download the HTML file and then open it in a browser to view test results

<br />

# How to run Tests

Once the Repo has been successfully cloned these steps can be used to set up the tests and execute them locally

1. Clone the repo if not done yet

````
 git clone https://github.com/Trishan97/Trishan-Vassan-Kza-Test-Automation-Challenge.git
````

or through Visual Studio or any other C# compatible IDE

![IDE clone](/Media/Screenshots/clone_vs.PNG)

<br />

2. Once the Repo has been successfully cloned , open up the TrishanKzaAssessment.sln solution file if it has not automatically been opened

   Once opened you may get a prompt to Install the targeting version of .NET if you do not have it installed yet  :

    **This is only relevant if you have recieved the above Prompt** =>
   Install the targeting version of .NET Core 3.1 Runtime (LTS) through the visual studio installer : 

<br />

   You may need to reopen visual studio and the project after installation and open the sln dile again

3. Once that has been installed successfully your solution should open up and look like this 

<br />

![Solution](/Media/Screenshots/solution.PNG)

<br />

4. The next step is to restore Nuget packages for the solution , this can be done by right clicking on the Solution file **Solution 'TrishanKzaAssessment' (2 of 2 projects)** and clicking on the **Restore NuGet Packages** Option 

![Nuget](/Media/Screenshots/RestoreNuget.PNG)