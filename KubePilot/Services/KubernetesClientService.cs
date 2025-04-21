using k8s;
using k8s.Autorest;
using k8s.KubeConfigModels;
using k8s.Models;
using KubePilot.Extensions;
using PutridParrot.Results;

namespace KubePilot.Services;

// https://kubernetes.io/docs/reference/generated/kubernetes-api/v1.32/

public class KubernetesClientService : IKubernetesClientService
{
    private KubernetesClientConfiguration? _kubernetesClientConfiguration;
    private IKubernetes? _kubernetes;
    private K8SConfiguration? _k8SConfiguration;

    public void LoadKubeConfig(string? configLocation = null)
    {
        _k8SConfiguration = KubernetesClientConfiguration.LoadKubeConfig(configLocation);
    }

    public void LoadConfig(string? configLocation, string? context)
    {
        _kubernetesClientConfiguration = KubernetesClientConfiguration.BuildConfigFromConfigFile(configLocation, currentContext: context);
        _kubernetes = new Kubernetes(_kubernetesClientConfiguration);
    }

    public void LoadConfig(string? configLocation, string? context, string? ns)
    {
        _kubernetesClientConfiguration = KubernetesClientConfiguration.BuildConfigFromConfigFile(configLocation, currentContext: context);
        _kubernetesClientConfiguration.Namespace = ns;
        _kubernetes = new Kubernetes(_kubernetesClientConfiguration);
    }

    public void SetNamespace(string? ns = null)
    {
        _kubernetesClientConfiguration.Namespace = ns;
        _kubernetes = new Kubernetes(_kubernetesClientConfiguration);
    }

    public string GetCurrentContext() => _kubernetesClientConfiguration.CurrentContext;
    public string GetCurrentNamespace() => _kubernetesClientConfiguration.Namespace;

    public K8SConfiguration GetClustersConfiguration() => _k8SConfiguration;

    public Task<List<Cluster>> GetClustersAsync()
    {
        return Task.FromResult(_k8SConfiguration.Clusters.ToList());
    }

    public async Task<IResult<V1PodList>> GetPodsAsync(string? nameSpace = null)
    {
        return await GetOrDefault(
            () => nameSpace.HasValue() ? _kubernetes.CoreV1.ListNamespacedPodAsync(nameSpace) : _kubernetes.CoreV1.ListPodForAllNamespacesAsync(), 
            () => new V1PodList([]));
    }

