module Hash

let hash256 (buffer : byte[]) = 
    use sha256 = System.Security.Cryptography.SHA256.Create()
    Ibasa.Numerics.UInt256(sha256.ComputeHash(buffer))
    
let hash160 (buffer : byte[]) =
    use sha256 = System.Security.Cryptography.SHA256.Create()
    let hash1 = sha256.ComputeHash(buffer)
    use ripemd160 = System.Security.Cryptography.RIPEMD160.Create()
    Ibasa.Numerics.UInt160(ripemd160.ComputeHash(hash1))


let private rotl32 x r = (x <<< r) ||| (x >>> (32 - r))

let murmerHash3 seed (data : byte[]) = 

    let mutable h1 = seed
    let c1 = 0xcc9e2d51
    let c2 = 0x1b873593

    let block_count = data.Length / 4
    let block_end = block_count * 4
    
    for i = -block_count to 0 do 
        let mutable k1 = System.BitConverter.ToInt32(data, block_end + i)

        k1 <- k1 * c1
        k1 <- rotl32 k1 15
        k1 <- k1 * c2

        h1 <- h1 ^^^ k1
        h1 <- rotl32 h1 13
        h1 <- h1 * 0xe6546b64
    
    let mutable k1 = 0

    if data.Length &&& 3 = 3 then
        k1 <- k1 ^^^ (int data.[block_end + 2] <<< 16)
    if data.Length &&& 3 >= 2 then
        k1 <- k1 ^^^ (int data.[block_end + 1] <<< 8)
    if data.Length &&& 3 >= 1 then
        k1 <- k1 ^^^ int data.[block_end]
        k1 <- k1 * c1
        k1 <- rotl32 k1 15
        k1 <- k1 * c2
        h1 <- h1 ^^^ k1
        
    h1 <- h1 ^^^ data.Length
    h1 <- h1 ^^^ (h1 >>> 16)
    h1 <- h1 * 0x85ebca6b
    h1 <- h1 ^^^ (h1 >>> 13)
    h1 <- h1 * 0xc2b2ae35
    h1 <- h1 ^^^ (h1 >>> 16)

    h1