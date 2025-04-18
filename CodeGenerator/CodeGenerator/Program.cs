using CodeGenerator;
using HandlebarsDotNet;
using YamlDotNet.Serialization;

const string root = @"D:\Development\putridparrot\KubePilot\CodeGenerator\CodeGenerator\";

var deserializer = new DeserializerBuilder()
    .Build();

Handlebars.RegisterHelper("PageName", (_, arguments) => 
    arguments[0] is string title ? title.Replace(" ", string.Empty).ToLower() : "<missing>");

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
