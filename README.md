# C# Backend for Logistics API

This backend API serves data related to menus and vehicles for the logistics assessment. It is designed to be accessed by a frontend React application, which interacts with this API through standard HTTP requests.

## Prerequisites

- **.NET 8+** is required to run this project.
- **CORS Configuration**: The backend is currently configured to allow cross-origin requests only from the frontend running on **port 3000**. This is hardcoded in the program.cs for development purposes.

## Key Features

- **Endpoints**: The backend provides APIs to retrieve data for:
  - **Menus**
  - **Vehicles**

- **Services & Repositories**:
  - The menu/vehicle controllers handle the incoming requests and delegate the data processing to the corresponding service.
  - The services interact with menu/vehicle Repositories to retrieve the data.
  - The repositories are singletons load the menu or vehicle data on initialization from JSON files that are stored in the `Assets` directory.
  - The `Program.cs` ensures that these JSON files (`menu.json` and `vehicle.json`) are present in the expected location during the application startup. If the files are not found, the application will stop with an error.