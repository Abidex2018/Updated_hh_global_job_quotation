FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["hh_global_job_quotation.csproj", "./"]
RUN dotnet restore "hh_global_job_quotation.csproj"
COPY . .

RUN dotnet publish "hh_global_job_quotation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "hh_global_job_quotation.dll"]
