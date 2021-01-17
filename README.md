# JPath

Navigate JsonElement with a key path.

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
      "menuitems": [
        { "value": "New", "onclick": "CreateNewDoc()" },
        { "value": "Open", "onclick": "OpenDoc()" },
        { "value": "Close", "onclick": "CloseDoc()" }
      ]
    }
  }
}
```

Say if you need to access the second `menuitems`'s `value`, with `JPath` this is a piece of cake:

```fsharp
open System.Text.Json
open JPath

let jdoc = JsonDocument.Parse jsonStr

// result is `New`
let result =
  jdoc.RootElement
  |> JPath.string "menu.popup.menuitems.1.value"
```

One thing to notice is that you must use a number as a key to access an array, like `menuitems.1` in the above example.

Except accessing string, `KeyPathJson` also provides:

```fsharp
namespace JPath
  open System.Text.Json

  module JPath = begin
    val property :
      keyPath:string ->
        jsonEl:JsonElement -> JsonElement
    val bool : (string -> JsonElement -> bool)
    val byte : (string -> JsonElement -> byte)
    val bytesFromBase64 : (string -> JsonElement -> byte [])
    val dateTime : (string -> JsonElement -> System.DateTime)
    val dateTimeOffset :
      (string -> JsonElement -> System.DateTimeOffset)
    val decimal : (string -> JsonElement -> decimal)
    val double : (string -> JsonElement -> float)
    val guid : (string -> JsonElement -> System.Guid)
    val int16 : (string -> JsonElement -> int16)
    val int32 : (string -> JsonElement -> int)
    val int64 : (string -> JsonElement -> int64)
    val rawText : (string -> JsonElement -> string)
    val sbyte : (string -> JsonElement -> sbyte)
    val single : (string -> JsonElement -> float32)
    val float32 : (string -> JsonElement -> float32)
    val string : (string -> JsonElement -> string)
    val uint16 : (string -> JsonElement -> uint16)
    val uint32 : (string -> JsonElement -> uint32)
    val uint64 : (string -> JsonElement -> uint64)
  end
```
