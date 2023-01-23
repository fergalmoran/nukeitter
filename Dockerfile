FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["nukitter-web.csproj", "./"]
RUN dotnet restore "nukitter-web.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "nukitter-web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "nukitter-web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "nukitter-web.dll"]
