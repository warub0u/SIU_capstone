FROM mcr.microsoft.com/dotnet/sdk:6.0 as buildStage 
#can be any name without spaces
WORKDIR /app
#indicates from where it will deploy your app
COPY *.csproj ./
#at the same place where the docker file is at; cs.proj contains all the dpeendencies of the project
RUN dotnet restore
COPY . ./
#copy all the files from the current location
RUN dotnet publish -c Release -o out 
#the application is compiled here in sdk; publish will build the project and put the released versiob of the app at the out folder within the container in docker

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /deploy
COPY --from=buildStage /app/out .
# you can use any name in place of buildStage. copy this from the current location represented by .
ENTRYPOINT ["dotnet", "LoginAPI.dll"] 
#this is the execution; insert the name of the app from VS
#in this case "DockerMVCDemo.dll"