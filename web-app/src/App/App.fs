module App

open Feliz

let render =
    React.functionComponent<unit>(fun _ ->
        let count, setCount = React.useState(0)
        Html.div [
            Html.h1 "Feliz + canopy + browserless"
            Html.h2 (sprintf "Count: %i" count)
            Html.button [
                prop.onClick(fun e ->
                    e.preventDefault()
                    setCount (count + 1)
                )
                prop.text "Increment"
            ]
            Html.button [
                prop.onClick(fun e ->
                    e.preventDefault()
                    setCount (count - 1)
                )
                prop.text "Decrement"
            ]
        ]
    )
