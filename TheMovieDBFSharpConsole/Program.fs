open System
open SharedUtils.Pipes
open TheMovieDBFSharp
open TheMovieDBFSharp.Main
open System.IO

printfn "Hello from TheMovieDBFSharp"
printfn $"apikey: {Main.apiKey}"
printf "Search movie: "
let movie = "back to the future" //Console.ReadLine()
printfn $"Getting results for '{movie}'..."
let mr = GetMovie movie
printfn $"Found: {mr.title}"
printfn $"{mr.overview}"
let poster = MatchResult.GetPosterFullUrl mr "w200"
printfn $"{poster}"
let genres =
    mr.genre_ids 
    |> Seq.map (fun g -> 
        let genre = 
            IdGenreMap.TryFind g
            |> function
            | Some y -> y
            | None -> ""
        $"#{genre}"
    )
    |> String.concat " "
printfn $"{genres}"
printfn ""
printfn "Finding by id..."
let fr = GetMovieById mr.id
let collection = 
    if fr.BelongsToCollection.IsSome then fr.BelongsToCollection.Value.Name else ""
printfn $"Collection: {collection}"
printfn $"IMDB ID: {fr.ImdbId}"

printfn "Cast:"
if fr.Credits.IsSome then
    let castMembers = 
        fr.Credits.Value.Cast
        //|> Seq.sortBy (fun x -> x.Order)
        |> Seq.map (fun cm -> cm.Name)
        |> String.concat ", "
    printfn $"{castMembers}"
else printfn "No cast found."
printfn "Done."
Console.ReadLine() |> ignore