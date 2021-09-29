FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY bin/Release/net5.0/publish/ App/
MAINTAINER Marco Antonio
ENV DOTNET_EnableDiagnostics=0
ENV RBHOST=rabbitmq
ENV RBUSER=admin
ENV RBPASS=123456
ENV RBQUEUE=guid
WORKDIR /App
ENTRYPOINT ["dotnet", "SendRabbitMq.dll"]