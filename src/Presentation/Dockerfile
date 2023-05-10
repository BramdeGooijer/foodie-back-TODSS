FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
ENV ASPNETCORE_URLS=http://+:5000;https://+:5001
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG NUGET_PASSWORD
WORKDIR /app/code
COPY ./src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./src/${file%.*}/ && mv $file ./src/${file%.*}/; done
RUN dotnet restore "src/Presentation/Presentation.csproj"
COPY . .
RUN dotnet publish "src/Presentation/Presentation.csproj" --no-restore -c Release -o /app/publish
RUN dotnet dev-certs https

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY --from=build /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/
ENTRYPOINT ["dotnet", "Presentation.dll"]
