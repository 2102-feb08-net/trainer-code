# to use this Dockerfile:
# 1. rm -rf ../5-soa/EmailApp/publish
# 2. dotnet publish ../5-soa/EmailApp/EmailApp.WebUI/ -r linux-x64 --no-self-contained -o ../5-soa/EmailApp/publish
# 3. docker build -t emailapp:1.0 -f EmailApp.deploy.dockerfile ../5-soa/EmailApp/publish
# 4. docker run --rm -it -p 5000:80 emailapp:1.0

FROM mcr.microsoft.com/dotnet/aspnet:5.0

COPY * /app/

CMD [ "dotnet", "/app/EmailApp.WebUI.dll" ]
