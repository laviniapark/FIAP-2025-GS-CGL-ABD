#!/bin/bash

LOCATION="eastus2"
RG="rg-gs2025-cgl"
APP_SERVICE_PLAN="plan-gs2025-cgl"
WEBAPP_NAME="cglgs2025webapp"

# Codigo comentado - Rodar Manualmente
# Acesse o seu Azure Portal e abra o CloudShell

# git clone https://github.com/laviniapark/FIAP-2025-GS-CGL-ABD.git

# mv FIAP-2025-GS-CGL-ABD/scripts/build.sh ~/

#chmod +x build.sh

# ./build.sh

az group create --name $RG --location $LOCATION

az appservice plan create \
  --name $APP_SERVICE_PLAN \
  --resource-group $RG \
  --location $LOCATION \
  --sku F1 \
  --is-linux

az webapp create \
  --name $WEBAPP_NAME \
  --resource-group "$RG" \
  --plan "$APP_SERVICE_PLAN" \
  --runtime "DOTNETCORE:9.0"
  
az webapp config appsettings set \
  --name "$WEBAPP_NAME" \
  --resource-group "$RG" \
  --settings \
   HEALTH_URL="cglgs2025webapp.azurewebsites.net/health"
   
# cd FIAP-2025-GS-CGL-ABD/AiManagementApp

# dotnet publish -c Release -o ./publish

# cd publish

# zip -r ../deploy.zip .

# cd ..

#az webapp deploy --name "cglgs2025webapp" --resource-group "rg-gs2025-cgl" --src-path "deploy.zip" --type zip
