
az acr login --name hendersonconsultregistry

dotnet publish --os linux  /t:PublishContainer -c Release