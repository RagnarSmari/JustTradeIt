FROM mcr.microsoft.com/dotnet/sdk:5.0

LABEL maintainer="Ragnar Smári"

ENV ASPNETCORE_URLS=http://0.0.0.0:5000


COPY . /app
WORKDIR /app/JustTradeIt.Software.API
RUN dotnet restore
ENTRYPOINT [ "dotnet", "run" ]
