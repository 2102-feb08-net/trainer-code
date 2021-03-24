# to use this Dockerfile:
# 1. docker build -t emailapp:2.0 -f EmailApp.buildanddeploy.dockerfile ../5-soa/EmailApp
# 2. docker run --rm -it -p 5000:80 -e ConnectionStrings__EmailDb="(connection string here)" -e AuthRequired="false" emailapp:2.0

# multi-stage build -
#    advantages of building in the consistent environment of docker
#   without the disadvantages of including the sdk & source code etc in the final image.
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /app/src

COPY EmailApp.Business/*.csproj EmailApp.Business/
COPY EmailApp.DataAccess/*.csproj EmailApp.DataAccess/
COPY EmailApp.IntegrationTests/*.csproj EmailApp.IntegrationTests/
COPY EmailApp.UnitTests/*.csproj EmailApp.UnitTests/
COPY EmailApp.WebUI/*.csproj EmailApp.WebUI/
COPY *.sln ./
RUN dotnet restore

# this way wouldn't be great because
# it has to compile the code again every time you start a new container
# CMD ["dotnet", "run", "-p", "/app/src/EmailApp.WebUI"]

COPY . ./

RUN dotnet publish --configuration Release --no-restore -o ../dist

# this won't work, the source code is still in the image
# RUN rm -rf /app/src

# ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/aspnet:5.0

WORKDIR /app

COPY --from=build /app/dist ./

CMD [ "dotnet", "EmailApp.WebUI.dll" ]
