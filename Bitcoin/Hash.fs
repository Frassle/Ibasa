module Hash

let hash256 (buffer : byte[]) = 
    let sha256 = Ibasa.Cryptography.SHA256()
    Ibasa.Numerics.UInt256(sha256.ComputeHash(Ibasa.ArraySegment(buffer)))
    
let hash160 (buffer : byte[]) =
    let sha256 = Ibasa.Cryptography.SHA256()
    let hash1 = sha256.ComputeHash(Ibasa.ArraySegment(buffer))
    use ripemd160 = System.Security.Cryptography.RIPEMD160.Create()
    Ibasa.Numerics.UInt160(ripemd160.ComputeHash(hash1))