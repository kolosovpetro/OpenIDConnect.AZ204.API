# OpenIDConnect.AZ204.API

![Architecture](./img/01_architrcture.PNG)

## Register the apps in Azure Active Directory

- Create resource group: `az group create --name "rg-open-id-connect" --location "centralus"`

#### Operations App

- Create AD app registration: `az ad app create --display-name "OperationsAgentApiApp"`
- Set Application ID URI at `App registrations -> OperationsAgentApiApp`
- Add a scope: `OperationsAgent.All`
- Client ID: `938ae338-fa49-4000-8642-dbad0541f6b7`
- Tenant ID: `b40a105f-0643-4922-8e60-10fc1abf9c4b`

#### Members App

- Create AD app registration: `az ad app create --display-name "MembersApiApp"`
- Set Application ID URI at `App registrations -> MembersApiApp`
- Add a scope: `OperationsAgent.All`
- `MembersApi` is consumed by `OperationsAgentApi` so add a client application on behalf of `MembersApi` providing the
  client ID `938ae338-fa49-4000-8642-dbad0541f6b7`
- Create role: `Members.Readonly`

#### Operations App

- Create role: `OperationsAgent`