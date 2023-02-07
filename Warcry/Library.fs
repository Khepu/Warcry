namespace Warcry

open System
open System.Globalization
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
    
    let recognitionHandler (speechRecognizedEventArgs: SpeechRecognizedEventArgs) = 
        let result = speechRecognizedEventArgs.Result

        if result.Confidence > 0.f then
            Console.WriteLine $"[f{result.Confidence}] {result.Text}"
    
    let main =
        let speechRecognition = new SpeechRecognitionEngine(CultureInfo("en-US"))
        try
            speechRecognition.SetInputToDefaultAudioDevice()
        with _ -> failwith "Failed to find default audio device!"

        speechRecognition.LoadGrammar(Grammar(grammar orders))
        speechRecognition.SpeechRecognized.Add(recognitionHandler)
        speechRecognition.RecognizeAsync(RecognizeMode.Multiple)