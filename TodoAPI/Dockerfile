FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app

EXPOSE 44361
EXPOSE 3001

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src

COPY ["TodoAPI/TodoAPI.csproj", "TodoAPI/"]
COPY ["Todo.BusinessLogic/Todo.BusinessLogic.csproj","Todo.BusinessLogic/"]
COPY ["Todo.DataAccess/Todo.DataAccess.csproj","Todo.DataAccess/"]
RUN dotnet restore "TodoAPI/TodoAPI.csproj"

COPY . .

WORKDIR "/src/TodoAPI"
RUN dotnet build "TodoAPI.csproj" -c $BUILD_CONFIGURATION -o "/app/build"

FROM build AS publish
ARG BUILD_CONFIGURATION=Develepment
RUN dotnet publish "TodoAPI.csproj" -c $BUILD_CONFIGURATION -o "/app/publish" /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "TodoAPI.dll"]