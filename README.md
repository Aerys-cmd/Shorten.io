# Shorten.io

## Installation
```bash
Works with a in memory database, so you will lose all your data when you restart the server.
Open the solution in Visual Studio and run the project on https Profile.
```

## Usage
```bash
The API is documented with Swagger, so you can test it there.

To create a new short url, you need to send a POST request to https://localhost:7222/api/shortened-urls with the following body:
{
  "url": "https://www.google.com",
  "customUrlCode" : null // This is optional. If you want to create a custom url, you can send it here.
}

To test the redirect, you need to send a GET request to https://localhost:7222/{shortenedUrlCode} on the browser.
```


Created By [Recep Obut](https://www.linkedin.com/in/recep-obut/)
