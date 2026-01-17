# BelanjaYuk Client - Frontend Configuration

This document explains how to configure the BelanjaYuk Blazor WebAssembly frontend for different environments.

## Configuration File

The frontend configuration is stored in:

```
wwwroot/appsettings.json
```

This file contains environment-specific settings that the application uses to connect to the backend API and handle JWT tokens.

## Configuration Properties

| Property     | Description                                             |
| ------------ | ------------------------------------------------------- |
| `ApiBaseUrl` | The base URL of the backend API server                  |
| `JwtKey`     | The secret key used for JWT token encryption/decryption |

## Environment Configurations

### Environment

For the development environment, use the following configuration:

```json
{
  "ApiBaseUrl": "BE_WEBSITE",
  "JwtKey": "YOUR_JWT_KEY"
}
```

## Running the Application

After configuring the appropriate environment settings:

```bash
# Development
dotnet run

# Production build
dotnet publish -c Release
```
