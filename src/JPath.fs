namespace JPath

module JPath =
  open System.Text.Json

  let private valueFor' (key: string) (jsonEl: JsonElement) =
    match jsonEl.ValueKind with
    | JsonValueKind.Object -> jsonEl.GetProperty(propertyName = key)
    | JsonValueKind.Array -> jsonEl.Item(int (key))
    | _ ->
      raise (
        System.InvalidOperationException(
          $"The `ValueKind` of {jsonEl} is neither Object nor Array."
        )
      )

  let rec private loop keys jel =
    match keys with
    | [] -> jel
    | k :: rest -> loop rest (valueFor' k jel)

  let property (keyPath: string) jsonEl =
    let keys = keyPath.Split '.' |> Array.toList
    loop keys jsonEl

  let private __ (converter: JsonElement -> 'a) (keyPath: string) (jsonEl: JsonElement) =
    property keyPath jsonEl |> converter

  let bool = __ (fun jel -> jel.GetBoolean())

  let byte = __ (fun jel -> jel.GetByte())

  let bytesFromBase64 = __ (fun jel -> jel.GetBytesFromBase64())

  let dateTime = __ (fun jel -> jel.GetDateTime())

  let dateTimeOffset = __ (fun jel -> jel.GetDateTimeOffset())

  let decimal = __ (fun jel -> jel.GetDecimal())

  let double = __ (fun jel -> jel.GetDouble())

  let guid = __ (fun jel -> jel.GetGuid())

  let int16 = __ (fun jel -> jel.GetInt16())

  let int32 = __ (fun jel -> jel.GetInt32())

  let int64 = __ (fun jel -> jel.GetInt64())

  let rawText = __ (fun jel -> jel.GetRawText())

  let sbyte = __ (fun jel -> jel.GetSByte())

  let single = __ (fun jel -> jel.GetSingle())

  let float32 = single

  let string = __ (fun jel -> jel.GetString())

  let uint16 = __ (fun jel -> jel.GetUInt16())

  let uint32 = __ (fun jel -> jel.GetUInt32())

  let uint64 = __ (fun jel -> jel.GetUInt64())
