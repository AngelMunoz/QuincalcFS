module App

open Sutil
open Sutil.DOM
open Sutil.Attr
open Types
open Pages
open Browser.Types

let setContext =
  function
  | Home -> Keyboard.setContext "home"
  | Expenses -> Keyboard.setContext "expenses"
  | Payments -> Keyboard.setContext "payments"
  | Settings -> Keyboard.setContext "settings"


Keyboard.start ()

let view () =

  let sub =
    Commands.CommandStream
    |> Store.subscribe (fun cmd -> printfn "%A" cmd)

  let onTabShow (event: Event) =
    let event =
      (event :?> CustomEvent<{| name: string |}>)

    match event.detail
          |> Option.map (fun detail -> detail.name) with
    | Some "home" -> setContext Home
    | Some "settings" -> setContext Settings
    | Some "expenses" -> setContext Expenses
    | Some "payments" -> setContext Payments
    | _ -> ()

  Html.app [
    disposeOnUnmount [ sub ]
    class' "app-content"
    Html.main [
      on "sl-tab-show" onTabShow []
      Html.custom (
        "sl-tab-group",
        [ Attr.custom ("placement", "end")
          Html.custom (
            "sl-tab",
            [ Attr.slot "nav"
              Attr.custom ("panel", "home")
              text "Home" ]
          )
          Html.custom (
            "sl-tab",
            [ Attr.slot "nav"
              Attr.custom ("panel", "expenses")
              text "Expenses" ]
          )
          Html.custom (
            "sl-tab",
            [ Attr.slot "nav"
              Attr.custom ("panel", "settings")
              text "Settings" ]
          )

          Html.custom ("sl-tab-panel", [ Attr.name "home"; Home.view () ])
          Html.custom (
            "sl-tab-panel",
            [ Attr.name "expenses"
              Expenses.view () ]
          )
          Html.custom (
            "sl-tab-panel",
            [ Attr.name "settings"
              Settings.view () ]
          ) ]
      )
    ]
  ]
