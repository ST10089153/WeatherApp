FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7097

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WeatherNewsAPI.csproj", "./"]
RUN dotnet restore "./WeatherNewsAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "WeatherNewsAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WeatherNewsAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# 👉 Bind the app to port 7097
ENV ASPNETCORE_URLS=http://+:7097

ENTRYPOINT ["dotnet", "WeatherNewsAPI.dll"]