module Main

open Sutil.DOM
open Fable.Core.JsInterop

Commands.registerShortcuts ()

importSideEffects "@picocss/pico/css/pico.min.css"
importSideEffects "@picocss/pico/css/themes/default.css"
importSideEffects "./styles.css"
// Start the app
App.view () |> mountElement "sutil-app"
