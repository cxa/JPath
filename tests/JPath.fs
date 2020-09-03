module Tests

open System
open System.Text.Json
open Xunit
open JPath

let requal (r1: Result<'a, _>) (r2: Result<'a, _>) =
  match r1, r2 with
  | Ok o1, Ok o2 -> Assert.True ((o1 = o2))
  | _ -> printfn "Exn: %A" r1; Assert.False (true)

[<Fact>]
let ``Test bool for keypath`` () =
  let t = true
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": %b}}""" t)
  jdoc.RootElement
  |> JPath.bool "a.b"
  |> requal (Ok t)

[<Fact>]
let ``Test bool for keypath with root array`` () =
  let t = true
  let jdoc = JsonDocument.Parse (sprintf """[%b]""" t)
  jdoc.RootElement
  |> JPath.bool "0"
  |> requal (Ok t)

[<Fact>]
let ``Test byte for keypath`` () =
  let c = byte 'a'
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": %i}}""" c)
  jdoc.RootElement
  |> JPath.byte "a.b"
  |> requal (Ok c)

[<Fact>]
let ``Test sbyte for keypath`` () =
  let sb = SByte.MaxValue
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": [%i]}}""" sb)
  jdoc.RootElement
  |> JPath.sbyte "a.b.0"
  |> requal (Ok sb)

[<Fact>]
let ``Test int16 for keypath`` () =
  let i = Int16.MaxValue
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": %i}}""" i)
  jdoc.RootElement
  |> JPath.int16 "a.b"
  |> requal (Ok i)

[<Fact>]
let ``Test uint16 for keypath`` () =
  let ui = UInt16.MaxValue
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": %i}}""" ui)
  jdoc.RootElement
  |> JPath.uint16 "a.b"
  |> requal (Ok ui)

[<Fact>]
let ``Test int32 for keypath`` () =
  let i = Int32.MaxValue
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": %i}}""" i)
  jdoc.RootElement
  |> JPath.int32 "a.b"
  |> requal (Ok i)

[<Fact>]
let ``Test uint32 for keypath`` () =
  let ui = UInt32.MaxValue
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": %i}}""" ui)
  jdoc.RootElement
  |> JPath.uint32 "a.b"
  |> requal (Ok ui)

[<Fact>]
let ``Test int64 for keypath`` () =
  let i = Int64.MaxValue
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": %i}}""" i)
  jdoc.RootElement
  |> JPath.int64 "a.b"
  |> requal (Ok i)

[<Fact>]
let ``Test uint64 for keypath`` () =
  let ui = UInt64.MaxValue
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": %i}}""" ui)
  jdoc.RootElement
  |> JPath.uint64 "a.b"
  |> requal (Ok ui)

[<Fact>]
let ``Test double for keypath`` () =
  let d = 3.14
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": %f}}""" d)
  jdoc.RootElement
  |> JPath.double "a.b"
  |> requal (Ok d)

[<Fact>]
let ``Test float for keypath`` () =
  let f = 3.14f
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": %f}}""" f)
  jdoc.RootElement
  |> JPath.float32 "a.b"
  |> requal (Ok f)

[<Fact>]
let ``Test decimal for keypath`` () =
  let d = 0.7833m
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": %M}}""" d)
  jdoc.RootElement
  |> JPath.decimal "a.b"
  |> requal (Ok d)

[<Fact>]
let ``Test string for keypath`` () =
  let s = "Test string for keypath"
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": "%s"}}""" s)
  jdoc.RootElement
  |> JPath.string "a.b"
  |> requal (Ok s)

[<Fact>]
let ``Test date time for keypath`` () =
  let dt = DateTime.Now
  let jdoc = JsonDocument.Parse (sprintf """{"key": %s}""" (JsonSerializer.Serialize dt))
  jdoc.RootElement
  |> JPath.dateTime "key"
  |> requal (Ok dt)

[<Fact>]
let ``Test date time offset for keypath`` () =
  let dto = DateTimeOffset (DateTime.Now)
  let jdoc = JsonDocument.Parse (sprintf """{"key": %s}""" (JsonSerializer.Serialize dto))
  jdoc.RootElement
  |> JPath.dateTimeOffset "key"
  |> requal (Ok dto)

[<Fact>]
let ``Test guid for keypath`` () =
  let guid = Guid.NewGuid ()
  let jdoc = JsonDocument.Parse (sprintf """{"key": %s}""" (JsonSerializer.Serialize guid))
  jdoc.RootElement
  |> JPath.guid "key"
  |> requal (Ok guid)
