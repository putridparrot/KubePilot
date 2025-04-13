using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace KubePilot.Utils;

public class Converter
{
    // var pod = await client.ReadNamespacedPodAsync(podName, namespaceName);
    public static string ToYaml<T>(T model)
    {
        var serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        return serializer.Serialize(model);
    }
}