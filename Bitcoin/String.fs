module String
    open System.Globalization
    let rev s =
        seq {
            let enum = StringInfo.GetTextElementEnumerator(s)
            while enum.MoveNext() do
                yield enum.GetTextElement()
        }
        |> Array.ofSeq
        |> Array.rev
        |> String.concat ""