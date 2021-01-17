module Tests

open System
open System.Text.Json
open Xunit
open FsUnit.Xunit
open JPath

[<Fact>]
let ``Test bool for keypath`` () =
  let t = true
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": %b}}""" t)
  jdoc.RootElement
  |> JPath.bool "a.b"
  |> should equal t

[<Fact>]
let ``Test bool for keypath with exn`` () =
  let jdoc = JsonDocument.Parse """{ "a" : {"b": 1}}"""
  (fun () -> jdoc.RootElement |> JPath.bool "a.b" |> ignore)
  |> should throw typeof<InvalidOperationException>

[<Fact>]
let ``Test bool for keypath with root array`` () =
  let t = true
  let jdoc = JsonDocument.Parse (sprintf """[%b]""" t)
  jdoc.RootElement
  |> JPath.bool "0"
  |> should equal t

[<Fact>]
let ``Test byte for keypath`` () =
  let c = byte 'a'
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": %i}}""" c)
  jdoc.RootElement
  |> JPath.byte "a.b"
  |> should equal c

[<Fact>]
let ``Test sbyte for keypath`` () =
  let sb = SByte.MaxValue
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": [%i]}}""" sb)
  jdoc.RootElement
  |> JPath.sbyte "a.b.0"
  |> should equal sb

[<Fact>]
let ``Test int16 for keypath`` () =
  let i = Int16.MaxValue
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": %i}}""" i)
  jdoc.RootElement
  |> JPath.int16 "a.b"
  |> should equal i

[<Fact>]
let ``Test uint16 for keypath`` () =
  let ui = UInt16.MaxValue
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": %i}}""" ui)
  jdoc.RootElement
  |> JPath.uint16 "a.b"
  |> should equal ui

[<Fact>]
let ``Test int32 for keypath`` () =
  let i = Int32.MaxValue
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": %i}}""" i)
  jdoc.RootElement
  |> JPath.int32 "a.b"
  |> should equal i

[<Fact>]
let ``Test uint32 for keypath`` () =
  let ui = UInt32.MaxValue
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": %i}}""" ui)
  jdoc.RootElement
  |> JPath.uint32 "a.b"
  |> should equal ui

[<Fact>]
let ``Test int64 for keypath`` () =
  let i = Int64.MaxValue
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": %i}}""" i)
  jdoc.RootElement
  |> JPath.int64 "a.b"
  |> should equal i

[<Fact>]
let ``Test uint64 for keypath`` () =
  let ui = UInt64.MaxValue
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": %i}}""" ui)
  jdoc.RootElement
  |> JPath.uint64 "a.b"
  |> should equal ui

[<Fact>]
let ``Test double for keypath`` () =
  let d = 3.14
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": %f}}""" d)
  jdoc.RootElement
  |> JPath.double "a.b"
  |> should equal d

[<Fact>]
let ``Test float for keypath`` () =
  let f = 3.14f
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": %f}}""" f)
  jdoc.RootElement
  |> JPath.float32 "a.b"
  |> should equal f

[<Fact>]
let ``Test decimal for keypath`` () =
  let d = 0.7833m
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": %M}}""" d)
  jdoc.RootElement
  |> JPath.decimal "a.b"
  |> should equal d

[<Fact>]
let ``Test string for keypath`` () =
  let s = "Test string for keypath"
  let jdoc = JsonDocument.Parse (sprintf """{ "a": {"b": "%s"}}""" s)
  jdoc.RootElement
  |> JPath.string "a.b"
  |> should equal s

[<Fact>]
let ``Test date time for keypath`` () =
  let dt = DateTime.Now
  let jdoc = JsonDocument.Parse (sprintf """{"key": %s}""" (JsonSerializer.Serialize dt))
  jdoc.RootElement
  |> JPath.dateTime "key"
  |> should equal dt

[<Fact>]
let ``Test date time offset for keypath`` () =
  let dto = DateTimeOffset (DateTime.Now)
  let jdoc = JsonDocument.Parse (sprintf """{"key": %s}""" (JsonSerializer.Serialize dto))
  jdoc.RootElement
  |> JPath.dateTimeOffset "key"
  |> should equal dto

[<Fact>]
let ``Test guid for keypath`` () =
  let guid = Guid.NewGuid ()
  let jdoc = JsonDocument.Parse (sprintf """{"key": %s}""" (JsonSerializer.Serialize guid))
  jdoc.RootElement
  |> JPath.guid "key"
  |> should equal guid
