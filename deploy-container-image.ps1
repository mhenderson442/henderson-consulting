
az acr login --name hendersonconsultregistry

dotnet publish --os linux  /t:PublishContainer -c Release

dotnet publish -c Release -p:PublishProfile=DefaultContainer

docker pull hendersonconsultregistry.azurecr.io/hendersonconsultingweb:1.1.6