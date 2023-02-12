#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HH_Global_Job_Quotation/hh_global_job_quotation.csproj", "HH_Global_Job_Quotation/"]
RUN dotnet restore "HH_Global_Job_Quotation/hh_global_job_quotation.csproj"
COPY . .
WORKDIR "/src/HH_Global_Job_Quotation"
RUN dotnet build "hh_global_job_quotation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "hh_global_job_quotation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "hh_global_job_quotation.dll"]