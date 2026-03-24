# Bruno Backend

A modern ASP.NET Core Web API backend built with .NET 10.

## Tech Stack

- **.NET 10** - Latest .NET framework
- **ASP.NET Core Minimal APIs** - Lightweight API framework
- **Swagger/OpenAPI** - API documentation and testing interface

## Project Structure

```
bruno_backend/
├── Controllers/       # API controllers (to be implemented)
├── Models/           # Data models and entities
├── Services/         # Business logic and services
├── Properties/       # Launch settings and configuration
├── Program.cs        # Application entry point
├── appsettings.json  # Application configuration
└── bruno_backend.csproj
```

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or higher
- A code editor (Visual Studio, VS Code, Rider, etc.)

## Getting Started

### Installation

1. Clone the repository:
```bash
git clone <repository-url>
cd bruno_backend
```

2. Restore dependencies:
```bash
dotnet restore
```

### Running the Application

Start the development server:
```bash
dotnet run
```

The API will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`

### Swagger UI

Once running, access the interactive API documentation at:
```
http://localhost:5000/swagger
```

## API Endpoints

### Health Check
- **GET** `/`
  - Returns: `"Hello World!"`
  - Description: Basic health check endpoint

## Development

### Build

```bash
dotnet build
```

### Watch Mode

Run the application with hot reload:
```bash
dotnet watch run
```

### Clean

```bash
dotnet clean
```

## Configuration

Application settings can be modified in:
- `appsettings.json` - General configuration
- `appsettings.Development.json` - Development-specific settings

## Project Status

This is a starter project with the following structure in place:
- Minimal API setup
- Swagger documentation
- Folder structure for Controllers, Models, and Services

## Contributing

1. Create a feature branch
2. Make your changes
3. Submit a pull request

## License

[Add your license here]
