# OpenIDConnect.AZ204.API

[![Run Build and Test](https://github.com/kolosovpetro/OpenIDConnect.AZ204.API/actions/workflows/run-build-and-test-dotnet.yml/badge.svg)](https://github.com/kolosovpetro/OpenIDConnect.AZ204.API/actions/workflows/run-build-and-test-dotnet.yml)

Out solution has the following architecture

![Architecture](./img/01_architrcture.PNG)

## Register the apps in Azure Active Directory

#### Operations App registration

- Create AD app registration using Azure Portal: `OperationsAgentApiApp`
- Set Application ID URI at `App registrations -> Expose API -> Set URI ID`
- Add a scope: `OperationsAgent.All`
- Client ID: `abae4fc8-c869-4edf-afdc-448c3029e88c`
- Tenant ID: `b40a105f-0643-4922-8e60-10fc1abf9c4b`

#### Members App registration

- Create AD app registration using Azure Portal: `MembersApiApp`
- Set Application ID URI at `App registrations -> Expose API -> Set URI ID`
- Add a scope: `MembersApi.All`
- Client ID: `9c8ab14b-7674-49a7-a5f0-a5681a415633`
- Tenant ID: `b40a105f-0643-4922-8e60-10fc1abf9c4b`
- `MembersApi` is consumed by `OperationsAgentApi` so add a client application on behalf of `MembersApi` providing the
  client ID `9c8ab14b-7674-49a7-a5f0-a5681a415633`
- Create role: `Members.Readonly`

#### Operations App registration

- Create role: `OperationsAgent`

## Configure OpenIDConnect.AZ204.Members ASP NET Core Web API

- Create .NET 6 Web API from template
- Install nuget packages:
    - `dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer`
    - `dotnet add package Microsoft.AspNetCore.Authentication.OpenIdConnect`
    - `dotnet add package Microsoft.Identity.Web`
    - `dotnet add package Microsoft.Identity.Web.UI`
    - `dotnet restore`
- Add Authentication to the project services DI: `builder.Services
  .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
  .AddMicrosoftIdentityWebApp(configurationSection);`
- Update appsettings.json with the following section:

```bash 
  "AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "Domain": "kolosovp94gmail.onmicrosoft.com",
    "TenantId": "b40a105f-0643-4922-8e60-10fc1abf9c4b",
    "ClientId": "9c8ab14b-7674-49a7-a5f0-a5681a415633",
    "CallbackPath": "/signin-oidc"
  }
```

## Configure app registration for Postman

- Create AD app registration using Azure Portal: `PostmanClientApp`
- Client ID: `31826fda-538d-4e09-8a67-e973c703a1c9`
- Add Platform: `App registrations -> PostmanClientApp -> Authentication -> Add Platform`
    - Type: `Single-page application`
    - Redirect URI: `https://oauth.pstmn.io/v1/callback`
    - Access tokens (used for implicit flows): `Checked`
    - ID tokens (used for implicit and hybrid flows): `Checked`

- Add `PostmanClientApp` as client to the `MembersApiApp`

- Configure Postman Request
    - Authorization tab:
        - Type: `OAuth 2.0`
        - Add authorization data to: `Request headers`
        - Callback URL: `https://oauth.pstmn.io/v1/callback`
        - Auth URL: `https://login.microsoftonline.com/b40a105f-0643-4922-8e60-10fc1abf9c4b/oauth2/v2.0/authorize`
        - Access Token URL: `https://login.microsoftonline.com/b40a105f-0643-4922-8e60-10fc1abf9c4b/oauth2/v2.0/token`
        - Client ID: `03e92f38-fb5f-4841-9fc7-506cb3af48f7`
        - Scope: `api://9c8ab14b-7674-49a7-a5f0-a5681a415633/MembersApi.All`
        - Client Secret: `Create on behalf of Postman app registration and copy paste`
        - Authorize using browser: `Checked`

- Configure Enterprise app roles
    - Go to: `Enterprise apps -> MembersApiApp -> Assign roles -> Assign readonly role to your account`

- Launch postman request and validate it

## Configure second API

- Configure same way as you did for the first one