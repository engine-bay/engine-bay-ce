FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS base

ARG VERSION=0.0.0

WORKDIR /tmp/build

# copy csproj and restore as distinct layers
COPY ./EngineBay.CommunityEdition.csproj ./EngineBay.CommunityEdition.csproj
RUN dotnet restore ./EngineBay.CommunityEdition.csproj

# copy everything else and build
COPY . .
RUN dotnet publish ./EngineBay.CommunityEdition.csproj --nologo --runtime linux-musl-x64 --self-contained --configuration Release -o /tmp/build/out /p:Version=$VERSION

# build runtime image
FROM mcr.microsoft.com/dotnet/runtime-deps:7.0-alpine
COPY --from=base /tmp/build/out /usr/local/sbin

EXPOSE 5050
ENV ASPNETCORE_URLS=http://*:5050
RUN mkdir /seed-data
ENTRYPOINT ["EngineBay.CommunityEdition"]