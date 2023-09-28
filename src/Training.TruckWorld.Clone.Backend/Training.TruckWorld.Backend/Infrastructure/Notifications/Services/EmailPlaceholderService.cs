using System.Data;
using System.Text;
using Training.TruckWorld.Backend.Application.Notifications.Services;
using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Infrastructure.Notifications.Services;

public class EmailPlaceholderService : IEmailPlaceholderService
{
    private readonly IUserService _userService;
    private const string _fullName = "{{FullName}}";
    private const string _firstName = "{{FirstName}}";
    private const string _lastName = "{{LastName}}";
    private const string _email = "{{EmailAddress}}";
    private const string _date = "{{Date}}";
    private const string _companyName = "{{CompanyName}}";

    public EmailPlaceholderService(IUserService userService)
    {
        _userService = userService;
    }

    public async ValueTask<(EmailTemplate, Dictionary<string, string>)> GetTemplateValues(Guid userId, EmailTemplate template)
    {
        var placeholders = GetPlaceholders(template.Body);

        var user = await _userService.GetByIdAsync(userId) ?? throw new ArgumentException();

        var result = placeholders.Select(placeholder =>
        {
            var value = placeholder switch
            {
                _fullName => string.Join(user.FirstName, " ", user.LastName),
                _firstName => user.FirstName,
                _lastName => user.LastName,
                _date => DateTime.Now.ToString("dd.MM.yyyy"),
                _companyName => "TruckWorld",
                _ => throw new EvaluateException("Invalid placeholder")
            };

            return new KeyValuePair<string, string>(placeholder, value);
        });
        var values = new Dictionary<string, string>(result);
        return (template, values);
    }

    private IEnumerable<string> GetPlaceholders(string body)
    {
        var placeholder = new StringBuilder();
        var isStartedToGather = false;
        for (int i = 0; i < body.Length; i++)
        {
            if (body[i] == '{')
            {
                i += 1;
                placeholder = new StringBuilder();
                placeholder.Append("{{");
                isStartedToGather = true;
            }
            else if (body[i] == '}')
            {
                i += 1;
                placeholder.Append("}}");
                isStartedToGather = false;
                yield return placeholder.ToString();
            }
            else if (isStartedToGather)
            {
                placeholder.Append(body[i]);
            }

        }
    }
}
