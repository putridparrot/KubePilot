using k8s.KubeConfigModels;
using k8s.Models;
using PutridParrot.Results;

namespace KubePilot.Services;

public interface IKubernetesClientService
{
    void LoadKubeConfig(string? configLocation = null);
    void LoadConfig(string? configLocation = null, string? context = null, string? ns = null);
    string GetCurrentContext();
    K8SConfiguration GetClustersConfiguration();

    void SetNamespace(string? ns = null);

    Task<List<Cluster>> GetClustersAsync();
    Task<IResult<V1PodList>> GetPodsAsync();
    Task<IResult<V1ConfigMapList>> GetConfigMapsAsync();
    Task<IResult<V1DaemonSetList>> GetDaemonSetsAsync();
    Task<IResult<V1DeploymentList>> GetDeploymentsAsync();
    Task<IResult<V1EndpointsList>> GetEndpointsAsync();
    Task<IResult<V1NamespaceList>> GetNamespacesAsync();
    Task<IResult<V1NodeList>> GetNodesAsync();
    Task<IResult<V1ReplicationControllerList>> GetReplicationControllersAsync();
    Task<IResult<V1ReplicaSetList>> GetReplicaSetsAsync();
    Task<IResult<V1ServiceList>> GetServicesAsync();
    Task<IResult<V1StatefulSetList>> GetStatefulSetsAsync();
    Task<IResult<V1CronJobList>> GetCronJobsAsync();
    Task<IResult<V1JobList>> GetJobsAsync();
    Task<IResult<V1EndpointSliceList>> GetEndpointSlicesAsync();
    Task<IResult<V1SecretList>> GetSecretsAsync();
    Task<IResult<V1PersistentVolumeList>> GetPersistentVolumesAsync();
    Task<IResult<V1IngressList>> GetIngressesAsync();
    Task<IResult<V1IngressClassList>> GetIngressClassesAsync();
    Task<IResult<V1PersistentVolumeClaimList>> GetPersistentVolumeClaimsAsync();
    Task<IResult<V1StorageClassList>> GetStorageClassesAsync();
    Task<IResult<V1VolumeAttachmentList>> GetVolumeAttachmentsAsync();
    Task<IResult<V1beta1VolumeAttributesClassList>> GetVolumeAttributesClassesAsync();
    Task<IResult<V1APIServiceList>> GetApiServicesAsync();
    Task<IResult<V1CertificateSigningRequestList>> GetCertificateSigningRequestsAsync();
    Task<IResult<V1ClusterRoleList>> GetClusterRolesAsync();
    Task<IResult<V1ClusterRoleBindingList>> GetClusterRoleBindingsAsync();
    Task<IResult<V1ComponentStatusList>> GetComponentStatusesAsync();
    Task<IResult<V1FlowSchemaList>> GetFlowSchemasAsync();
    Task<IResult<V1beta1IPAddressList>> GetIPAddressesAsync();
    Task<IResult<V1LeaseList>> GetLeasesAsync();
    Task<IResult<V1alpha2LeaseCandidateList>> GetLeaseCandidatesAsync();
    Task<IResult<V1PriorityLevelConfigurationList>> GetPriorityLevelConfigurationsAsync();
    Task<IResult<V1ResourceQuotaList>> GetResourceQuotasAsync();
    Task<IResult<V1RoleList>> GetRolesAsync();
    Task<IResult<V1RoleBindingList>> GetRoleBindingsAsync();
    Task<IResult<V1RuntimeClassList>> GetRuntimeClassesAsync();
    Task<IResult<V1ServiceAccountList>> GetServiceAccountsAsync();
    Task<IResult<V1beta1ServiceCIDRList>> GetServiceCIDRSAsync();
    Task<IResult<V1alpha1StorageVersionList>> GetStorageVersionsAsync();
    Task<IResult<V1alpha1StorageVersionMigrationList>> GetStorageVersionMigrationsAsync();
    Task<IResult<V1alpha1ClusterTrustBundleList>> GetClusterTrustBundlesAsync();
    Task<IResult<V1ControllerRevisionList>> GetControllerRevisionsAsync();
    Task<IResult<V1CustomResourceDefinitionList>> GetCustomResourceDefinitionsAsync();
    Task<IResult<V1beta1DeviceClassList>> GetDeviceClassesAsync();
    Task<IResult<V1LimitRangeList>> GetLimitRangesAsync();
    Task<IResult<V2HorizontalPodAutoscalerList>> GetHorizontalPodAutoscalersAsync();
    Task<IResult<V1MutatingWebhookConfigurationList>> GetMutatingWebhookConfigurationsAsync();
    Task<IResult<V1PodTemplateList>> GetPodTemplatesAsync();
    Task<IResult<V1PodDisruptionBudgetList>> GetPodDisruptionBudgetsAsync();
    Task<IResult<V1PriorityClassList>> GetPriorityClassesAsync();
    Task<IResult<V1beta1ResourceClaimList>> GetResourceClaimsAsync();
    Task<IResult<V1beta1ResourceClaimTemplateList>> GetResourceClaimTemplatesAsync();
    Task<IResult<V1beta1ResourceSliceList>> GetResourceSlicesAsync();
    Task<IResult<V1ValidatingAdmissionPolicyList>> GetValidatingAdmissionPoliciesAsync();
    Task<IResult<V1ValidatingAdmissionPolicyBindingList>> GetValidatingAdmissionPolicyBindingsAsync();
    Task<IResult<V1ValidatingWebhookConfigurationList>> GetValidatingWebhookConfigurationsAsync();
    Task<IResult<V1NetworkPolicyList>> GetNetworkPoliciesAsync();
    Task<IResult<V1alpha1MutatingAdmissionPolicyList>> GetMutatingAdmissionPoliciesAsync();
    Task<IResult<V1alpha1MutatingAdmissionPolicyBindingList>> GetMutatingAdmissionPolicyBindingsAsync();

    Task<string> GetPodLogsAsync(string nameSpace, string podName);
    Task<IResult<PodMetricsList>> GetPodMetricsAsync();
    Task<IResult<NodeMetricsList>> GetNodeMetricsAsync();
    Task<IResult<V1APIResourceList>> GetApiResourcesAsync();
    Task<IResult<V1APIGroupList>> GetAPIVersionsAsync();
    Task<IResult<Corev1EventList>> GetEventsAsync();
    Task<IResult<V1CSIDriverList>> GetCsiDriversAsync();
    Task<IResult<V1CSINodeList>> GetCsiNodesAsync();
    Task<IResult<V1CSIStorageCapacityList>> GetCsiStorageCapacitiesAsync();

    Task<IResult<V1Pod>> DescribePodAsync(string podName, string nameSpace);
}