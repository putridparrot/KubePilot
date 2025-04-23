using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using CodeGenerator;
using HandlebarsDotNet;
using YamlDotNet.Serialization;

var root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

var deserializer = new DeserializerBuilder()
    .Build();

Handlebars.RegisterHelper("PageName", (_, arguments) => 
    arguments[0] is string title ? Functions.Normalize(title).ToLower() : "<missing>");
Handlebars.RegisterHelper("MethodName", (_, arguments) =>
    arguments[0] is string title ? Functions.Normalize(title) : "<missing>");
Handlebars.RegisterHelper("VariableName", (_, arguments) =>
    arguments[0] is string title ? Functions.VariableName(title) : "<missing>");
Handlebars.RegisterHelper("FilterName", (_, arguments) =>
    arguments[0] is string title ? $"{Functions.VariableName(title)}Filter" : "<missing>");
Handlebars.RegisterHelper("HasFilter", (_, arguments) =>
    arguments[0] is List<Field> fields && Functions.HasFilter(fields));

var operations = deserializer.Deserialize<Application>(File.OpenText(Path.Combine(root, "configuration.yaml")));

if (operations.Definitions is null)
{
    Console.WriteLine("No Definitions found");
    return;
}

var pageTemplate = Handlebars.Compile(File.ReadAllText(Path.Combine(root, "razor.template")));

var outputFolder = Path.Combine(root, ".Generated");

if (Directory.Exists(outputFolder))
{
    Directory.Delete(outputFolder, true);
}

var pagesFolder = Path.Combine(outputFolder, "pages");

Directory.CreateDirectory(pagesFolder);

foreach (var definition in operations.Definitions)
{
    var code = pageTemplate(definition);

    File.WriteAllText($"{Path.Combine(pagesFolder, definition.Title.Replace(" ", ""))}.razor", code);
}

// generate navmenu

var navigationFolder = Path.Combine(outputFolder, "navigation");

Directory.CreateDirectory(navigationFolder);

var nameMenuTemplate = Handlebars.Compile(File.ReadAllText(Path.Combine(root, "navmenu.template")));

var orderedDefinitions = operations.Definitions.OrderBy(d => d.Title).ToArray();
var navMenuCode = nameMenuTemplate(orderedDefinitions);
File.WriteAllText(Path.Combine(navigationFolder, "NavMenu.razor"), navMenuCode);
