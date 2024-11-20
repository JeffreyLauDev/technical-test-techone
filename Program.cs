using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddScoped<INumberConverter, NumberConverter>();

var app = builder.Build();

app.UseDefaultFiles(); // Serve index.html by default
app.UseStaticFiles();  // Enable serving static files from wwwroot

app.MapPost("/convert", (InputModel input, INumberConverter converter) =>
{
    if (!decimal.TryParse(input.Number, out decimal value))
        return Results.BadRequest(new { error = "Invalid Input" });

    var result = converter.NumberToWords(value);
    return Results.Ok(new { words = result });
});

app.Run();

public interface INumberConverter
{
    string NumberToWords(decimal number);
}

public class NumberConverter : INumberConverter
{
    public string NumberToWords(decimal number)
    {
        if (number == 0)
            return "ZERO DOLLARS";

        var integerPart = (int)number;
        var fractionalPart = (int)((number - integerPart) * 100);

        var integerWords = ConvertIntegerToWords(integerPart);
        var fractionalWords = fractionalPart > 0
            ? $" AND {ConvertIntegerToWords(fractionalPart)} CENTS"
            : "";

        return $"{integerWords} DOLLARS{fractionalWords}";
    }

    private string ConvertIntegerToWords(int number)
    {
        if (number == 0)
            return "ZERO";

        string[] ones = { "", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE" };
        string[] teens = { "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
        string[] tens = { "", "", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

        string ConvertLargeNumber(int value, int divisor, string label)
        {
            int quotient = value / divisor;
            int remainder = value % divisor;
            return remainder == 0
                ? $"{ConvertIntegerToWords(quotient)} {label}"
                : $"{ConvertIntegerToWords(quotient)} {label} {ConvertIntegerToWords(remainder)}";
        }

        if (number < 10) return ones[number];
        if (number < 20) return teens[number - 10];
        if (number < 100) return $"{tens[number / 10]} {ones[number % 10]}".Trim();
        if (number < 1000) return $"{ones[number / 100]} HUNDRED {ConvertIntegerToWords(number % 100)}".Trim();
        if (number < 1000000) return ConvertLargeNumber(number, 1000, "THOUSAND");
        return ConvertLargeNumber(number, 1000000, "MILLION");
    }
}

public class InputModel
{
    public string Number { get; set; } = string.Empty; // Default to empty string
}
