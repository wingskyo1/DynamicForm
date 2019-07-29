#FROM node:lts-alpine as vue
#WORKDIR /app/spa
#COPY ClientApp/package*.json ./
#RUN npm install
#COPY ClientApp/. .
#RUN npm run build

# Stage 1
FROM mcr.microsoft.com/dotnet/core/sdk AS builder
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.csproj .
RUN dotnet restore

# set up node
ENV NODE_VERSION 8.9.4
ENV NODE_DOWNLOAD_SHA 21fb4690e349f82d708ae766def01d7fec1b085ce1f5ab30d9bda8ee126ca8fc
RUN curl -SL "https://nodejs.org/dist/v${NODE_VERSION}/node-v${NODE_VERSION}-linux-x64.tar.gz" --output nodejs.tar.gz \
    && echo "$NODE_DOWNLOAD_SHA nodejs.tar.gz" | sha256sum -c - \
    && tar -xzf "nodejs.tar.gz" -C /usr/local --strip-components=1 \
    && rm nodejs.tar.gz \
    && ln -s /usr/local/bin/node /usr/local/bin/nodejs

# copy and build everything else
COPY . .
RUN dotnet publish -c Release -o /app/

# Stage 2
FROM mcr.microsoft.com/dotnet/core/aspnet
WORKDIR /app
COPY --from=builder /app .
ENTRYPOINT ["dotnet", "web.dll"]

# how to share container/image?
# use docker container export as tarball -> docker import/load in other host
# private registry
# SSL - http://parsstudent.com/asp-net-core-2-1-application-in-docker-with-https-enabled/