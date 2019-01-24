FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["NetPeakTestAssigment/NetPeakTestAssigment.csproj", "NetPeakTestAssigment/"]
RUN dotnet restore "NetPeakTestAssigment/NetPeakTestAssigment.csproj"
COPY . .
WORKDIR "/src/NetPeakTestAssigment"
RUN dotnet build "NetPeakTestAssigment.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "NetPeakTestAssigment.csproj" -c Release -o /app

#RUN ASPNETCORE_ENVIRONMENT="Development"

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NetPeakTestAssigment.dll"]
