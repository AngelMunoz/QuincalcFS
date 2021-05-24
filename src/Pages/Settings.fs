module Pages.Settings

open Sutil
open Sutil.Bulma
open Sutil.DOM
open Sutil.Attr
open Types
open Sutil.Styling

let view () =
  let theme = Store.make (Settings.GetTheme())

  let overrideBrowserTheme = Store.make (Settings.DidSaveTheme())

  let sub =
    Settings.OnThemeChanged(fun updated -> Store.set theme updated)

  let switchTheme _ = Settings.SwitchTheme()

  let saveSettings _ =
    let overrideBro = Store.get overrideBrowserTheme
    let actualTheme = Settings.GetTheme()

    if overrideBro then
      Settings.OverrideTheme(Some actualTheme)
    else
      Settings.OverrideTheme None


  Html.article [
    onUnmount
      (fun _ ->
        if Settings.DidSaveTheme() |> not then
          Settings.RestoreBrowserTheme())
      []
    disposeOnUnmount [ theme; sub ]
    class' "container"
    Html.header [
      text "Quincalc Preferences"
    ]
    Html.div [
      Html.p [
        text
          "These are your current settings, Once you save them we'll use them on the next page load"
      ]
      Html.label [
        text "Override browser theme "
        Html.input [
          type' "checkbox"
          Bind.attr ("checked", overrideBrowserTheme)
        ]
      ]
    ]
    Html.footer [
      class' "grid"
      Html.a [
        class' "outline contrast"
        Attr.role "button"
        bindFragment theme
        <| fun theme ->
             let t =
               if theme = Light then
                 "Dark 🌚"
               else
                 "Light 🌞"

             text $"Switch {t} theme"
        onClick switchTheme []
      ]
      Html.a [
        class' "outline"
        Attr.role "button"
        text "Save Settings"
        onClick saveSettings []
      ]
    ]
  ]
  |> withStyle [
       rule "header" [ Css.textAlignCenter ]
       rule
         "div"
         [ Css.displayFlex
           Css.flexDirectionColumn
           Css.justifyContentCenter
           Css.alignItemsCenter ]
     ]
