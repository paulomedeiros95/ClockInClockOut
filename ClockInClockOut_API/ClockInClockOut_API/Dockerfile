#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ClockInClockOut_API/ClockInClockOut_API.csproj", "ClockInClockOut_API/"]
RUN dotnet restore "ClockInClockOut_API/ClockInClockOut_API.csproj"
COPY . .
WORKDIR "/src/ClockInClockOut_API"
RUN dotnet build "ClockInClockOut_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClockInClockOut_API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClockInClockOut_API.dll"]