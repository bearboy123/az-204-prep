# MyTimerFunctionApp

## Overview
MyTimerFunctionApp is an Azure Functions application that contains a timer-triggered function. The function executes every 10 seconds and logs the execution time along with the next scheduled execution time.

## Project Structure
```
MyTimerFunctionApp
├── src
│   └── MyTimerTrigger.cs       # Contains the timer trigger function logic
├── MyTimerFunctionApp.csproj   # Project file defining dependencies and settings
├── host.json                   # Global configuration options for Azure Functions host
├── local.settings.json          # Local development settings (not for deployment)
├── .vscode
│   ├── launch.json             # Debugging configuration for local development
│   └── tasks.json              # Task definitions for building and running the app
├── .gitignore                  # Files and directories to ignore in Git
└── README.md                   # Documentation for the project
```

## Setup Instructions
1. **Clone the Repository**
   ```bash
   git clone <repository-url>
   cd MyTimerFunctionApp
   ```

2. **Install Dependencies**
   Ensure you have the Azure Functions Core Tools installed. You can install them via npm:
   ```bash
   npm install -g azure-functions-core-tools@3 --unsafe-perm true
   ```

3. **Local Development**
   - Open the project in your preferred IDE.
   - Use the `local.settings.json` file to configure any necessary local settings, such as connection strings.

4. **Run the Function Locally**
   Use the following command to start the Azure Functions host:
   ```bash
   func start
   ```

## Usage
The timer trigger function will log the execution time every 10 seconds. You can view the logs in the console output when running locally.

## Deployment
To deploy the function to Azure, use the Azure CLI or Azure Functions extension in your IDE. Ensure that you have configured your Azure subscription and created a Function App in Azure.

## License
This project is licensed under the MIT License.