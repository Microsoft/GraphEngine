﻿git submodule init
git submodule update --recursive
echo "Setup environment"

foreach($_ in cmd /c " $PSScriptRoot\..\..\..\tools\setenv.bat > `"nul`" 2>&1 && SET") {
	if ($_ -match '^([^=]+)=(.*)') {
		[System.Environment]::SetEnvironmentVariable($matches[1], $matches[2])
	}
}

$REPO_ROOT = $env:REPO_ROOT
$SOL_ROOT  = "$REPO_ROOT\src\Modules\Trinity.FFI"

echo "Cleanning caches"
if ($true) {
    Invoke-Command -ScriptBlock {
        $package_loc = (& $env:NUGET_EXE locals global-packages -list)
        $package_loc = $package_loc.Substring(17)
        Get-ChildItem $package_loc -Filter "graphengine*" | Remove-Item -Recurse -Force -ErrorAction Ignore
        Remove-Item -Recurse -Force $REPO_ROOT\bin\lib.win-amd64-3.6 -ErrorAction Ignore
        Remove-Item -Recurse -Force $REPO_ROOT\bin\temp.win-amd64-3.6 -ErrorAction Ignore
        Remove-Item -Recurse -Force $REPO_ROOT\bin\trinity_ffi.*
    } -ErrorAction Ignore
}

function Build($proj, $action, $config)
{
    $cmd = @'
 & dotnet.exe $action /p:Configuration=$config "$proj"
'@
    echo "Building $proj with action $action, config=$config..."
    Invoke-Expression $cmd
}

function MSBuild($proj)
{
    $cmd = @'
 & $env:MSBUILD_EXE "/p:Configuration=Release;Platform=x64" "$proj"
'@
    Invoke-Expression $cmd
}

Build "$SOL_ROOT\pythonnet\src\runtime\Python.Runtime.15.csproj" -action restore -config ReleaseWinPY3
Build "$SOL_ROOT\pythonnet\src\runtime\Python.Runtime.15.csproj" -action build -config ReleaseWinPY3
Build "$SOL_ROOT\pythonnet\src\runtime\Python.Runtime.15.csproj" -action pack -config ReleaseWinPY3
Move-Item -Path "$SOL_ROOT\pythonnet\src\runtime\bin\Python.Runtime.2.4.0.nupkg" -Destination $env:REPO_ROOT\bin\coreclr -Force

MSBuild "$SOL_ROOT\Trinity.FFI.Native\Trinity.FFI.Native.vcxproj"

Build "$SOL_ROOT\Trinity.FFI\Trinity.FFI.csproj" -action restore -config Release
Build "$SOL_ROOT\Trinity.FFI\Trinity.FFI.csproj" -action build -config Release
Build "$SOL_ROOT\Trinity.FFI\Trinity.FFI.csproj" -action pack -config Release

Build "$SOL_ROOT\Trinity.FFI.UnitTests\Trinity.FFI.UnitTests.csproj" -action restore -config Release
Build "$SOL_ROOT\Trinity.FFI.UnitTests\Trinity.FFI.UnitTests.csproj" -action build -config Release

Build "$SOL_ROOT\Trinity.FFI.Python\Trinity.FFI.Python.csproj" -action restore -config Release
Build "$SOL_ROOT\Trinity.FFI.Python\Trinity.FFI.Python.csproj" -action build -config Release
Build "$SOL_ROOT\Trinity.FFI.Python\Trinity.FFI.Python.csproj" -action pack -config Release

Build "$SOL_ROOT\Trinity.FFI.Python.UnitTests\Trinity.FFI.Python.UnitTests.csproj" -action restore -config Release
Build "$SOL_ROOT\Trinity.FFI.Python.UnitTests\Trinity.FFI.Python.UnitTests.csproj" -action build -config Release

Build "$SOL_ROOT\Trinity.FFI.Playground\Trinity.FFI.Playground.csproj" -action restore -config Release
Build "$SOL_ROOT\Trinity.FFI.Playground\Trinity.FFI.Playground.csproj" -action build -config Release