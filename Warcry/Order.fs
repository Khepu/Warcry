namespace Warcry

module Order =

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
        ]

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