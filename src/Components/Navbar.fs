module Components.Navbar

open Types
open Sutil
open Sutil.Bulma
open Sutil.DOM
open Sutil.Attr
open Sutil.Styling

let private getTheme (theme: Theme) =
  match theme with
  | Dark -> text "🌞"
  | Light -> text "🌚"

let view (navigateTo: Option<Page -> unit>) =
  let onMenuItemClick (page: Page) =
    Option.iter (fun fn -> fn page) navigateTo

  let theme =
    Settings.AppSettings
    |> Store.map
         (fun (store: AppSettings) ->
           store.theme
           |> Option.defaultValue (Settings.GetTheme()))

  Html.nav [

    Html.ul [
      Html.li [
        Html.a [
          text "Quincalc"
          onClick (fun _ -> onMenuItemClick Home) []
        ]
      ]
    ]
    Html.ul [
      Html.li [
        text "Settings"
        onClick (fun _ -> onMenuItemClick Settings) []
      ]
      Html.li [
        text "Switch Theme "
        bindFragment theme <| fun theme -> getTheme theme
        onClick (fun _ -> Settings.SwitchTheme()) []
      ]
    ]
  ]
  |> withStyle [
       rule
         "nav"
         [ Css.paddingLeft (Feliz.length.em (1))
           Css.paddingRight (Feliz.length.em (1)) ]
       rule "li" [ Css.cursorPointer ]
     ]
