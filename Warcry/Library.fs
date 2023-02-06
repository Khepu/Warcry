namespace Warcry

open System.Speech.Recognition

module Warcry =
    let set = Set.ofList

    type Order =
        | Word of string
        | Sequence of Order list
        | Choice of Order Set
        | Dictation

    let position =
        Choice (set [
            Sequence [
                Word "move"
                Choice (set [
                    Word "here"
                ])
            ]
            Word "follow me"
            Word "charge"
            Word "advance"
            Word "fall back"
            Choice (set [
                Word "halt"
                Word "stop"
            ])
            Word "retreat"
            Word "cancel that"
        ])

    let direction =
        Sequence [
            Word "face"
            Choice (set [
                Word "the enemy"
                Word "this way"
            ])
        ]
    
    let formation =
        Sequence [
            Word "take"
            Choice (set [
                Word "line"
                Word "shield wall"
                Word "loose"
                Word "circle"
                Word "square"
                Word "skein"
                Word "column"
                Word "scatter"
            ])
            Word "formation"
        ]
    
    let unit =
        Choice (set [
            Word "soldiers"
            Word "archers"
            Word "cavalry"
            Word "horse archers"
        ])
    
    let orders =
        Sequence [
            unit
            Choice (set [
                position
                direction
                formation
                Word "fire at will"
                Choice (set [
                    Word "hold your fire"
                    Word "cease fire"
                ])
                Word "dismount"
                Word "get on your horses"
            ])
        ]
