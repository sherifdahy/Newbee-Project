using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting; 

namespace Newbee.BLL.Services.Email;

public class EmailBuilder
{
    private readonly IWebHostEnvironment _env;

    public EmailBuilder(IWebHostEnvironment env)
    {
        _env = env;
    }
    public string GenerateEmailBody(string template, Dictionary<string, string> templateModel)
    {
        var templatePath = Path.Combine(_env.WebRootPath, "Templates", $"{template}.html");

        if (!File.Exists(templatePath))
            throw new FileNotFoundException($"Template not found: {templatePath}");

        var body = File.ReadAllText(templatePath);

        foreach (var item in templateModel)
            body = body.Replace(item.Key, item.Value);

        return body;
    }
}
