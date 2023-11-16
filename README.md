# cutli-backend

Url Shortener API backend built in .NET and MySQL

## Table of Contents

- [Getting Started](#getting-started)
- [Usage](#usage)
- [Endpoints](#endpoints)
- [Contributing](#contributing)
- [License](#license)

## Getting Started

### Prerequisites

 - MySQL Server
 - .NET 7.0
 - dotnet-ef

### Installing

Clone the repository
```bash
$ git clone https://github.com/fzbian/cutli-backend
```

Download the packages
```bash
$ dotnet restore
```

Config `appsettings.json` for database connection

```json
"ConnectionStrings": {
    "WebApiDatabase": "Server=server;Port=port;Database=db;User=user;Password=pass;"
}
```

Migrate the database
```bash
$ dotnet ef migrations add "init"
$ dotnet ef database update
```

Run program
```bash
$ dotnet run
```

## Usage

Use HTTP methods (GET, POST, PUT, DELETE) to interact with the API endpoints.

## Endpoints

### Create Short URL

- **Method:** `POST`
- **Endpoint:** `/api/shorturl`
- **Description:** Create a short URL from the provided original URL.
- **Request Body:**
  - `OriginalUrl` (string, required): The original URL that you want to shorten.
- **Response:**
  - `200 OK` with the shortened URL if successful.
  - `400 Bad Request` if the `OriginalUrl` is null or not a valid URL.

### Redirect to Original URL

- **Method:** `GET`
- **Endpoint:** `/{shortUrl}`
- **Description:** Redirect to the original URL associated with the provided short URL.
- **Response:**
  - `302 Found` and a redirect to the original URL if the short URL is found.
  - `404 Not Found` if the short URL is not found.

## Contributing

Feel free to contribute to this project.

## License

This project is licensed under the [MIT license] - see the [LICENSE FILE](LICENSE) file for details.
