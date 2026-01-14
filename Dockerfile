FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["BelanjaYuk.Client.csproj", "./"]
RUN dotnet restore "BelanjaYuk.Client.csproj"
COPY . .
RUN dotnet build "BelanjaYuk.Client.csproj" -c Release -o /app/build
RUN dotnet publish "BelanjaYuk.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=build /app/publish/wwwroot .
EXPOSE 80
