module Base58

let private alphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz" |> Seq.toArray
let private encoding = Ibasa.Text.BaseEncoding(alphabet)

let encode (bytes : byte[]) = 
    encoding.GetString(bytes)

let decode (code : string) =
    encoding.GetBytes(code)