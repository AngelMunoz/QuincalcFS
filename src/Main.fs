module Main

open Fable.Core
open Sutil.DOM
open Fable.Core.JsInterop

Commands.registerShortcuts ()

importSideEffects "@shoelace-style/shoelace/dist/themes/base.css"
importSideEffects "@shoelace-style/shoelace/dist/themes/dark.css"
importSideEffects "./styles.css"

importDefault "@shoelace-style/shoelace/dist/components/alert/alert.js"
|> ignore

importDefault "@shoelace-style/shoelace/dist/components/button/button.js"
|> ignore

importDefault "@shoelace-style/shoelace/dist/components/card/card.js"
|> ignore

importDefault "@shoelace-style/shoelace/dist/components/form/form.js"
|> ignore

importDefault "@shoelace-style/shoelace/dist/components/icon/icon.js"
|> ignore

importDefault "@shoelace-style/shoelace/dist/components/input/input.js"
|> ignore

importDefault "@shoelace-style/shoelace/dist/components/skeleton/skeleton.js"
|> ignore

importDefault "@shoelace-style/shoelace/dist/components/spinner/spinner.js"
|> ignore

importDefault "@shoelace-style/shoelace/dist/components/tab/tab.js"
|> ignore

importDefault "@shoelace-style/shoelace/dist/components/tab-panel/tab-panel.js"
|> ignore

importDefault "@shoelace-style/shoelace/dist/components/textarea/textarea.js"
|> ignore

importDefault "@shoelace-style/shoelace/dist/components/tooltip/tooltip.js"
|> ignore

importDefault
  "@shoelace-style/shoelace/dist/components/format-number/format-number.js"
|> ignore

importDefault
  "@shoelace-style/shoelace/dist/components/format-date/format-date.js"
|> ignore

[<ImportMember("@shoelace-style/shoelace/dist/utilities/base-path.js")>]
let setBasePath (path: string) : unit = jsNative

setBasePath "shoelace"
// Start the app
App.view () |> mountElement "sutil-app"
