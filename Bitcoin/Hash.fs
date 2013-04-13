module Hash

let hash256 (buffer : byte[]) = 
    use sha256 = System.Security.Cryptography.SHA256.Create()
    Ibasa.Numerics.UInt256(sha256.ComputeHash(buffer))
    
let hash160 (buffer : byte[]) =
    use sha256 = System.Security.Cryptography.SHA256.Create()
    let hash1 = sha256.ComputeHash(buffer)
    use ripemd160 = System.Security.Cryptography.RIPEMD160.Create()
    Ibasa.Numerics.UInt160(ripemd160.ComputeHash(hash1))