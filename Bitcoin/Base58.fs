module Base58

let private alphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz" |> Seq.toArray
let private encoding = Ibasa.Numerics.Encoding(alphabet)

let encode (bytes : byte[]) = 
    encoding.Encode(bytes)

let decode (code : string) =
    encoding.Decode(code)