FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /app
RUN apt-get update -yq && apt-get upgrade -yq && apt-get install -yq curl git nano
RUN curl -sL https://deb.nodesource.com/setup_16.x | bash - && apt-get install -yq nodejs build-essential

COPY ./ .
RUN dotnet restore

WORKDIR /app/BlazorDiffusion
RUN dotnet publish -c release -o /out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS runtime
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "BlazorDiffusion.dll"]
