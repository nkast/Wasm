dotnet pack Wasm.JSInterop\Wasm.JSInterop.csproj /p:Configuration=Release -o bin\publish
dotnet pack Wasm.Dom\Wasm.Dom.csproj /p:Configuration=Release -o bin\publish
dotnet pack Wasm.XHR\Wasm.XHR.csproj /p:Configuration=Release -o bin\publish
dotnet pack Wasm.Clipboard\Wasm.Clipboard.csproj /p:Configuration=Release -o bin\publish
dotnet pack Wasm.Canvas\Wasm.Canvas.csproj /p:Configuration=Release -o bin\publish
dotnet pack Wasm.XR\Wasm.XR.csproj /p:Configuration=Release -o bin\publish
dotnet pack Wasm.Audio\Wasm.Audio.csproj /p:Configuration=Release -o bin\publish

set username=username

"C:\Program Files (x86)\nuget\nuget.exe" add bin\publish\nkast.Wasm.JSInterop.8.0.11.nupkg -Source "C:\Users\%username%\.nuget\localPackages"
"C:\Program Files (x86)\nuget\nuget.exe" add bin\publish\nkast.Wasm.Dom.8.0.11.nupkg -Source "C:\Users\%username%\.nuget\localPackages"
"C:\Program Files (x86)\nuget\nuget.exe" add bin\publish\nkast.Wasm.XHR.8.0.11.nupkg -Source "C:\Users\%username%\.nuget\localPackages"
"C:\Program Files (x86)\nuget\nuget.exe" add bin\publish\nkast.Wasm.Clipboard.8.0.11.nupkg -Source "C:\Users\%username%\.nuget\localPackages"
"C:\Program Files (x86)\nuget\nuget.exe" add bin\publish\nkast.Wasm.Canvas.8.0.11.nupkg -Source "C:\Users\%username%\.nuget\localPackages"
"C:\Program Files (x86)\nuget\nuget.exe" add bin\publish\nkast.Wasm.XR.8.0.11.nupkg -Source "C:\Users\%username%\.nuget\localPackages"
"C:\Program Files (x86)\nuget\nuget.exe" add bin\publish\nkast.Wasm.Audio.8.0.11.nupkg -Source "C:\Users\%username%\.nuget\localPackages"
