using k8s.KubeConfigModels;
using k8s.Models;

namespace KubePilot.Components.ViewModels;

public class ClusterViewModel(Cluster cluster, Context context)
{
    public Cluster Cluster { get; set; } = cluster;
    public Context Context { get; set; } = context;
    public IList<string>? Namespaces { get; set; }
}