namespace Warcry

open System.Speech.Recognition

module Warcry =
    let set = Set.ofList

    type Order =
        | Word of string
        | Sequence of Order list
        | Choice of Order list

    let position =
        Choice [
            Sequence [
                Word "move"
                Choice [
                    Word "here"
                ]
            ]
            Word "follow me"
            Word "charge"
            Word "advance"
            Word "fall back"
            Choice [
                Word "halt"
                Word "stop"
            ]
            Word "retreat"
            Word "cancel that"
        ])

    let direction =
        Sequence [
            Word "face"
            Choice [
                Word "the enemy"
                Word "this way"
            ]
        ]
    
    let formation =
        Sequence [
            Word "take"
            Choice [
                Word "line"
                Word "shield wall"
                Word "loose"
                Word "circle"
                Word "square"
                Word "skein"
                Word "column"
                Word "scatter"
            ]
            Word "formation"
        ]
    
    let unit =
        Choice [
            Word "soldiers"
            Word "archers"
            Word "cavalry"
            Word "horse archers"
        ]
    
    let orders =
        Sequence [
            unit
            Choice [
                position
                direction
                formation
                Word "fire at will"
                Choice [
                    Word "hold your fire"
                    Word "cease fire"
                ]
                Word "dismount"
                Word "get on your horses"
            ]
        ]
    
    let rec grammar = function
        | Word word -> GrammarBuilder(word)
        | Sequence orders ->
            let builder = GrammarBuilder()
            List.iter (fun order -> builder.Append(grammar order)) orders
            builder
        | Choice choices -> GrammarBuilder(Choices(List.map grammar choices |> Array.ofList))
