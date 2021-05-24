module Main

open Sutil
open Sutil.DOM
open Fable.Core.JsInterop

Commands.registerShortcuts ()

importSideEffects "bulma/css/bulma.css"
importSideEffects "./styles.css"
// Start the app
App.view () |> mountElement "sutil-app"
