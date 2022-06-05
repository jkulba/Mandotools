# Create Container App

## Description
Azure CLI commands 

## Source
https://docs.microsoft.com/en-us/azure/container-apps/get-started?ocid=AID3042118&tabs=bash

### Step 1
Login to Azure using the Azure CLI.
```shell
az login
```

### Step 2
Register 
* Azure Container Apps extension
* Azure App provider
* Azure OperationalInsights provider for the Azure Monitor Log Analytics Workspace
```shell
az extension add --name containerapp --upgrade
```
```shell
az provider register --namespace Microsoft.App
```
```shell
az provider register --namespace Microsoft.OperationalInsights
```

### Step 3
Add environment variables to your shell
```shell
RESOURCE_GROUP="rg-bucketgroup-001"
LOCATION="westus"
CONTAINERAPPS_ENVIRONMENT="DEVELOPMENT"
```

### Step 4
Create Resource Group
```shell
az group create \
  --name $RESOURCE_GROUP \
  --location $LOCATION
```

### Step 5
Create Container App Environment
```shell
az containerapp env create \
  --name $CONTAINERAPPS_ENVIRONMENT \
  --resource-group $RESOURCE_GROUP \
  --location $LOCATION
```

### Step 6
Create Container App
```shell
az containerapp create \
  --name jk-helloworld-app \
  --resource-group $RESOURCE_GROUP \
  --environment $CONTAINERAPPS_ENVIRONMENT \
  --image mcr.microsoft.com/azuredocs/containerapps-helloworld:latest \
  --target-port 80 \
  --ingress 'external' \
  --query properties.configuration.ingress.fqdn
 ```

If all went well the web app is available at:

https://jk-helloworld-app.agreeablebush-6a5531c3.westus.azurecontainerapps.io/
