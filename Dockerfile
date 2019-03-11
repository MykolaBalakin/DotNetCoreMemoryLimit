FROM mcr.microsoft.com/dotnet/core-nightly/sdk:3.0-alpine as builder
 
COPY WebApp /src
RUN dotnet publish /src/WebApp.csproj -c Release -o /app -r alpine-x64

FROM mcr.microsoft.com/dotnet/core-nightly/runtime-deps:3.0-alpine

WORKDIR /app

COPY --from=builder /app .

CMD ["sh", "-c", "cd /app && ./WebApp"]
EXPOSE 80
