open canopy.classic
open canopy.runner.classic
open canopy.types
open OpenQA.Selenium.Chrome

let chromeConfig = Config.ChromeConfig.Load()
let clientConfig = Config.ClientConfig.Load()

canopy.configuration.chromeDir <- chromeConfig.DriverDir
// NB: set a specific port so it works correctly in docker
canopy.configuration.webdriverPort <- Some chromeConfig.DriverPort

let startBrowser () =
    let chromeOptions = ChromeOptions()
    chromeOptions.AddArgument("--no-sandbox")
    chromeOptions.AddArgument("--headless")
    let remote = Remote(chromeConfig.DriverUrl, chromeOptions.ToCapabilities())
    start remote

let startApp () =
    url clientConfig.Url
    waitForElement "#app"

"test increment and decrement" &&& fun _ ->
    startApp()
    describe "waiting for count to load..."
    waitFor (fun () ->
        let count = read "#count"
        count <> ""
    )
    let initialCount = read "#count" |> int
    describe "incrementing..."
    "#increment-input" << "5"
    click "#increment"
    "#count" == string (initialCount + 5)
    describe "decrementing..."
    "#decrement-input" << "2"
    click "#decrement"
    "#count" == string (initialCount + 5 - 2)

[<EntryPoint>]
let main _ =
    try
        startBrowser()
        run()
        quit()
        0
    with ex ->
        printfn "Error! %s" ex.Message
        quit()
        1

