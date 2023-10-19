Invoke-WebRequest -Uri "https://download.visualstudio.microsoft.com/download/pr/a9f212b6-c33b-45e6-9c78-785c29b8c743/3cf0e2811c9b056f37b3b0d15b4c24b3/dotnet-sdk-5.0.404-win-x64.exe" -OutFile dotnet-sdk-installer.exe
Start-Process -Wait dotnet-sdk-installer.exe

$NugetConfigContent = @"
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="MyBaGetRepo" value="http://localhost:5000/v3/index.json" />
  </packageSources>
</configuration>
"@

$NugetConfigContent | Out-File "$env:APPDATA\NuGet\NuGet.Config"
dotnet tool install --global --add-source http://localhost:5000/v3/index.json lab-work-4-sy --version 1.0.0
