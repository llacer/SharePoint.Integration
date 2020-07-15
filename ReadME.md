# SharePoint Application Permission using Azure Access Control (ACS)

Access SharePoint resource using 

1. Azure AD authentication 
2. Tenant scoped permissions
3. .NET Standard SharePoint client object model (CSOM) Library

```
Important

Azure Access Control (ACS), a service of Azure Active Directory (Azure AD), has been retired on November 7, 2018. This retirement does not impact the SharePoint Add-in model, which uses the https://accounts.accesscontrol.windows.net hostname (which is not impacted by this retirement).
```

## Azure AD Setup

## SharePoint Setup

Grant Permission to an App
https://hostname.sharepoint.com/sites/{SiteName}/_layouts/15/appinv.aspx

```
App Id: Azure App Client Id [Lookup)] 
App Domain: www.localhost.com
Permission Request XML:
<AppPermissionRequests AllowAppOnlyPolicy="true">  
    <AppPermissionRequest Scope="http://sharepoint/content/sitecollection" Right="FullControl" />
</AppPermissionRequests>
```
**Trust the App** by clicking 'Trust It' Button

## Nuget Packages

1. Microsoft.Extensions.Configuration
2. Microsoft.Extensions.Configuration.Binder
3. Microsoft.Extensions.Configuration.Json
4. Microsoft.Extensions.DependencyInjection
5. Microsoft.SharePointOnline.CSOM

## References

1. https://docs.microsoft.com/en-us/sharepoint/dev/solution-guidance/security-apponly-azureacs
2. https://medium.com/ng-sp/sharepoint-add-in-permission-xml-cheat-sheet-64b87d8d7600
3. https://www.linkedin.com/pulse/csom-net-standard-sharepoint-app-only-principal-elnur-babayev/
4. https://www.anexinet.com/blog/getting-an-access-token-for-sharepoint-online/

