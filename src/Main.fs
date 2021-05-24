module Main

open Sutil
open Sutil.DOM
open Fable.Core.JsInterop

importSideEffects "bulma/css/bulma.css"
importSideEffects "./styles.css"
// Start the app
App.view () |> mountElement "sutil-app"
