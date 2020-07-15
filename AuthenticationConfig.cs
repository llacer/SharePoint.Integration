namespace SharePoint.Integration.ACS
{
    public class AuthenticationConfig
    {
        public string AuthEndPoint => $"https://accounts.accesscontrol.windows.net/{TenantId}/tokens/OAuth/2";
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string HostName { get; set; }
        public string TenantId { get; set; }
        public string Resource => $"00000003-0000-0ff1-ce00-000000000000/{HostName}@{TenantId}";
    }
}