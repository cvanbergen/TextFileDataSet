@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)
 
set version=1.0.0
if not "%PackageVersion%" == "" (
   set version=%PackageVersion%
)

set nuget=
if "%nuget%" == "" (
	set nuget=nuget
)

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild TextFileDataSet\TextFileDataSet.csproj /p:Configuration="%config%"  /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false

%nuget% pack "TextFileDataSet.nuspec" -NoPackageAnalysis -verbosity detailed -Version %version% -p Configuration="%config%"