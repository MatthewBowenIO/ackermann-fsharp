open System

let rec ack x y = if x = 0 then (y + 1) elif y = 0 then ack (x-1) 1 else ack (x-1) (ack x (y-1))

let acks = Async.Parallel [ for i in 1..3 -> async { return ack i i+1 } ] |> Async.RunSynchronously

[<EntryPoint>]
let main argv = 
    let sw = Diagnostics.Stopwatch.StartNew()
    let ackReturns = acks
    sw.Stop()

    printfn "%f" sw.Elapsed.TotalMilliseconds
    for i in ackReturns do printfn "%A" i
    0