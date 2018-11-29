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
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild TextFileDataSet_v30\TextFileDataSet_v30.csproj /p:Configuration="%config%"  /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild TextFileDataSet_v35\TextFileDataSet_v35.csproj /p:Configuration="%config%"  /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild TextFileDataSet_v40\TextFileDataSet_v40.csproj /p:Configuration="%config%"  /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild TextFileDataSet_v47\TextFileDataSet_v47.csproj /p:Configuration="%config%"  /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=diag /nr:false

%nuget% pack "TextFileDataSet.nuspec" -NoPackageAnalysis -verbosity detailed -Version %version% -p Configuration="%config%"