    public async Task<IResult<V1ConfigMapList>> GetConfigMapsAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.CoreV1.ListNamespacedConfigMapAsync(nameSpace) : _kubernetes.CoreV1.ListConfigMapForAllNamespacesAsync(),
            () => new V1ConfigMapList([]));
    }

    public async Task<IResult<V1DaemonSetList>> GetDaemonSetsAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.AppsV1.ListNamespacedDaemonSetAsync(nameSpace) : _kubernetes.AppsV1.ListDaemonSetForAllNamespacesAsync(),
            () => new V1DaemonSetList([]));
    }

    public async Task<IResult<V1DeploymentList>> GetDeploymentsAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.AppsV1.ListNamespacedDeploymentAsync(nameSpace) : _kubernetes.AppsV1.ListDeploymentForAllNamespacesAsync(),
            () => new V1DeploymentList([]));
    }

    public async Task<IResult<V1EndpointsList>> GetEndpointsAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.CoreV1.ListNamespacedEndpointsAsync(nameSpace) : _kubernetes.CoreV1.ListEndpointsForAllNamespacesAsync(), 
            () => new V1EndpointsList([]));
    }

    public async Task<IResult<V1NamespaceList>> GetNamespacesAsync()
    {
        return await GetOrDefault(() => _kubernetes.CoreV1.ListNamespaceAsync(), () => new V1NamespaceList([]));
    }

    public async Task<IResult<V1NodeList>> GetNodesAsync()
    {
        return await GetOrDefault(() => _kubernetes.CoreV1.ListNodeAsync(), () => new V1NodeList([]));
    }

    public async Task<IResult<V1ReplicationControllerList>> GetReplicationControllersAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.CoreV1.ListNamespacedReplicationControllerAsync(nameSpace) : _kubernetes.CoreV1.ListReplicationControllerForAllNamespacesAsync(), 
            () => new V1ReplicationControllerList([]));
    }

    public async Task<IResult<V1ReplicaSetList>> GetReplicaSetsAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.AppsV1.ListNamespacedReplicaSetAsync(nameSpace) : _kubernetes.AppsV1.ListReplicaSetForAllNamespacesAsync(), 
            () => new V1ReplicaSetList([]));
    }

    public async Task<IResult<V1ServiceList>> GetServicesAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.CoreV1.ListNamespacedServiceAsync(nameSpace) : _kubernetes.CoreV1.ListServiceForAllNamespacesAsync(),
            () => new V1ServiceList([]));
    }

    public async Task<IResult<V1StatefulSetList>> GetStatefulSetsAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.AppsV1.ListNamespacedStatefulSetAsync(nameSpace) : _kubernetes.AppsV1.ListStatefulSetForAllNamespacesAsync(),
            () => new V1StatefulSetList([]));
    }

    public async Task<IResult<V1CronJobList>> GetCronJobsAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.BatchV1.ListNamespacedCronJobAsync(nameSpace) : _kubernetes.BatchV1.ListCronJobForAllNamespacesAsync(),
            () => new V1CronJobList([]));
    }

    public async Task<IResult<V1JobList>> GetJobsAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.BatchV1.ListNamespacedJobAsync(nameSpace) : _kubernetes.BatchV1.ListJobForAllNamespacesAsync(), 
            () => new V1JobList([]));
    }

    public async Task<IResult<V1EndpointSliceList>> GetEndpointSlicesAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.DiscoveryV1.ListNamespacedEndpointSliceAsync(nameSpace) : _kubernetes.DiscoveryV1.ListEndpointSliceForAllNamespacesAsync(),
            () => new V1EndpointSliceList([]));
    }

    public async Task<IResult<V1SecretList>> GetSecretsAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.CoreV1.ListNamespacedSecretAsync(nameSpace) : _kubernetes.CoreV1.ListSecretForAllNamespacesAsync(),
            () => new V1SecretList([]));
    }

    public async Task<IResult<V1PersistentVolumeList>> GetPersistentVolumesAsync()
    {
        return await GetOrDefault(() => _kubernetes.CoreV1.ListPersistentVolumeAsync(),
            () => new V1PersistentVolumeList([]));
    }

    public async Task<IResult<V1IngressList>> GetIngressesAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.NetworkingV1.ListNamespacedIngressAsync(nameSpace) : _kubernetes.NetworkingV1.ListIngressForAllNamespacesAsync(),
            () => new V1IngressList([]));
    }

    public async Task<IResult<V1IngressClassList>> GetIngressClassesAsync()
    {
        return await GetOrDefault(() => _kubernetes.NetworkingV1.ListIngressClassAsync(),
            () => new V1IngressClassList([]));
    }

    public async Task<IResult<V1PersistentVolumeClaimList>> GetPersistentVolumeClaimsAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.CoreV1.ListNamespacedPersistentVolumeClaimAsync(nameSpace) : _kubernetes.CoreV1.ListPersistentVolumeClaimForAllNamespacesAsync(),
            () => new V1PersistentVolumeClaimList([]));
    }

    public async Task<IResult<V1StorageClassList>> GetStorageClassesAsync()
    {
        return await GetOrDefault(() => _kubernetes.StorageV1.ListStorageClassAsync(),
            () => new V1StorageClassList([]));
    }

    public async Task<IResult<V1VolumeAttachmentList>> GetVolumeAttachmentsAsync()
    {
        return await GetOrDefault(() => _kubernetes.StorageV1.ListVolumeAttachmentAsync(),
            () => new V1VolumeAttachmentList([]));
    }

    public async Task<IResult<V1beta1VolumeAttributesClassList>> GetVolumeAttributesClassesAsync()
    {
        return await GetOrDefault(() => _kubernetes.StorageV1beta1.ListVolumeAttributesClassAsync(),
            () => new V1beta1VolumeAttributesClassList([]));
    }

    public async Task<IResult<V1APIServiceList>> GetApiServicesAsync()
    {
        return await GetOrDefault(() => _kubernetes.ApiregistrationV1.ListAPIServiceAsync(),
            () => new V1APIServiceList([]));
    }

    public async Task<IResult<V1CertificateSigningRequestList>> GetCertificateSigningRequestsAsync()
    {
        return await GetOrDefault(() => _kubernetes.CertificatesV1.ListCertificateSigningRequestAsync(),
            () => new V1CertificateSigningRequestList([]));
    }

    public async Task<IResult<V1ClusterRoleList>> GetClusterRolesAsync()
    {
        return await GetOrDefault(() => _kubernetes.RbacAuthorizationV1.ListClusterRoleAsync(),
            () => new V1ClusterRoleList([]));
    }

    public async Task<IResult<V1ClusterRoleBindingList>> GetClusterRoleBindingsAsync()
    {
        return await GetOrDefault(() => _kubernetes.RbacAuthorizationV1.ListClusterRoleBindingAsync(),
            () => new V1ClusterRoleBindingList([]));
    }

    public async Task<IResult<V1ComponentStatusList>> GetComponentStatusesAsync()
    {
        return await GetOrDefault(() => _kubernetes.CoreV1.ListComponentStatusAsync(),
            () => new V1ComponentStatusList([]));
    }

    public async Task<IResult<V1FlowSchemaList>> GetFlowSchemasAsync()
    {
        return await GetOrDefault(() => _kubernetes.FlowcontrolApiserverV1.ListFlowSchemaAsync(),
            () => new V1FlowSchemaList([]));
    }

    public async Task<IResult<V1beta1IPAddressList>> GetIPAddressesAsync()
    {
        return await GetOrDefault(() => _kubernetes.NetworkingV1beta1.ListIPAddressAsync(), () => new V1beta1IPAddressList([]));
    }

    public async Task<IResult<V1LeaseList>> GetLeasesAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.CoordinationV1.ListNamespacedLeaseAsync(nameSpace) : _kubernetes.CoordinationV1.ListLeaseForAllNamespacesAsync(), 
            () => new V1LeaseList([]));
    }

    public async Task<IResult<V1alpha2LeaseCandidateList>> GetLeaseCandidatesAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.CoordinationV1alpha2.ListNamespacedLeaseCandidateAsync(nameSpace) : _kubernetes.CoordinationV1alpha2.ListLeaseCandidateForAllNamespacesAsync(),
            () => new V1alpha2LeaseCandidateList([]));
    }

    public async Task<IResult<V1PriorityLevelConfigurationList>> GetPriorityLevelConfigurationsAsync()
    {
        return await GetOrDefault(() => _kubernetes.FlowcontrolApiserverV1.ListPriorityLevelConfigurationAsync(),
            () => new V1PriorityLevelConfigurationList([]));
    }

    public async Task<IResult<V1ResourceQuotaList>> GetResourceQuotasAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.CoreV1.ListNamespacedResourceQuotaAsync(nameSpace) : _kubernetes.CoreV1.ListResourceQuotaForAllNamespacesAsync(), 
            () => new V1ResourceQuotaList([]));
    }

    public async Task<IResult<V1RoleList>> GetRolesAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.RbacAuthorizationV1.ListNamespacedRoleAsync(nameSpace) : _kubernetes.RbacAuthorizationV1.ListRoleForAllNamespacesAsync(),
            () => new V1RoleList([]));
    }

    public async Task<IResult<V1RoleBindingList>> GetRoleBindingsAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.RbacAuthorizationV1.ListNamespacedRoleBindingAsync(nameSpace) : _kubernetes.RbacAuthorizationV1.ListRoleBindingForAllNamespacesAsync(),
            () => new V1RoleBindingList([]));
    }

    public async Task<IResult<V1RuntimeClassList>> GetRuntimeClassesAsync()
    {
        return await GetOrDefault(() => _kubernetes.NodeV1.ListRuntimeClassAsync(), () => new V1RuntimeClassList([]));
    }

    public async Task<IResult<V1ServiceAccountList>> GetServiceAccountsAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.CoreV1.ListNamespacedServiceAccountAsync(nameSpace) : _kubernetes.CoreV1.ListServiceAccountForAllNamespacesAsync(),
            () => new V1ServiceAccountList([]));
    }

    private static async Task<IResult<T>> GetOrDefault<T>(Func<Task<T>> getter, Func<T> defaultResult)
    {
        try
        {
            return Result.Success(await getter());
        }
        catch (HttpOperationException e)
        {
            return Result.Failure(defaultResult(), $"Error - {e.Response.ReasonPhrase}");
        }
        catch (Exception e)
        {
            return Result.Failure(defaultResult(), e.Message);
        }
    }

    public async Task<IResult<V1beta1ServiceCIDRList>> GetServiceCIDRSAsync()
    {
        return await GetOrDefault(() => _kubernetes.NetworkingV1beta1.ListServiceCIDRAsync(),
            () => new V1beta1ServiceCIDRList([]));
    }

    public async Task<IResult<V1alpha1StorageVersionList>> GetStorageVersionsAsync()
    {
        return await GetOrDefault(() => _kubernetes.InternalApiserverV1alpha1.ListStorageVersionAsync(),
            () => new V1alpha1StorageVersionList([]));
    }

    public async Task<IResult<V1alpha1StorageVersionMigrationList>> GetStorageVersionMigrationsAsync()
    {
        return await GetOrDefault(() => _kubernetes.StoragemigrationV1alpha1.ListStorageVersionMigrationAsync(),
            () => new V1alpha1StorageVersionMigrationList([]));
    }

    public async Task<IResult<V1alpha1ClusterTrustBundleList>> GetClusterTrustBundlesAsync()
    {
        return await GetOrDefault(() => _kubernetes.CertificatesV1alpha1.ListClusterTrustBundleAsync(),
            () => new V1alpha1ClusterTrustBundleList([]));
    }

    public async Task<IResult<V1ControllerRevisionList>> GetControllerRevisionsAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.AppsV1.ListNamespacedControllerRevisionAsync(nameSpace) : _kubernetes.AppsV1.ListControllerRevisionForAllNamespacesAsync(),
            () => new V1ControllerRevisionList([]));
    }

    public async Task<IResult<V1CustomResourceDefinitionList>> GetCustomResourceDefinitionsAsync()
    {
        return await GetOrDefault(() => _kubernetes.ApiextensionsV1.ListCustomResourceDefinitionAsync(),
            () => new V1CustomResourceDefinitionList([]));
    }

    public async Task<IResult<V1beta1DeviceClassList>> GetDeviceClassesAsync()
    {
        return await GetOrDefault(() => _kubernetes.ResourceV1beta1.ListDeviceClassAsync(),
            () => new V1beta1DeviceClassList([]));
    }

    public async Task<IResult<V1LimitRangeList>> GetLimitRangesAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.CoreV1.ListNamespacedLimitRangeAsync(nameSpace) : _kubernetes.CoreV1.ListLimitRangeForAllNamespacesAsync(),
            () => new V1LimitRangeList([]));
    }

    public async Task<IResult<V2HorizontalPodAutoscalerList>> GetHorizontalPodAutoscalersAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.AutoscalingV2.ListNamespacedHorizontalPodAutoscalerAsync(nameSpace) : _kubernetes.AutoscalingV2.ListHorizontalPodAutoscalerForAllNamespacesAsync(),
            () => new V2HorizontalPodAutoscalerList([]));
    }

    public async Task<IResult<V1MutatingWebhookConfigurationList>> GetMutatingWebhookConfigurationsAsync()
    {
        return await GetOrDefault(() => _kubernetes.AdmissionregistrationV1.ListMutatingWebhookConfigurationAsync(),
            () => new V1MutatingWebhookConfigurationList([]));
    }

    public async Task<IResult<V1PodTemplateList>> GetPodTemplatesAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.CoreV1.ListNamespacedPodTemplateAsync(nameSpace) : _kubernetes.CoreV1.ListPodTemplateForAllNamespacesAsync(),
            () => new V1PodTemplateList([]));
    }

    public async Task<IResult<V1PodDisruptionBudgetList>> GetPodDisruptionBudgetsAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.PolicyV1.ListNamespacedPodDisruptionBudgetAsync(nameSpace) : _kubernetes.PolicyV1.ListPodDisruptionBudgetForAllNamespacesAsync(),
            () => new V1PodDisruptionBudgetList([]));
    }

    public async Task<IResult<V1PriorityClassList>> GetPriorityClassesAsync()
    {
        return await GetOrDefault(() => _kubernetes.SchedulingV1.ListPriorityClassAsync(),
            () => new V1PriorityClassList([]));
    }

    public async Task<IResult<V1beta1ResourceClaimList>> GetResourceClaimsAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.ResourceV1beta1.ListNamespacedResourceClaimAsync(nameSpace) : _kubernetes.ResourceV1beta1.ListResourceClaimForAllNamespacesAsync(),
            () => new V1beta1ResourceClaimList([]));
    }

    public async Task<IResult<V1beta1ResourceClaimTemplateList>> GetResourceClaimTemplatesAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.ResourceV1beta1.ListNamespacedResourceClaimTemplateAsync(nameSpace) : _kubernetes.ResourceV1beta1.ListResourceClaimTemplateForAllNamespacesAsync(),
            () => new V1beta1ResourceClaimTemplateList([]));
    }

    public async Task<IResult<V1beta1ResourceSliceList>> GetResourceSlicesAsync()
    {
        return await GetOrDefault(() => _kubernetes.ResourceV1beta1.ListResourceSliceAsync(),
            () => new V1beta1ResourceSliceList([]));
    }

    public async Task<IResult<V1ValidatingAdmissionPolicyList>> GetValidatingAdmissionPoliciesAsync()
    {
        return await GetOrDefault(() => _kubernetes.AdmissionregistrationV1.ListValidatingAdmissionPolicyAsync(),
            () => new V1ValidatingAdmissionPolicyList([]));
    }

    public async Task<IResult<V1ValidatingAdmissionPolicyBindingList>> GetValidatingAdmissionPolicyBindingsAsync()
    {
        return await GetOrDefault(() => _kubernetes.AdmissionregistrationV1.ListValidatingAdmissionPolicyBindingAsync(),
            () => new V1ValidatingAdmissionPolicyBindingList([]));
    }

    public async Task<IResult<V1ValidatingWebhookConfigurationList>> GetValidatingWebhookConfigurationsAsync()
    {
        return await GetOrDefault(() => _kubernetes.AdmissionregistrationV1.ListValidatingWebhookConfigurationAsync(),
            () => new V1ValidatingWebhookConfigurationList([]));
    }

    public async Task<IResult<V1NetworkPolicyList>> GetNetworkPoliciesAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.NetworkingV1.ListNamespacedNetworkPolicyAsync(nameSpace) : _kubernetes.NetworkingV1.ListNetworkPolicyForAllNamespacesAsync(),
            () => new V1NetworkPolicyList([]));
    }

    public async Task<IResult<V1alpha1MutatingAdmissionPolicyList>> GetMutatingAdmissionPoliciesAsync()
    {
        return await GetOrDefault(() => _kubernetes.AdmissionregistrationV1alpha1.ListMutatingAdmissionPolicyAsync(),
            () => new V1alpha1MutatingAdmissionPolicyList([]));
    }

    public async Task<IResult<V1alpha1MutatingAdmissionPolicyBindingList>> GetMutatingAdmissionPolicyBindingsAsync()
    {
        return await GetOrDefault(
            () => _kubernetes.AdmissionregistrationV1alpha1.ListMutatingAdmissionPolicyBindingAsync(),
            () => new V1alpha1MutatingAdmissionPolicyBindingList([]));
    }

    public async Task<string> GetPodLogsAsync(string nameSpace, string podName)
    {
        var stream = await _kubernetes.CoreV1.ReadNamespacedPodLogAsync(
            name: podName, nameSpace, 
            //container:
            follow: false,
            tailLines:100);
        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }

    public async Task<IResult<PodMetricsList>> GetPodMetricsAsync()
    {
        // kubectl get apiservices (v1beta1.metrics.k8s.io. If it's missing, you'll need to install the Metrics Server)
        return Result.Success(await _kubernetes.GetKubernetesPodsMetricsAsync());
    }

    public async Task<IResult<NodeMetricsList>> GetNodeMetricsAsync()
    {
        // kubectl get apiservices (v1beta1.metrics.k8s.io. If it's missing, you'll need to install the Metrics Server)
        return Result.Success(await _kubernetes.GetKubernetesNodesMetricsAsync());
    }

    public async Task<IResult<V1APIResourceList>> GetApiResourcesAsync()
    {
        return await GetOrDefault(
            () => _kubernetes.CoreV1.GetAPIResourcesAsync(),
            () => new V1APIResourceList(string.Empty, []));
    }

    public async Task<IResult<V1APIGroupList>> GetAPIVersionsAsync()
    {
        return await GetOrDefault(
            () => _kubernetes.Apis.GetAPIVersionsAsync(),
            () => new V1APIGroupList());
    }

    public async Task<IResult<Corev1EventList>> GetEventsAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.CoreV1.ListNamespacedEventAsync(nameSpace) : _kubernetes.CoreV1.ListEventForAllNamespacesAsync(),
            () => new Corev1EventList());
    }

    public async Task<IResult<V1CSIDriverList>> GetCsiDriversAsync()
    {
        return await GetOrDefault(
            () => _kubernetes.StorageV1.ListCSIDriverAsync(),
            () => new V1CSIDriverList());
    }
    public async Task<IResult<V1CSINodeList>> GetCsiNodesAsync()
    {
        return await GetOrDefault(
            () => _kubernetes.StorageV1.ListCSINodeAsync(),
            () => new V1CSINodeList());
    }

    public async Task<IResult<V1CSIStorageCapacityList>> GetCsiStorageCapacitiesAsync(string? nameSpace = null)
    {
        return await GetOrDefault(() => nameSpace.HasValue() ? _kubernetes.StorageV1.ListNamespacedCSIStorageCapacityAsync(nameSpace) : _kubernetes.StorageV1.ListCSIStorageCapacityForAllNamespacesAsync(),
            () => new V1CSIStorageCapacityList());
    }

    public async Task<IResult<V1Pod>> DescribePodAsync(string podName, string nameSpace)
    {
        return await GetOrDefault(
            () => _kubernetes.CoreV1.ReadNamespacedPodAsync(podName, nameSpace),
            () => new V1Pod());
    }

    public async Task<IResult<V1Node>> DescribeNodeAsync(string nodeName)
    {
        return await GetOrDefault(
            () => _kubernetes.CoreV1.ReadNodeAsync(nodeName),
            () => new V1Node());
    }

    // kubectl get deployment my-app -o yaml
    public async Task<IResult<V1Deployment>> DescribeDeploymentAsync(string deploymentName, string namespaceName = "default")
    {
        return await GetOrDefault(
            () => _kubernetes.AppsV1.ReadNamespacedDeploymentAsync(deploymentName, namespaceName),
            () => new V1Deployment());
    }

    public async Task<IResult<V1Ingress>> DescribeIngressAsync(string ingressName, string namespaceName = "default")
    {
        return await GetOrDefault(() => _kubernetes.NetworkingV1.ReadNamespacedIngressAsync(ingressName, namespaceName),
            () => new V1Ingress());
    }

}