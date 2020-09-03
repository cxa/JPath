# JPath

Navigate System.Text.Json.JsonElement with a key path.

(For navigating `System.Json`, use [KeyPathJson](https://github.com/cxa/KeyPathJson))

## Install

Simply drop `JPath.fsproj` or `JPath.fs` to your project, or search `JPath` on NuGet.

## Example

Given a JSON like this:

```json
{
  "menu": {
    "id": "file",
    "value": "File",
    "popup": {
      "menuitem": [
        { "value": "New", "onclick": "CreateNewDoc()" },
        { "value": "Open", "onclick": "OpenDoc()" },
        { "value": "Close", "onclick": "CloseDoc()" }
      ]
    }
  }
}
```

Say you need to access the second `menuitem`'s `value`, with `JPath` this is a piece of cake:

```fsharp
open System.Text.Json
open JPath

let jdoc = JsonDocument.Parse jsonStr

// result is a Result<string, string>
let result =
  jdoc.RootElement
  |> JPath.string "menu.popup.menuitem.1.value"

```

One thing to aware is that you must use a number as a key to access array, like `menuitem.1` in the above example.

Except accessing string, `KeyPathJson` also provides:

```fsharp
namespace JPath
  module JPath = begin
    open System.Text.Json

    val property : keyPath:string -> jsonEl:JsonElement -> Result<JsonElement,string>
    val bool : keyPath:string -> jsonEl:JsonElement -> Result<bool,string>
    val byte : keyPath:string -> jsonEl:JsonElement -> Result<byte,string>
    val bytesFromBase64 :
      keyPath:string -> jsonEl:JsonElement -> Result<byte [],string>
    val dateTime :
      keyPath:string -> jsonEl:JsonElement -> Result<System.DateTime,string>
    val dateTimeOffset :
      keyPath:string -> jsonEl:JsonElement ->
         Result<System.DateTimeOffset,string>
    val decimal :
      keyPath:string -> jsonEl:JsonElement -> Result<decimal,string>
    val double :
      keyPath:string -> jsonEl:JsonElement -> Result<float,string>
    val guid :
      keyPath:string -> jsonEl:JsonElement -> Result<System.Guid,string>
    val int16 : keyPath:string -> jsonEl:JsonElement -> Result<int16,string>
    val int32 : keyPath:string -> jsonEl:JsonElement -> Result<int,string>
    val int64 : keyPath:string -> jsonEl:JsonElement -> Result<int64,string>
    val rawText :
      keyPath:string -> jsonEl:JsonElement -> Result<string,string>
    val sbyte : keyPath:string -> jsonEl:JsonElement -> Result<sbyte,string>
    val single :
      keyPath:string -> jsonEl:JsonElement -> Result<float32,string>
    val float32 :
      keyPath:string -> jsonEl:JsonElement -> Result<float32,string>
    val string :
      keyPath:string -> jsonEl:JsonElement -> Result<string,string>
    val uint16 :
      keyPath:string -> jsonEl:JsonElement -> Result<uint16,string>
    val uint32 :
      keyPath:string -> jsonEl:JsonElement -> Result<uint32,string>
    val uint64 :
      keyPath:string -> jsonEl:JsonElement -> Result<uint64,string>
  end
```