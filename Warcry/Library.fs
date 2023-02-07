namespace Warcry

open System.Speech.Recognition
open Warcry.Order

module Core =

    let rec grammar = function
        | Word word -> GrammarBuilder(word)
        | Sequence orders ->
            let builder = GrammarBuilder()
            List.iter (fun order -> builder.Append(grammar order)) orders
            builder
        | Choice choices -> GrammarBuilder(Choices(List.map grammar choices |> Array.ofList))
