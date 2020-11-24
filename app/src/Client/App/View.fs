module App.View

open Elmish
open Domain
open Feliz

type InputType =
    | Increment
    | Decrement

let inputGroup =
    React.functionComponent(fun (props:{| inputType: InputType; onClick: int -> unit |}) ->
        let value, setValue = React.useState("1")
        let inputId = match props.inputType with Increment -> "increment-input" | Decrement -> "decrement-input"
        let buttonId = match props.inputType with Increment -> "increment" | Decrement -> "decrement"
        let label = match props.inputType with Increment -> "Increment By:" | Decrement -> "Decrement By:"
        Html.div [
            Html.label [
                prop.for' inputId
                prop.style [
                    style.display.inlineBlock
                    style.width 100
                ]
                prop.text label
            ]
            Html.input [
                prop.id inputId
                prop.style [
                    style.width 50
                ]
                prop.type' "number"
                prop.value value
                prop.onChange(setValue)
            ]
            Html.button [
                prop.id buttonId
                prop.style [
                    style.width 150
                ]
                prop.text (buttonId.ToUpper())
                prop.onClick (fun e ->
                    e.preventDefault()
                    props.onClick (int value)
                )
            ]
        ]
    )

let render (state:State) (dispatch:Msg -> unit) =
    Html.div [
        match state.Error with
        | Some error -> Browser.Dom.window.alert error
        | None -> ()
        Html.h1 "Fable + canopy + browserless"
        Html.h2 [
            Html.text "Count: "
            if state.IsLoading then
                Html.text "Loading..."
            else
                Html.span [
                    prop.id "count"
                    prop.text state.Count
                ]
        ]
        Html.div [
            inputGroup {| inputType = Increment; onClick = IncrementBy >> dispatch |}
            inputGroup {| inputType = Decrement; onClick = DecrementBy >> dispatch |}
        ]
    ]
