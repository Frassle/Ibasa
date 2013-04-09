// Learn more about F# at http://fsharp.net. See the 'F# Tutorial' project
// for more guidance on F# programming.

#r @"D:\Visual Studio\Projects\Ibasa\Numerics\bin\Debug\Ibasa.Numerics.dll"
#load "Base58.fs"
open Base58

// Define your library scripting code here

let code = Base58.encode [| 58uy; 4uy |]
let data = Base58.decode code

printfn "%A" code
printfn "%A" data