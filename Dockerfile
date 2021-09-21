FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY *.sln .
COPY Todo.Api/*.csproj ./Todo.Api/
COPY Todo.Domain/*.csproj ./Todo.Domain/
COPY Todo.Infra/*.csproj ./Todo.Infra/

RUN dotnet restore "Todo.Api/Todo.Api.csproj"

COPY Todo.Api/. ./Todo.Api/
COPY Todo.Domain/. ./Todo.Domain/
COPY Todo.Infra/. ./Todo.Infra/

WORKDIR /app
RUN dotnet publish Todo.Api/Todo.Api.csproj -c Release -o ./Todo.Api/out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app

COPY --from=build-env /app/Todo.Api/out .
ENTRYPOINT [ "dotnet", "Todo.Api.dll" ]