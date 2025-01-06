# Sử dụng hình ảnh cơ bản của .NET
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ThanhThoaiRestaurant.csproj", "./"]
RUN dotnet restore "./ThanhThoaiRestaurant.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet publish "ThanhThoaiRestaurant.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ThanhThoaiRestaurant.dll"]
