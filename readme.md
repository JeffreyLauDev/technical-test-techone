# NumberToWords Project

The **NumberToWords** project is a simple .NET application that converts a decimal number into its word representation in the format "X DOLLARS AND Y CENTS" (if applicable).

## Prerequisites

Ensure you have the following installed:

- [.NET SDK 9.0](https://dotnet.microsoft.com/download/dotnet)
- [Visual Studio Code](https://code.visualstudio.com/) or any C# compatible IDE
- [Git](https://git-scm.com/)

---

## Getting Started

### 1. Clone the Repository

Start by cloning the project to your local machine:

### 2. Restore Dependencies

Run the following command to restore the necessary NuGet packages for the project:

```bash
dotnet restore
```

---

## Running the Application

To run the NumberToWords application:

1. Open a terminal or command prompt.
2. Navigate to the project directory.
3. Run the application with the following command:

```bash
dotnet run --project NumberToWords
```

The application will start up and expose an API endpoint (`/convert`) where you can post numbers for conversion.

### Example Request (using `curl`)

To test the API, you can use `curl` to send a POST request:

```bash
curl -X POST "http://localhost:5000/convert" -H "Content-Type: application/json" -d '{"Number": "1234.56"}'
```

The expected response will be:

```json
{
  "words": "ONE THOUSAND TWO HUNDRED THIRTY FOUR DOLLARS AND FIFTY SIX CENTS"
}
```

---

## Running Tests

To run the tests, follow these steps:

### 1. Restore Test Dependencies

If you have not done so already, restore dependencies for the test project:

```bash
dotnet restore --project NumberToWords.Tests
```

### 2. Run Tests

To run the tests for the project:

```bash
dotnet test --project NumberToWords.Tests
```

This will execute all unit tests and show the results in the terminal.

---

## Running Tests with Verbose Output

For more detailed output (helpful for debugging), you can use:

```bash
dotnet test --verbosity detailed
```

---

## Testing API with Postman (Optional)

If you prefer a GUI-based method to test the API, you can use Postman to make API requests:

1. Open Postman.
2. Set the request type to `POST` and use `http://localhost:5000/convert` as the URL.
3. In the body, choose `raw` and set the type to `JSON`, then input:

```json
{
  "Number": "5678.90"
}
```

4. Hit `Send`, and the response will be:

```json
{
  "words": "FIVE THOUSAND SIX HUNDRED SEVENTY EIGHT DOLLARS AND NINETY CENTS"
}
```

---

## Project Structure

- `NumberToWords/` - Main application code
- `NumberToWords.Tests/` - Unit tests for the project

---

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
