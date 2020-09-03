namespace JPath

module JPath =
  open System.Text.Json
  
  let private valueFor' (key:string) (jsonEl:JsonElement) =
    match jsonEl.ValueKind with
    | JsonValueKind.Object -> jsonEl.GetProperty (propertyName = key) |> Ok
    | JsonValueKind.Array -> jsonEl.Item (int (key)) |> Ok
    | t -> sprintf "Expect an Object or Array to access key %A, but actual a %A" key t |> Error

  let rec private loop keys result =
    match keys, result with
    | [], _
    | _, Error _ ->
      result
    | [k], Ok jval ->
      valueFor' k jval
    | k::rest, Ok jval ->
      loop rest <| valueFor' k jval

  let property (keyPath:string) jsonEl =
    let keys = keyPath.Split '.' |> Array.toList
    loop keys (Ok jsonEl)

  let private __ (converter: JsonElement -> 'a) (keyPath:string) (jsonEl:JsonElement)  =
    property keyPath jsonEl
    |> Result.bind (fun jel ->
      try Ok (converter jel)
      with exn -> Error (sprintf "Value for %A fail to convert: %A" keyPath exn.Message)
    )

  let bool = __ (fun jel -> jel.GetBoolean ())

  let byte = __ (fun jel -> jel.GetByte ())

  let bytesFromBase64 = __ (fun jel -> jel.GetBytesFromBase64 ())

  let dateTime = __ (fun jel -> jel.GetDateTime ())

  let dateTimeOffset = __ (fun jel -> jel.GetDateTimeOffset ())

  let decimal = __ (fun jel -> jel.GetDecimal ())

  let double = __ (fun jel -> jel.GetDouble ())

  let guid = __ (fun jel -> jel.GetGuid ())

  let int16 = __ (fun jel -> jel.GetInt16 ())

  let int32 = __ (fun jel -> jel.GetInt32 ())

  let int64 = __ (fun jel -> jel.GetInt64 ())

  let rawText = __ (fun jel -> jel.GetRawText ())

  let sbyte = __ (fun jel -> jel.GetSByte ())

  let single = __ (fun jel -> jel.GetSingle ())

  let float32 = single

  let string = __ (fun jel -> jel.GetString ())

  let uint16 = __ (fun jel -> jel.GetUInt16 ())

  let uint32 = __ (fun jel -> jel.GetUInt32 ())

  let uint64 = __ (fun jel -> jel.GetUInt64 ())