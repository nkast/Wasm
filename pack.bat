dotnet pack Wasm.Dom\Wasm.Dom.csproj /p:Configuration=Release -o bin\publish
dotnet pack Wasm.XHR\Wasm.XHR.csproj /p:Configuration=Release -o bin\publish
dotnet pack Wasm.Canvas\Wasm.Canvas.csproj /p:Configuration=Release -o bin\publish

"C:\Program Files (x86)\nuget\nuget.exe" add Wasm.Dom\bin\release\nkast.Wasm.Dom.6.0.0.nupkg -Source "C:\Users\usename\.nuget\localPackages"
"C:\Program Files (x86)\nuget\nuget.exe" add Wasm.XHR\bin\release\nkast.Wasm.XHR.6.0.0.nupkg -Source "C:\Users\usename\.nuget\localPackages"
"C:\Program Files (x86)\nuget\nuget.exe" add Wasm.Canvas\bin\release\nkast.Wasm.Canvas.6.0.0.nupkg -Source "C:\Users\usename\.nuget\localPackages"
